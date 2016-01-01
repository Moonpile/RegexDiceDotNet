using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexDiceDotNet
{
    public static class Dice
    {
        public static Random Random = new Random();
        public static Regex diceregex = new Regex(@"^(?<numdice>\d*)(?<dsides>(?<type>[dDeEsS]|eu|EU|ed|ED|su|SU|sd|SD)(?<numsides>\d+))(?<threshold>(?<thresholdtype>[b|B|t|T])(?<thresholdlimit>(?<thresholdsign>[\+\-])?(?<thresholdvalue>\d*)))?(?<modifier>(?<sign>[\+\-])(?<addend>\d*))?(?<floor>(?<floorseperator>[f|F])(?<floorlimit>(?<floorsign>[\+\-])?(?<floorlimiter>\d*)))?(?<ceiling>(?<ceilingseperator>[c|C])(?<ceilinglimit>(?<ceilingsign>[\+\-])?(?<ceilinglimiter>\d*)))?$");

        /// <summary>
        /// This is the base method that all other dice methods should use.  It rolls one die of NumSides.
        /// </summary>
        /// <param name="NumSides"></param>
        /// <returns></returns>
        public static int Roll(int NumSides)
        {
            if (NumSides == 0)
            {
                return 0;
            }
            else
            {
                return (Dice.Random.Next(NumSides) + 1);
            }
        }

        public static int Roll(int NumDice, int NumSides, int Modifier)
        {
            int result = 0;
            for (int i = 1; (i <= NumDice); i++)
            {
                result = result + (Dice.Roll(NumSides));
            }
            return (result + Modifier);
        }

        public static int Roll(string DiceExpression)
        { return RollExpanded(DiceExpression).Result; }

        private static int? ParseNullableInteger(string StringValue)
        {
            int i;
            int? result = null;
            if (Int32.TryParse(StringValue, out i))
            {
                result = i;
            }
            return result;
        }

        public static ExpandedDiceRoll RollExpanded(string DiceExpression)
        {
            ExpandedDiceRoll result = new ExpandedDiceRoll();

            //Allow a literal number to override a dice roll.
            if (Int32.TryParse(DiceExpression, out result.Result))
            {
                return result;
            }

            Match match = diceregex.Match(DiceExpression);
            if (!match.Success)
            {
                result.Result = 0;
                result.FullResults = "\"" + DiceExpression + "\" is not a valid dice expression";
                return result;
            }

            int numDice = 1;
            int numSides = 0;
            int modifier = 0;
            string diceType = match.Groups["type"].Value.ToLower();
            int? threshold = ParseNullableInteger(match.Groups["thresholdvalue"].Value);
            string thresholdType = "";
            if (threshold.HasValue)
            {
                thresholdType = match.Groups["thresholdtype"].Value.ToLower();
            }


            int? lowerlimit = ParseNullableInteger(match.Groups["floorlimit"].Value);
            int? upperlimit = ParseNullableInteger(match.Groups["ceilinglimit"].Value);



            if (!string.IsNullOrWhiteSpace(match.Groups["numdice"].Value))
            {
                numDice = Int32.Parse(match.Groups["numdice"].Value);
            }
            if (!string.IsNullOrWhiteSpace(match.Groups["numsides"].Value))
            {
                numSides = Int32.Parse(match.Groups["numsides"].Value);
            }
            if (!string.IsNullOrWhiteSpace(match.Groups["modifier"].Value))
            {
                modifier = Int32.Parse(match.Groups["modifier"].Value);
            }


            //Normal dice
            if (diceType == "d"
                && !threshold.HasValue)
            {
                for (int i = 1; (i <= numDice); i++)
                {
                    int roll = Dice.Roll(numSides);
                    result.FullResults += roll;
                    result.Result += roll;

                    if ((i < numDice))
                    {
                        result.FullResults += " + ";
                    }
                    //else
                    //{
                    //    result.FullResults += " = ";
                    //}
                }
            }

            //Non-smooth exploding dice
            if ((diceType == "e"
                || diceType == "eu"
                || diceType == "ed")
                && !threshold.HasValue)
            {
                for (int i = 1; (i <= numDice); i++)
                {
                    int roll = Dice.Roll(numSides);
                    string explodedResultText = "";

                    if (roll == numSides && (diceType == "e" || diceType == "eu"))
                    {
                        //Non-smooth explode up
                        explodedResultText += "(UP: " + roll + "+";
                        int explodedRoll = 0;

                        while (explodedRoll == 0 || explodedRoll == numSides)
                        {
                            explodedRoll = Dice.Roll(numSides);
                            if (explodedRoll == numSides)
                            {
                                explodedResultText += explodedRoll + "+";
                                roll += explodedRoll;
                            }
                            else
                            {
                                explodedResultText += explodedRoll + ")";
                                roll += explodedRoll;
                            }
                        }
                    }
                    else if (roll == 1 && (diceType == "e" || diceType == "ed"))
                    {
                        //Non-smooth explode down
                        explodedResultText += "(DN: " + roll + "-";
                        int explodedRoll = 0;

                        while (explodedRoll == 0 || explodedRoll == numSides)
                        {
                            explodedRoll = Dice.Roll(numSides);
                            if (explodedRoll == numSides)
                            {
                                explodedResultText += explodedRoll + "-";
                                roll -= explodedRoll;
                            }
                            else
                            {
                                explodedResultText += explodedRoll + ")";
                                roll -= explodedRoll;
                            }
                        }
                    }
                    explodedResultText = roll + " " + explodedResultText;

                    result.Result += roll;

                    if (!string.IsNullOrWhiteSpace(explodedResultText))
                    {
                        result.FullResults += explodedResultText;
                    }
                    if ((i < numDice))
                    {
                        result.FullResults += " + ";
                    }
                    //else
                    //{
                    //    result.FullResults += " = ";
                    //}
                }
            }


            //Smooth exploding dice
            if ((diceType == "s"
                || diceType == "su"
                || diceType == "sd")
                && !threshold.HasValue)
            {
                for (int i = 1; (i <= numDice); i++)
                {
                    int smoothSides = 1;
                    if (diceType == "s") { smoothSides = 2; }

                    int roll = Dice.Roll(numSides + smoothSides);

                    string explodedResultText = "";

                    if (roll == numSides + smoothSides && (diceType == "s" || diceType == "su"))
                    {
                        //Smooth explode up
                        roll -= smoothSides;

                        explodedResultText += "(UP: " + roll + "+";
                        int explodedRoll = 0;

                        while (explodedRoll == 0 || explodedRoll == numSides + 1)
                        {
                            explodedRoll = Dice.Roll(numSides + 1);
                            if (explodedRoll == numSides + 1)
                            {
                                explodedResultText += numSides + "+";
                                roll += numSides;
                            }
                            else
                            {
                                explodedResultText += explodedRoll + ")";
                                roll += explodedRoll;
                            }
                        }
                    }
                    else if (roll == numSides + 1 && (diceType == "s" || diceType == "sd"))
                    {
                        //Smooth explode down
                        roll = 1;

                        explodedResultText += "(DN: " + roll + "-";
                        int explodedRoll = 0;

                        while (explodedRoll == 0 || explodedRoll == numSides + 1)
                        {
                            explodedRoll = Dice.Roll(numSides + 1);
                            if (explodedRoll == numSides + 1)
                            {
                                explodedResultText += numSides + "-";
                                roll -= numSides;
                            }
                            else
                            {
                                explodedResultText += explodedRoll + ")";
                                roll -= explodedRoll;
                            }
                        }
                    }

                    explodedResultText = roll + " " + explodedResultText;

                    result.Result += roll;

                    if (!string.IsNullOrWhiteSpace(explodedResultText))
                    {
                        result.FullResults += explodedResultText;
                    }
                    if ((i < numDice))
                    {
                        result.FullResults += " + ";
                    }
                    //else
                    //{
                    //    result.FullResults += " = ";
                    //}
                }

            }

            //Threshold dice
            if (threshold.HasValue)
            {
                int successes = 0;
                string tresultText = "";

                //roll just one die of the type 
                string newDiceExpression = match.Groups["dsides"].Value;
                for (int i = 1; (i <= numDice); i++)
                {

                    int tresult = Dice.Roll(newDiceExpression);
                    tresultText += tresult;
                    if (tresult >= threshold.Value)
                    {
                        successes += 1;
                        tresultText += "*";
                        if (thresholdType == "b" && tresult >= numSides)
                        {
                            //max rolls "burst" to new chances

                            int burstRoll = 0;
                            tresultText += "(BURST: ";

                            while (burstRoll == 0 || burstRoll >= numSides)
                            {
                                burstRoll = Dice.Roll(newDiceExpression);
                                tresultText += burstRoll;
                                if (burstRoll >= threshold)
                                {
                                    successes += 1;
                                    tresultText += "*";
                                }
                                if (burstRoll == numSides)
                                {
                                    tresultText += ", ";
                                }
                            }
                            tresultText += ")";

                        }
                    }
                    if (i != numDice)
                    {
                        tresultText += ", ";
                    }
                }

                result.Result = successes;

                if (!string.IsNullOrWhiteSpace(tresultText))
                {
                    result.FullResults += tresultText;
                }
                //result.FullResults += " = ";

            }


            result.Result += modifier;



            if ((modifier < 0))
            {
                result.FullResults += " (" + modifier + ")";
            }
            if ((modifier > 0))
            {
                result.FullResults += " (+" + modifier + ")";
            }

            // Limits occur after Modifiers are applied        
            if ((lowerlimit.HasValue && (result.Result < lowerlimit)))
            {
                result.FullResults = result.Result + " (FLOOR " + lowerlimit.Value + ") = " + result.FullResults;
                result.Result = lowerlimit.Value;

            }
            // Limits occur after Modifiers are applied        
            if ((upperlimit.HasValue && (result.Result > upperlimit)))
            {
                result.FullResults = result.Result + " (CEILING " + upperlimit.Value + ") = " + result.FullResults;
                result.Result = upperlimit.Value;

            }

            result.FullResults = result.Result + " = " + result.FullResults;
            return result;
        }



        public static bool ValidateDiceExpression(string DiceExpression)
        {
            if (String.IsNullOrWhiteSpace(DiceExpression))
            {
                return false;
            }
            else
            {
                return diceregex.IsMatch(DiceExpression);
            }
        }


        public static string NumberToText(int n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            else
            {
                return myNumberToText(n).Trim();
            }
        }

        private static string myNumberToText(int n)
        {
            if (n < 0)
                return "Minus " + myNumberToText(-n);
            else if (n == 0)
                return "";
            else if (n <= 19)
                return new string[] {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", 
         "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", 
         "Seventeen", "Eighteen", "Nineteen"}[n - 1] + " ";
            else if (n <= 99)
                return new string[] {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", 
         "Eighty", "Ninety"}[n / 10 - 2] + " " + myNumberToText(n % 10);
            else if (n <= 199)
                return "One Hundred " + myNumberToText(n % 100);
            else if (n <= 999)
                return myNumberToText(n / 100) + "Hundred " + myNumberToText(n % 100);
            else if (n <= 1999)
                return "One Thousand " + myNumberToText(n % 1000);
            else if (n <= 999999)
                return myNumberToText(n / 1000) + "Thousand " + myNumberToText(n % 1000);
            else if (n <= 1999999)
                return "One Million " + myNumberToText(n % 1000000);
            else if (n <= 999999999)
                return myNumberToText(n / 1000000) + "Million " + myNumberToText(n % 1000000);
            else if (n <= 1999999999)
                return "One Billion " + myNumberToText(n % 1000000000);
            else
                return myNumberToText(n / 1000000000) + "Billion " + myNumberToText(n % 1000000000);
        }

    }


    public struct ExpandedDiceRoll
    {
        public string FullResults;
        public int Result;
    }
}

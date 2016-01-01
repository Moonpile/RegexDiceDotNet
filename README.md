## Synopsis

RegexDiceDotNet is a c# library for generating random numbers from "dice expressions" such as "3d6" (three six-sided dice), "d100" (one one hundred-sided die).

## Code Example

The Dice object is static and contains and exposes its own Random object.  The Roll and RollExpanded methods will be the most used.  

The "public static int Roll(string DiceExpression)" and "public static ExpandedDiceRoll RollExpanded(string DiceExpression)" methods are the real reason for this library.  They accept a dice expression, evaluate it against a Regex and returns a random result.  The RollExpanded method returns the result as an "ExpandedDiceRoll" object which contains the "Result" as an int and the "FullResults" as a string which is a shorthand accounting of the die roll.

```c#
int roll = Dice.Roll("3d6+5"); // sum of three six-sided dice plus five.
int roll2 = Dice.Roll("10d10t7); // ten ten sided dice, count only those which roll 7 or more (threshold).
```

```c#
ExpandedDiceRoll result = Dice.RollExpanded("15d100"); // sum of fifteen ten-sided dice.
// Example FullResult: 601 = 12 + 7 + 79 + 34 + 16 + 68 + 59 + 42 + 26 + 4 + 55 + 32 + 26 + 79 + 62
ExpandedDiceRoll result = Dice.RollExpanded("10su4"); // sum of 10 smoothly exploding four-sided dice.
// Example FullResult: 51 = 7 (UP: 4+3) + 9 (UP: 4+4+1) + 4  + 2  + 7 (UP: 4+3) + 2  + 4  + 9 (UP: 4+4+1) + 4  + 3 
```


The int Roll(int NumSides) method returns a random number between 1 and the NumSides, inclusive, with an even chance of returning any integer in that range.  Use this method to quickly roll one die.

```c#
int roll3 = Dice.Roll(6); 
```

Roll(int NumDice, int NumSides, int Modifier) provides a quick way to roll some number of dice and sum them up, and add an optional modifier.

```c#
int roll4 = Dice.Roll(3,6,5); // three six-sided dice plus five.
```

## Dice Syntax

### Dice Expression Examples

* 3d6 - sum three six-sided dice.
* 3e6 - sum three exploding six-sided dice.
* 3eu6 - sum three upward exploding six-sided dice.
* 3ed6 - sum three downward exploding six-sided dice.
* 3s6 - sum three smoothly exploding six-sided dice.
* 3su6 - sum three upward smoothly exploding six-sided dice.
* 3sd6 - sum three downward smoothly exploding six-sided dice.
* d100 - roll one one hundred-sided die.
* d1000 - roll one one thousand-sided die.
* 1d20 - roll one twenty-sided die.
* 1d20+10 - roll one twenty-sided die and add ten.
* 1d20-10 - roll one twenty-sided die and subtract ten.
* 10d10t7 - roll ten ten-sided dice, and count those that result in seven or more.
* 10e3f0 - sum ten exploding ten sided dice and change any result below zero to zero.
* 10e3c50 - sum ten exploding ten sided dice and change any result above fifty to fifty.


### Details

The regex used to parse Dice Expressions, accepts values that meet these requirements:

* (optional) Number of Dice to roll.  If absent, one die is assumed.
* Dice Type:
  * "d" or "D": Normal dice.
  * "e" or "E": Exploding Dice.  If the roll of one die is "1", roll another die and subtract that from 1. If the roll equals the number of sides (maximum result), roll another die and add that to the 1.  In either case, if the "exploded" die is the maximum result, continue rolling another exploded die.  An exploding d6 will never roll a 1 or a 6.
    * "eu" or "EU": Exploding Dice, Up: As Exploding Dice, but dice only "explode" on a maximum roll.
    * "ed" or "ED": Exploding Dice, Down: As Exploding Dice, but dice only "explode" on a 1.
  * "s" or "S": Smoothly Exploding Dice: similar to Exploding Dice, but the die rolled has two more sides than the number of sides specified in the dice expression.  One result indicates subtracting a die roll from 1 and the other indicates adding a die roll to the maximum result (number of sides).  A smoothly exploding d6 may roll a 1 or a 6.
    * "su" or "SU": Smoothly Exploding Dice, Up: As Smoothly Exploding Dice, but the die only has one extra side and only explodes up on that result.
    * "sd" or "SD": Smoothly Exploding Dice, Down: As Smoothly Exploding Dice, but the die only has one extra side and only explodes down on that result.
* Number of sides
* (optional) "t" or "T" and digits: Threshold: roll the specified number of dice, but instead of summing them up, add one for every die with a result equal to or greater than the specified number (next).
* (optional) "b" or "B" and digits: Bursting Threshold Dice: As with Threshold Dice, except roll another die whenever a die has a maximum result.
* (optional) "+" or "-" and digits: Modifier sign indicating whether or add or subtract from the final result.
* (optional) "f" or "F", (optional) "+" or "-", and digits: Floor: Any result below this value will be returned as this value.
* (optional) "c" or "C", (optional) "+" or "-", and digits: Ceiling: Any result above this value will be returned as this value.

## Test Harness

The accompanying Test Harness is a WinForms application that allows you to test Dice expressions and perform simple analysis on them.

## Contributors

While I will eventually welcome contributors, I am just getting started with Github and want to wait until I'm comfortable with it before attempting to fold in others' efforts.  In the meantime I welcome any bug reports, enhancement suggestions, or other feedback.

## License

RegexDiceDotNet is provided under the MIT License.
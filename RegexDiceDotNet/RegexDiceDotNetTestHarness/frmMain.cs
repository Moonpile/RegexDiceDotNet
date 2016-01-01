using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RegexDiceDotNet;

namespace RegexDiceDotNetTestHarness
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            string diceExpression = cmbDiceExpression.Text;
            int nRolls = Convert.ToInt32(numRolls.Value);

            StringBuilder sb = new StringBuilder();

            if (this.chkAnalysis.Checked)
            {
                //Roll dice in Analysis mode
                string lineFormat = "{0,5}:\t{1,8}\t{2,3}";
                Dictionary<int, int> ValueCounts = new Dictionary<int, int>();

                for (int i = 0; i < nRolls; i++)
                {
                    int result = Dice.Roll(diceExpression);
                    if (ValueCounts.ContainsKey(result))
                    {
                        ValueCounts[result] += 1;
                    }
                    else
                    {
                        ValueCounts.Add(result, 1);
                    }
                }

                sb.AppendLine("Analysis of " + nRolls + " rolls of \"" + diceExpression + "\":");

                List<int> keys = ValueCounts.Keys.ToList();
                keys.Sort();

                foreach (int key in keys)
                {
                    int count = ValueCounts[key];
                    string percentage = ((decimal)count / nRolls).ToString("p");
                    sb.AppendLine(string.Format(lineFormat, key, count, percentage));
                }

                txtResults.Text = sb.ToString();
            }
            else
            {
                //Roll dice normally
                string lineFormat = "{0}:\t{1}:\t{2,5}\tFull Result: {3}";

                for (int i = 0; i < nRolls; i++)
                {
                    ExpandedDiceRoll result = Dice.RollExpanded(diceExpression);
                    sb.AppendLine(string.Format(lineFormat, i + 1, diceExpression, result.Result, result.FullResults));
                }

                txtResults.Text = sb.ToString();
            }
        }


        private void RegexDiceRoller_Load(object sender, EventArgs e)
        {
            this.numRolls.Maximum = Int32.MaxValue;
        }

        private void btnMillionRolls_Click(object sender, EventArgs e)
        {
            this.numRolls.Value = 1000000;
        }

    }
}

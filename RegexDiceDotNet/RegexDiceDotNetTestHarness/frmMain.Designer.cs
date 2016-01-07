namespace RegexDiceDotNetTestHarness
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMillionRolls = new System.Windows.Forms.Button();
            this.chkAnalysis = new System.Windows.Forms.CheckBox();
            this.cmbDiceExpression = new System.Windows.Forms.ComboBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.btnRoll = new System.Windows.Forms.Button();
            this.numRolls = new System.Windows.Forms.NumericUpDown();
            this.lblDiceExpression = new System.Windows.Forms.Label();
            this.btnRollOneOfEach = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numRolls)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMillionRolls
            // 
            this.btnMillionRolls.Location = new System.Drawing.Point(652, 7);
            this.btnMillionRolls.Name = "btnMillionRolls";
            this.btnMillionRolls.Size = new System.Drawing.Size(75, 23);
            this.btnMillionRolls.TabIndex = 11;
            this.btnMillionRolls.Text = " Million Rolls";
            this.btnMillionRolls.UseVisualStyleBackColor = true;
            this.btnMillionRolls.Click += new System.EventHandler(this.btnMillionRolls_Click);
            // 
            // chkAnalysis
            // 
            this.chkAnalysis.AutoSize = true;
            this.chkAnalysis.Location = new System.Drawing.Point(574, 5);
            this.chkAnalysis.Name = "chkAnalysis";
            this.chkAnalysis.Size = new System.Drawing.Size(64, 17);
            this.chkAnalysis.TabIndex = 10;
            this.chkAnalysis.Text = "Analysis";
            this.chkAnalysis.UseVisualStyleBackColor = true;
            // 
            // cmbDiceExpression
            // 
            this.cmbDiceExpression.AutoCompleteCustomSource.AddRange(new string[] {
            "3d6",
            "3o6",
            "3u6",
            "3s6",
            "3q6",
            "3r6",
            "10d10t7",
            "d100",
            "d1000",
            "10s3f0"});
            this.cmbDiceExpression.FormattingEnabled = true;
            this.cmbDiceExpression.Items.AddRange(new object[] {
            "3d6",
            "3e6",
            "3eu6",
            "3ed6",
            "3s6",
            "3su6",
            "3sd6",
            "d100",
            "d1000",
            "1d20",
            "1d20+10",
            "1d20-10",
            "10d10t7",
            "10e3f0",
            "10e3c10",
            "2heythisisn\'tadiceexpresion123"});
            this.cmbDiceExpression.Location = new System.Drawing.Point(106, 7);
            this.cmbDiceExpression.MaxDropDownItems = 20;
            this.cmbDiceExpression.Name = "cmbDiceExpression";
            this.cmbDiceExpression.Size = new System.Drawing.Size(191, 21);
            this.cmbDiceExpression.TabIndex = 9;
            this.cmbDiceExpression.Text = "d100";
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(12, 36);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(1012, 461);
            this.txtResults.TabIndex = 8;
            this.txtResults.WordWrap = false;
            // 
            // btnRoll
            // 
            this.btnRoll.Location = new System.Drawing.Point(395, 5);
            this.btnRoll.Name = "btnRoll";
            this.btnRoll.Size = new System.Drawing.Size(75, 23);
            this.btnRoll.TabIndex = 7;
            this.btnRoll.Text = "&Roll!";
            this.btnRoll.UseVisualStyleBackColor = true;
            this.btnRoll.Click += new System.EventHandler(this.btnRoll_Click);
            // 
            // numRolls
            // 
            this.numRolls.Location = new System.Drawing.Point(303, 8);
            this.numRolls.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numRolls.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRolls.Name = "numRolls";
            this.numRolls.Size = new System.Drawing.Size(86, 20);
            this.numRolls.TabIndex = 12;
            this.numRolls.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRolls.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblDiceExpression
            // 
            this.lblDiceExpression.AutoSize = true;
            this.lblDiceExpression.Location = new System.Drawing.Point(14, 12);
            this.lblDiceExpression.Name = "lblDiceExpression";
            this.lblDiceExpression.Size = new System.Drawing.Size(86, 13);
            this.lblDiceExpression.TabIndex = 13;
            this.lblDiceExpression.Text = "Dice Expression:";
            // 
            // btnRollOneOfEach
            // 
            this.btnRollOneOfEach.Location = new System.Drawing.Point(476, 5);
            this.btnRollOneOfEach.Name = "btnRollOneOfEach";
            this.btnRollOneOfEach.Size = new System.Drawing.Size(75, 23);
            this.btnRollOneOfEach.TabIndex = 14;
            this.btnRollOneOfEach.Text = "One of Each";
            this.btnRollOneOfEach.UseVisualStyleBackColor = true;
            this.btnRollOneOfEach.Click += new System.EventHandler(this.btnRollOneOfEach_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 509);
            this.Controls.Add(this.btnRollOneOfEach);
            this.Controls.Add(this.lblDiceExpression);
            this.Controls.Add(this.numRolls);
            this.Controls.Add(this.btnMillionRolls);
            this.Controls.Add(this.chkAnalysis);
            this.Controls.Add(this.cmbDiceExpression);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnRoll);
            this.Name = "frmMain";
            this.Text = "RegexDiceDotNet";
            this.Load += new System.EventHandler(this.RegexDiceRoller_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numRolls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMillionRolls;
        private System.Windows.Forms.CheckBox chkAnalysis;
        private System.Windows.Forms.ComboBox cmbDiceExpression;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Button btnRoll;
        private System.Windows.Forms.NumericUpDown numRolls;
        private System.Windows.Forms.Label lblDiceExpression;
        private System.Windows.Forms.Button btnRollOneOfEach;
    }
}


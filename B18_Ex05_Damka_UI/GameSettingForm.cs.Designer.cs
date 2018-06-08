namespace B18_Ex05_Damka_UI
{
    public partial class GameSettingForm
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
            this.buttonDone = new System.Windows.Forms.Button();
            this.textBoxPlayerOneName = new System.Windows.Forms.TextBox();
            this.radioButton6On6 = new System.Windows.Forms.RadioButton();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButton8On8 = new System.Windows.Forms.RadioButton();
            this.radioButton10On10 = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayerOne = new System.Windows.Forms.Label();
            this.checkBoxPlayerTwo = new System.Windows.Forms.CheckBox();
            this.textBoxSecondPlayer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(280, 312);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(200, 63);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // textBoxPlayerOneName
            // 
            this.textBoxPlayerOneName.Location = new System.Drawing.Point(229, 188);
            this.textBoxPlayerOneName.Name = "textBoxPlayerOneName";
            this.textBoxPlayerOneName.Size = new System.Drawing.Size(251, 35);
            this.textBoxPlayerOneName.TabIndex = 0;
            // 
            // radioButton6On6
            // 
            this.radioButton6On6.AutoSize = true;
            this.radioButton6On6.Checked = true;
            this.radioButton6On6.Location = new System.Drawing.Point(42, 79);
            this.radioButton6On6.Name = "radioButton6On6";
            this.radioButton6On6.Size = new System.Drawing.Size(87, 33);
            this.radioButton6On6.TabIndex = 1;
            this.radioButton6On6.TabStop = true;
            this.radioButton6On6.Text = "6X6";
            this.radioButton6On6.UseVisualStyleBackColor = true;
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(12, 38);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(137, 29);
            this.labelBoardSize.TabIndex = 2;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // radioButton8On8
            // 
            this.radioButton8On8.AutoSize = true;
            this.radioButton8On8.Location = new System.Drawing.Point(169, 79);
            this.radioButton8On8.Name = "radioButton8On8";
            this.radioButton8On8.Size = new System.Drawing.Size(87, 33);
            this.radioButton8On8.TabIndex = 3;
            this.radioButton8On8.Text = "8X8";
            this.radioButton8On8.UseVisualStyleBackColor = true;
            // 
            // radioButton10On10
            // 
            this.radioButton10On10.AutoSize = true;
            this.radioButton10On10.Location = new System.Drawing.Point(307, 79);
            this.radioButton10On10.Name = "radioButton10On10";
            this.radioButton10On10.Size = new System.Drawing.Size(113, 33);
            this.radioButton10On10.TabIndex = 4;
            this.radioButton10On10.Text = "10X10";
            this.radioButton10On10.UseVisualStyleBackColor = true;
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(12, 141);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(99, 29);
            this.labelPlayers.TabIndex = 5;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayerOne
            // 
            this.labelPlayerOne.AutoSize = true;
            this.labelPlayerOne.Location = new System.Drawing.Point(37, 191);
            this.labelPlayerOne.Name = "labelPlayerOne";
            this.labelPlayerOne.Size = new System.Drawing.Size(106, 29);
            this.labelPlayerOne.TabIndex = 6;
            this.labelPlayerOne.Text = "Player 1:";
            // 
            // checkBoxPlayerTwo
            // 
            this.checkBoxPlayerTwo.AutoSize = true;
            this.checkBoxPlayerTwo.Location = new System.Drawing.Point(42, 251);
            this.checkBoxPlayerTwo.Name = "checkBoxPlayerTwo";
            this.checkBoxPlayerTwo.Size = new System.Drawing.Size(138, 33);
            this.checkBoxPlayerTwo.TabIndex = 7;
            this.checkBoxPlayerTwo.Text = "Player 2:";
            this.checkBoxPlayerTwo.UseVisualStyleBackColor = true;
            this.checkBoxPlayerTwo.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxSecondPlayer
            // 
            this.textBoxSecondPlayer.Enabled = false;
            this.textBoxSecondPlayer.Location = new System.Drawing.Point(229, 249);
            this.textBoxSecondPlayer.Name = "textBoxSecondPlayer";
            this.textBoxSecondPlayer.Size = new System.Drawing.Size(251, 35);
            this.textBoxSecondPlayer.TabIndex = 8;
            this.textBoxSecondPlayer.Text = "[Computer]";
            // 
            // GameSettingForm
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(515, 402);
            this.Controls.Add(this.textBoxSecondPlayer);
            this.Controls.Add(this.checkBoxPlayerTwo);
            this.Controls.Add(this.labelPlayerOne);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButton10On10);
            this.Controls.Add(this.radioButton8On8);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.radioButton6On6);
            this.Controls.Add(this.textBoxPlayerOneName);
            this.Controls.Add(this.buttonDone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GameSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.TextBox textBoxPlayerOneName;
        private System.Windows.Forms.RadioButton radioButton6On6;
        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButton8On8;
        private System.Windows.Forms.RadioButton radioButton10On10;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayerOne;
        private System.Windows.Forms.CheckBox checkBoxPlayerTwo;
        private System.Windows.Forms.TextBox textBoxSecondPlayer;
    }
}
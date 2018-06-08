using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B18_Ex05_Damka_UI
{
    public partial class GameSettingForm : Form
    {
        public GameSettingForm()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBoxisSignleMode = sender as CheckBox;
            if (checkBoxisSignleMode.Checked)
            {
                textBoxSecondPlayer.Enabled = true;
                textBoxSecondPlayer.Text = string.Empty;
            }
            else
            {
                textBoxSecondPlayer.Enabled = false;
                textBoxSecondPlayer.Text = "[Computer]";
            }
        }

        public int BoardSize
        {
            get
            {
                int boardSize; 
                if (radioButton6On6.Checked)
                {
                    boardSize = 6;
                }
                else if (radioButton8On8.Checked)
                {
                    boardSize = 8;
                }
                else
                {
                    boardSize = 10;
                }

                return boardSize;
            }
        }

        public string FirstPlayerName
        {
            get { return textBoxPlayerOneName.Text; }
        }

        public string SecondPlayerName
        {
            get
            {
                string secondPlayerName;
                if (IsComputerMode)
                {
                    secondPlayerName = "Computer";
                }
                else
                {
                    secondPlayerName = textBoxSecondPlayer.Text;
                }

                return secondPlayerName;
            }
        }

        public bool IsComputerMode
        {
            get
            {
                bool isComputerMode = true;
                if (checkBoxPlayerTwo.Checked)
                {
                    isComputerMode = false;
                }

                return isComputerMode;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            bool isNameOK = false;
            string playerName = textBoxPlayerOneName.Text;
            if (playerName.Length <= 20 && !isTabsInString(playerName) && !playerName.Equals(string.Empty))
            {
                isNameOK = true;
            }
            
            if (checkBoxPlayerTwo.Checked)
            {
                playerName = textBoxSecondPlayer.Text;
                if (playerName.Length <= 20 && !isTabsInString(playerName) && !playerName.Equals(string.Empty))
                {
                    isNameOK = true;
                }
                else
                {
                    isNameOK = false;
                }
            }

            if (!isNameOK)
            {
                string message = string.Format("Invalid Name.{0} Maximun length 20 and without spaces. Try Again!", Environment.NewLine);
                string title = "Invalid Input!";
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                (sender as Button).DialogResult = DialogResult.None;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool isTabsInString(string i_Name)
        {
            bool isTabsInString = false;

            for (int i = 0; i < i_Name.Length; i++)
            {
                if (i_Name[i] == ' ')
                {
                    isTabsInString = true;
                    break;
                }
            }

            return isTabsInString;
        }
    }
}

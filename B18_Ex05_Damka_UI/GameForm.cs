using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B18_Ex05_Damka_BL;

namespace B18_Ex05_Damka_UI
{
    public partial class GameForm : Form
    {
        private const int k_BoardTopSize = 50;
        private const int k_BoardBorders = 12;
        private const int k_SquareSize = 60;
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private readonly bool r_IsComputerMode;
        private readonly BoardButton[,] r_ButtonMatrix;
        private Label labelFirstPlayerName;
        private Label labelSecondPlayerName;
        private Label labelTurnNow;
        private BoardButton m_FirstSelectedButton = null;
        private Game m_Game;

        public GameForm(string i_FirstPlayerName, string i_SecondPlayerName, int i_BoardSize, bool i_IsComputerMode)
        {
            InitializeComponent();
            r_IsComputerMode = i_IsComputerMode;
            r_ButtonMatrix = new BoardButton[i_BoardSize, i_BoardSize];
            r_Player1Name = i_FirstPlayerName;
            r_Player2Name = i_SecondPlayerName;
            m_Game = new Game(i_BoardSize, i_FirstPlayerName, i_SecondPlayerName, i_IsComputerMode);
            buildGameSizeAndGameScore(i_FirstPlayerName, i_SecondPlayerName, i_BoardSize);
            buildGameBoard();
        }

        private string PlayingColor()
        {
            string playingColor = "White";

            if (m_Game.CurrentTurn == Checkers.eCheckerGroup.X)
            {
                playingColor = "Black";
            }

            return playingColor;
        }

        private void buildGameSizeAndGameScore(string i_FirstPlayerName, string i_SecondPlayerName, int i_BoardSize)
        {
            this.ClientSize = new Size((k_SquareSize * i_BoardSize) + (2 * k_BoardBorders), (k_SquareSize * i_BoardSize) + k_BoardTopSize + k_BoardBorders);
            this.StartPosition = FormStartPosition.CenterScreen;
            labelFirstPlayerName = new Label();
            labelFirstPlayerName.BackColor = Color.Turquoise;
            labelFirstPlayerName.Text = i_FirstPlayerName + "'s Score: " + m_Game.FirstPlayer.TotalScore.ToString();
            labelFirstPlayerName.Top += 20;
            labelFirstPlayerName.Left += (ClientSize.Width / 2) - (k_SquareSize * 3);
            labelFirstPlayerName.Width = 120;
            labelSecondPlayerName = new Label();
            labelSecondPlayerName.BackColor = Color.Turquoise;
            labelSecondPlayerName.Text = i_SecondPlayerName + "'s Score: " + m_Game.SecondtPlayer.TotalScore.ToString();
            labelSecondPlayerName.Top += 20;
            labelSecondPlayerName.Left += (ClientSize.Width / 2) + k_SquareSize;
            labelSecondPlayerName.Width = 120;

            labelTurnNow = new Label();
            labelTurnNow.BackColor = Color.Teal;
            labelTurnNow.Text = "Turn Now:   " + PlayingColor();
            labelTurnNow.Font = new Font(labelTurnNow.Font, FontStyle.Bold);
            labelTurnNow.Top += 20;
            labelTurnNow.Left += (ClientSize.Width / 2) - 48;
            labelTurnNow.AutoSize = true;

            this.Controls.Add(labelFirstPlayerName);
            this.Controls.Add(labelSecondPlayerName);
            this.Controls.Add(labelTurnNow);
        }

        private void buildGameBoard()
        {
            int top = k_BoardTopSize;
            int left = k_BoardBorders;
            Checkers[,] boardMatrix  = m_Game.Board.GetBoardMatrix();
            char rowIndex = 'a';
            char columnIndex = 'A';
            for (int i = 0; i < boardMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < boardMatrix.GetLength(0); j++)
                {
                    BoardButton newButton = new BoardButton(rowIndex, columnIndex);
                    newButton.Top = top;
                    newButton.Left = left;
                    newButton.Size = new Size(k_SquareSize, k_SquareSize);
                    newButton.Click += new EventHandler(button_Click);
                    r_ButtonMatrix[i, j] = newButton;
                    if (boardMatrix[i, j].Group == Checkers.eCheckerGroup.O)
                    {
                        newButton.BackgroundImage = Properties.Resources.WhiteSolider;
                        newButton.BackgroundImageLayout = ImageLayout.Stretch;
                        newButton.TagName = "O";
                    }
                    else if (boardMatrix[i, j].Group == Checkers.eCheckerGroup.X)
                    {
                        newButton.BackgroundImage = Properties.Resources.BlackSolider;
                        newButton.BackgroundImageLayout = ImageLayout.Stretch;
                        newButton.TagName = "X";
                    }
                    else if (boardMatrix[i, j].Group == Checkers.eCheckerGroup.Empty)
                    {
                        newButton.TagName = string.Empty;
                    }

                    this.Controls.Add(newButton);
                    left += k_SquareSize;
                    columnIndex = (char)(columnIndex + 1);
                }

                left = k_BoardBorders;
                top += k_SquareSize;
                columnIndex = 'A';
                rowIndex = (char)(rowIndex + 1);
            }  
        }

        private void updateScoreStatus()
        {
            labelFirstPlayerName.Text = string.Format("{0} : {1}", r_Player1Name, m_Game.FirstPlayer.TotalScore);
            labelSecondPlayerName.Text = string.Format("{0} : {1}", r_Player2Name, m_Game.SecondtPlayer.TotalScore);
        }

        private bool checkEndOfTheGame()
        {
            bool isGameEnd = false;

            if (m_Game.IsTie || m_Game.IsGameEnd)
            {
                isGameEnd = true;
                updateScoreStatus();
                bool wantAnotherGame = askForAnotherGame(m_Game.IsGameEnd, m_Game.IsTie);

                if (wantAnotherGame)
                {
                    startAnotherGame();
                }
                else
                {
                    this.Close();
                }
            }

            return isGameEnd;
        }

        private void tryToPlaySelectedMovment(string i_Movement)
        {
            if (m_Game.ConvertMovementFromStringToIntAndPlayMovmentIfPossibleMovement(i_Movement))
            {
                printGameBoard();
                bool isGameEnd = checkEndOfTheGame();
                while (!isGameEnd && m_Game.CurrentTurn == Checkers.eCheckerGroup.O && r_IsComputerMode)
                {
                    m_Game.RandomMovment();
                    printGameBoard();
                    isGameEnd = checkEndOfTheGame();
                }
            }
            else
            {
                MessageBox.Show("Invalid movment!, Try again");
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            BoardButton selectedButton = sender as BoardButton;
            Checkers.eCheckerGroup currentTurn = m_Game.CurrentTurn;
            if (object.ReferenceEquals(selectedButton, m_FirstSelectedButton))
             {
                selectedButton.BackColor = default(Color);
                m_FirstSelectedButton = null;
            }
            else
            {
                if (currentTurn == Checkers.eCheckerGroup.O)
                {
                    if (m_FirstSelectedButton == null && (selectedButton.TagName == "O" || selectedButton.TagName == "U"))
                    {
                        selectedButton.BackColor = Color.LightBlue;
                        m_FirstSelectedButton = selectedButton;
                    }
                    else if(m_FirstSelectedButton != null)
                    {
                        string movement = m_FirstSelectedButton.ColumnIndex + string.Empty + m_FirstSelectedButton.RowIndex + ">" + selectedButton.ColumnIndex + selectedButton.RowIndex;
                        tryToPlaySelectedMovment(movement);
                        m_FirstSelectedButton.BackColor = default(Color);
                        m_FirstSelectedButton = null;
                    }
                }
                else
                {
                    if (m_FirstSelectedButton == null && (selectedButton.TagName == "X" || selectedButton.TagName == "K"))
                    {
                        selectedButton.BackColor = Color.LightBlue;
                        m_FirstSelectedButton = selectedButton;
                    }
                    else if (m_FirstSelectedButton != null)
                    {
                        string movement = m_FirstSelectedButton.ColumnIndex + string.Empty + m_FirstSelectedButton.RowIndex + ">" + selectedButton.ColumnIndex + selectedButton.RowIndex;
                        tryToPlaySelectedMovment(movement);
                        m_FirstSelectedButton.BackColor = default(Color);
                        m_FirstSelectedButton = null;
                    }
                }
            }
        }

        private void startAnotherGame()
        {
            m_Game.ResetGame();
            printGameBoard();
        }

        private bool askForAnotherGame(bool i_GameWon, bool i_GameTie)
        {
            string message = string.Empty;
            string title = string.Empty;

            if (i_GameWon)
            {
                message = creatWonMessage();
                title = "A Win!";
            }
            else if (i_GameTie)
            {
                message = string.Format("Tie!!{0}Another Round?", Environment.NewLine);
                title = "A Tie!";
            }

            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            bool anotherGame = false;

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                anotherGame = true;
            }

            return anotherGame;
        }

        private string creatWonMessage()
        {
            StringBuilder wonStringBuilder = new StringBuilder();

            wonStringBuilder.Append(m_Game.WinnerName);
            wonStringBuilder.AppendLine(" Won!");
            wonStringBuilder.AppendLine("Another Round?");

            return wonStringBuilder.ToString();
        }

        private void printGameBoard()
        {
            labelTurnNow.Text = "Turn Now:   " + PlayingColor();
            Checkers[,] boardMatrix = m_Game.Board.GetBoardMatrix();
            for (int i = 0; i < r_ButtonMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < r_ButtonMatrix.GetLength(0); j++)
                {
                    if (boardMatrix[i, j].Group == Checkers.eCheckerGroup.O)
                    {
                        r_ButtonMatrix[i, j].TagName = "O";
                        r_ButtonMatrix[i, j].BackgroundImage = Properties.Resources.WhiteSolider;
                    }
                    else if (boardMatrix[i, j].Group == Checkers.eCheckerGroup.OKing)
                    {
                        r_ButtonMatrix[i, j].TagName = "U";
                        r_ButtonMatrix[i, j].BackgroundImage = Properties.Resources.WhiteKing;
                    }
                    else if (boardMatrix[i, j].Group == Checkers.eCheckerGroup.X)
                    {
                        r_ButtonMatrix[i, j].TagName = "X";
                        r_ButtonMatrix[i, j].BackgroundImage = Properties.Resources.BlackSolider;
                    }
                    else if (boardMatrix[i, j].Group == Checkers.eCheckerGroup.XKing)
                    {
                        r_ButtonMatrix[i, j].TagName = "K";
                        r_ButtonMatrix[i, j].BackgroundImage = Properties.Resources.BlackKing;
                    }
                    else if (boardMatrix[i, j].Group == Checkers.eCheckerGroup.Empty)
                    {
                        r_ButtonMatrix[i, j].TagName = string.Empty;
                        r_ButtonMatrix[i, j].BackgroundImage = null;
                    }

                    r_ButtonMatrix[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }
    }
}

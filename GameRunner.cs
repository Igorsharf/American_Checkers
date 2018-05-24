using System;
using System.Collections.Generic;
using System.Text;

namespace DN_IDC_2018B_Ex02
{
    public class GameRunner
    {
        private const string k_EnterPlayerName = "Hi, please enter your name:";
        private const string k_EnterSecondPlayerName = "Hi competitor, please enter your name:";
        private const string k_EnterBoardSize = "Please choose your desired board size 6, 8 or 10";
        private const string k_GameMode = "Please enter 1 to play agains the computer or 2 to play against your friend";
        private const string k_PlayAnotherGame = "Please enter 'y' if you want to play another round and 'n' if you want to quit";
        private const string k_GameOver = "Game Over! Bye Bye";
        private const int k_MaxNameLength = 20;
        private const int k_LengthOfInput = 5;

        private string m_FirstPlayerName;
        private string m_SecondPlayerName;
        private int m_BoardSize;
        private Game.eGameMode m_GameMode;
        private Game m_Game;

        internal void Run()
        {
            getGameSettings();
            m_Game = new Game(m_BoardSize, m_FirstPlayerName, m_SecondPlayerName, m_GameMode);
            Ex02.ConsoleUtils.Screen.Clear();
            playNewGame();
        }

        internal void playNewGame()
        {
            printGameBoard(m_Game.Board);
            printNewTurn();

            playMatch();

            if (m_Game.IsTie)
            {
                string tie = string.Format("it is a tie! Total score: {0} - {1} points, {2} - {3} points.", m_Game.FirstPlayer.Name, m_Game.FirstPlayer.TotalScore, m_Game.SecondtPlayer.Name, m_Game.SecondtPlayer.TotalScore);
                Console.WriteLine(tie);
            }
            else if (m_Game.IsGameEnd || m_Game.Quit)
            {
                string win;

                if (m_Game.FirstPlayer.CurrentGameScore > m_Game.SecondtPlayer.CurrentGameScore)
                {
                    win = string.Format("{0} won! Total score: {0} - {1} points, {2} - {3} points.", m_Game.FirstPlayer.Name, m_Game.FirstPlayer.TotalScore, m_Game.SecondtPlayer.Name, m_Game.SecondtPlayer.TotalScore);
                }
                else if (m_Game.FirstPlayer.CurrentGameScore < m_Game.SecondtPlayer.CurrentGameScore)
                {
                    win = string.Format("{0} won! Total score: {0} - {1} points, {2} - {3} points.", m_Game.SecondtPlayer.Name, m_Game.SecondtPlayer.TotalScore, m_Game.FirstPlayer.Name, m_Game.FirstPlayer.TotalScore);
                }
                else
                {
                    win = string.Format("It is a tie! Total score: {0} - {1} points, {2} - {3} points.", m_Game.FirstPlayer.Name, m_Game.FirstPlayer.TotalScore, m_Game.SecondtPlayer.Name, m_Game.SecondtPlayer.TotalScore);
                }

                Console.WriteLine(Environment.NewLine + win);
                bool anotherRound = isWantAnotherRound();
                if (anotherRound)
                {
                    m_Game.ResetGame();
                    Ex02.ConsoleUtils.Screen.Clear();
                    playNewGame();
                }
                else
                {
                    Console.WriteLine(k_GameOver);
                }
            }
        }

        private void playMatch()
        {
            while (!m_Game.Quit && !m_Game.IsTie && !m_Game.IsGameEnd)
            {
                getAndCheckMovmentFromUser();
                Ex02.ConsoleUtils.Screen.Clear();
                printGameBoard(m_Game.Board);
                if (!m_Game.Quit)
                {
                    printLastTurn();
                    printNewTurn();
                }
                else
                {
                    string quitMessage = string.Format("{0} quitted.", m_Game.GetCurrentPlayerName());
                    Console.WriteLine(quitMessage);
                }
            }
        }

        private bool isWantAnotherRound()
        {
            Console.WriteLine(k_PlayAnotherGame);
            bool isWantAnotherRound = false;
            bool isValidInput = false;

            do
            {
                string answer = Console.ReadLine();
                if (answer.Equals("y"))
                {
                    isWantAnotherRound = true;
                    isValidInput = true;
                }
                else if (answer.Equals("n"))
                {
                    isValidInput = true;
                }

                if (!isValidInput)
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            while (!isValidInput);

            return isWantAnotherRound;
        }

        private void printNewTurn()
        {
            string printTurn;

            switch (m_Game.CurrentTurn)
            {
                case Checkers.eCheckerGroup.O:
                    printTurn = string.Format("{0}'s turn (O):", m_SecondPlayerName);
                    break;
                default:
                    printTurn = string.Format("{0}'s turn (X):", m_FirstPlayerName);
                    break;
            }

            Console.Write(printTurn);
        }

        private void printLastTurn()
        {
            string printTurn = string.Format("{0}'s move was ({1}): {2}", m_Game.LastPlayerThatPlayed.Name, m_Game.LastPlayerThatPlayed.BelongingGroup, m_Game.LastMovment);
            Console.WriteLine(printTurn);
        }

        private void getGameSettings()
        {
            m_FirstPlayerName = getPlayerName(k_EnterPlayerName);
            getBoardSize();
            int gameMode = getGameMode();
            m_GameMode = (Game.eGameMode)Enum.ToObject(typeof(Game.eGameMode), gameMode);
            if (m_GameMode == Game.eGameMode.MultiMode)
            {
                m_SecondPlayerName = getPlayerName(k_EnterSecondPlayerName);
            }
            else
            {
                m_SecondPlayerName = "Computer";
            }
        }

        private int getGameMode()
        {
            bool validInput = false;
            int gameMode;
            Console.WriteLine(k_GameMode);

            do
            {
                if (int.TryParse(Console.ReadLine(), out gameMode) && (gameMode == 1 || gameMode == 2))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            while (!validInput);

            return gameMode;
        }

        private void getBoardSize()
        {
            bool isValidBoardSize = false;
            Console.WriteLine(k_EnterBoardSize);

            do
            {
                bool isValidNumber = int.TryParse(Console.ReadLine(), out m_BoardSize);
                if (isValidNumber && ((m_BoardSize == 6) || (m_BoardSize == 8) || (m_BoardSize == 10)))
                {
                    isValidBoardSize = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid board size");
                }
            }
            while (!isValidBoardSize);
        }

        private string getPlayerName(string welcomeSentences)
        {
            bool isValidFirstPlayerName = false;
            string playerName;
            Console.WriteLine(welcomeSentences);

            do
            {
                playerName = Console.ReadLine();
                if (playerName.Length <= k_MaxNameLength && !isTabsInString(playerName) && !playerName.Equals(string.Empty))
                {
                    isValidFirstPlayerName = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid name (Max 20 chars and without spaces)");
                }
            }
            while (!isValidFirstPlayerName);

            return playerName;
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

        private void printGameBoard(Board i_Board)
        {
            StringBuilder consoleBoardStringBuilder = new StringBuilder();
            Checkers[,] checkersMatrix = i_Board.GetBoardMatrix();
            ////first Row
            consoleBoardStringBuilder.Append(" ");
            for (int i = 0; i < m_BoardSize; i++)
            {
                consoleBoardStringBuilder.Append("  " + (char)('A' + i) + " ");
            }

            StringBuilder seperatorlLine = new StringBuilder();
            seperatorlLine.Append(" =");

            ////seperator Row
            for (int i = 0; i < m_BoardSize * 4; i++)
            {
                seperatorlLine.Append("=");
            }

            consoleBoardStringBuilder.Append(Environment.NewLine + seperatorlLine + Environment.NewLine);

            for (int i = 0; i < m_BoardSize; i++)
            {
                consoleBoardStringBuilder.Append((char)('a' + i) + "|");
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (checkersMatrix[i, j].Group == Checkers.eCheckerGroup.O)
                    {
                        consoleBoardStringBuilder.Append(" O |");
                    }
                    else if (checkersMatrix[i, j].Group == Checkers.eCheckerGroup.X)
                    {
                        consoleBoardStringBuilder.Append(" X |");
                    }
                    else if (checkersMatrix[i, j].Group == Checkers.eCheckerGroup.Empty)
                    {
                        consoleBoardStringBuilder.Append("   |");
                    }
                    else if (checkersMatrix[i, j].Group == Checkers.eCheckerGroup.OKing)
                    {
                        consoleBoardStringBuilder.Append(" U |");
                    }
                    else
                    {
                        consoleBoardStringBuilder.Append(" K |");
                    }
                }

                if (i != m_BoardSize - 1)
                {
                    consoleBoardStringBuilder.Append(Environment.NewLine + seperatorlLine + Environment.NewLine);
                }
                else
                {
                    consoleBoardStringBuilder.Append(Environment.NewLine + seperatorlLine);
                }
            }

            Console.WriteLine(consoleBoardStringBuilder);
        }

        private void getAndCheckMovmentFromUser()
        {
            string movementInput;
            bool isValid = false;
            bool isQuit = false;
            if (m_Game.Mode == Game.eGameMode.MultiMode || m_Game.CurrentTurn == Checkers.eCheckerGroup.X)
            {
                do
                {
                    movementInput = Console.ReadLine();
                    isValid = checkInputFromUser(movementInput);
                    if (!isValid && movementInput.Equals("Q"))
                    {
                        isQuit = checkIsQuit(movementInput);
                        if (!isQuit)
                        {
                            Console.WriteLine("Don't quit you are leading!");
                        }
                    }

                    if (isValid)
                    {
                        isValid = m_Game.ConvertMovementFromStringToIntAndPlayMovmentIfPossibleMovement(movementInput);
                    }
                    else if (isQuit)
                    {
                        isValid = true;
                        m_Game.Quit = true;
                    }

                    if (!isValid)
                    {
                        Console.WriteLine("Invalid Input. Please try again: ");
                    }
                }
                while (!isValid);
            }
            else
            {
                m_Game.RandomMovment();
            }
        }

        private bool checkIsQuit(string i_MovementInput)
        {
            bool isValidToQuit = false;
            int firstPlayerScore = m_Game.FirstPlayer.CurrentGameScore;
            int secondPlayerScore = m_Game.SecondtPlayer.CurrentGameScore;

            if (m_Game.CurrentTurn == Checkers.eCheckerGroup.O && secondPlayerScore <= firstPlayerScore)
            {
                isValidToQuit = true;
            }
            else if (m_Game.CurrentTurn == Checkers.eCheckerGroup.X && secondPlayerScore >= firstPlayerScore)
            {
                isValidToQuit = true;
            }

            return isValidToQuit;
        }

        private bool checkInputFromUser(string i_MovementInput)
        {
            bool isValid = true;
            int firstPlayerScore = m_Game.FirstPlayer.CurrentGameScore;
            int secondPlayerScore = m_Game.SecondtPlayer.CurrentGameScore;

            if (i_MovementInput.Length != k_LengthOfInput && i_MovementInput.Length != 1)
            {
                isValid = false;
            }
            else if (i_MovementInput.Equals("Q"))
            {
                isValid = false;
            }
            else
            {
                if (i_MovementInput[0] < 'A' || i_MovementInput[0] > (char)('A' + m_BoardSize))
                {
                    isValid = false;
                }

                if (i_MovementInput[3] < 'A' || i_MovementInput[3] > (char)('A' + m_BoardSize))
                {
                    isValid = false;
                }

                if (i_MovementInput[1] < 'a' || i_MovementInput[1] > (char)('a' + m_BoardSize))
                {
                    isValid = false;
                }

                if (i_MovementInput[4] < 'a' || i_MovementInput[4] > (char)('a' + m_BoardSize))
                {
                    isValid = false;
                }

                if (!i_MovementInput[2].Equals('>'))
                {
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DN_IDC_2018B_Ex02
{
    internal class Player
    {
        private const int k_NumOfEmtyRowsAtBeginingOfTheGame = 2;

        private string m_Name;
        private Checkers[] m_CheckersArray;
        private int m_RemainCheckers;
        private List<Checkers> m_PosibleMovesLocations;
        private List<Checkers> m_PosibleMovesDestinations;
        private int m_NumberMovementOppertunities = 0;
        private int m_TotalScore = 0;
        private int m_CurrentGameScore = 0;
        private Checkers.eCheckerGroup m_BelongingGroup;
        private bool m_IsPossibleEat = false;

        internal Player(int io_BoardSize, Checkers.eCheckerGroup i_BelongingGroup, string i_Name)
        {
            this.m_Name = i_Name;
            this.m_RemainCheckers = ((io_BoardSize - k_NumOfEmtyRowsAtBeginingOfTheGame) / 2) * (io_BoardSize / 2);
            this.m_CheckersArray = new Checkers[m_RemainCheckers];
            this.m_BelongingGroup = i_BelongingGroup;
            initializeCheckerArrayAtBeginingOfTheGame(io_BoardSize);
        }

        internal void ResetPlayer(int io_BoardSize)
        {
            this.m_RemainCheckers = ((io_BoardSize - k_NumOfEmtyRowsAtBeginingOfTheGame) / 2) * (io_BoardSize / 2);
            this.m_CheckersArray = new Checkers[m_RemainCheckers];
            initializeCheckerArrayAtBeginingOfTheGame(io_BoardSize);
            m_CurrentGameScore = 0;
            m_IsPossibleEat = false;
        }

        private void initializeCheckerArrayAtBeginingOfTheGame(int i_BoardSize)
        {
            int insertIndex = 0;
            int numberOfRowsPerPlayer = (i_BoardSize - k_NumOfEmtyRowsAtBeginingOfTheGame) / 2;

            if (m_BelongingGroup == Checkers.eCheckerGroup.O)
            {
                for (int i = 0; i < numberOfRowsPerPlayer; i++)
                {
                    for (int j = 0; j < i_BoardSize; j++)
                    {
                        if (j % 2 != i % 2)
                        {
                            m_CheckersArray[insertIndex] = new Checkers(Checkers.eCheckerGroup.O, i, j);
                            insertIndex++;
                        }
                    }
                }
            }
            else
            {
                for (int i = i_BoardSize - numberOfRowsPerPlayer; i < i_BoardSize; i++)
                {
                    for (int j = 0; j < i_BoardSize; j++)
                    {
                        if (j % 2 != i % 2)
                        {
                            m_CheckersArray[insertIndex] = new Checkers(Checkers.eCheckerGroup.X, i, j);
                            insertIndex++;
                        }
                    }
                }
            }
        }

        internal void BuildPlayerPossibleMovments(Board o_Board)
        {
            m_PosibleMovesLocations = new List<Checkers>();
            m_PosibleMovesDestinations = new List<Checkers>();
            m_NumberMovementOppertunities = 0;
            Checkers.eCheckerGroup oppositeGroup;
            m_IsPossibleEat = false;

            switch (m_BelongingGroup)
            {
                case Checkers.eCheckerGroup.O:
                    oppositeGroup = Checkers.eCheckerGroup.X;
                    break;
                default:
                    oppositeGroup = Checkers.eCheckerGroup.O;
                    break;
            }

            Checkers.eCheckerGroup oppositeGroupKing = GetOppositePlayerKing(oppositeGroup);

            foreach (Checkers current in CheckersArray)
            {
                if (current.Group != Checkers.eCheckerGroup.Empty)
                {
                    if (m_BelongingGroup == Checkers.eCheckerGroup.O)
                    {
                        checkPossibleGoingDownAndRight(current, o_Board, oppositeGroup, oppositeGroupKing);
                        checkPossibleGoingDownAndLeft(current, o_Board, oppositeGroup, oppositeGroupKing);
                        if (current.Group == Checkers.eCheckerGroup.OKing)
                        {
                            checkPossibleGoingUpAndRight(current, o_Board, oppositeGroup, oppositeGroupKing);
                            checkPossibleGoingUpAndLeft(current, o_Board, oppositeGroup, oppositeGroupKing);
                        }
                    }
                    else
                    {
                        checkPossibleGoingUpAndRight(current, o_Board, oppositeGroup, oppositeGroupKing);
                        checkPossibleGoingUpAndLeft(current, o_Board, oppositeGroup, oppositeGroupKing);
                        if (current.Group == Checkers.eCheckerGroup.XKing)
                        {
                            checkPossibleGoingDownAndRight(current, o_Board, oppositeGroup, oppositeGroupKing);
                            checkPossibleGoingDownAndLeft(current, o_Board, oppositeGroup, oppositeGroupKing);
                        }
                    }
                }
            }
        }

        private void checkPossibleGoingUpAndRight(Checkers io_CurrentCheckerToCheck, Board i_Board, Checkers.eCheckerGroup i_OppositeCheckerGroup, Checkers.eCheckerGroup i_OppositeGroupKing)
        {
            int boardMaxIndex = i_Board.BoardSize - 1;
            Checkers[,] boardMatrix = i_Board.GetBoardMatrix();
            int currentCheckerRow = io_CurrentCheckerToCheck.Row;
            int currentCheckerColumn = io_CurrentCheckerToCheck.Column;
            if (currentCheckerRow != 0 && currentCheckerColumn != boardMaxIndex)
            {
                if ((boardMatrix[currentCheckerRow - 1, currentCheckerColumn + 1].Group == i_OppositeCheckerGroup || boardMatrix[currentCheckerRow - 1, currentCheckerColumn + 1].Group == i_OppositeGroupKing) && currentCheckerRow != 1 && currentCheckerColumn != boardMaxIndex - 1)
                {
                    if (boardMatrix[currentCheckerRow - 2, currentCheckerColumn + 2].Group == Checkers.eCheckerGroup.Empty)
                    {
                        if (!m_IsPossibleEat)
                        {
                            initializeEatingArrays(io_CurrentCheckerToCheck);
                        }
                        else
                        {
                            m_PosibleMovesLocations.Add(io_CurrentCheckerToCheck);
                        }

                        m_PosibleMovesDestinations.Add(boardMatrix[currentCheckerRow - 2, currentCheckerColumn + 2]);
                        m_NumberMovementOppertunities++;
                    }
                }
                else if (boardMatrix[currentCheckerRow - 1, currentCheckerColumn + 1].Group == Checkers.eCheckerGroup.Empty && !m_IsPossibleEat)
                {
                    m_PosibleMovesLocations.Add(io_CurrentCheckerToCheck);
                    m_PosibleMovesDestinations.Add(boardMatrix[currentCheckerRow - 1, currentCheckerColumn + 1]);
                    m_NumberMovementOppertunities++;
                }
            }
        }

        private void checkPossibleGoingUpAndLeft(Checkers io_CurrentCheckerToCheck, Board i_Board, Checkers.eCheckerGroup i_OppositeCheckerGroup, Checkers.eCheckerGroup i_OppositeGroupKing)
        {
            Checkers[,] boardMatrix = i_Board.GetBoardMatrix();
            int currentCheckerRow = io_CurrentCheckerToCheck.Row;
            int currentCheckerColumn = io_CurrentCheckerToCheck.Column;
            if (currentCheckerRow != 0 && currentCheckerColumn != 0)
            {
                if ((boardMatrix[currentCheckerRow - 1, currentCheckerColumn - 1].Group == i_OppositeCheckerGroup || boardMatrix[currentCheckerRow - 1, currentCheckerColumn - 1].Group == i_OppositeGroupKing) && currentCheckerRow != 1 && currentCheckerColumn != 1)
                {
                    if (boardMatrix[currentCheckerRow - 2, currentCheckerColumn - 2].Group == Checkers.eCheckerGroup.Empty)
                    {
                        if (!m_IsPossibleEat)
                        {
                            initializeEatingArrays(io_CurrentCheckerToCheck);
                        }
                        else
                        {
                            m_PosibleMovesLocations.Add(io_CurrentCheckerToCheck);
                        }

                        m_PosibleMovesDestinations.Add(boardMatrix[currentCheckerRow - 2, currentCheckerColumn - 2]);
                        m_NumberMovementOppertunities++;
                    }
                }
                else if (boardMatrix[currentCheckerRow - 1, currentCheckerColumn - 1].Group == Checkers.eCheckerGroup.Empty && !m_IsPossibleEat)
                {
                    m_PosibleMovesLocations.Add(io_CurrentCheckerToCheck);
                    m_PosibleMovesDestinations.Add(boardMatrix[currentCheckerRow - 1, currentCheckerColumn - 1]);
                    m_NumberMovementOppertunities++;
                }
            }
        }

        private void checkPossibleGoingDownAndRight(Checkers io_CurrentCheckerToCheck, Board i_Board, Checkers.eCheckerGroup i_OppositeCheckerGroup, Checkers.eCheckerGroup i_OppositeGroupKing)
        {
            int boardMaxIndex = i_Board.BoardSize - 1;
            Checkers[,] boardMatrix = i_Board.GetBoardMatrix();
            int currentCheckerRow = io_CurrentCheckerToCheck.Row;
            int currentCheckerColumn = io_CurrentCheckerToCheck.Column;
            if (currentCheckerRow != boardMaxIndex && currentCheckerColumn != boardMaxIndex)
            {
                if ((boardMatrix[currentCheckerRow + 1, currentCheckerColumn + 1].Group == i_OppositeCheckerGroup || boardMatrix[currentCheckerRow + 1, currentCheckerColumn + 1].Group == i_OppositeGroupKing) && currentCheckerRow != boardMaxIndex - 1 && currentCheckerColumn != boardMaxIndex - 1)
                {
                    if (boardMatrix[currentCheckerRow + 2, currentCheckerColumn + 2].Group == Checkers.eCheckerGroup.Empty)
                    {
                        if (!m_IsPossibleEat)
                        {
                            initializeEatingArrays(io_CurrentCheckerToCheck);
                        }
                        else
                        {
                            m_PosibleMovesLocations.Add(io_CurrentCheckerToCheck);
                        }

                        m_PosibleMovesDestinations.Add(boardMatrix[currentCheckerRow + 2, currentCheckerColumn + 2]);
                        m_NumberMovementOppertunities++;
                    }
                }
                else if (boardMatrix[currentCheckerRow + 1, currentCheckerColumn + 1].Group == Checkers.eCheckerGroup.Empty && !m_IsPossibleEat)
                {
                    m_PosibleMovesLocations.Add(io_CurrentCheckerToCheck);
                    m_PosibleMovesDestinations.Add(boardMatrix[currentCheckerRow + 1, currentCheckerColumn + 1]);
                    m_NumberMovementOppertunities++;
                }
            }
        }

        private void checkPossibleGoingDownAndLeft(Checkers io_CurrentCheckerToCheck, Board i_Board, Checkers.eCheckerGroup i_OppositeCheckerGroup, Checkers.eCheckerGroup i_OppositeGroupKing)
        {
            int boardMaxIndex = i_Board.BoardSize - 1;
            Checkers[,] boardMatrix = i_Board.GetBoardMatrix();
            int currentCheckerRow = io_CurrentCheckerToCheck.Row;
            int currentCheckerColumn = io_CurrentCheckerToCheck.Column;
            if (currentCheckerRow != boardMaxIndex && currentCheckerColumn != 0)
            {
                if ((boardMatrix[currentCheckerRow + 1, currentCheckerColumn - 1].Group == i_OppositeCheckerGroup || boardMatrix[currentCheckerRow + 1, currentCheckerColumn - 1].Group == i_OppositeGroupKing) && currentCheckerRow != boardMaxIndex - 1 && currentCheckerColumn != 1)
                {
                    if (boardMatrix[currentCheckerRow + 2, currentCheckerColumn - 2].Group == Checkers.eCheckerGroup.Empty)
                    {
                        if (!m_IsPossibleEat)
                        {
                            initializeEatingArrays(io_CurrentCheckerToCheck);
                        }
                        else
                        {
                            m_PosibleMovesLocations.Add(io_CurrentCheckerToCheck);
                        }

                        m_PosibleMovesDestinations.Add(boardMatrix[currentCheckerRow + 2, currentCheckerColumn - 2]);
                        m_NumberMovementOppertunities++;
                    }
                }
                else if (boardMatrix[currentCheckerRow + 1, currentCheckerColumn - 1].Group == Checkers.eCheckerGroup.Empty && !m_IsPossibleEat)
                {
                    m_PosibleMovesLocations.Add(io_CurrentCheckerToCheck);
                    m_PosibleMovesDestinations.Add(boardMatrix[currentCheckerRow + 1, currentCheckerColumn - 1]);
                    m_NumberMovementOppertunities++;
                }
            }
        }

        private void initializeEatingArrays(Checkers o_CurrentCheckerToCheck)
        {
            m_PosibleMovesLocations = new List<Checkers>();
            m_PosibleMovesDestinations = new List<Checkers>();
            m_NumberMovementOppertunities = 0;
            m_IsPossibleEat = true;
            m_PosibleMovesLocations.Add(o_CurrentCheckerToCheck);
        }

        internal string GetRandomMovment(Board o_Board)
        {
            this.BuildPlayerPossibleMovments(o_Board);
            Random random = new Random();
            int choosenIndex = random.Next(0, m_NumberMovementOppertunities);
            string movment = m_PosibleMovesLocations[choosenIndex].Row.ToString() + m_PosibleMovesLocations[choosenIndex].Column.ToString() + ">" + m_PosibleMovesDestinations[choosenIndex].Row.ToString() + m_PosibleMovesDestinations[choosenIndex].Column.ToString();

            return movment;
        }

        internal void CalcCurrentGameScore()
        {
            int gameScore = 0;

            foreach (Checkers current in CheckersArray)
            {
                if (current.Group == Checkers.eCheckerGroup.O || current.Group == Checkers.eCheckerGroup.X) 
                {
                    gameScore++;
                }
                else if (current.Group == Checkers.eCheckerGroup.OKing || current.Group == Checkers.eCheckerGroup.XKing)
                    {
                        gameScore += 4;
                    }
            }

            m_CurrentGameScore = gameScore;
        }

        internal string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        internal Checkers[] CheckersArray
        {
            get
            {
                return m_CheckersArray;
            }

            set
            {
                m_CheckersArray = value;
            }
        }

        internal List<Checkers> PosibleMovesLocations
        {
            get
            {
                return m_PosibleMovesLocations;
            }

            set
            {
                m_PosibleMovesLocations = value;
            }
        }

        internal List<Checkers> PosibleMovesDestinations
        {
            get
            {
                return m_PosibleMovesDestinations;
            }

            set
            {
                m_PosibleMovesDestinations = value;
            }
        }

        internal bool IsPossibleEat
        {
            get
            {
                return m_IsPossibleEat;
            }

            set
            {
                m_IsPossibleEat = value;
            }
        }

        internal int RemainCheckers
        {
            get
            {
                return m_RemainCheckers;
            }

            set
            {
                m_RemainCheckers = value;
            }
        }

        internal int NumberMovementOppertunities
        {
            get
            {
                return m_NumberMovementOppertunities;
            }

            set
            {
                m_NumberMovementOppertunities = value;
            }
        }

        internal int TotalScore
        {
            get
            {
                return m_TotalScore;
            }

            set
            {
                m_TotalScore = value;
            }
        }

        internal int CurrentGameScore
        {
            get
            {
                CalcCurrentGameScore();
                return m_CurrentGameScore;
            }
        }

        internal Checkers.eCheckerGroup BelongingGroup
        {
            get
            {
                return m_BelongingGroup;
            }

            set
            {
                m_BelongingGroup = value;
            }
        }

        internal Checkers.eCheckerGroup GetOppositePlayerKing(Checkers.eCheckerGroup i_Group)
        {
            if (i_Group == Checkers.eCheckerGroup.O)
            {
                return Checkers.eCheckerGroup.OKing;
            }
            else
            {
                return Checkers.eCheckerGroup.XKing;
            }
        }
    }
}

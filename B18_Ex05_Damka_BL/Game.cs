﻿using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05_Damka_BL
{
    public class Game
    {
        internal enum eGameMode
        {
            SingleMode = 1,
            MultiMode = 2,
        }

        private Board m_Board;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private eGameMode m_GameMode;
        private Checkers.eCheckerGroup m_CurrentTurn = Checkers.eCheckerGroup.X;
        private bool m_IsGameEnd = false;
        private bool m_Tie = false;
        private bool m_Quit = false;
        private string m_LastMovment;
        private string m_WinnerName = null;
        private Player m_LastPlayerThatPlayed = null;
        private bool m_isLastMoveWasEat = false;
        private bool m_IsLastCheckerUpdatedToKing = false;

        public Game(int io_BoardSize, string io_FirstName, string io_SecondName, bool i_IsComputerMode)
        {
            m_FirstPlayer = new Player(io_BoardSize, Checkers.eCheckerGroup.X, io_FirstName);
            m_SecondPlayer = new Player(io_BoardSize, Checkers.eCheckerGroup.O, io_SecondName);
            if (i_IsComputerMode)
            {
                m_GameMode = eGameMode.SingleMode;
            }
            else
            {
                m_GameMode = eGameMode.MultiMode;
            }

            m_Board = new Board(io_BoardSize);
            m_Board.BuildBoard(m_FirstPlayer, m_SecondPlayer);
            m_LastPlayerThatPlayed = m_FirstPlayer;
        }

        public Checkers.eCheckerGroup CurrentTurn
        {
            get
            {
                return m_CurrentTurn;
            }
        }

        internal string LastMovment
        {
            get
            {
                return m_LastMovment;
            }
        }

        public string WinnerName
        {
            get
            {
                return m_WinnerName;
            }
        }

        internal eGameMode Mode
        {
            get
            {
                return m_GameMode;
            }
        }

        public bool IsGameEnd
        {
            get
            {
                return m_IsGameEnd;
            }
        }

        public bool IsLastMoveWasEat
        {
            get
            {
                return m_isLastMoveWasEat;
            }
        }

        public bool IsLastCheckerUpdatedToKing
        {
            get
            {
                return m_IsLastCheckerUpdatedToKing;
            }
        }

        public bool IsTie
        {
            get
            {
                return m_Tie;
            }
        }

        public bool Quit
        {
            get
            {
                return m_Quit;
            }

            set
            {
                if (value == true)
                {
                    UpdatePlayersScoreAtTheEndOfMatch();
                }

                m_Quit = value;
            }
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }
        }

        internal Player LastPlayerThatPlayed
        {
            get
            {
                return m_LastPlayerThatPlayed;
            }
        }

        public Player SecondtPlayer
        {
            get
            {
                return m_SecondPlayer;
            }
        }

        private bool isEndMatch()
        {
            if (m_SecondPlayer.RemainCheckers == 0)
            {
                m_WinnerName = m_FirstPlayer.Name;
                m_IsGameEnd = true;
            }

            if (m_FirstPlayer.RemainCheckers == 0)
            {
                m_WinnerName = m_SecondPlayer.Name;
                m_IsGameEnd = true; 
            }

            if (m_IsGameEnd)
            {
                UpdatePlayersScoreAtTheEndOfMatch();
            }

            return m_IsGameEnd;
        }

        private void changeTurn()
        {
            if (!isEndMatch())
            {
                if (m_CurrentTurn == Checkers.eCheckerGroup.O)
                {
                    m_isLastMoveWasEat = m_SecondPlayer.IsPossibleEat;
                    if (m_isLastMoveWasEat)
                    {
                        m_SecondPlayer.BuildPlayerPossibleMovments(m_Board);
                        deletePossibleMovmentExepctGivenMovment(m_SecondPlayer);
                    }

                    if (m_SecondPlayer.NumberMovementOppertunities == 0 && !m_isLastMoveWasEat)
                    {
                        m_FirstPlayer.BuildPlayerPossibleMovments(m_Board);
                        if (m_FirstPlayer.NumberMovementOppertunities == 0)
                        {
                            m_Tie = true;
                        }
                        else
                        {
                            m_IsGameEnd = true;
                        }

                        UpdatePlayersScoreAtTheEndOfMatch();
                    }

                    if (!m_isLastMoveWasEat || !m_SecondPlayer.IsPossibleEat)
                    {
                        m_CurrentTurn = Checkers.eCheckerGroup.X;
                        m_LastPlayerThatPlayed = m_SecondPlayer;
                    }
                }
                else
                {
                    m_isLastMoveWasEat = m_FirstPlayer.IsPossibleEat;
                    m_FirstPlayer.BuildPlayerPossibleMovments(m_Board);
                    if (m_isLastMoveWasEat)
                    {
                        m_FirstPlayer.BuildPlayerPossibleMovments(m_Board);
                        deletePossibleMovmentExepctGivenMovment(m_FirstPlayer);
                    }

                    if (m_FirstPlayer.NumberMovementOppertunities == 0 && !m_isLastMoveWasEat)
                    {
                        m_SecondPlayer.BuildPlayerPossibleMovments(m_Board);
                        if (m_SecondPlayer.NumberMovementOppertunities == 0)
                        {
                            m_Tie = true;
                        }
                        else
                        {
                            m_IsGameEnd = true;
                        }

                        UpdatePlayersScoreAtTheEndOfMatch();
                    }

                    if (!m_isLastMoveWasEat || !m_FirstPlayer.IsPossibleEat)
                    {
                        m_CurrentTurn = Checkers.eCheckerGroup.O;
                        m_LastPlayerThatPlayed = m_FirstPlayer;
                    }
                }
            }
        }

        public bool ConvertMovementFromStringToIntAndPlayMovmentIfPossibleMovement(string i_Movment)
        {
            Player currentPlayer;
            Player oppositePlayer;

            if (m_CurrentTurn == Checkers.eCheckerGroup.O)
            {
                currentPlayer = SecondtPlayer;
                oppositePlayer = FirstPlayer;
            }
            else
            {
                currentPlayer = FirstPlayer;
                oppositePlayer = SecondtPlayer;
            }

            int beginningColumn = i_Movment[0] - 'A';
            int beginningRow = i_Movment[1] - 'a';
            int destinationgColumn = i_Movment[3] - 'A';
            int destinationgRow = i_Movment[4] - 'a';
            m_LastMovment = i_Movment;

            return CheckPossibleOfMovment(beginningRow, beginningColumn, destinationgRow, destinationgColumn, currentPlayer, oppositePlayer);
        }

        internal bool CheckPossibleOfMovment(int io_BeginningRow, int io_BeginningColumn, int io_DestinationgRow, int io_DestinationgColumn, Player io_CurrentPlayer, Player o_OppositePlayer)
        {
            bool isPossibleMovment = false;
            io_CurrentPlayer.BuildPlayerPossibleMovments(m_Board);

            for (int i = 0; i < io_CurrentPlayer.NumberMovementOppertunities; i++)
            {
                if (io_CurrentPlayer.PosibleMovesLocations[i].Row == io_BeginningRow && io_CurrentPlayer.PosibleMovesLocations[i].Column == io_BeginningColumn)
                {
                    if (io_CurrentPlayer.PosibleMovesDestinations[i].Row == io_DestinationgRow && io_CurrentPlayer.PosibleMovesDestinations[i].Column == io_DestinationgColumn)
                    {
                        isPossibleMovment = true;
                        break;
                    }
                }
            }

            if (isPossibleMovment)
            {
                playMovment(io_BeginningRow, io_BeginningColumn, io_DestinationgRow, io_DestinationgColumn, io_CurrentPlayer, o_OppositePlayer);
            }

            return isPossibleMovment;
        }

        private void playMovment(int i_BeginningRow, int i_BeginningColumn, int i_DestinationgRow, int i_DestinationgColumn, Player i_CurrentPlayer, Player i_OppositePlayer)
        {
            foreach (Checkers currentChecker in i_CurrentPlayer.CheckersArray)
            {
                if (currentChecker.Row == i_BeginningRow && currentChecker.Column == i_BeginningColumn && currentChecker.Group != Checkers.eCheckerGroup.Empty)
                {
                    currentChecker.Row = i_DestinationgRow;
                    currentChecker.Column = i_DestinationgColumn;
                    checkUpdateToKing(currentChecker, i_CurrentPlayer.BelongingGroup, i_DestinationgRow);

                    if (i_CurrentPlayer.IsPossibleEat)
                    {
                        int rowToDelete = (i_BeginningRow + i_DestinationgRow) / 2;
                        int columnToDelete = (i_BeginningColumn + i_DestinationgColumn) / 2;
                        foreach (Checkers currentOppositeChecker in i_OppositePlayer.CheckersArray)
                        {
                            if (currentOppositeChecker.Column == columnToDelete && currentOppositeChecker.Row == rowToDelete && currentOppositeChecker.Group != Checkers.eCheckerGroup.Empty)
                            {
                                currentOppositeChecker.Group = Checkers.eCheckerGroup.Empty;
                                i_OppositePlayer.RemainCheckers--;
                                break;
                            }
                        }
                    }

                    m_Board.BuildBoard(m_FirstPlayer, m_SecondPlayer);
                    changeTurn();
                    break;
                }
            }
        }

        public void RandomMovment()
        {
            string movement = SecondtPlayer.GetRandomMovment(m_Board);
            if (movement != null)
            {
                m_LastMovment = Convert.ToChar(int.Parse(movement[1].ToString()) + 'A').ToString() + Convert.ToChar(int.Parse(movement[0].ToString()) + 'a').ToString() + ">" + Convert.ToChar(int.Parse(movement[4].ToString()) + 'A').ToString() + Convert.ToChar(int.Parse(movement[3].ToString()) + 'a').ToString();
                playMovment(int.Parse(movement[0].ToString()), int.Parse(movement[1].ToString()), int.Parse(movement[3].ToString()), int.Parse(movement[4].ToString()), m_SecondPlayer, m_FirstPlayer);
            }
        }

        private void checkUpdateToKing(Checkers currentChecker, Checkers.eCheckerGroup checkerGroup, int rowDestination)
        {
            m_IsLastCheckerUpdatedToKing = false;

            if (Checkers.eCheckerGroup.O == checkerGroup && rowDestination == m_Board.BoardSize - 1)
            {
                currentChecker.Group = Checkers.eCheckerGroup.OKing;
                m_IsLastCheckerUpdatedToKing = true;
            }
            else if (Checkers.eCheckerGroup.X == checkerGroup && rowDestination == 0)
            {
                currentChecker.Group = Checkers.eCheckerGroup.XKing;
                m_IsLastCheckerUpdatedToKing = true;
            }
        }

        public void UpdatePlayersScoreAtTheEndOfMatch()
        {
            FirstPlayer.CalcCurrentGameScore();
            SecondtPlayer.CalcCurrentGameScore();
            int currentFirstPlayerScore = m_FirstPlayer.CurrentGameScore;
            int currentSecondPlayerScore = m_SecondPlayer.CurrentGameScore;
            int differenceScore = Math.Abs(currentFirstPlayerScore - currentSecondPlayerScore);

            if (currentFirstPlayerScore > currentSecondPlayerScore)
            {
                m_FirstPlayer.TotalScore += differenceScore;
            }
            else
            {
                m_SecondPlayer.TotalScore += differenceScore;
            }
        }

        public void ResetGame()
        {
            m_CurrentTurn = Checkers.eCheckerGroup.X;
            m_LastPlayerThatPlayed = m_FirstPlayer;
            m_Board = new Board(m_Board.BoardSize);
            m_Quit = false;
            m_Tie = false;
            m_IsGameEnd = false;
            m_FirstPlayer.ResetPlayer(m_Board.BoardSize);
            m_SecondPlayer.ResetPlayer(m_Board.BoardSize);
            m_Board.BuildBoard(m_FirstPlayer, m_SecondPlayer);
        }

        private void deletePossibleMovmentExepctGivenMovment(Player io_PlayerToUpdate)
        {
            int rowToCheck = m_LastMovment[4] - 'a';
            int columToCheck = m_LastMovment[3] - 'A';

            for (int i = io_PlayerToUpdate.PosibleMovesLocations.Count - 1; i >= 0; i--)
            {
                if (io_PlayerToUpdate.PosibleMovesLocations[i].Row == rowToCheck && io_PlayerToUpdate.PosibleMovesLocations[i].Column == columToCheck)
                {
                    continue;
                }
                else
                {
                    io_PlayerToUpdate.PosibleMovesLocations.Remove(io_PlayerToUpdate.PosibleMovesLocations[i]);
                    io_PlayerToUpdate.NumberMovementOppertunities--;
                    io_PlayerToUpdate.PosibleMovesDestinations.Remove(io_PlayerToUpdate.PosibleMovesDestinations[i]);
                }
            }

            if (io_PlayerToUpdate.NumberMovementOppertunities == 0)
            {
                io_PlayerToUpdate.IsPossibleEat = false;
            }
            else
            {
                m_LastPlayerThatPlayed = io_PlayerToUpdate;
            }
        }

        internal string GetCurrentPlayerName()
        {
            string playerName;
            switch (m_CurrentTurn)
            {
                case Checkers.eCheckerGroup.O:
                    playerName = m_SecondPlayer.Name;
                    break;
                default:
                    playerName = m_FirstPlayer.Name;
                    break;
            }

            return playerName;
        }
    }
}

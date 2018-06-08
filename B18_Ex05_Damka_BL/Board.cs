using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05_Damka_BL
{
    public class Board
    {
        private readonly int r_BoardSize;
        private readonly Checkers[,] r_Board;

        internal Board(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            r_Board = new Checkers[i_BoardSize, i_BoardSize];
            ClearBoard();
        }

        internal int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        public Checkers[,] GetBoardMatrix()
        {
            return r_Board;
        }

        internal void BuildBoard(Player i_First, Player i_Second)
        {
            ClearBoard();

            /////building first player;
            foreach (Checkers currentChecker in i_First.CheckersArray)
            {
                if (currentChecker.Group != Checkers.eCheckerGroup.Empty)
                {
                    r_Board[currentChecker.Row, currentChecker.Column] = currentChecker;
                }
            }

            /////building second player
            foreach (Checkers currentChecker in i_Second.CheckersArray)
            {
                if (currentChecker.Group != Checkers.eCheckerGroup.Empty)
                {
                    r_Board[currentChecker.Row, currentChecker.Column] = currentChecker;
                }
            }
        }

        internal void ClearBoard()
        {
            //// Set all cells to Empty
            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    r_Board[i, j] = new Checkers(Checkers.eCheckerGroup.Empty, i, j);
                }
            }
        }
    }
}

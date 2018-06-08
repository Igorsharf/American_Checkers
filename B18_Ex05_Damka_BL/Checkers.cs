using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex05_Damka_BL
{
    public class Checkers
    {
        public enum eCheckerGroup
        {
            O = 0,
            OKing = 1,
            X = 2,
            XKing = 3,
            Empty = 4,
        }

        private eCheckerGroup m_Group;
        private int m_RowLocation;
        private int m_ColumnLocation;

        internal Checkers(eCheckerGroup i_Group, int i_RowLocation, int i_ColumnLocation)
        {
            this.m_Group = i_Group;
            this.m_RowLocation = i_RowLocation;
            this.m_ColumnLocation = i_ColumnLocation;
        }

        public eCheckerGroup Group
        {
            get
            {
                return m_Group;
            }

            set
            {
                m_Group = value;
            }
        }

        internal int Row
        {
            get
            {
                return m_RowLocation;
            }

            set
            {
                m_RowLocation = value;
            }
        }

        internal int Column
        {
            get
            {
                return m_ColumnLocation;
            }

            set
            {
                m_ColumnLocation = value;
            }
        }
    }
}

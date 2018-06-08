using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using B18_Ex05_Damka_BL;

namespace B18_Ex05_Damka_UI
{
    public class BoardButton : Button
    {
        private char r_RowIndex;
        private char r_ColumnIndex;
        private string r_TagName;

        public BoardButton(char i_RowIndex, char i_ColumnIndex)
        {
            this.r_RowIndex = i_RowIndex;
            this.r_ColumnIndex = i_ColumnIndex;
            if (i_RowIndex % 2 == i_ColumnIndex % 2)
            {
                this.Enabled = false;
                this.BackColor = Color.Gray;
            }
        }

        internal char RowIndex
        {
            get { return this.r_RowIndex; }
        }

        internal char ColumnIndex
        {
            get { return this.r_ColumnIndex; }
        }

        internal string TagName
        {
            get { return this.r_TagName; }
            set { this.r_TagName = value; }
        }
    }
}

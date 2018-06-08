using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace B18_Ex05_Damka_UI
{
    public class Program
    {
        public static void Main()
        {
            GameSettingForm gameSettingForm = new GameSettingForm();
            gameSettingForm.ShowDialog();
            if (gameSettingForm.DialogResult == DialogResult.OK)
            {
                GameForm gameForm = new GameForm(gameSettingForm.FirstPlayerName, gameSettingForm.SecondPlayerName, gameSettingForm.BoardSize, gameSettingForm.IsComputerMode);
                gameForm.ShowDialog();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FlashTicTacToe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string flash_file = System.IO.Path.GetFullPath("tictactoe.swf");

            if (System.IO.File.Exists(flash_file) == false)
            {
                MessageBox.Show("Cannot locate data file '" + flash_file + "'.\nExiting...", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                Application.Run(new MainForm(flash_file));
            }
        }
    }
}

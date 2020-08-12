using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashTicTacToe
{
    public partial class MainForm : Form
    {
        public MainForm(string flash)
        {
            InitializeComponent();

            this.axShockwaveFlash1.Movie = flash;
            this.axShockwaveFlash1.Play();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}

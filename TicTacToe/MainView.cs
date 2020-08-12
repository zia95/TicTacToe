using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TicTacToe
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }


        private System.Media.SoundPlayer sp_marked;
        private System.Media.SoundPlayer sp_roundbegin;
        private System.Media.SoundPlayer sp_roundend;
        
        private string sound_marked = Path.GetFullPath("marked.wav");
        private string sound_roundbegin = Path.GetFullPath("roundbegin.wav");
        private string sound_roundend = Path.GetFullPath("roundend.wav");

        private void SetupSound()
        {
            if(File.Exists(this.sound_marked))
            {
                this.sp_marked = new System.Media.SoundPlayer(this.sound_marked);
            }
            else
                MessageBox.Show("Cannot find '"+ sound_marked + "' -- sound file.", "Sound file not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (File.Exists(this.sound_roundbegin))
            {
                this.sp_roundbegin = new System.Media.SoundPlayer(this.sound_roundbegin);
            }
            else
                MessageBox.Show("Cannot find '" + sound_roundbegin + "' -- sound file.", "Sound file not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (File.Exists(this.sound_roundend))
            {
                this.sp_roundend = new System.Media.SoundPlayer(this.sound_roundend);
            }
            else
                MessageBox.Show("Cannot find '" + sound_roundend + "' -- sound file.", "Sound file not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private enum GSound { Marked, RoundBegin, RoundEnd }
        private void PlaySound(GSound s)
        {
            switch (s)
            {
                case GSound.Marked:
                    sp_marked?.Play();
                    break;
                case GSound.RoundBegin:
                    sp_roundbegin?.Play();
                    break;
                case GSound.RoundEnd:
                    sp_roundend?.Play();
                    break;
            }
        }





        public byte ScoreP1 { get { return this._scoreP1; } }
        private byte _scoreP1 = 0;

        public byte ScoreP2 { get { return this._scoreP2; } }
        private byte _scoreP2 = 0;

        public byte RoundsPlayed { get { return this._rounds_played; } }
        private byte _rounds_played = 0;
        
        public string PlayerOneMarker { get; set; } = "X";
        public string PlayerTwoMarker { get; set; } = "0";



        private string CurrentMarker { get { return this.TurnP1OrP2 ? this.PlayerOneMarker : this.PlayerTwoMarker; } }

        private Button[] Tiles = new Button[9];
        private bool TurnP1OrP2 = true;
        public void Reset()
        {
            this.PlaySound(GSound.RoundBegin);
            foreach (Button b in this.Tiles)
            {
                b.Text = string.Empty;
            }

            this._scoreP1 = 0;
            this._scoreP2 = 0;
            this._rounds_played = 1;
        }
        private Button GetTile(string tile_number)
        {
            foreach(Button b in this.Tiles)
            {
                string t = b.Tag as string;
                if (t == tile_number)
                    return b;
            }
            return null;
        }
        private bool CheckTileNum(string tile_number, string NOT_USED = null)
        {
            var t = this.GetTile(tile_number);
            if (t != null)
                return (t.Text == CurrentMarker);
            return false;
        }
        private void EndRound()
        {
            var dres = MessageBox.Show("Stalemate!!\nP1 Score: " + this.ScoreP1 + "\nP2 Score: " + this.ScoreP2 + "\nYes=Start next round\nNo=Reset\nCancel=Quit game",
                    "Round End", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (dres == DialogResult.Yes)
            {
                this.PlaySound(GSound.RoundBegin);
                this._rounds_played++;

                foreach (Button b in this.Tiles)
                {
                    b.Text = string.Empty;
                }
            }
            else if (dres == DialogResult.No)
            {
                this.Reset();
            }
            else if (dres == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }
        private bool EndRound(Button match_tile)
        {
            
            if(match_tile != null)
            {
                this.PlaySound(GSound.RoundEnd);

                if (match_tile.Text == this.PlayerOneMarker)
                {
                    this._scoreP1++;
                }
                else
                {
                    this._scoreP2++;
                }

                var dres = MessageBox.Show(
                    "Player " + (match_tile.Text == this.PlayerOneMarker ? "1" : "2") + " wins!!\nP1 Score: " + 
                    this.ScoreP1 + "\nP2 Score: " + this.ScoreP2 + "\nYes=Start next round\nNo=Reset\nCancel=Quit game",
                    "Round End", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if(dres == DialogResult.Yes)
                {
                    this.PlaySound(GSound.RoundBegin);
                    this._rounds_played++;

                    foreach (Button b in this.Tiles)
                    {
                        b.Text = string.Empty;
                    }
                }
                else if (dres == DialogResult.No)
                {
                    this.Reset();
                }
                else if (dres == DialogResult.Cancel)
                {
                    Application.Exit();
                }

                return true;
            }
            return false;
        }
        private void TileClicked(Button tile)
        {
            if(tile.Text == string.Empty)
            {
                this.PlaySound(GSound.Marked);
                this.TurnP1OrP2 = !this.TurnP1OrP2;
                tile.Text = CurrentMarker;

                string num = tile.Tag as string;
                if(num == "11")
                {
                    if ((CheckTileNum("12", tile.Text) && CheckTileNum("13", tile.Text)) ||
                        (CheckTileNum("21", tile.Text) && CheckTileNum("31", tile.Text)) ||
                        (CheckTileNum("22", tile.Text) && CheckTileNum("33", tile.Text)))
                        this.EndRound(tile);
                }
                else if (num == "12")
                {
                    if ((CheckTileNum("11", tile.Text) && CheckTileNum("13", tile.Text)) ||
                        (CheckTileNum("22", tile.Text) && CheckTileNum("32", tile.Text)))
                        this.EndRound(tile);
                }
                else if (num == "13")
                {
                    if ((CheckTileNum("11", tile.Text) && CheckTileNum("12", tile.Text)) ||
                        (CheckTileNum("23", tile.Text) && CheckTileNum("33", tile.Text)) ||
                        (CheckTileNum("22", tile.Text) && CheckTileNum("31", tile.Text)))
                        this.EndRound(tile);
                }
                // second row
                else if (num == "21")
                {
                    if ((CheckTileNum("11", tile.Text) && CheckTileNum("31", tile.Text)) ||
                        (CheckTileNum("22", tile.Text) && CheckTileNum("23", tile.Text)))
                        this.EndRound(tile);
                }
                else if (num == "22")
                {
                    if ((CheckTileNum("11", tile.Text) && CheckTileNum("33", tile.Text)) ||
                        (CheckTileNum("12", tile.Text) && CheckTileNum("32", tile.Text)) ||
                        (CheckTileNum("13", tile.Text) && CheckTileNum("31", tile.Text)) ||
                        (CheckTileNum("21", tile.Text) && CheckTileNum("23", tile.Text)))
                        this.EndRound(tile);
                }
                else if (num == "23")
                {
                    if ((CheckTileNum("21", tile.Text) && CheckTileNum("22", tile.Text)) ||
                        (CheckTileNum("13", tile.Text) && CheckTileNum("33", tile.Text)))
                        this.EndRound(tile);
                }
                // third row
                else if (num == "31")
                {
                    if ((CheckTileNum("11", tile.Text) && CheckTileNum("21", tile.Text)) ||
                        (CheckTileNum("32", tile.Text) && CheckTileNum("33", tile.Text)) ||
                        (CheckTileNum("22", tile.Text) && CheckTileNum("13", tile.Text)))
                        this.EndRound(tile);
                }
                else if (num == "32")
                {
                    if ((CheckTileNum("31", tile.Text) && CheckTileNum("33", tile.Text)) ||
                        (CheckTileNum("22", tile.Text) && CheckTileNum("12", tile.Text)))
                        this.EndRound(tile);
                }
                else if (num == "33")
                {
                    if ((CheckTileNum("31", tile.Text) && CheckTileNum("32", tile.Text)) ||
                        (CheckTileNum("23", tile.Text) && CheckTileNum("13", tile.Text)) ||
                        (CheckTileNum("22", tile.Text) && CheckTileNum("11", tile.Text)))
                        this.EndRound(tile);
                }

                for(int i = 0; i < this.Tiles.Length; i++)
                {
                    if (this.Tiles[i].Enabled)
                        break;
                    else if(this.Tiles[i].Enabled == false && i == (this.Tiles.Length-1))
                    {
                        this.EndRound();
                    }
                }
            }
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.SetupSound();
            this.BackColor = Color.White;

            this.Tiles[0] = this.btnTile11;
            this.Tiles[1] = this.btnTile12;
            this.Tiles[2] = this.btnTile13;

            this.Tiles[3] = this.btnTile21;
            this.Tiles[4] = this.btnTile22;
            this.Tiles[5] = this.btnTile23;

            this.Tiles[6] = this.btnTile31;
            this.Tiles[7] = this.btnTile32;
            this.Tiles[8] = this.btnTile33;

            this.Tiles[0].Tag = "11";
            this.Tiles[1].Tag = "12";
            this.Tiles[2].Tag = "13";

            this.Tiles[3].Tag = "21";
            this.Tiles[4].Tag = "22";
            this.Tiles[5].Tag = "23";

            this.Tiles[6].Tag = "31";
            this.Tiles[7].Tag = "32";
            this.Tiles[8].Tag = "33";


            foreach (Button b in this.Tiles)
                b.Click += delegate { this.TileClicked(b); };

            this.Reset();
        }
    }
}

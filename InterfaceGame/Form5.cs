using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cards;

namespace InterfaceGame
{
    public partial class Form5 : Form
    {
        private Form2 f2;
        private Player player1;
        public Form5(Form2 f2, Player player1)
        {
            InitializeComponent();
            this.f2 = f2;
            this.player1 = player1;
            UpdateGraveyard();
        }
        private void UpdateGraveyard()
        {
            int idphoto = 1;
            bool pos = false;
            for (int i = 1; i <= 15; i++)
            {
                if (i - 1 < player1.Graveyard.Count)
                {
                    this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(player1.Graveyard[i - 1].FrontImage);
                    idphoto = i;
                    pos = true;
                }
                else
                {
                    this.Controls["pictureBox" + i].BackgroundImage = null;
                }
            }
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Enabled = true;
            this.Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
        }
    }
}

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
    public partial class Form4 : Form
    {
        private Form2 f2;
        private Player p;
        private int picture;
        private int index;
        private int picture2;
        private int index2;
        public Form4(Form2 f2, Player p, int picture, int index,int picture2, int index2)
        {
            InitializeComponent();
            this.f2 = f2;
            this.p = p;
            this.picture = picture;
            this.index = index;
            this.picture2 = picture2;
            this.index2 = index2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Enabled = true;
            if(p.Hand[index] is MonsterCard)
            {
                if (((MonsterCard)p.Hand[index]).Nivel > 5)
                {
                    DialogResult dr = MessageBox.Show("Seleccione la carta a sacrificar", "Opciones de Sacrificio", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if(dr == DialogResult.Cancel)
                    {
                        f2.SetOption(0);
                    }
                    else
                        f2.SetOption(1);
                }

                else
                    f2.invocar(p, picture, index, -1, -1);
            }
            else
            {
                f2.invocar(p, picture, index, -1,-1);
            }


            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Enabled = true;
            f2.SetOption(2);
            MessageBox.Show("Seleccione la carta a afectar", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Enabled = true;
            f2.SetOption(3);
            MessageBox.Show("Seleccione la carta a atacar", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
            if (p.MonsterField.Count > index)
            {
                if (!((MonsterCard)p.MonsterField[index]).CanATK)
                    button3.Enabled = false;
                if (!((MonsterCard)p.MonsterField[index]).CanEffect)
                    button2.Enabled = false;
            }
            if (picture > 0 && picture <= 10)
                button1.Enabled = false;
            if (picture > 10 && picture <= 20)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
            if(picture > 5 && picture <= 10)
            {
                button1.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
            f2.SetOption(0);
            this.Hide();
        }
    }
}

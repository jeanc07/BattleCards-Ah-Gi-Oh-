using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Cards;

namespace InterfaceGame
{
    public partial class Form3 : Form
    {
        private Form1 f1;
        public Form3(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = openFileDialog1.FileName;
                pictureBox1.Load(textBox6.Text);
            }

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool name = false;
            bool element = false;
            bool tipe = false;
            bool atk = false;
            bool lvl = false;
            bool def = false;
            bool effect = false;
            bool image = false;
            if(comboBox1.Text == "Monster")
            {
                name = CheckCardName();
                element = CheckCardElement();
                tipe = CheckCardTipe();
                atk = CheckCardAtk();
                lvl = CheckCardLevel();
                def = CheckCardDef();
                effect = CheckEffectCode();
                image = CheckIMage();
            }
            else
            {
                name = CheckCardName();
                element = CheckCardElement();
                tipe = CheckCardTipe();
                effect = CheckEffectCode();
                image = CheckIMage();
            }
            
            if(name)
                MessageBox.Show("Nombre de la carta no valido. Pruebe de nuevo.", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(element)
                MessageBox.Show("Elemento de la carta no valido. Pruebe de nuevo.", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(tipe)
                MessageBox.Show("Tipo de la carta no valido. Pruebe de nuevo.", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (atk)
                MessageBox.Show("Ataque no valido, debe tener un valor que este entre 0 y 3000. Pruebe de nuevo.", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (lvl)
                MessageBox.Show("Nivel no valido, debe tener un valor que este entre 0 y 10. Pruebe de nuevo.", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (def)
                MessageBox.Show("Defensa no valida, debe tener un valor que este entre 0 y 3000. Pruebe de nuevo.", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (effect)
                MessageBox.Show("Lineas de efecto no validas. Pruebe de nuevo.", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (image)
                MessageBox.Show("Imagen no valida o no seleccionada. Pruebe de nuevo.", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(!name && !element && !tipe && !atk && !lvl && !def && !effect && !image)
            {
                CreateCustomCard();
                this.Close();
            }
        }
        private bool CheckCardName()
        {
            bool result = false;
            string temp = textBox1.Text;
            temp.Trim();
            if (temp == "" || temp == " " || temp == null)
                return true;

            return result;
        }
        private bool CheckCardElement()
        {
            bool result = false;
            string temp = textBox2.Text;
            temp.Trim();
            if (temp == "" || temp == " " || temp == null)
                return true;

            return result;
        }
        private bool CheckCardTipe()
        {
            bool result = false;
            string temp = textBox7.Text;
            temp.Trim();
            if (temp == "" || temp == " " || temp == null)
                return true;

            return result;
        }
        private bool CheckCardLevel() //True si esta mal , False si esta bien
        {
            bool result = false;
            string temp = textBox3.Text.ToString();
            temp.Trim();
            if (temp == "" || temp == null)
                return true;
            int tempint = Convert.ToInt32(temp);
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] < 48 && temp[i] > 57)
                {
                    result = true;
                }
            }
            if (tempint < 0 && tempint > 10)
                result = true;
            return result;
        }
        private bool CheckCardAtk()
        {
            bool result = false;
            string temp = textBox4.Text.ToString();
            temp.Trim();
            if (temp == "" || temp == null)
                return true;
            int tempint = Convert.ToInt32(temp);
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] < 48 && temp[i] > 57)
                {
                    result = true;
                }
            }
            if (tempint < 0 && tempint > 3000)
                result = true;
            return result;
        }
        private bool CheckCardDef()
        {
            bool result = false;
            string temp = textBox5.Text.ToString();
            temp.Trim();
            if (temp == "" || temp == null)
                return true;
            int tempint = Convert.ToInt32(temp);
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] < 48 && temp[i] > 57)
                {
                    result = true;
                }
            }
            if (tempint < 0 && tempint > 3000)
                result = true;
            return result;
        }
        private bool CheckIMage()
        {
            bool result = false;
            string temp = textBox6.Text.ToString();
            if (temp == "" || temp == null)
                result = true;

            return result;
        }
        private bool CheckEffectCode()
        {
            bool result = false;
            if (richTextBox1.Text == "" || richTextBox1.Text == null)
                return false;
            Compiler effect = new Compiler();
            List<string> lines = new List<string>();
            string temp = richTextBox1.Text.ToLower();
            lines.Add(temp);
            bool syntax = effect.syntaxisAnalysis(lines);
            bool semantyc = effect.SemanticAnalysis(lines);

            if (syntax || semantyc)
                result = true;


            return result;
        }
        public void CreateCustomCard()
        {
            int id = 400;
            string sourcepath = @"CustomCards";
            TextWriter cardtext = new StreamWriter($@"CustomCards\{textBox1.Text}.txt");
            var arrayDirectory = Directory.EnumerateFiles(sourcepath);
            id += arrayDirectory.Count();
            if (comboBox1.Text == "Monster")
            {
                cardtext.WriteLine($"{id}");
                cardtext.WriteLine(comboBox1.Text);
                cardtext.WriteLine(textBox1.Text);
                cardtext.WriteLine(textBox2.Text);
                if(richTextBox1.Text != "")
                    cardtext.WriteLine(richTextBox1.Text.ToLower());
                cardtext.WriteLine(textBox7.Text);
                cardtext.WriteLine(textBox3.Text);
                cardtext.WriteLine(textBox4.Text);
                cardtext.WriteLine(textBox5.Text);
                cardtext.WriteLine("0"); //Posicion
                cardtext.WriteLine(textBox6.Text);
                cardtext.WriteLine(@"photos\IMG_5639.png");
                //cardtext.WriteLine(@"photos\IMG_5639.png");
            }
            else
            {
                cardtext.WriteLine($"{id}");
                cardtext.WriteLine(comboBox1.Text);
                cardtext.WriteLine(textBox1.Text);
                cardtext.WriteLine(textBox2.Text);
                cardtext.WriteLine(textBox7.Text);
                if(richTextBox1.Text != "")
                    cardtext.WriteLine(richTextBox1.Text.ToLower());
                cardtext.WriteLine("0"); //Posicion
                string split = SplitText(textBox6.Text);
                cardtext.WriteLine(textBox6.Text);
                cardtext.WriteLine(@"photos\IMG_5639.png");
            }

            cardtext.Close();
        }

        private string SplitText(string text)
        {
            string result = "";


            return result;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
        }
    }
}

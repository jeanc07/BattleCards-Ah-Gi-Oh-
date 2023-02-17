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
    public partial class Form2 : Form
    {
        private Form1 f1;
        private Player player1;
        private Player IABot;
        private List<Card> allCards;
        private Compiler compiler;
        private int count;
        private bool draw;
        private int playerOption;
        private List<int> sacrifice;
        private int idGlobal;
        private int idGlobal2;
        private int picture;
        private int index;
        private int picture2;
        private int index2;
        private bool clicked;
        private bool finished;
        public void SetOption(int po) { playerOption = po; }

        public Form2(Form1 f1)
        {
            InitializeComponent();
            this.picture = 0;
            this.index = 0;
            this.picture2 = 0;
            this.index2 = 0;
            this.idGlobal = 0;
            this.idGlobal2 = 0;
            this.count = 0;
            this.f1 = f1;
            this.sacrifice = new List<int>();
            this.playerOption = 0;
            this.draw = false;
            this.player1 = new Player();
            this.IABot = new Player();
            this.allCards = new List<Card>();
            this.compiler = new Compiler();
            this.clicked = false;
            this.finished = false;
            LoadCards();
            shuffleCardsByPlayer();
            Operations.DrawNormal(player1, 5);
            Operations.DrawNormal(IABot, 5);
            LoadImageInitialCard();
            InitialPlay();
        }

        private void LoadCards()
        {
            string sourcepath = @"Content";
            string sourcepath2 = @"CustomCards";
            List<string> temp = new List<string>();
            Card card = new Card();
            var arrayDirectory = Directory.EnumerateFiles(sourcepath);
            var arrayDirectory2 = Directory.EnumerateFiles(sourcepath2);
            for (int j = 0; j < arrayDirectory2.Count(); j++)
            {

                temp = File.ReadAllLines(arrayDirectory2.ElementAt(j)).ToList();

                if (temp.ElementAt(1) == "Monster")
                {
                    card = new MonsterCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(5), temp.ElementAt(4), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(9))),
                                        temp.ElementAt(10), temp.ElementAt(11), true, Convert.ToInt32(temp.ElementAt(6)), Convert.ToInt32(temp.ElementAt(7)), Convert.ToInt32(temp.ElementAt(8)), true);
                }
                else if (temp.ElementAt(1) == "Magic")
                {
                    card = new MagicCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(4), temp.ElementAt(5), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(6))), temp.ElementAt(7), temp.ElementAt(8), true);
                }
                else if (temp.ElementAt(1) == "Trap")
                {
                    card = new TrapCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(4), temp.ElementAt(5), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(6))), temp.ElementAt(7), temp.ElementAt(8), true);
                }

                allCards.Add(card);
            }
            for (int j = 0; j < arrayDirectory.Count(); j++)
            {

                temp = File.ReadAllLines(arrayDirectory.ElementAt(j)).ToList();

                if (temp.ElementAt(1) == "Monster")
                {
                    card = new MonsterCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(5), temp.ElementAt(4), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(9))),
                                        temp.ElementAt(10), temp.ElementAt(11),true, Convert.ToInt32(temp.ElementAt(6)), Convert.ToInt32(temp.ElementAt(7)), Convert.ToInt32(temp.ElementAt(8)), true);
                }
                else if (temp.ElementAt(1) == "Magic")
                {
                    card = new MagicCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(4), temp.ElementAt(5), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(6))), temp.ElementAt(7), temp.ElementAt(8),true);
                }
                else if (temp.ElementAt(1) == "Trap")
                {
                    card = new TrapCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(4), temp.ElementAt(5), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(6))), temp.ElementAt(7), temp.ElementAt(8),true);
                }

                allCards.Add(card);
            }
        }

        private void shuffleCardsByPlayer()
        {
            Card[] tempCard = new Card[allCards.Count];
            allCards.CopyTo(0, tempCard, 0, allCards.Count);
            bool firstPlayer = false;

            Random d = new Random();
            int r = d.Next(0, 256);
            if (r % 2 == 0) firstPlayer = true;        // si es true player1 uno recibe primera carta

            for (int i = 0; i < allCards.Count; i++)
            {
                Random rnd = new Random();
                r = rnd.Next(0, tempCard.Length);

                if (firstPlayer && i % 2 == 0) player1.Deck.Add(tempCard.ElementAt(r));
                else if (firstPlayer && i % 2 != 0) IABot.Deck.Add(tempCard.ElementAt(r));
                else if (!firstPlayer && i % 2 == 0) IABot.Deck.Add(tempCard.ElementAt(r));
                else if (!firstPlayer && i % 2 != 0) player1.Deck.Add(tempCard.ElementAt(r));

                List<Card> t = tempCard.ToList();
                t.RemoveAt(r);
                tempCard = t.ToArray();

                if (player1.Deck.Count == 15 && IABot.Deck.Count == 15)
                    return;
            }
        }

        private void CheckDraw()
        {
            if (!draw)
                MessageBox.Show("Haga click en el Deck para robar una carta e iniciar su turno","Ah-Gi-Oh!",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void LoadImageInitialCard()
        {
            int h = 5;
            for (int i = 11; i <= 20; i++)
            {
                if (this.Controls["pictureBox" + i].BackgroundImage == null && h > 0)
                {
                    this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(player1.Hand[5 - h].FrontImage);
                    h--;
                }
            }

            h = 5;
            for (int i = 31; i <= 40; i++)
            {
                if (this.Controls["pictureBox" + i].BackgroundImage == null && h > 0)
                {
                    this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(IABot.Hand[5 - h].FrontImage);
                    h--;
                }
            }
        }

        private void UpdateHandCardPlayer1(int picture, int index, bool d)
        {
            int h = player1.Hand.Count;
            if (d)
            {
                for (int i = picture; i <= 20; i++)
                {
                    if (this.Controls["pictureBox" + i].BackgroundImage == null && index < h)
                    {
                        this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(player1.Hand[index].FrontImage);
                        index++;
                    }
                }
            }
            else
            {
                for (int i = picture; i <= 20; i++)
                {
                    if (index < h)
                    {
                        this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(player1.Hand[index].FrontImage);
                        index++;
                    }
                    else
                    {
                        this.Controls["pictureBox" + i].BackgroundImage = null;
                    }
                }

            }
        }

        private void UpdateMosterFieldCardPlayer1()
        {
            int idphoto = 1;
            bool pos = false;
            for (int i = 1; i <= 5; i++)
            {
                if (i - 1 < player1.MonsterField.Count)
                {
                    this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(player1.MonsterField[i - 1].FrontImage);
                    idphoto = i;
                    pos = true;
                }
                else
                {
                    this.Controls["pictureBox" + i].BackgroundImage = null;
                }
            }

        }

        private void UpdateMosterFieldCardIABot()
        {
            int indexphoto = 1;
            for (int i = 21; i <= 25; i++)
            {
                if (i - 21 < IABot.MonsterField.Count)
                {
                    this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(IABot.MonsterField[i - 21].FrontImage);
                    indexphoto = i;
                }
                else
                {
                    this.Controls["pictureBox" + i].BackgroundImage = null;
                }
            }
        }

        private void UpdateMagicFieldCardPlayer1()
        {
            int indexphoto = 1;
            for (int i = 6; i <= 10; i++)
            {
                if (i - 6 < player1.MagicField.Count)
                {
                    this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(player1.MagicField[i - 6].FrontImage);
                    indexphoto = i;
                }
                else
                {
                    this.Controls["pictureBox" + i].BackgroundImage = null;
                }
            }
        }

        private void UpdateMagicFieldCardIABot()
        {
            for (int i = 26; i <= 30; i++)
            {
                if (i - 26 < IABot.MagicField.Count)
                {
                    this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(IABot.MagicField[i - 26].FrontImage);
                }
                else
                {
                    this.Controls["pictureBox" + i].BackgroundImage = null;
                }
            }
        }

        private void UpdateHandCardIabot()
        {
            int h = IABot.Hand.Count;
            int indexphoto = 0;
            for (int i = 31; i <= 40; i++)
            {
                if (i-31 < h && IABot.Hand[i-31] != null)
                {
                    this.Controls["pictureBox" + i].BackgroundImage = Image.FromFile(IABot.Hand[i - 31].FrontImage);
                    indexphoto = i;
                }
            }
            for(int i = indexphoto+1; i <= 40; i++)
            {
                this.Controls["pictureBox" + i].BackgroundImage = null;
            }
        }

        void InitialPlay()
        {
            label11.Text = player1.Vida.ToString();
            label8.Text = IABot.Vida.ToString();
            Random d = new Random();
            int r = d.Next(0, 256);
            if (r % 2 == 0)
            {
                Operations.ChangeTurnToPlay(player1);
                label10.Text = "En Turno";
                label10.ForeColor = Color.Green;

                Operations.ChangeTurn(IABot);
                label9.Text = "En Espera";
                label9.ForeColor = Color.Red;
                CheckDraw();
            }
            else
            {
                Operations.ChangeTurn(player1);
                label10.Text = "En Espera";
                label10.ForeColor = Color.Red;

                Operations.ChangeTurnToPlay(IABot);
                label9.Text = "En Turno";
                label9.ForeColor = Color.Green;
                IAPlay();
            }

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Show();
        }

        private void pictureBox42_Click(object sender, EventArgs e)
        {
            if (player1.CanInvoke && draw == false)
            {
                draw = true;
                Operations.DrawNormal(player1);
                UpdateHandCardPlayer1(11, player1.Hand.Count - 1, true);
            }
        }

        private void pictureBox44_Click(object sender, EventArgs e)
        {
            if (IABot.CanInvoke)
                draw = true;
        }


        private int SacrificeCount(Card card)
        {
            int result = 0;
            if (((MonsterCard)card).Nivel > 5)
                result = 1;
                return result;
        }

        public void invocar(Player p, int picture, int index,int picture2,int index2)
        {
            idGlobal = p.Hand[index].Id;
            if(p.MonsterField.Count > 0 && index2 != -1)
                idGlobal2 = p.MonsterField[index2].Id;
            if (p.existCardInTheHand(idGlobal))
            {
                if (p.Hand[index] is MonsterCard)
                {
                    int sacrificecount = SacrificeCount(p.Hand[index]);
                    sacrifice = new List<int>();
                    if(sacrificecount != 0 && picture2 > 0 && index2 >= 0 && p.existCardInTheField(idGlobal2))
                    {
                        timer3.Enabled = false;
                        DialogResult dr = MessageBox.Show("Posicion de defensa ?", "Opciones de posicion", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            ((MonsterCard)p.Hand[index]).Position = true;
                        }
                        p.MonsterField.Add(p.Hand[index]);
                        p.Hand.Remove(p.Hand[index]);
                        p.Graveyard.Add(p.MonsterField[index2]);
                        p.MonsterField.Remove(p.MonsterField[index2]);
                        p.CanInvoke = false;
                        UpdateHandCardPlayer1(picture, index, false);
                        UpdateMosterFieldCardPlayer1();
                        sacrificecount--;
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("Posicion de defensa ?", "Opciones de posicion", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                        bool position = false;
                        if (dr == DialogResult.Yes)
                        {
                            position = true;
                        }
                        bool posible = Operations.InvocarNormal(player1, player1.cardPositionInTheHand(idGlobal), sacrifice, position);

                        if (posible == false)
                            MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            UpdateHandCardPlayer1(picture, index, false);
                            UpdateMosterFieldCardPlayer1();
                        }
                    }
                }
                else
                {
                    bool posible = Operations.InvocarNormal(player1, player1.cardPositionInTheHand(idGlobal), sacrifice);
                    if (posible == false)
                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        UpdateHandCardPlayer1(picture, index, false);
                        UpdateMagicFieldCardPlayer1();
                    }
                }
            }
            else
                MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Updatelabels();
            playerOption = 0;
        }

        public void Atacar(int picture, int index, int picture2, int index2)
        {
            idGlobal = player1.MonsterField.ElementAt(index).Id;
            timer1.Enabled = true;
            if (IABot.MonsterField.Count > 0)
                idGlobal2 = IABot.MonsterField.ElementAt(index2).Id;
            else
                idGlobal2 = -1;

            if (player1.existCardInTheField(idGlobal))   
            {
                Operations.Atacar(player1, IABot, player1.cardPositionInTheMonsterField(idGlobal), IABot.cardPositionInTheMonsterField(idGlobal2));
                UpdateMosterFieldCardPlayer1();
                UpdateMagicFieldCardPlayer1();
                UpdateMosterFieldCardIABot();
                UpdateMagicFieldCardIABot();
            }
            else
                MessageBox.Show("No se puede realizar la accion","Ah-Gi-Oh!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            label11.Text = player1.Vida.ToString();
            label8.Text = IABot.Vida.ToString();
            CheckLife();
            Updatelabels();
            EnoughinTurn();
        }

        public void ActivarEfecto(int picture, int index , int picture2, int index2)
        {
            timer1.Enabled = true;
            if (picture > 0 && picture <= 5)
                idGlobal = player1.MonsterField[index].Id;
            else
                idGlobal = player1.MagicField[index].Id;
            if (picture2 > 20 && picture2 <= 25)
                idGlobal2 = IABot.MonsterField[index2].Id;
            else if (picture2 > 25 && picture2 <= 30)
                idGlobal2 = IABot.MagicField[index2].Id;
            else if (picture2 > 0 && picture2 <= 5)
                idGlobal2 = player1.MonsterField[index2].Id;
            else if (picture2 > 5 && picture2 <= 10)
                idGlobal2 = player1.MagicField[index2].Id;
            else if (picture2 > 10 && picture2 <= 20)
                idGlobal2 = player1.Hand[index2].Id;
               
            bool posible = false;
            if (player1.existCardInTheField(idGlobal))
            {
                Card card = allCards.Find(x => x.Id == idGlobal);

                if (card is CustomCard)
                    compiler.ExecuteEffects((CustomCard)card, player1, IABot);
                else
                {
                    if (card.CanEffect)
                    {
                        card.CanEffect = false;
                        switch (card.Efecto)
                        {
                            case "inv;":
                                {
                                    if(picture2 <= 10 && picture2 > 20)
                                    {
                                        MessageBox.Show("No se puede realizar la accion. Carta no valida", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        playerOption = 0;
                                        card.CanEffect = true;
                                        break;
                                    }
                                    posible = Operations.InvocarEspecial(player1, idGlobal2, card);
                                    if (posible == false)
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            case "red;":
                                {
                                    Operations.ReducirVida(player1, IABot, card);
                                    break;
                                }
                            case "rea;":
                                {
                                    if (IABot.existCardInTheField(idGlobal2))
                                    {
                                        if (picture2 <= 20 && picture2 > 25)
                                        {
                                            MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            playerOption = 0;
                                            card.CanEffect = true;
                                            break;
                                        }
                                        posible = Operations.ReducirAtaque(player1, card, IABot, idGlobal2);
                                        if (posible == false)
                                            MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            case "atq;": 
                                {
                                    if((picture2 <= 20 && picture2 > 25) || picture2 != -1)
                                    {
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        playerOption = 0;
                                        card.CanEffect = true;
                                        break;
                                    }
                                    if (IABot.existCardInTheField(idGlobal2))
                                    {
                                        Operations.AtacarEspecial(player1, IABot, player1.cardPositionInTheMonsterField(idGlobal), IABot.cardPositionInTheMonsterField(idGlobal2));
                                    }
                                    else if(IABot.MonsterField.Count == 0)
                                    {
                                        Operations.AtacarEspecial(player1, IABot, player1.cardPositionInTheMonsterField(idGlobal), -1);
                                    }
                                    else
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            case "cam;":
                                {
                                    if(picture2 <= 20 && picture2 > 25)
                                    {
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        playerOption = 0;
                                        card.CanEffect = true;
                                        break;
                                    }
                                    if (IABot.existCardInTheField(idGlobal2))
                                    {
                                        posible = Operations.Cambiar(player1, card, IABot, idGlobal2);
                                        if (posible == false)
                                            MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            case "roc;":
                                {
                                    Operations.Draw(player1, card, 1);
                                    break;
                                }
                            case "rev;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a seleccionar");
                                    if (player1.existCardInTheGraveyard(idGlobal2))
                                    {
                                        posible = Operations.Revivir(player1, card, idGlobal2);
                                        if (posible == false)
                                            System.Console.WriteLine("No puede realizar esa accion");
                                    }
                                    else
                                        System.Console.WriteLine("No puede realizar esa accion");
                                    break;
                                }
                            case "con;":
                                {
                                    if(picture2 <= 20 && picture2 > 25)
                                    {
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        playerOption = 0;
                                        card.CanEffect = true;
                                        break;
                                    }
                                    if (IABot.existCardInTheField(idGlobal2))
                                    {
                                        posible = Operations.Controlar(player1, card, IABot, idGlobal2);
                                        if (posible == false)
                                            MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            case "inc;":
                                {
                                    if(picture2 <= 0 && picture2 > 5)
                                    {
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        playerOption = 0;
                                        card.CanEffect = true;
                                        break;
                                    }
                                    Operations.IncrementarAtaque(player1, card, idGlobal2);
                                    break;
                                }
                            case "sub;":
                                {
                                    if(picture2 <= 20 && picture2 > 25)
                                    {
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        playerOption = 0;
                                        card.CanEffect = true;
                                        break;
                                    }
                                    if (IABot.existCardInTheField(idGlobal2))
                                    {
                                        posible = Operations.Subir(player1, card, IABot, idGlobal2);
                                        if (posible == false)
                                            MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            case "dev;":
                                {

                                    break;
                                }
                            case "rov;":
                                {
                                    Operations.RobarVida(player1, card, IABot);
                                    break;
                                }
                            case "des;":
                                {
                                    if(picture2 <= 20 && picture2 > 30)
                                    {
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        playerOption = 0;
                                        card.CanEffect = true;
                                        break;
                                    }
                                    if (IABot.existCardInTheField(idGlobal2))
                                    {
                                        posible = Operations.Destruir(player1, IABot, idGlobal2, card);
                                        if (posible == false)
                                            MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                        MessageBox.Show("No se puede realizar la accion", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                        }
                    }
                }
            }
            timer1.Enabled = false;
            label11.Text = player1.Vida.ToString();
            label8.Text = IABot.Vida.ToString();
            UpdateMosterFieldCardPlayer1();
            UpdateMagicFieldCardPlayer1();
            UpdateMosterFieldCardIABot();
            UpdateMagicFieldCardIABot();
            UpdateHandCardIabot();
            CheckLife();
            Updatelabels();
            EnoughinTurn();
        }

        private void EnoughinTurn()
        {
            bool invoque = false;
            bool ataqued = false;
            bool effected = false;
            int count = 0, count2 = 0;
            for(int i = 0; i < player1.MonsterField.Count; i++)
            {
                if (((MonsterCard)player1.MonsterField.ElementAt(i)).CanATK == false)
                {
                    count++;
                    if (count == player1.MonsterField.Count)
                        ataqued = true;
                }
            }
            for (int i = 0; i < player1.MonsterField.Count; i++)
            {
                if (((MonsterCard)player1.MonsterField.ElementAt(i)).CanEffect == false)
                {
                    count2++;
                    if (count == player1.MonsterField.Count)
                        effected = true;
                }
            }
            if (player1.CanInvoke == false)
                invoque = true;

            if(invoque && ataqued && effected)
            {
                idGlobal = 0;
                idGlobal2 = 0;
                picture = 0;
                picture2 = 0;
                index = 0;
                index2 = 0;
                sacrifice.Clear();
                playerOption = 0;
                draw = false;

                label11.Text = player1.Vida.ToString();
                label8.Text = IABot.Vida.ToString();
                CheckLife();
                Updatelabels();

                Operations.ChangeTurn(player1);
                label10.Text = "En Espera";
                label10.ForeColor = Color.Red;

                Operations.ChangeTurnToPlay(IABot);
                label9.Text = "En Turno";
                label9.ForeColor = Color.Green;

                IAPlay();
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(12, 1);
            if (pictureBox12.BackgroundImage != null)
            {
                if(draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke) || player1.Hand[1] is MagicCard || player1.Hand[1] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 12, 1, 0, 0);
                            picture = 12;
                            index = 1;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }else if(playerOption == 2)
                    {
                        picture2 = 12;
                        index2 = 1;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }  
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(13, 2);
            if (pictureBox13.BackgroundImage != null)
            {
                if(draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke && draw == true) || player1.Hand[2] is MagicCard || player1.Hand[2] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 13, 2, 0, 0);
                            picture = 13;
                            index = 2;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }else if(playerOption == 2)
                    {
                        picture2 = 13;
                        index2 = 2;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(14, 3);
            if (pictureBox14.BackgroundImage != null)
            {
                if(draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke) || player1.Hand[3] is MagicCard || player1.Hand[3] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 14, 3, 0, 0);
                            picture = 14;
                            index = 3;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }else if(playerOption == 2)
                    {
                        picture2 = 14;
                        index2 = 3;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(15, 4);
            if (pictureBox15.BackgroundImage != null)
            {
                if(draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke && draw == true) || player1.Hand[4] is MagicCard || player1.Hand[4] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 15, 4, 0, 0);
                            picture = 15;
                            index = 4;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }else if(playerOption == 2)
                    {
                        picture2 = 15;
                        index2 = 4;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void IAPlay()
        {
            playerOption = 0;
            IABot ia = new IABot(count);
            ia.IAStart(IABot, player1);
            count++;
            UpdateMosterFieldCardIABot();
            UpdateMagicFieldCardIABot();
            UpdateHandCardIabot();

            UpdateMosterFieldCardPlayer1();
            UpdateMagicFieldCardPlayer1();
           

            label11.Text = player1.Vida.ToString();
            label8.Text = IABot.Vida.ToString();
            CheckLife();
            Updatelabels();

            Operations.ChangeTurnToPlay(player1);
            label10.Text = "En Turno";
            label10.ForeColor = Color.Green;

            Operations.ChangeTurn(IABot);
            label9.Text = "En Espera";
            label9.ForeColor = Color.Red;
            CheckDraw();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(16, 5);
            if (pictureBox16.BackgroundImage != null)
            {
                if (draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke && draw == true) || player1.Hand[5] is MagicCard || player1.Hand[5] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 16, 5, 0, 0);
                            picture = 16;
                            index = 5;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }
                    else if (playerOption == 2)
                    {
                        picture2 = 16;
                        index2 = 5;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(17, 6);
            if (pictureBox17.BackgroundImage != null)
            {
                if (draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke && draw == true) || player1.Hand[6] is MagicCard || player1.Hand[6] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 17, 6, 0, 0);
                            picture = 17;
                            index = 6;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }
                    else if (playerOption == 2)
                    {
                        picture2 = 17;
                        index2 = 6;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(18, 7);
            if (pictureBox18.BackgroundImage != null)
            {
                if (draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke && draw == true) || player1.Hand[7] is MagicCard || player1.Hand[7] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 18, 7, 0, 0);
                            picture = 18;
                            index = 7;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }
                    else if (playerOption == 2)
                    {
                        picture2 = 18;
                        index2 = 7;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(19, 8);
            if (pictureBox19.BackgroundImage != null)
            {
                if (draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke && draw == true) || player1.Hand[8] is MagicCard || player1.Hand[8] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 19, 8, 0, 0);
                            picture = 19;
                            index = 8;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }
                    else if (playerOption == 2)
                    {
                        picture2 = 19;
                        index2 = 8;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(20, 9);
            if (pictureBox20.BackgroundImage != null)
            {
                if (draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke && draw == true) || player1.Hand[9] is MagicCard || player1.Hand[9] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 20, 9, 0, 0);
                            picture = 20;
                            index = 9;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }
                    else if (playerOption == 2)
                    {
                        picture2 = 20;
                        index2 = 9;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(21, 0);
            if (playerOption == 3 && pictureBox21.BackgroundImage != null)
            {
                picture2 = 21;
                index2 = 0;
                clicked = true;
                timer1.Enabled = true;
            }
            if(playerOption == 2 && pictureBox21.BackgroundImage != null)
            {
                picture2 = 21;
                index2 = 0;
                clicked = true;
                timer2.Enabled = true;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(1, 0);
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            if (draw)
            {
                if (playerOption == 2)
                {
                    picture2 = 1;
                    index2 = 0;
                    clicked = true;
                    timer2.Enabled = true;
                }
                else if (playerOption == 1)
                {
                    picture2 = 1;
                    index2 = 0;
                    clicked = true;
                    timer3.Enabled = true;
                }
                else if (pictureBox1.BackgroundImage != null && playerOption != 2 && playerOption != 1)
                {
                    picture = 1;
                    index = 0;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            idGlobal = 0;
            idGlobal2 = 0;
            picture = 0;
            picture2 = 0;
            index = 0;
            index2 = 0;
            sacrifice.Clear();
            playerOption = 0;
            draw = false;

            Operations.ChangeTurn(player1);
            label10.Text = "En Espera";
            label10.ForeColor = Color.Red;

            Operations.ChangeTurnToPlay(IABot);
            label9.Text = "En Turno";
            label9.ForeColor = Color.Green;

            IAPlay();
        }

        private void Updatelabels()
        {
            if (!finished)
            {
                for (int i = 20; i <= 24; i++)
                {
                    if (i - 20 < IABot.MonsterField.Count)
                    {
                        if (IABot.MonsterField[i - 20].Position)
                            this.Controls["label" + i].Text = "Defensa";
                        else
                            this.Controls["label" + i].Text = "Ataque";
                    }
                    else
                    {
                        this.Controls["label" + i].Text = "";
                    }
                }
                for (int i = 15; i <= 19; i++)
                {
                    if (i - 15 < player1.MonsterField.Count)
                    {
                        if (player1.MonsterField[i - 15].Position)
                            this.Controls["label" + i].Text = "Defensa";
                        else
                            this.Controls["label" + i].Text = "Ataque";
                    }
                    else
                    {
                        this.Controls["label" + i].Text = "";
                    }
                }
            }
            
        }

        public void CheckLife()
        {
            int result = 0;
            if (player1.Vida < 0 || player1.Deck.Count == 0)
                result = 2;
            if (IABot.Vida < 0 || IABot.Deck.Count == 0)
                result = 1;

            if(result == 1)
            {
                finished = true;
                timer1.Enabled = false;
                timer2.Enabled = false;
                MessageBox.Show("Ganó el Jugador 1", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            if(result == 2)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                finished = true;
                MessageBox.Show("Ganó el Jugador 2", "Ah-Gi-Oh!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (clicked && playerOption == 3)
            {
                Atacar(picture, index, picture2, index2);
                timer1.Enabled = false;
                clicked = false;
                picture2 = 0;
                index2 = 0;
                playerOption = 0;
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (playerOption == 3 && IABot.MonsterField.Count == 0)
            {
                picture2 = -1;
                index2 = -1;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2)
            {
                picture2 = -1;
                index2 = -1;
                clicked = true;
                timer2.Enabled = true;
            }

        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(22, 1);
            if (playerOption == 3 && pictureBox22.BackgroundImage != null)
            {
                picture2 = 22;
                index2 = 1;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox22.BackgroundImage != null)
            {
                picture2 = 22;
                index2 = 1;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(23, 2);
            if (playerOption == 3 && pictureBox23.BackgroundImage != null)
            {
                picture2 = 23;
                index2 = 2;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox23.BackgroundImage != null)
            {
                picture2 = 23;
                index2 = 2;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(24, 3);
            if (playerOption == 3 && pictureBox24.BackgroundImage != null)
            {
                picture2 = 24;
                index2 = 3;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox24.BackgroundImage != null)
            {
                picture2 = 24;
                index2 = 3;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(25, 4);
            if (playerOption == 3 && pictureBox25.BackgroundImage != null)
            {
                picture2 = 25;
                index2 = 4;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox25.BackgroundImage != null)
            {
                picture2 = 25;
                index2 = 4;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(2, 1);
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            if (draw)
            {
                if (playerOption == 2)
                {
                    picture2 = 2;
                    index2 = 1;
                    clicked = true;
                    timer2.Enabled = true;
                }
                else if (playerOption == 1)
                {
                    picture2 = 2;
                    index2 = 1;
                    clicked = true;
                    timer3.Enabled = true;
                }
                else if (pictureBox1.BackgroundImage != null && playerOption != 2)
                {
                    picture = 2;
                    index = 1;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(3, 2);
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            if (draw)
            {
                if (playerOption == 2)
                {
                    picture2 = 3;
                    index2 = 2;
                    clicked = true;
                    timer2.Enabled = true;
                }
                else if (playerOption == 1)
                {
                    picture2 = 3;
                    index2 = 2;
                    clicked = true;
                    timer3.Enabled = true;
                }else if (pictureBox1.BackgroundImage != null && playerOption != 2)
                {
                    picture = 3;
                    index = 2;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(4, 3);
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            if (draw)
            {
                if (playerOption == 2)
                {
                    picture2 = 4;
                    index2 = 3;
                    clicked = true;
                    timer2.Enabled = true;
                }
                else if (playerOption == 1)
                {
                    picture2 = 4;
                    index2 = 3;
                    clicked = true;
                    timer3.Enabled = true;
                }
                else if (pictureBox1.BackgroundImage != null && playerOption != 2)
                {
                    picture = 4;
                    index = 3;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(5, 4);
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            if (draw)
            {
                if (playerOption == 2)
                {
                    picture2 = 5;
                    index2 = 4;
                    clicked = true;
                    timer2.Enabled = true;
                }
                else if (playerOption == 1)
                {
                    picture2 = 5;
                    index2 = 4;
                    clicked = true;
                    timer3.Enabled = true;
                }
                else if (pictureBox1.BackgroundImage != null && playerOption != 2)
                {
                    picture = 5;
                    index = 4;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(6, 0);
            timer1.Enabled = false;
            timer2.Enabled = false;
            if (draw)
            {
                if (pictureBox6.BackgroundImage != null)
                {
                    picture = 6;
                    index = 0;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (clicked && playerOption == 2)
            {
                ActivarEfecto(picture, index, picture2, index2);
                timer2.Enabled = false;
                clicked = false;
                picture2 = 0;
                index2 = 0;
                playerOption = 0;
            }

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(7, 1);
            timer1.Enabled = false;
            timer2.Enabled = false;
            if (draw)
            {
                if (pictureBox7.BackgroundImage != null)
                {
                    picture = 7;
                    index = 1;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(8, 2);
            timer1.Enabled = false;
            timer2.Enabled = false;
            if (draw) 
            {
                if (pictureBox8.BackgroundImage != null)
                {
                    picture = 8;
                    index = 2;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                } 
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(9, 3);
            timer1.Enabled = false;
            timer2.Enabled = false;
            if (draw)
            {
                if (pictureBox9.BackgroundImage != null)
                {
                    picture = 9;
                    index = 3;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            UpdateVisualicer(10, 4);
            timer1.Enabled = false;
            timer2.Enabled = false;
            if (draw)
            {
                if (pictureBox10.BackgroundImage != null)
                {
                    picture = 10;
                    index = 4;
                    picture2 = 0;
                    index2 = 0;
                    Form4 f4 = new Form4(this, player1, picture, index, picture2, index2);
                    f4.Owner = this;
                    f4.Show();
                }
            }
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(26, 0);
            if (playerOption == 3 && pictureBox26.BackgroundImage != null)
            {
                picture2 = 26;
                index2 = 0;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox26.BackgroundImage != null)
            {
                picture2 = 26;
                index2 = 0;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox41_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5(this,player1);
            f5.Owner = this;
            f5.Show();
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(27, 1);
            if (playerOption == 3 && pictureBox27.BackgroundImage != null)
            {
                picture2 = 27;
                index2 = 1;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox27.BackgroundImage != null)
            {
                picture2 = 27;
                index2 = 1;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(28, 2);
            if (playerOption == 3 && pictureBox28.BackgroundImage != null)
            {
                picture2 = 28;
                index2 = 2;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox28.BackgroundImage != null)
            {
                picture2 = 28;
                index2 = 2;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(29, 3);
            if (playerOption == 3 && pictureBox29.BackgroundImage != null)
            {
                picture2 = 29;
                index2 = 3;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox29.BackgroundImage != null)
            {
                picture2 = 29;
                index2 = 3;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(30, 4);
            if (playerOption == 3 && pictureBox30.BackgroundImage != null)
            {
                picture2 = 30;
                index2 = 4;
                clicked = true;
                timer1.Enabled = true;
            }
            if (playerOption == 2 && pictureBox30.BackgroundImage != null)
            {
                picture2 = 30;
                index2 = 4;
                clicked = true;
                timer2.Enabled = true;
            }
        }

        private void pictureBox43_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5(this, IABot);
            f5.Owner = this;
            f5.Show();
        }

        private void pictureBox42_DoubleClick(object sender, EventArgs e)
        {
            Form6 f6 = new Form6(this,player1);
            f6.Owner = this;
            f6.Show();
        }

        private void UpdateVisualicer(int picture, int index)
        {
            if(this.Controls["pictureBox" + picture].BackgroundImage != null)
            {
                pictureBox45.BackgroundImage = this.Controls["pictureBox" + picture].BackgroundImage;
                if(picture > 0 && picture <= 5)
                {
                    label36.Text = player1.MonsterField[index].Nombre;
                    label37.Text = ((MonsterCard)player1.MonsterField[index]).Nivel.ToString();
                    label38.Text = ((MonsterCard)player1.MonsterField[index]).ATK.ToString();
                    label39.Text = ((MonsterCard)player1.MonsterField[index]).ATK.ToString();
                    label40.Text = player1.MonsterField[index].Elemento;
                    label41.Text = player1.MonsterField[index].Efecto;
                }
                else if (picture > 5 && picture <= 10)
                {
                    label36.Text = player1.MagicField[index].Nombre;
                    label37.Text = "";
                    label38.Text = "";
                    label39.Text = "";
                    label40.Text = player1.MagicField[index].Elemento;
                    label41.Text = player1.MagicField[index].Efecto;
                }
                else if (picture > 10 && picture <= 20)
                {
                    label36.Text = player1.Hand[index].Nombre;
                    if (player1.Hand[index] is MonsterCard)
                    {
                        label37.Text = ((MonsterCard)player1.Hand[index]).Nivel.ToString();
                        label38.Text = ((MonsterCard)player1.Hand[index]).ATK.ToString();
                        label39.Text = ((MonsterCard)player1.Hand[index]).ATK.ToString();
                    }
                    else
                    {
                        label37.Text = "";
                        label38.Text = "";
                        label39.Text = "";
                    }
                    label40.Text = player1.Hand[index].Elemento;
                    label41.Text = player1.Hand[index].Efecto;
                }
                else if (picture > 20 && picture <= 25)
                {
                    label36.Text = IABot.MonsterField[index].Nombre;
                    label37.Text = ((MonsterCard)IABot.MonsterField[index]).Nivel.ToString();
                    label38.Text = ((MonsterCard)IABot.MonsterField[index]).ATK.ToString();
                    label39.Text = ((MonsterCard)IABot.MonsterField[index]).ATK.ToString();
                    label40.Text = IABot.MonsterField[index].Elemento;
                    label41.Text = IABot.MonsterField[index].Efecto;
                }
                else if (picture > 25 && picture <= 30)
                {
                    label36.Text = IABot.MagicField[index].Nombre;
                    label37.Text = "";
                    label38.Text = "";
                    label39.Text = "";
                    label40.Text = IABot.MagicField[index].Elemento;
                    label41.Text = IABot.MagicField[index].Efecto;
                }
                else if (picture > 30 && picture <= 40)
                {
                    label36.Text = IABot.Hand[index].Nombre;
                    if (IABot.Hand[index] is MonsterCard)
                    {
                        label37.Text = ((MonsterCard)IABot.Hand[index]).Nivel.ToString();
                        label38.Text = ((MonsterCard)IABot.Hand[index]).ATK.ToString();
                        label39.Text = ((MonsterCard)IABot.Hand[index]).ATK.ToString();
                    }
                    else
                    {
                        label37.Text = "";
                        label38.Text = "";
                        label39.Text = "";
                    }
                    label40.Text = IABot.Hand[index].Elemento;
                    label41.Text = IABot.Hand[index].Efecto;
                }

            }
        }

       
        private void pictureBox40_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(40, 9);
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            if(playerOption == 1 && clicked)
            {
                invocar(player1, picture, index, picture2, index2);
                timer3.Enabled = false;
                clicked = false;
                picture2 = 0;
                index2 = 0;
                playerOption = 0;
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(11, 0);
            if (pictureBox11.BackgroundImage != null)
            {
                if (draw == true)
                {
                    if (playerOption == 0)
                    {
                        if ((player1.CanInvoke) || player1.Hand[0] is MagicCard || player1.Hand[0] is TrapCard)
                        {
                            Form4 f4 = new Form4(this, player1, 11, 0, 0, 0);
                            picture = 11;
                            index = 0;
                            f4.Owner = this;
                            f4.Show();
                        }
                    }
                    else if (playerOption == 2)
                    {
                        picture2 = 11;
                        index2 = 0;
                        clicked = true;
                        timer2.Enabled = true;
                    }
                }
            }
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(31, 0);
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(32, 1);
        }

        private void pictureBox33_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(33, 2);
        }

        private void pictureBox34_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(34, 3);
        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(35, 4);
        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(36, 5);
        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(37, 6);
        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(38, 7);
        }

        private void pictureBox39_Click(object sender, EventArgs e)
        {
            UpdateVisualicer(39, 8);
        }
    }
}
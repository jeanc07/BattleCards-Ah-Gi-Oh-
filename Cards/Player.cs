namespace Cards
{

    public class Player
    {
        private int live;
        private List<Card> deck;
        private List<Card> hand;

        private List<Card> monsterField;

        private List<Card> magicField;

        private List<Card> graveyard;

        private bool caninvk;   // True: si puede invocar  False: no puede invocar

        public int Vida{get => live; set => live = value;}
        public List<Card> Deck{get => deck; set => deck = value;}
        public List<Card> Hand{get => hand; set => hand = value;}

        public List<Card> MonsterField{get => monsterField; set => monsterField = value;}

        public List<Card> MagicField{get => magicField; set => magicField = value;}

        public List<Card> Graveyard{get => graveyard; set => graveyard = value;}

        public bool CanInvoke {get => caninvk; set => caninvk = value;}

        public Player(int live, List<Card> deck, List<Card> hand)
        {
            this.live = live;
            this.deck = deck;
            this.hand = hand;
            this.graveyard = new List<Card>();
            this.monsterField = new List<Card>(5);
            this.magicField = new List<Card>(5);
            this.caninvk = false;
        }


        public Player(int live)
        {
            this.live = live;
            this.deck = new List<Card>();
            this.hand = new List<Card>();
            this.graveyard = new List<Card>();
            this.monsterField = new List<Card>(5);
            this.magicField = new List<Card>(5);   
            this.caninvk = false;         
        }       

        public Player()
        {
            this.live = 5000;
            this.deck = new List<Card>();
            this.hand = new List<Card>();
            this.graveyard = new List<Card>();
            this.monsterField = new List<Card>(5);
            this.magicField = new List<Card>(5);      
            this.caninvk = false;      
        }            

        public bool existCardInTheField(int id)
        {
            bool exist = false;
            for (int i = 0; i < MonsterField.Count && exist == false; i++)
            {
                if(id == MonsterField.ElementAt(i).Id)   
                {
                    exist = true;                
                }
            }

            for (int i = 0; i < MagicField.Count && exist == false; i++)
            {
                if(id == MagicField.ElementAt(i).Id)   
                {
                    exist = true;                
                }
            }        

            return exist;    
        } 

        public bool existCardInTheHand(int id)
        {
            bool exist = false;
            for (int i = 0; i < Hand.Count && exist == false; i++)
            {
                if(id == Hand.ElementAt(i).Id)   
                {
                    exist = true;                
                }
            }    

            return exist;    
        }      

        public bool existCardInTheGraveyard(int id)
        {
            bool exist = false;
            for (int i = 0; i < Graveyard.Count && exist == false; i++)
            {
                if(id == Graveyard.ElementAt(i).Id)   
                {
                    exist = true;                
                }
            }    

            return exist;    
        }          

        public int cardPositionInTheMonsterField(int id)
        {
            int pos = -1;

            for (int i = 0; i < MonsterField.Count && pos == -1; i++)
            {
                if(id == MonsterField.ElementAt(i).Id)   
                {
                    pos = i;              
                }
            }     

            return pos;       
        }  

        public int cardPositionInTheHand(int id)
        {
            int pos = -1;

            for (int i = 0; i < Hand.Count && pos == -1; i++)
            {
                if(id == Hand.ElementAt(i).Id)   
                {
                    pos = i;              
                }
            }     

            return pos;       
        }         
    }
}
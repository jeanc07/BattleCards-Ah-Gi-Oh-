namespace Cards
{
    public class MonsterCard : Card
    {
        protected int niveldelacarta;

        protected int ataquedelacarta;

        protected int defensadelacarta;        

        protected bool canatq;          // True: si puede atacar, False: si no puede atacar

        public MonsterCard(int id, string nombredelacarta,string elementodelacarta, string tipodelacarta, string efectodelacarta, 
                           bool position,string frontimage, string backimage,bool caneffect, int niveldelacarta, int ataquedelacarta, int defensadelacarta, bool canatq)
        : base(id, nombredelacarta, elementodelacarta,  tipodelacarta,  efectodelacarta, position, frontimage, backimage,caneffect)
        {
            this.niveldelacarta = niveldelacarta;
            this.ataquedelacarta = ataquedelacarta;
            this.defensadelacarta = defensadelacarta;    
            this.canatq = true;        
        }

        public MonsterCard()
        : base()
        {
            this.niveldelacarta = 0;
            this.ataquedelacarta = 0;
            this.defensadelacarta = 0;      
            this.canatq = true;     
        }        

    public int Nivel{get => niveldelacarta; set => niveldelacarta = value;}
    public int ATK{get => ataquedelacarta; set => ataquedelacarta = value;}
    public int DEF{get => defensadelacarta; set => defensadelacarta = value;}           

    public bool CanATK{get => canatq; set => canatq = value;}     
    }
}
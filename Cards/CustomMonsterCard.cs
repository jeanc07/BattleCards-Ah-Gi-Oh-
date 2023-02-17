namespace Cards
{
    public class CustomMonsterCard : MonsterCard
    {
        List<CustomAttribute> attribute;

        public CustomMonsterCard(int id, string nombredelacarta,string elementodelacarta, string tipodelacarta, string efectodelacarta, 
                           bool position,string frontimage,string backimage,bool caneffect, int niveldelacarta, int ataquedelacarta, int defensadelacarta, bool canatq)
        : base(id, nombredelacarta, elementodelacarta,  tipodelacarta,  efectodelacarta, position,frontimage,backimage,caneffect,
               niveldelacarta, ataquedelacarta, defensadelacarta, canatq)
        {
            attribute = new List<CustomAttribute>();     
        }        

        public CustomMonsterCard()
        : base()
        {
          attribute = new List<CustomAttribute>();
        }     

        public List<CustomAttribute> Attribute {get => attribute; set => attribute = value;}     
    }
}
namespace Cards
{
    public class CustomMagicCard : MagicCard
    {
        List<CustomAttribute> attribute;

        public CustomMagicCard(int id, string nombredelacarta,string elementodelacarta, string tipodelacarta, string efectodelacarta, bool position,string frontimage,string backimage,bool caneffect)
        : base(id, nombredelacarta, elementodelacarta,  tipodelacarta,  efectodelacarta, position,frontimage,backimage,caneffect)
        {
          attribute = new List<CustomAttribute>();
        }

        public CustomMagicCard()
        : base()
        {
          attribute = new List<CustomAttribute>();
        }     

        public List<CustomAttribute> Attribute {get => attribute; set => attribute = value;}     
    }
}
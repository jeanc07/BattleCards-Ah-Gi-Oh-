namespace Cards
{
    public class CustomTrapCard : TrapCard
    {
        List<CustomAttribute> attribute;

        public CustomTrapCard(int id, string nombredelacarta,string elementodelacarta, string tipodelacarta, string efectodelacarta, bool position,string frontimage,string backimage,bool caneffect)
        : base(id, nombredelacarta, elementodelacarta,  tipodelacarta,  efectodelacarta, position,frontimage,backimage,caneffect)
        {
          attribute = new List<CustomAttribute>();
        }

        public CustomTrapCard()
        : base()
        {
          attribute = new List<CustomAttribute>();
        }     

        public List<CustomAttribute> Attribute {get => attribute; set => attribute = value;}     
    }
}
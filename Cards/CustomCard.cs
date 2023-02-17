namespace Cards
{
    public class CustomCard : Card
    {
        List<CustomAttribute> attribute;

        public CustomCard(int id, string nombredelacarta,string elementodelacarta, string tipodelacarta, string efectodelacarta, bool position ,string frontimage,string backimage,bool caneffect)
        : base(id, nombredelacarta, elementodelacarta,  tipodelacarta,  efectodelacarta, position,frontimage,backimage,caneffect)
        {
             this.attribute = new List<CustomAttribute>();
        }

        public CustomCard()
        : base()
        {
            this.attribute = new List<CustomAttribute>();
        }     

        public List<CustomAttribute> Attribute {get => attribute; set => attribute = value;}     
    }
}
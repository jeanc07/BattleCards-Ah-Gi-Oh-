namespace Cards
{
    public class TrapCard : Card
    {
        public TrapCard(int id, string nombredelacarta,string elementodelacarta, string tipodelacarta, string efectodelacarta, bool position, string frontimage, string backimage,bool caneffect)
        : base(id, nombredelacarta, elementodelacarta,  tipodelacarta,  efectodelacarta, position, frontimage, backimage,caneffect)
        {

        }

        public TrapCard()
        : base()
        {

        }        
    }
}
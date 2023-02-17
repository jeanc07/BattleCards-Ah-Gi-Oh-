namespace Cards
{
    public class MagicCard : Card
    {
        public MagicCard(int id, string nombredelacarta,string elementodelacarta, string tipodelacarta, string efectodelacarta, bool position, string frontimage, string backimage,bool caneffect)
               : base(id, nombredelacarta, elementodelacarta,  tipodelacarta,  efectodelacarta, position, frontimage, backimage,caneffect)
        {

        }

        public MagicCard()
               : base()
        {

        }        
    }
}
namespace Cards
{
    public class CardAttribute : Attribute
    {
        public CardAttribute(string nombre, string tipoDato, object? dato, bool mandatory)
        :base(nombre, tipoDato, dato, mandatory)
        {

        }
    }
}
namespace Cards
{
    public class CustomAttribute : Attribute
    {
        List<string> efectos;
        List<string> lines;

        public List<string> Efectos {get => efectos; set => efectos = value;} 

        public List<string> Lines {get => lines; set => lines = value;} 

        public CustomAttribute(string nombre, string tipoDato, object? dato, bool mandatory)
        :base(nombre, tipoDato, dato, mandatory)
        {
            efectos = new List<string>();
        }

        public CustomAttribute()
        :base()
        {
            efectos = new List<string>();
        }        
    }
}
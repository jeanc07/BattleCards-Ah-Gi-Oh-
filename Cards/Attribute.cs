namespace Cards
{
    public class Attribute
    {
        private string nombre;
        private string tipoDato;
        private object? dato;
        private bool mandatory;

        public Attribute(string nombre, string tipoDato, object? dato, bool mandatory)
        {
          this.nombre = nombre;
          
          this.tipoDato = tipoDato;

          this.dato = dato;

          this.mandatory = mandatory;
        }

        public Attribute()
        {
          this.nombre = "";
          
          this.tipoDato = "";

          this.dato = null;

          this.mandatory = false;
        }        

        public string Nombre {get; set;}

        public string Tipo {get; set;}

        public bool Mandatory {get; set;}
    }
}
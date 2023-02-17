namespace Cards
{
public class Card
{
    protected int id;
    protected string nombredelacarta;
    protected string elementodelacarta;
    protected string tipodelacarta;
    protected string efectodelacarta;
    protected bool position;
    protected string frontimage;
    protected string backimage;
    protected bool caneffect;   //True si puede activar effecto


    public string Nombre{get => nombredelacarta; set => nombredelacarta = value; }
    public string Elemento{get => elementodelacarta; set => elementodelacarta = value; }
    public string Tipo{get => tipodelacarta; set => tipodelacarta = value; }
    public string Efecto{get => efectodelacarta; set => efectodelacarta = value; }
    public int Id {get => id; set => id = value; }
    public bool Position{get => position; set => position = value; } 
    public string FrontImage { get => frontimage; set => frontimage = value; }
    public string BackImage { get => backimage; set => backimage = value; }
    public bool CanEffect { get => caneffect; set => caneffect = value; }
    
    public Card(int id, string nombredelacarta,string elementodelacarta, string tipodelacarta, string efectodelacarta, bool position, string frontimage, string backimage,bool caneffect)
    {
        this.nombredelacarta = nombredelacarta;
        this.tipodelacarta = tipodelacarta;
        this.efectodelacarta = efectodelacarta;
        this.elementodelacarta = elementodelacarta;
        this.id = id;
        this.position = position;
        this.frontimage = frontimage;
        this.backimage = backimage;
        this.caneffect = caneffect;
    }

    public Card()
    {
        this.nombredelacarta = "";
        this.tipodelacarta = "";
        this.efectodelacarta = "";
        this.elementodelacarta = "";
        this.id = 0;
        this.position = false;
        this.frontimage = "";
        this.backimage = "";
        this.caneffect = true;
    }
}
}
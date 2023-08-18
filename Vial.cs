namespace VialSort.Vial;
struct Color
{
    public int red {get;}
    public int green {get;}
    public int blue {get;}
    public ConsoleColor color {get;}


    public Color(int red,int green,int blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
        this.color = ConsoleColor.Black;
    }
    public Color(ConsoleColor color)
    {
        this.red = 0;
        this.green = 0;
        this.blue = 0;
        this.color = color;
    }
}
class Colors
{
    public List<Color> colors {get;}
    Colors(Color empty,Color unknown)
    {
        this.colors = new List<Color> {empty,unknown};
    }
    public void AddColor(Color Color)
    {
        colors.Add(Color);
    }
}


class Vial
{
    int[] Liquids;
    bool HiddenLiquids {get;}
    public Vial(int[] Liquids, bool HiddenLiquids)
    {
        this.Liquids = Liquids;
        this.HiddenLiquids = HiddenLiquids;

    }
    public int[] GetTopLiquid()
    {
        List<int> temp = new List<int> {};

        int TopColor = this.Liquids[0];
        for (int l =0; l < this.Liquids.Length;l++)
        {
            if(HiddenLiquids & this.Liquids[l] != TopColor)
            {
                temp.Add(1); // 1 is unknown
                continue;
            }
            temp.Add(Liquids[l]);
        }
    
        return temp.ToArray();

    }
    public Vial MoveTopToVial(Vial Vial)
    {
        // do checks here
        // temp
        return new Vial(new int[] {},true);

    }
}
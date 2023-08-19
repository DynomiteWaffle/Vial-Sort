namespace VialSort.Vial;

enum ColorLocations
{
    empty = -1,
    unknown = -2
}
struct Color
{
    public int red {get;}
    public int green {get;}
    public int blue {get;}
    public ConsoleColor color {get;}


    public Color(int red,int green,int blue,ConsoleColor color)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
        this.color = color;
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
    // public Dictionary<ConsoleColor,Color> colors {get;}
    Colors(Color empty,Color unknown)
    {
        this.colors = new List<Color> {empty,unknown};
        // this.colors = new Dictionary<ConsoleColor, Color> {};
        // this.colors[empty.color] = empty; 
        // this.colors[unknown.color] = unknown; 
    }
    public void AddColor(Color Color)
    {
        colors.Add(Color);
        // colors[Color.color] = Color;
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

        int TopColor = 0;
        foreach(int c in this.Liquids)
        {
            if(c != (int)ColorLocations.empty)
            {
                TopColor = c;
                break;
            }
        }

        // get top
        for (int l =0; l < this.Liquids.Length;l++)
        {
            if(HiddenLiquids & this.Liquids[l] != TopColor & this.Liquids[l] != (int)ColorLocations.empty)
            {
                temp.Add((int)ColorLocations.unknown);
                continue;
            }
            temp.Add(Liquids[l]);
        }
    
        return temp.ToArray();

    }
    public int[] GetOnlyTopLiquid()
    {
         List<int> temp = new List<int> {};

        int TopColor = 0;
        foreach(int c in this.Liquids)
        {
            if(c != (int)ColorLocations.empty)
            {
                TopColor = c;
                break;
            }
        }
        // Console.WriteLine("TopColor: "+TopColor);
        // get top
        for (int l = 0; l < this.Liquids.Length;l++)
        {
            // if(this.Liquids[l] == (int)ColorLocations.empty & this.Liquids[l] != TopColor)
            // {
            //     break;
            // }
            if(this.Liquids[l] == (int)ColorLocations.empty){continue;}
            if(this.Liquids[l] != TopColor){break;}
            temp.Add(Liquids[l]);
        }
        // foreach(int c in temp){Console.Write(c);}
        // Console.WriteLine(" <- OnlyTopVial");
    
        return temp.ToArray();
    }
    public bool AddToTop(int[] Vial)
    {
        // foreach(int c in Vial){Console.Write(c);}
        // Console.WriteLine(" <- Vial");
        // checks
        // check if it will fit
        // check if top color == added color

        // add to top of liquids
        int emptiesOnTop = 0;
        bool top = true;
        bool[] empties = new bool[this.Liquids.Length];
        for(int e=0; e<this.Liquids.Length;e++)
        {
            if(this.Liquids[e] == (int)ColorLocations.empty)
            {
                // Console.WriteLine("newEmptie");
                if(top){emptiesOnTop++;}
                empties[e] = true;
            }
            else
            {
                top = false;
                empties[e] = false;
            }
        }
        // too Big
        // Console.WriteLine(emptiesOnTop+"\n"+Vial.Length);
        if(emptiesOnTop < Vial.Length){Console.WriteLine("A");return false;}
        // wrong color
        // Console.WriteLine(Vial[0]);
        if(Vial[0] != this.GetOnlyTopLiquid()[0]){Console.WriteLine("B");return false;}

        // add to top
        for(int add=emptiesOnTop-Vial.Length;add<=Vial.Length;add++)
        {
            this.Liquids[add] = Vial[0];
        }

        return true;
    }
    public void MoveTopToVial(Vial Vial)
    {
        if(Vial.AddToTop(this.GetOnlyTopLiquid()))
        {
            int emptiesOnTop = 0;
            bool top = true;
            for(int e=0; e<this.Liquids.Length;e++)
            {
                if(this.Liquids[e] == (int)ColorLocations.empty)
                {
                    // Console.WriteLine("newEmptie");
                    if(top){emptiesOnTop++;}
                }
                else
                {
                    top = false;
                }
            }
            Console.WriteLine(emptiesOnTop);
            // remove top --doesnt work
            for(int c=emptiesOnTop;c<= this.GetOnlyTopLiquid().Length+emptiesOnTop-1;c++)
            {
                this.Liquids[c] = (int)ColorLocations.empty;
            }
        }
        else
        {
            Console.WriteLine("Moved Failed");
        }

    }
}
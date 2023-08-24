using System.Runtime.Serialization;

namespace VialSort.GameObjects;

enum ColorLocations
{
    empty = 0,
    unknown = 1,
}
[Serializable()]
public struct Color : ISerializable
{
    public int red = 0;
    public int green = 0;
    public int blue = 0 ;

    public Color() {}
    public Color(int red,int green,int blue)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }

     void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("red",this.red);
        info.AddValue("green",this.green);
        info.AddValue("blue",this.blue);
    }
    public Color(SerializationInfo info, StreamingContext context)
    {
        this.red = (int)info.GetValue("red", typeof(int));
        this.green = (int)info.GetValue("green", typeof(int));
        this.blue = (int)info.GetValue("blue", typeof(int));
    }
}
// [Serializable()]
// public class Colors : ISerializable
// {
//     public List<Color> colors {get;}
//     // public Dictionary<ConsoleColor,Color> colors {get;}
//     public Colors()
//     {
//         this.colors = new List<Color> {new Color(0,0,0),new Color(0,0,0)};
//     }
//     public Colors(Color empty,Color unknown)
//     {
//         this.colors = new List<Color> {empty,unknown};
//         // this.colors = new Dictionary<ConsoleColor, Color> {};
//         // this.colors[empty.color] = empty; 
//         // this.colors[unknown.color] = unknown; 
//     }
//     public void AddColor(Color Color)
//     {
//         colors.Add(Color);
//         // colors[Color.color] = Color;
//     }

//     void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
//     {
//         foreach(Color c in this.colors)
//         {
//             info.AddValue("colors",c);
//         }
//     }
//     // public Colors(SerializationInfo info, StreamingContext context)
//     // {
//     //      this.colors = (List<Color>)info.GetValue("colors", typeof(List<Color>));
//     // }
// }

class Vial : ICloneable
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
        bool onlyhidden = false;
        for (int l =0; l < this.Liquids.Length;l++)
        {
            if(onlyhidden)
            {
                temp.Add((int)ColorLocations.unknown);
            }
            if(HiddenLiquids & this.Liquids[l] != TopColor & this.Liquids[l] != (int)ColorLocations.empty)
            {
                onlyhidden = true;
                temp.Add((int)ColorLocations.unknown);
                continue;
            }
            temp.Add(Liquids[l]);
        }
        // fix for empty vials
        if(temp.Count == 0){
            temp.Add((int)ColorLocations.empty);
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
        // Console.WriteLine(temp.Count);
        // fix for empty vials
        if(temp.Count == 0){
            temp.Add((int)ColorLocations.empty);
        }
    
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
        // Console.WriteLine("VLen "+Vial.Length);
        bool EmptyVial = true;
        foreach(int l in Vial)
        {
            if(l != (int)ColorLocations.empty){EmptyVial = false;}
        }
        if(EmptyVial){Console.WriteLine("Nothing To Add");return false;}



        bool completelyEmpty = false;

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
        if(emptiesOnTop == this.Liquids.Length){completelyEmpty = true;}
        // Console.WriteLine(emptiesOnTop+"\n"+Vial.Length);
        if(!completelyEmpty)
        {
            // too Big
            if(emptiesOnTop < Vial.Length){Console.WriteLine("Not Enough Space");return false;}
            // wrong color
            // Console.WriteLine(Vial[0]);
            if(Vial[0] != this.GetOnlyTopLiquid()[0]){Console.WriteLine("Colors Dont Match");return false;}
        }

        // Console.WriteLine(completelyEmpty);
        if(completelyEmpty)
        {
            // add to top
            for(int add=0;add < Vial.Length;add++)
            {
                // Console.WriteLine(add);
                this.Liquids[this.Liquids.Length-add-1] = Vial[0];
            }
            return true;
        }

        // add to top
        for(int add=Vial.Length;add<=Vial.Length;add++)
        {
            this.Liquids[emptiesOnTop-add] = Vial[0];
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
            // Console.WriteLine(emptiesOnTop);
            // fully empty
            if(this.GetOnlyTopLiquid().Length + emptiesOnTop == this.Liquids.Length)
            {

                // set to empty
                for(int i =0; i< this.Liquids.Length;i++)
                {
                    this.Liquids[i] = (int)ColorLocations.empty;
                }
                return;
            }
            // remove layer
            for(int c=0;c <= this.GetOnlyTopLiquid().Length;c++)
            {
                this.Liquids[c+emptiesOnTop-1] = (int)ColorLocations.empty;
            }
        }
        else
        {
            Console.WriteLine("Moved Failed");
        }

    }
    public int GetLenght()
    {
        return this.Liquids.Length;
    }
    public int GetPos(int pos)
    {
        // error checks
        if(pos>this.Liquids.Length){return (int)ColorLocations.empty;}
        if(pos<0){return (int)ColorLocations.empty;}
        if(this.HiddenLiquids)
        {
            var temp = this.GetTopLiquid();
            if(pos > temp.Length){return (int)ColorLocations.unknown;}
            
            return temp[pos];
        }
        else
        {
            return this.Liquids[pos];
        }
    }
    public object Clone()
    {
        var temp = (Vial) this.MemberwiseClone();
        temp.Liquids = (int[]) this.Liquids.Clone();
        return temp;
    }
}
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
        // get empties
        int emptiesOnTop = 0;
        foreach(int l in this.Liquids)
        {
            if(l==(int)ColorLocations.empty)
            {
                emptiesOnTop++;
            }
            else
            {
                break;
            }
        }

        // foreach(int c in Vial){Console.Write(c);}
        // Console.WriteLine(" <- Vial");
        // foreach(int c in this.Liquids){Console.Write(c);}
        // Console.WriteLine(" <- This Vial");

        // checks
        // check empty vial
        if(Vial[0] == (int)ColorLocations.empty)
        {
            Console.WriteLine("Nothing To Move");
            return false;
        }
        // check right color
        if(this.Liquids.Length != emptiesOnTop & this.GetOnlyTopLiquid()[0] != Vial[0])
        {
            Console.WriteLine("Wrong Color");
            return false;
        }
        // check if vial empty
        // check if it will fit
        if(emptiesOnTop < Vial.Length)
        {
            Console.WriteLine("Vial Too Full");
            return false;
        }

        // add vial
        foreach(int i in Enumerable.Range(emptiesOnTop-Vial.Length,Vial.Length))
        {
            Console.WriteLine(i);
            this.Liquids[i] = Vial[0];
        }

        return true;
    }
    public void MoveTopToVial(Vial Vial)
    {
        if(Vial.AddToTop(this.GetOnlyTopLiquid()))
        {
            int emptiesOnTop = 0;
            foreach(int l in this.Liquids)
            {
                if(l==(int)ColorLocations.empty)
                {
                    emptiesOnTop++;
                }
                else
                {
                    break;
                }
            }
            // remove liquids
            foreach(int i in Enumerable.Range(emptiesOnTop,this.GetOnlyTopLiquid().Length)) 
            {
                this.Liquids[i] = (int)ColorLocations.empty;
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
    public bool isSolid()
    {
        bool check = true;
        foreach(int l in this.Liquids)
        {
            if(l != this.Liquids[0])
            {
                check = false;
                break;
            }
        }
        return check;
    }
    public object Clone()
    {
        var temp = (Vial) this.MemberwiseClone();
        temp.Liquids = (int[]) this.Liquids.Clone();
        return temp;
    }
}
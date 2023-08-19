namespace VialSort;
class Program
{
    static void Main(string[] args)
    {
        // temp
        var vialA = new GameObjects.Vial(new int[] {-1,1,1,5},true);
        var vialB = new GameObjects.Vial(new int[] {-1,1,2,3},true);
        foreach(int c in vialA.GetTopLiquid()){Console.Write(c);}
        Console.WriteLine();
        foreach(int c in vialB.GetTopLiquid()){Console.Write(c);}
        Console.WriteLine();
        vialA.MoveTopToVial(vialB);
        Console.WriteLine("move");
        foreach(int c in vialA.GetTopLiquid()){Console.Write(c+":");}
        Console.WriteLine();
        foreach(int c in vialB.GetTopLiquid()){Console.Write(c+":");}
        Console.WriteLine();

    }
}

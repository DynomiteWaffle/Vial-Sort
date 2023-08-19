namespace VialSort;
class Program
{
    static void Main(string[] args)
    {
        // mode
        bool ansi = true;
        bool graphics = true;

        // set one render type
        if(args.Length >= 1)
        {
            if(args[0] == "--ansi"){graphics = false;}
            if(args[0] == "--graphics"){ansi = false;}
        }
        
        
        // game inti
        int index = 0;
        int selected = -1;
        List<GameObjects.Vial> vials = new List<GameObjects.Vial> {};
        var vialA = new GameObjects.Vial(new int[] {-1,1,1,5},true);
        var vialB = new GameObjects.Vial(new int[] {-1,1,2,3},true);
        vials.Add(vialA);
        vials.Add(vialB);

        // Render Init
        Render.Ansi Ansi = Ansi = new Render.Ansi(vials.ToArray(),ref index,ref selected,ConsoleColor.White);
        // graphics
        // TODO:
        // gameloop
        if(ansi)
        {
            Ansi.DrawVials();
        }
        if(graphics)
        {
            // TODO:
        }
    }
}

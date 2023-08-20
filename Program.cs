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
        
        
        // game init
        int index = 0;
        int selected = -1;
        List<GameObjects.Vial> vials = new List<GameObjects.Vial> {};
        var vialA = new GameObjects.Vial(new int[] {-1,-1,-1,-1},true);
        var vialC = new GameObjects.Vial(new int[] {-1,-1,-1,-1},true);
        var vialB = new GameObjects.Vial(new int[] {-1,1,2,1},true);
        vials.Add(vialA);
        vials.Add(vialC);
        vials.Add(vialB);
        // reset value
        // clone
        var resetVials = vials;
        


        ConsoleKeyInfo keypress;

        // Render Init
        bool updateScreen = true;
        Render.Ansi Ansi = Ansi = new Render.Ansi(vials.ToArray(),ConsoleColor.White);
        // graphics
        // TODO:
        // gameloop
        // initDraw
        DrawScreen();
        while (true)
        {

            if(Console.KeyAvailable)
            {
                keypress = Console.ReadKey(true);
            }
            else{
                keypress = new ConsoleKeyInfo() {};
            }

            // left
            if(keypress.Key == ConsoleKey.A & index > 0)
            {
                index--;
                updateScreen = true;
            }
            // right
            if(keypress.Key == ConsoleKey.D & index < vials.Count-1)
            {
                index++;
                updateScreen = true;
            }
            // select/move liquid
            if(keypress.Key == ConsoleKey.Spacebar | keypress.Key == ConsoleKey.W)
            {
                // select
                if(selected == -1)
                {
                    selected = index;
                }
                // move
                else
                {
                    if(selected == index)
                    {
                        Console.WriteLine("Cannot Move To Self");
                    }
                    else
                    {
                        vials[selected].MoveTopToVial(vials[index]);
                    }
                    selected = -1;
                }
                updateScreen = true;

            }
            if(keypress.Key != 0)
            {
                // Console.WriteLine(keypress.KeyChar);
            }
            // TODO: this
            // undo
            if(keypress.Key == ConsoleKey.Q)
            {

            }
            // redo
            if(keypress.Key == ConsoleKey.E)
            {

            }
            // reset
            if(keypress.Key == ConsoleKey.R)
            {
                Console.WriteLine("RESET");
                
            }
            // Console.WriteLine(keypress.Key);

            if(updateScreen)
            {
                DrawScreen();
            }
            updateScreen = false;
        }

        void DrawScreen()
        {
            if(ansi)
            {
                Console.Clear();
                Ansi.DrawVials(index,selected);
            }
            if(graphics)
            {
                // TODO:
            }
        }
    }
   
}

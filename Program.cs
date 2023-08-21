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
        bool keypressed = false;
        while (true)
        {

            keypress = Console.ReadKey(true);
            // if(Console.KeyAvailable)
            // {
            // }
            // else{
            //     keypress = new ConsoleKeyInfo() {};
            // }
            if(keypress.Key == ConsoleKey.Escape)
            {
                break;
            }
            // left
            if(keypress.Key == ConsoleKey.A | keypress.Key == ConsoleKey.LeftArrow)
            {
                if(index > 0)
                {
                    index--;
                    updateScreen = true;
                }
                keypressed = true;
            }
            // right
            if(keypress.Key == ConsoleKey.D | keypress.Key == ConsoleKey.RightArrow)
            {
                if(index < vials.Count-1)
                {
                    index++;
                    updateScreen = true;
                }
                keypressed = true;
            }
             // TODO: this
            // undo
            if(keypress.Key == ConsoleKey.Q)
            {
                keypressed = true;
            }
            // redo
            if(keypress.Key == ConsoleKey.E)
            {
                keypressed = true;
            }
            // reset
            if(keypress.Key == ConsoleKey.R)
            {
                Console.WriteLine("RESET");
                keypressed = true;   
            }
            // select/move liquid
            if(!keypressed)
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
            // reset keypressed
            keypressed = false;
           
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

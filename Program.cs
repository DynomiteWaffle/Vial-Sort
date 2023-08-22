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
        // menu loop
        ConsoleKey prevButton = (ConsoleKey)0;
        int activeOption = 0;
        List<int> options = new List<int> {};
        // 0 amount vials
        options.Add(8);
        // 1 hidden vials
        options.Add(0);
        // 2 vials length
        options.Add(5);
        // initDraw
        DrawScreen(0);
        while (true)
        {

            int buttonPressed = 0;
            if(Console.KeyAvailable)
            {
                keypress = Console.ReadKey(true);
            }
            else
            {
                keypress = new ConsoleKeyInfo {};
            }
            if(keypress.Key == ConsoleKey.RightArrow)
            {
                // right
                buttonPressed = 1;
            }
            if(keypress.Key == ConsoleKey.LeftArrow)
            {
                // left
                buttonPressed = 2;
            }
            if(keypress.Key == ConsoleKey.UpArrow & prevButton != ConsoleKey.UpArrow)
            {
                // up
                if(activeOption < 1){activeOption++;}
                activeOption--;
                buttonPressed = 3;
            }
            if(keypress.Key == ConsoleKey.DownArrow & prevButton != ConsoleKey.DownArrow)
            {
                // down
                if(activeOption > options.Count-2){activeOption--;}
                activeOption++;
                buttonPressed = 4;
            }
            // 
            if(keypress.Key == ConsoleKey.Escape)
            {
                // exit
                // buttonPressed = 5;
                System.Environment.Exit(0);
            }
            if(keypress.Key == ConsoleKey.Enter)
            {
                // start
                break;
            }
            // prevbutton
            prevButton = keypress.Key;

            // settings change
            switch(activeOption)
            {
                case(0):
                // vials
                    // right
                    if(buttonPressed == 1)
                    {
                        options[activeOption]++;
                    }
                    // left
                    if(buttonPressed == 2)
                    {
                        if(options[activeOption] < 2+1)
                        {
                            options[activeOption]++;
                        }
                        options[activeOption]--;
                    }
                    break;
                case(1):
                // hidden vials
                    // right
                    if(buttonPressed == 1)
                    {
                        options[activeOption] = 1;
                    }
                    // left
                    if(buttonPressed == 2)
                    {
                        options[activeOption] = 0;
                    }
                    break;
                 case(2):
                // vials length
                   // right
                    if(buttonPressed == 1)
                    {
                        options[activeOption]++;
                    }
                    // left
                    if(buttonPressed == 2)
                    {
                        if(options[activeOption] < 2+1)
                        {
                            options[activeOption]++;
                        }
                        options[activeOption]--;
                    }
                    break;
            }
            if(buttonPressed != 0)
            {
                DrawScreen(0);
            }

        }
        // destroy menu objects(dont know how)

        // TODO:
        // game loop
        // initDraw
        DrawScreen(1);
        bool keypressed = false;
        while (true)
        {
            // is blocking - but thats ok for now
            keypress = Console.ReadKey(true);

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
            // TODO: check if game won

            if(updateScreen)
            {
                DrawScreen(1);
            }
            updateScreen = false;
        }

        void DrawScreen(int state)
        {
            if(ansi)
            {
                Console.Clear();
                switch(state)
                {
                    case(0):
                    // menu
                        Console.WriteLine("Vial Game Setup - Press Enter To Start");
                        for(int i =0;i<= options.Count;i++)
                        {
                            if(activeOption == i)
                            {
                                Console.Write("> ");
                            }
                            else
                            {
                                Console.Write("  ");
                            }
                            switch(i)
                            {
                                case(0):
                                // vials
                                    Console.WriteLine("Vials: "+options[i]);
                                    break;
                                case(1):
                                // hidden
                                    Console.WriteLine("VialsHidden: "+Convert.ToBoolean(options[i]));
                                    break;
                                case(2):
                                // length
                                    Console.WriteLine("VialsLength: "+options[i]);
                                    

                                    break;
                            }
                        }
                        break;
                    case(1):
                    // game
                        Ansi.DrawVials(index,selected);
                        break;
                }
            }
            if(graphics)
            {
                // TODO:
            }
        }
    }
   
}

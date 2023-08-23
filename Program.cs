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
        // temperary
        GameState curentGame = new GameState(new GameObjects.Vial[]{}, 0,-1);        

        ConsoleKeyInfo keypress;

        // Render Init
        bool updateScreen = true;
        Render.Ansi Ansi = Ansi = new Render.Ansi(ConsoleColor.White);
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

         // init game
        // temp
        List<GameObjects.Vial> vials = new List<GameObjects.Vial> {};
        var vialA = new GameObjects.Vial(new int[] {-1,-1,-1,-1},true);
        var vialC = new GameObjects.Vial(new int[] {-1,-1,-1,-1},true);
        var vialB = new GameObjects.Vial(new int[] {-1,1,2,1},true);
        vials.Add(vialA);
        vials.Add(vialC);
        vials.Add(vialB);
        // gen vials

        curentGame = new GameState(vials.ToArray(),curentGame.index,curentGame.selected);
        // reset value
        // var resetGame = (GameState)curentGame.Clone();
        // saves
        int savesLocation = 0;
        List<GameState> saves = new List<GameState> {(GameState)curentGame.Clone()};
       
        // TODO:
        // game loop
        // initDraw
        DrawScreen(1);
        bool keypressed = false;
        bool newsave = false;
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
                if(curentGame.index > 0)
                {
                    curentGame.index--;
                    updateScreen = true;
                }
                keypressed = true;
                // newsave = true;
            }
            // right
            if(keypress.Key == ConsoleKey.D | keypress.Key == ConsoleKey.RightArrow)
            {
                if(curentGame.index < curentGame.vials.Length-1)
                {
                    curentGame.index++;
                    updateScreen = true;
                }
                keypressed = true;
                // newsave = true;
            }
             // TODO: this
            // undo
            if(keypress.Key == ConsoleKey.Q)
            {
                savesLocation--;
                if(savesLocation<0){savesLocation=0;}
                curentGame = (GameState)saves[savesLocation].Clone();
                keypressed = true;
                updateScreen = true;
            }
            // redo
            if(keypress.Key == ConsoleKey.E)
            {
                savesLocation++;
                if(savesLocation>saves.Count-1){savesLocation=saves.Count-1;}
                curentGame = (GameState)saves[savesLocation].Clone();
                keypressed = true;
                updateScreen = true;
            }
            // reset
            if(keypress.Key == ConsoleKey.R)
            {
                Console.WriteLine("RESET");
                keypressed = true;   
                curentGame = (GameState) saves[0].Clone();
                saves.RemoveRange(1,saves.Count-1);
                updateScreen = true;
            }
            // select/move liquid
            if(!keypressed)
            {
                // select
                if(curentGame.selected == -1)
                {
                    curentGame.selected = curentGame.index;
                }
                // move
                else
                {
                    if(curentGame.selected == curentGame.index)
                    {
                        Console.WriteLine("Cannot Move To Self");
                    }
                    else
                    {
                        curentGame.vials[curentGame.selected].MoveTopToVial(curentGame.vials[curentGame.index]);
                        // create backup
                        newsave = true;
                    }
                    curentGame.selected = -1;
                }
                updateScreen = true;

            }
            if(newsave)
            {
                saves.RemoveRange(savesLocation+1,saves.Count-savesLocation-1);
                saves.Add((GameState)curentGame.Clone());
                savesLocation++;
            }
            // reset keypressed
            keypressed = false;
            newsave = false;
           
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
                    // menu
                    case(0):
                        Console.WriteLine("Vial Game Setup - Press Enter To Start");
                        for(int i =0;i< options.Count;i++)
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
                    // game
                    case(1):
                        Ansi.DrawVials(curentGame.vials,curentGame.index,curentGame.selected);
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
class GameState : ICloneable
{
    public GameObjects.Vial[] vials;
    public int index;
    public int selected;
    public GameState(GameObjects.Vial[] Vials,int Index,int Selected)
    {
        this.vials = Vials;
        this.index = Index;
        this.selected = Selected;
    }


    public object Clone()
    {
        var temp = (GameState) this.MemberwiseClone();
        List<GameObjects.Vial> V = new List<GameObjects.Vial> {};
        foreach(GameObjects.Vial v in this.vials)
        {
            V.Add((GameObjects.Vial)v.Clone());
        }
        temp.vials = V.ToArray();
        return temp;
    }
}
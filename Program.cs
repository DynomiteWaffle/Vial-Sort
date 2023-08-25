using System.Text.Json;
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
        // Default Colors
            List<GameObjects.Color> DefaultColors = new List<GameObjects.Color> {};
            DefaultColors.Add(new GameObjects.Color(0,0,0));
            DefaultColors.Add(new GameObjects.Color(255,255,255));
            // colors
            DefaultColors.Add(new GameObjects.Color(255,0,0));
            DefaultColors.Add(new GameObjects.Color(0,255,0));
            DefaultColors.Add(new GameObjects.Color(0,0,255));

            DefaultColors.Add(new GameObjects.Color(255,0,255));
            DefaultColors.Add(new GameObjects.Color(0,255,255));
            DefaultColors.Add(new GameObjects.Color(255,255,0));


            DefaultColors.Add(new GameObjects.Color(255,122,0));
            DefaultColors.Add(new GameObjects.Color(128,0,255));
            DefaultColors.Add(new GameObjects.Color(0,255,134));

            DefaultColors.Add(new GameObjects.Color(255,134,255));
            DefaultColors.Add(new GameObjects.Color(0,58,0));
            DefaultColors.Add(new GameObjects.Color(83,43,13));

            DefaultColors.Add(new GameObjects.Color(175,255,0));
            DefaultColors.Add(new GameObjects.Color(155,74,16));
            DefaultColors.Add(new GameObjects.Color(80,72,255));

            DefaultColors.Add(new GameObjects.Color(171,137,0));
        // load colors
        List<GameObjects.Color> colors = new List<GameObjects.Color> {};
        FileStream colorsfs;
        System.Xml.Serialization.XmlSerializer colorsxml = new System.Xml.Serialization.XmlSerializer(typeof(List<GameObjects.Color>));
    
        // Console.WriteLine(File.Exists("Colors.xml"));
        if(!File.Exists("Colors.xml"))
        {
            colorsfs = File.Create("Colors.xml");
            colorsxml.Serialize(colorsfs,DefaultColors);
            colorsfs.Close();

        }
        try
        {
            colorsfs = File.OpenRead("Colors.xml");

            colors = (List<GameObjects.Color>) colorsxml.Deserialize(colorsfs);
            if(colors == null)
            {
                colors = DefaultColors;
            }

            colorsfs.Close();
        }
        catch
        {
            Console.WriteLine("ERROR: Could Not Read Colors.xml - Try Deleteing It");
            System.Environment.Exit(1);
        }
        // GameObjects.Colors colors = new GameObjects.Colors(new GameObjects.Color(0,0,0),new GameObjects.Color(255,255,255));
        // colors.AddColor(new GameObjects.Color(255,0,0));
        // colors.AddColor(new GameObjects.Color(0,255,0));
        // colors.AddColor(new GameObjects.Color(0,0,255));

        // Render Init
        bool updateScreen = true;
        Render.Ansi Ansi = Ansi = new Render.Ansi(colors);
        // graphics
        // menu loop
        ConsoleKey prevButton = (ConsoleKey)0;
        int activeOption = 0;
        int optionsAmount = 3;
        // List<int> options = new List<int> {};
        // System.Xml.XmlDocument Settings = new System.Xml.XmlDocument();
        Settings settings = new Settings();
        System.Xml.Serialization.XmlSerializer settingsxml = new System.Xml.Serialization.XmlSerializer(typeof(Settings));
        FileStream settingsfs;
        if(!File.Exists("Settings.xml"))
        {
            settingsfs = new FileStream("Settings.xml",FileMode.CreateNew);
            settingsxml.Serialize(settingsfs,settings);
            settingsfs.Close();


        }
        else
        {
            try
            {
                settingsfs = new FileStream("Settings.xml",FileMode.Open);
                settings = (Settings) settingsxml.Deserialize(settingsfs);
                settingsfs.Close();

            }
            catch
            {
                File.Delete("Settings.xml");
                settingsfs = new FileStream("Settings.xml",FileMode.CreateNew);
                settingsxml.Serialize(settingsfs,settings);
                settingsfs.Close();
            }
        }
        if(settings == null)
        {
            settings = new Settings();
        }

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
                if(activeOption > optionsAmount-2){activeOption--;}
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
                        if(settings.Vials > colors.Count-3)
                        {
                            settings.Vials--;
                        }
                        settings.Vials++;
                    }
                    // left
                    if(buttonPressed == 2)
                    {
                        if(settings.Vials < 2+1)
                        {
                            settings.Vials++;
                        }
                        settings.Vials--;
                    }
                    break;
                case(1):
                // hidden vials
                    // right
                    if(buttonPressed == 1)
                    {
                        settings.HiddenLiquids = true;
                    }
                    // left
                    if(buttonPressed == 2)
                    {
                        settings.HiddenLiquids = false;
                    }
                    break;
                 case(2):
                // vials length
                   // right
                    if(buttonPressed == 1)
                    {
                        settings.VialsLength++;
                    }
                    // left
                    if(buttonPressed == 2)
                    {
                        if(settings.VialsLength < 2+1)
                        {
                            settings.VialsLength++;
                        }
                        settings.VialsLength--;
                    }
                    break;
            }
            if(buttonPressed != 0)
            {
                DrawScreen(0);
            }

        }
        // Settings.Save("Settings.xml");
        settingsfs = new FileStream("Settings.xml",FileMode.Create);
        settingsxml.Serialize(settingsfs,settings);
        settingsfs.Close();
        // destroy menu objects(dont know how)


        // Game timer
        System.Diagnostics.Stopwatch Timer = new System.Diagnostics.Stopwatch();

         // init game
        //  random
         string seed = string.Empty;
         seed+=DateTime.Now.Year;
         if(DateTime.Now.Month < 10){seed+="0";}
         seed+=DateTime.Now.Month;
         if(DateTime.Now.Day < 10){seed+="0";}
         seed+=DateTime.Now.Day;
         var rnd = new Random(Convert.ToInt32(seed));
        // temp
        List<GameObjects.Vial> vials = new List<GameObjects.Vial> {};
        vials.Add( new GameObjects.Vial(new int[] {2+2,1+2,1+2,4+2},true));
        vials.Add( new GameObjects.Vial(new int[] {1+2,3+2,2+2,4+2},true));
        vials.Add( new GameObjects.Vial(new int[] {3+2,4+2,2+2,3+2},true));
        vials.Add( new GameObjects.Vial(new int[] {4+2,3+2,1+2,2+2},true));
        vials.Add( new GameObjects.Vial(new int[] {0,0,0,0},true));
        vials.Add( new GameObjects.Vial(new int[] {0,0,0,0},true));
        // gen vials
        // 0 vials count
        // 1 hidden vials
        // 2 vials lenght

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
        bool gameWon = false;
        Timer.Start();
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
                savesLocation = 0;
                saves.RemoveRange(1,saves.Count-1);
                updateScreen = true;
                Timer.Restart();
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
                Console.WriteLine("SAVES COUNT "+saves.Count);
                Console.WriteLine("SAVES loc "+savesLocation);
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
                // check if game won
                bool check = true;
                foreach(GameObjects.Vial v in curentGame.vials)
                {
                    if(!v.isSolid())
                    {
                        check = false;
                        break;
                    }
                }
                // check win
                if(check)
                {
                    // game won
                    Timer.Stop();
                    Console.WriteLine("Time: "+Timer.ElapsedMilliseconds/1000f+"s");
                    break;

                }

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
                        for(int i =0;i < optionsAmount;i++)
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
                                    Console.WriteLine("Vials: "+settings.Vials);
                                    break;
                                case(1):
                                // hidden
                                    Console.WriteLine("VialsHidden: "+settings.HiddenLiquids);
                                    break;
                                case(2):
                                // length
                                    Console.WriteLine("VialsLength: "+settings.VialsLength);
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
public class Settings
{
    public int Vials = 5;
    public int VialsLength = 5;
    public bool HiddenLiquids = false;
    public Settings(){}
}
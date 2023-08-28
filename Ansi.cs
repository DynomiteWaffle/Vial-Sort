namespace Render;

class Ansi
{
    int emptyColor = 0;
    int unknownColor = 1;
    string background = "";
    string foreground = "";

    List<List<int>> grid;

    List<VialSort.GameObjects.Color> colors;
    public Ansi(List<VialSort.GameObjects.Color> Colors)
    {
        // init grid
        this.grid = new List<List<int>> {};
        // colors
        this.colors = Colors;
        this.foreground = "\x1b[38;2;"+this.colors[(int)VialSort.GameObjects.ColorLocations.empty].red+";"+this.colors[(int)VialSort.GameObjects.ColorLocations.empty].green+";"+this.colors[(int)VialSort.GameObjects.ColorLocations.empty].blue+"m";
        this.background = "\x1b[48;2;"+this.colors[(int)VialSort.GameObjects.ColorLocations.empty].red+";"+this.colors[(int)VialSort.GameObjects.ColorLocations.empty].green+";"+this.colors[(int)VialSort.GameObjects.ColorLocations.empty].blue+"m";
    }
    
    // TODO:BUG: doesn't draw correctly
    public void DrawVials(VialSort.GameObjects.Vial[] Vials,int index,int selected)
    {

        if(selected >= 0)
        {
            if(selected == index)
            {
                GoTo(selected*2+1,0);
                DrawPixel(1,1);
            }
            else
            {
                GoTo(selected*2+1,0);
                DrawPixel(0,1);

                GoTo(index*2+1,0);
                DrawPixel(1,0);
            }

        }
        else
        {
            GoTo(index*2+1,0);
            DrawPixel(1,0);
        }
        // background
        // │



        int cur = 0;
        int yoff = 1;
        foreach(VialSort.GameObjects.Vial v in Vials)
        {
            foreach(int i in Enumerable.Range(0,(int)Math.Ceiling(v.GetLenght()/2f)))
            {

                // background
                GoTo(cur*2,i+yoff);
                Console.Write("│");

                GoTo(cur*2+1,i+yoff);
                DrawPixel(v.GetPos(i*2),v.GetPos(i*2+1));
                // Console.Write(v.GetPos(i));
                // GoTo(cur*2,i+yoff+v.GetLenght());
                // SetColor(0,v.GetPos(i));
                Console.Write("│");
                // Console.ResetColor();
                
            }
            cur ++;
            
        }
        Console.WriteLine("");
        foreach(int _ in Enumerable.Range(0,Vials.Count()*2+1)){Console.Write("▔");}
        Console.WriteLine("");
        
        // empty grid
        // this.grid = new List<List<int>> {};
        // // init grid
        // foreach(int i in Enumerable.Range(0,Vials[0].GetLenght()))
        // {
        //     grid.Add(new List<int> {});
        //     foreach(var v in Vials)
        //     {
        //         grid[grid.Count-1].Add(v.GetPos(i));
        //     }
        // }

        // // print grid
        // Console.WriteLine("");
        // foreach(List<int> g in grid){foreach(int r in g){Console.Write("|"+r);}Console.WriteLine("|");}


    }
    public void DrawPixel(int topColor,int bottomColor)
    {
        // pixel
        if(topColor != emptyColor)
        {
            Console.Write("\x1b[38;2;"+this.colors[topColor].red+";"+this.colors[topColor].green+";"+this.colors[topColor].blue+"m"); // topcolor
        }
        else
        {
            Console.ForegroundColor = (ConsoleColor)0; //color of background
        }
        if(bottomColor != emptyColor)
        {
            Console.Write("\x1b[48;2;"+this.colors[bottomColor].red+";"+this.colors[bottomColor].green+";"+this.colors[bottomColor].blue+"m"); // bottomcolor
        }
        else
        {
            // Console.BackgroundColor = (ConsoleColor)0; //color of background
        }
        Console.Write("▀");
        Console.Write("\x1b[0m"); // resetcolors
    }
    public void GoTo(int x,int y)
    {
        Console.CursorTop = y;
        Console.CursorLeft = x;
        // Console.Write("\x1b["+x+";"+y);
    }
}
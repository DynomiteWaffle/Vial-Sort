namespace Render;

class Ansi
{
    int emptyColor = 0;
    int unknownColor = 1;

    List<VialSort.GameObjects.Color> colors;
    public Ansi(List<VialSort.GameObjects.Color> Colors)
    {
        // colors
        this.colors = Colors;
    }
    
    public void DrawVials(VialSort.GameObjects.Vial[] Vials,int index,int selected)
    {
        // draw index
        // 🭭 🭯 │
        int offset = 1;
        int newIndex = index*3+offset;
        int newSelected = selected*3+offset;
        for(int i=0;i<Vials.Length*3;i++)
        {
            if(i==newIndex)
            {
                Console.Write("🭭");
            }
            else if(i == newSelected)
            {
                Console.Write("🭯");

            }
            else
            {
                Console.Write(' ');
            }
        }
        Console.WriteLine();

        for(int l=0;l<=Math.Ceiling(Vials[0].GetLenght()/2f);l+=2)
        {
            // Console.WriteLine(l);
            foreach(VialSort.GameObjects.Vial v in Vials)
            {
                Console.Write("│");
                // colors
                int top = v.GetPos(l);
                int bot = v.GetPos(l+1);
                DrawPixel(top,bot);
                Console.Write("│");
            }
            Console.WriteLine();
        }
        // bottoms of vials
        // ▔
        for(int i=0;i<Vials.Length*3;i++)
        {
            Console.Write("▔");
        }
        Console.WriteLine();
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
            Console.BackgroundColor = (ConsoleColor)0; //color of background
        }
        Console.Write("▀");
        Console.Write("\x1b[0m"); // resetcolors
    }
}
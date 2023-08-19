namespace Render;

class Ansi
{
    VialSort.GameObjects.Vial[] Vials;
    int Index;
    int Selected;

    int emptyColor = (int)ConsoleColor.Black;
    int unknownColor;
    public Ansi(VialSort.GameObjects.Vial[] Vials,ref int index,ref int selected,ConsoleColor unknownColor)
    {
        this.Vials = Vials;
        this.Index = index;
        this.Selected = selected;
        // colors
        this.unknownColor = (int)unknownColor;
    }
    
    public void DrawVials()
    {
        // draw index
        // 🭭 🭯 │
        int offset = 1;
        int newIndex = this.Index*3+offset;
        int newSelected = this.Selected*3+offset;
        for(int i=0;i<this.Vials.Length*3;i++)
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

        for(int l=0;l<=Math.Ceiling(this.Vials[0].GetLenght()/2f);l+=2)
        {
            // Console.WriteLine(l);
            foreach(VialSort.GameObjects.Vial v in this.Vials)
            {
                Console.Write("│");
                // colors
                int top = v.GetPos(l);
                int bot = v.GetPos(l+1);
                if(top == (int)VialSort.GameObjects.ColorLocations.empty){top = this.emptyColor;}
                if(top == (int)VialSort.GameObjects.ColorLocations.unknown){top = this.unknownColor;}

                if(bot == (int)VialSort.GameObjects.ColorLocations.empty){bot = this.emptyColor;}
                if(bot == (int)VialSort.GameObjects.ColorLocations.unknown){bot = this.unknownColor;}
                DrawPixel((ConsoleColor)top,(ConsoleColor)bot);
                Console.Write("│");
            }
            Console.WriteLine();
        }
        // bottoms of vials
        // ▔
        for(int i=0;i<this.Vials.Length*3;i++)
        {
            Console.Write("▔");
        }
        Console.WriteLine();
    }
    public void DrawPixel(int x,int y,ConsoleColor top,ConsoleColor bottom)
    {
        // pixel
        Console.ForegroundColor = top;
        Console.BackgroundColor = bottom;
        Console.Write("▀");
        Console.ResetColor();
    }
    public void DrawPixel(ConsoleColor top,ConsoleColor bottom)
    {
        // pixel
        Console.ForegroundColor = top;
        Console.BackgroundColor = bottom;
        Console.Write("▀");
        Console.ResetColor();
    }
    public void DrawPixel(ConsoleColor color,bool istop)
    {
        if(istop)
        {
            Console.ForegroundColor = color;
        }
        else{
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
        // pixel
        Console.Write("▀");
        Console.ResetColor();
    }
}
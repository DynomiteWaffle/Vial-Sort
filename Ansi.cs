namespace Render;

class Ansi
{
    int emptyColor = 0;
    int unknownColor = 1;
    List<List<int>> grid;

    List<VialSort.GameObjects.Color> colors;
    public Ansi(List<VialSort.GameObjects.Color> Colors)
    {
        // init grid
        this.grid = new List<List<int>> {};
        // colors
        this.colors = Colors;
    }
    
    // TODO:BUG: doesn't draw correctly
    public void DrawVials(VialSort.GameObjects.Vial[] Vials,int index,int selected)
    {
        int bufferWidth = Vials.Length+1;
        string[] buffer = new string[((int)Math.Ceiling(Vials[0].GetLength()/2f)+1)*bufferWidth];

        foreach(int i in Enumerable.Range(0,buffer.Length))
        {
            buffer[i] = " ";
        }

        if(selected >= 0)
        {
            if(selected == index)
            {
                // GoTo(selected*2+1,0);
                SetAt(selected,0,bufferWidth,ref buffer,DrawPixel(1,1));
            }
            else
            {
                // GoTo(selected*2+1,0);
                // DrawPixel(0,1);
                SetAt(selected,0,bufferWidth,ref buffer,DrawPixel(0,1));


                // GoTo(index*2+1,0);
                // DrawPixel(1,0);
                SetAt(index,0,bufferWidth,ref buffer,DrawPixel(1,0));

            }

        }
        else
        {
            // GoTo(index*2+1,0);
            // DrawPixel(1,0);
            SetAt(index,0,bufferWidth,ref buffer,DrawPixel(1,0));

        }
        // background
        // │



        int cur = 0;
        int yoff = 1;
        foreach(VialSort.GameObjects.Vial v in Vials)
        {
            foreach(int i in Enumerable.Range(0,(int)Math.Ceiling(v.GetLength()/2f)))
            {

                SetAt(cur,i+yoff,bufferWidth,ref buffer,DrawPixel(v.GetPos(i*2),v.GetPos(i*2+1)));
                
            }
            cur ++;
            
        }
        // draw buffer
        foreach(int i in Enumerable.Range(0,buffer.Length/bufferWidth))
        {
            if(i==0)
            {
                Console.WriteLine(" "+string.Join(" ",buffer,i*bufferWidth,bufferWidth));

            }
            else
            {
                Console.WriteLine("│"+string.Join("│",buffer,i*bufferWidth,bufferWidth));
            }


        }

        foreach(int _ in Enumerable.Range(0,Vials.Count()*2+1)){Console.Write("▔");}
        Console.WriteLine("");

    }
    public string DrawPixel(int topColor,int bottomColor)
    {
        bool isTop = false;
        if (topColor == emptyColor)
        {
            isTop = false;
        }
        else
        {
            isTop = true;
        }
        string output = string.Empty;
        // pixel
        if(isTop)
        {
            output +=("\x1b[38;2;"+this.colors[topColor].red+";"+this.colors[topColor].green+";"+this.colors[topColor].blue+"m"); // topcolor
            if(bottomColor != emptyColor)
            {
                output +=("\x1b[48;2;"+this.colors[bottomColor].red+";"+this.colors[bottomColor].green+";"+this.colors[bottomColor].blue+"m"); // bottomColor
            }
        }
        else
        {
            output +=("\x1b[38;2;"+this.colors[bottomColor].red+";"+this.colors[bottomColor].green+";"+this.colors[bottomColor].blue+"m"); // bottomColor
        }
        // add cube
        if(isTop)
        {
            output +="▀";
        }
        else if (!isTop & bottomColor == emptyColor)
        {
            output += " ";
        }
        else
        {
            output += "▄";
        }
        output +=("\x1b[0m"); // resetcolors

        return output;
    }
    public void GoTo(int x,int y)
    {
        Console.CursorTop = y;
        Console.CursorLeft = x;
        // Console.Write("\x1b["+x+";"+y);
    }
    public void SetAt(int x,int y,int bufferWidth,ref string[] buffer, string Char)
    {

        if(x+(bufferWidth*y) > buffer.Length-1)
        {
            return;
        }
        if(x+(bufferWidth*y) < 0)
        {
            return;
        }


        buffer[x+(bufferWidth*y)] = Char;
    }
}
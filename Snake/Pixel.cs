using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Pixel
    {
        public int _width;

        public Pixel(int width)
        { _width = width; }
        
        public int Width
        { get { return _width; } }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Fruit
    {
        private Rectangle _rect;
        private Random _random;
        private int _helper;
        private bool _empty;

        public Fruit(Rectangle rect) { _rect = rect; }

        public void Eat(List<Snake> snakes)
        {
            _empty = false;
            while (_empty == false)
            {
                for (int i = 0; i < snakes.Count; i++)
                {

                }
            }
        }
    }
}

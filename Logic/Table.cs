using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Table
    {
        public int _height {  get; set; }
        public int _width { get; set; }

        public Table(int height, int width)
        {
            this._height = height;
            this._width = width;
        }
    }
}

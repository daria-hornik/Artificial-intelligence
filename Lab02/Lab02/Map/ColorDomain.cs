using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02.Map
{
    public enum Color
    {
        Red,
        Green,
        Blue,
        Grey
    }

    class ColorDomain
    {
        public List<Color> Colors { get; set; }
        public ColorDomain()
        {
            Colors = new List<Color>() { Color.Blue, Color.Green, Color.Grey, Color.Red };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistogramExercise
{
    class BasicColor
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public BasicColor(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }
        public static BasicColor[] BasicColorsArray
        {
            get
            {
                return new BasicColor[11] {
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("black", new Color(0, 0, 0)) };
            }
        }
    }
}

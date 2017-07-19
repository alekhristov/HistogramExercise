using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistogramExercise
{
    class Colors
    {
        private readonly double r;
        private readonly double g;
        private readonly double b;

        public Colors(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }


    }
}

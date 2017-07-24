namespace HistogramExercise
{
    internal class Color
    {
        private double r;
        private double g;
        private double b;

        public Color(double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public double R
        {
            get { return this.r; }
            private set { this.r = value; }
        }

        public double G
        {
            get { return this.g; }
            private set { this.g = value; }
        }

        public double B
        {
            get { return this.b; }
            private set { this.b = value; }
        }
    }
}
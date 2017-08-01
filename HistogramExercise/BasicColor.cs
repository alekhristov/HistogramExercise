namespace HistogramExercise
{
    internal class BasicColor
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }

        private static BasicColor[] basicColors = new BasicColor[13] {
                new BasicColor("Black", new Color(0, 0, 0)),
                new BasicColor("White", new Color(255, 255, 255)),
                new BasicColor("Red", new Color(255, 0, 0)),
                new BasicColor("Blue", new Color(0, 0, 255)),
                new BasicColor("Yellow", new Color(255, 255, 0)),
                new BasicColor("Orange", new Color(255, 165, 0)),
                new BasicColor("Grey", new Color(128, 128, 128)),
                new BasicColor("Green", new Color(0, 128, 0)),
                new BasicColor("Purple", new Color(128, 0, 128)),
                new BasicColor("Pink", new Color(255, 192, 203)),
                new BasicColor("Sky blue", new Color(0, 191, 255)),
                new BasicColor("Dark red", new Color(139, 0, 0)),
                new BasicColor("Beige", new Color(245, 245, 220)) };


        public BasicColor(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }

        public static BasicColor[] BasicColorsArray
        {
            get
            {
                return basicColors;
            }
        }
    }
}
namespace HistogramExercise
{
    internal class BasicColor
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        private static BasicColor[] basicColors = new BasicColor[14] {
                new BasicColor("black", new Color(0, 0, 0)),
                new BasicColor("white", new Color(255, 255, 255)),
                new BasicColor("red", new Color(255, 0, 0)),
                new BasicColor("blue", new Color(0, 0, 255)),
                new BasicColor("yellow", new Color(255, 255, 0)),
                new BasicColor("orange", new Color(255, 165, 0)),
                new BasicColor("grey", new Color(128, 128, 128)),
                new BasicColor("green", new Color(0, 128, 0)),
                new BasicColor("purple", new Color(128, 0, 128)),
                new BasicColor("pink", new Color(255, 192, 203)),
                new BasicColor("dark blue", new Color(0, 0, 139)),
                new BasicColor("dark red", new Color(139, 0, 0)),
                new BasicColor("beige", new Color(245, 245, 220)),
                new BasicColor("brown", new Color(160, 82, 45)) };


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
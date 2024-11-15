namespace DemonstrateSearchFilter.Models
{
    public class Box
    {
        public long Id { get; set; }
        public string Name { get; set; } = "Cube";
        public string Color { get; set; } = "White";
        public int height { get; set; } = 1;
        public int width { get; set; } = 1;
        public int depth { get; set; } = 1;
        public int volume { get; set; } = 1;

        public Box(long id, string name, string color, int height, int width, int depth)
        {
            Id = id;
            Name = name;
            Color = color;
            this.height = height;
            this.width = width;
            this.depth = depth;
            this.volume = height*width*depth;
        }
    }

}

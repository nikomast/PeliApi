namespace PeliApi.Models
{
    public class Ball
    {
        public int XPosition { get; set; } = 300; // starting in the middle
        public int YPosition { get; set; } = 200;
        public int XVelocity { get; set; } = 10;
        public int YVelocity { get; set; } = 10;
    }
}

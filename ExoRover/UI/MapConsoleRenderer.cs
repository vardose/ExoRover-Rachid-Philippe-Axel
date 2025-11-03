namespace ExoRover.UI;

public class MapConsoleRenderer
{
    public class MapRenderer : IMapRenderer
    {
        public int RoverX { get; set; } = -1;
        public int RoverY { get; set; } = -1;

        public void Render(IMap map)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (x == RoverX && y == RoverY)
                        Console.Write("R "); // Rover
                    else
                        Console.Write(". "); // Case vide
                }
                Console.WriteLine();
            }
        }
    }
}
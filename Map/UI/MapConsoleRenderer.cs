namespace Map;

public class MapConsoleRenderer
{
    public class MapRenderer
    {
        public int RoverX { get; set; } = -1;
        public int RoverY { get; set; } = -1;

        public void Render(Map map)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (x == RoverX && y == RoverY)
                        Console.Write("R "); // Rover
                    else if (map.hasObstacle(x, y))
                        Console.Write("X "); // Obstacle (test)
                    else
                        Console.Write(". "); // Case vide
                }
                Console.WriteLine();
            }
        }
    }
}
namespace Map;

public class RandomObstacleGenerator
{
    private static readonly Random random = new Random();

    public static void GenerateObstacles(IMap map, int count)
    {
        int maxX = 10; // Selon ta map
        int maxY = 10;

        int generated = 0;

        while (generated < count)
        {
            int x = random.Next(0, maxX);
            int y = random.Next(0, maxY);

            // Si pas déjà un obstacle, on ajoute
            if (!map.hasObstacle(x, y))
            {
                map.addObstacle(new Obstacle(longitude: x, latitude: y));
                generated++;
            }
        }
    }
}
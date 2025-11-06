using Map;

namespace MissionControl;

public class MapRenderer
{
    private const  int         mapWidth      = 10;
    private const  int         mapHeight     = 10;
    private static char[,]     mapData       = new char[10, 10];
    private static bool[,]     visibilityMap = new bool[10, 10];
    public         int         RoverX { get; set; } = -1;
    public         int         RoverY { get; set; } = -1;
    public         Orientation orientation = Orientation.Nord;

    public void Render(Map.Map map)
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (map.hasObstacle(x, y) && visibilityMap[x, y])
                    mapData[x, y] = 'X';
                else if (visibilityMap[x, y])
                {
                    mapData[x, y] = '.';

                    if (RoverX == x && RoverY == y)
                    {
                        if (orientation == Orientation.Nord) mapData[x, y]  = '^';
                        if (orientation == Orientation.Est) mapData[x, y]   = '>';
                        if (orientation == Orientation.Sud) mapData[x, y]   = 'v';
                        if (orientation == Orientation.Ouest) mapData[x, y] = '<';
                    }
                }

                Console.Write(visibilityMap[x, y] ? mapData[x, y] : '?');
                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }

    public void UpdateVisibility(int playerX, int playerY, int radius = 1)
    {
        RoverX = playerX;
        RoverY = playerY;
        for (int y = playerY - radius; y <= playerY + radius; y++)
        {
            for (int x = playerX - radius; x <= playerX + radius; x++)
                visibilityMap[(x + mapWidth) % mapWidth, (y + mapHeight) % mapHeight] = true;
        }
    }
}
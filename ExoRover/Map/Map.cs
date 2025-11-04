namespace ExoRover
{
    public class Map : IMap
    {
        private readonly bool[][] obstacleMap = new bool[10][];

        public Map() => InitializeObstacleMap();

        // Création d'une carte 10x10
        private void InitializeObstacleMap()
        {
            for (int i = 0; i < obstacleMap.Length; i++)
                obstacleMap[i] = new bool[10];
        }

        // Ajout d'un obstacle
        public void addObstacle(IObstacle obstacle)
        {
            int x = obstacle.Position.Longitude;
            int y = obstacle.Position.Latitude;

            if (x < 0 || x >= obstacleMap.Length || y < 0 || y >= obstacleMap[x].Length)
                throw new ArgumentOutOfRangeException("Coordonnées hors carte");

            obstacleMap[x][y] = true;
        }

        // Vérification de la présence d'un obstacle
        public bool hasObstacle(int x, int y) =>
            (x >= 0 && x < obstacleMap.Length && y >= 0 && y < obstacleMap[x].Length)
            && obstacleMap[x][y];
    }
}
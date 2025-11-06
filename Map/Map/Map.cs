namespace Map
{
    public class Map : IMap
    {
        // Tableau d'obstacles
        public bool[][] Obstacles { get; set; } = new bool[10][];

        public Map()
        {
            for (int i = 0; i < Obstacles.Length; i++)
                Obstacles[i] = new bool[10];
        }

        // Ajout d'un obstacle
        public void addObstacle(IObstacle obstacle)
        {
            int x = obstacle.Position.Longitude;
            int y = obstacle.Position.Latitude;

            if (x < 0 || x >= Obstacles.Length || y < 0 || y >= Obstacles[x].Length)
                throw new ArgumentOutOfRangeException("Coordonnées hors carte");

            Obstacles[x][y] = true;
        }

        // Vérification de la présence d'un obstacle
        public bool hasObstacle(int x, int y) =>
            (x >= 0 && x < Obstacles.Length && y >= 0 && y < Obstacles[x].Length)
            && Obstacles[x][y];
    }
}
namespace ExoRover
{
    public class Map
    {
        #region Fields

        private string mapName;
        private bool[][] ObstacleMap = new bool[10][]; // 10 colonnes

        #endregion

        #region Properties

        public string MapName
        {
            get => mapName;
            set => mapName = value;
        }

        #endregion

        #region Constructors

        public Map()
        {
            InitializeObstacleMap();
        }

        public Map(string mapName)
        {
            this.mapName = mapName;
            InitializeObstacleMap();
        }

        private void InitializeObstacleMap()
        {
            for (int i = 0; i < ObstacleMap.Length; i++)
            {
                ObstacleMap[i] = new bool[10];  // chaque ligne = 10 colonnes
            }
        }

        #endregion

        #region Methods

        public void AddObstacleMap(Obstacle obstacle)
        {
            int x = obstacle.Positions.Longitude;
            int y = obstacle.Positions.Latitude;

            if (x < 0 || x >= ObstacleMap.Length || y < 0 || y >= ObstacleMap[x].Length)
                throw new ArgumentOutOfRangeException("Les coordonnées de l'obstacle sont en dehors de la carte.");

            ObstacleMap[x][y] = true;
        }

        public bool HasObstacle(int x, int y)
        {
            if (x < 0 || x >= ObstacleMap.Length || y < 0 || y >= ObstacleMap[x].Length)
                return false;

            return ObstacleMap[x][y];
        }

        #endregion
    }
}
namespace ExoRover
{
    public class Position
    {
        public int Longitude { get; set; }
        public int Latitude { get; set; }

        public Position(int longitude = 0, int latitude = 0)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
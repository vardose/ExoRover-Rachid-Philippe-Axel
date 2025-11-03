namespace ExoRover;

public class Obstacle : IObstacle
{
    public Position Position { get; private set; }

    public Obstacle(int latitude, int longitude)
    {
        Position = new Position { Latitude = latitude, Longitude = longitude };
    }
}
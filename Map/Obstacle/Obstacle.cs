namespace Map;

public class Obstacle
{
    public Position Position { get; }

    public Obstacle(int latitude, int longitude)
    {
        Position = new Position { Latitude = latitude, Longitude = longitude };
    }
}
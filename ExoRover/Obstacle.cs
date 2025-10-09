namespace ExoRover;

public class Obstacle
{
    #region Fields

    private int longitude;
    private int latitude;
    private int altitude;

    #endregion

    #region Properties

    public int Longitude
    {
        get => longitude;
        set => longitude = value;
    }

    public int Latitude
    {
        get => latitude;
        set => latitude = value;
    }

    public int Altitude
    {
        get => altitude;
        set => altitude = value;
    }

    #endregion

    #region Constructors

    public Obstacle()
    {
    }

    public Obstacle(int longitude, int latitude)
    {
    }

    public Obstacle(int longitude, int latitude, int altitude)
    {
    }

    #endregion
}
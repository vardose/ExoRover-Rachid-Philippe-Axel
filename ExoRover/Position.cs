namespace ExoRover;

public class Position
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

    public Position()
    {
    }

    public Position(int longitude, int latitude)
    {
    }

    public Position(int longitude, int latitude, int altitude)
    {
    }

    #endregion
}
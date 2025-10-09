namespace ExoRover;

public class Movement
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

    public Movement()
    {
    }

    public Movement(int longitude, int latitude)
    {
    }

    public Movement(int longitude, int latitude, int altitude)
    {
    }

    #endregion
}
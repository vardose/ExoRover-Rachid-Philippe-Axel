namespace ExoRover;

public class Position
{
    #region Fields

    private int longitude;
    private int latitude;

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

    #endregion

    #region Constructors
    
    public Position(int longitude = 0, int latitude = 0)
    {
        this.longitude = longitude;
        this.latitude  = latitude;
    }

    #endregion
}
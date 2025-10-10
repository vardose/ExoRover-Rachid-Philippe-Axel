namespace ExoRover;

public class Obstacle
{
    #region Fields

    private Position position = new Position();

    #endregion

    #region Properties

    public  Position Positions
    {
        get => position;
        set => position = value;
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
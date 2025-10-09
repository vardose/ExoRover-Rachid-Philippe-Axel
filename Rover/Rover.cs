using ExoRover;

namespace Rover;

public class Rover
{
    #region Fields

    public enum CardinalPoints
    {
        North,
        South,
        East,
        West
    }

    private Position position;
    private bool isObstacle;
    private CardinalPoints orientation;

    #endregion

    #region Properties

    public Position Position
    {
        get => position;
        set => position = value;
    }

    public bool IsObstacle
    {
        get => isObstacle;
        set => isObstacle = value;
    }

    public CardinalPoints Orientation
    {
        get => orientation;
        set => orientation = value;
    }

    #endregion

    #region Constructors

    public Rover(Position position, bool isObstacle, CardinalPoints orientation)
    {

    }

    #endregion
}
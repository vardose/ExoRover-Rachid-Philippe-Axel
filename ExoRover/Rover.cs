namespace ExoRover;

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

    // récupération du fichier config
    private readonly Config _config;

    public Rover(Config config)
    {
        _config = config;
    }

    // Connection au reseau
    public void Initialize(Command instruction)
    {
        Console.WriteLine("=== Rover ===");
        Console.WriteLine($"Connexion à {_config.Communication.Host}:{_config.Communication.RoverPort}");
        Console.WriteLine($"Position initiale: {string.Join(",", _config.RoverSettings.InitialPosition)}");
        Console.WriteLine($"Orientation initiale: {_config.RoverSettings.Orientation}");
        Console.WriteLine($"Instruction recue: {instruction}");
    }

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
namespace ExoRover;

public class Command
{
    private readonly string _representation;

    public static Command Avancer        = new("A");
    public static Command Reculer        = new("R");
    public static Command TournerAGauche = new("G");
    public static Command TournerADroite = new("D");

    private Command(string representation)
    {
        _representation = representation;
    }

    public static Command operator +(Command a, Command b)
        => new(a._representation + b._representation);

    public override string ToString() => _representation;
}

public record Point(int X, int Y);

public class Orientation
{
    private readonly int _vecteurX;
    private readonly int _vecteurY;

    public static Orientation Nord  { get; } = new(0, 1);
    public static Orientation Sud   { get; } = new(0, -1);
    public static Orientation Est   { get; } = new(1, 0);
    public static Orientation Ouest { get; } = new(0, -1);

    private Orientation(int vecteurX, int vecteurY)
    {
        _vecteurX = vecteurX;
        _vecteurY = vecteurY;
    }

    public Point Avancer(Point p) => new(p.X + _vecteurX, p.Y + _vecteurY);
    public Point Reculer(Point p) => new(p.X - _vecteurX, p.Y - _vecteurY);

    public Orientation RotationHoraire()
    {
        if (this == Nord) return Est;
        if (this == Est) return Sud;
        if (this == Sud) return Ouest;
        if (this == Ouest) return Nord;
        throw new InvalidDataException();
    }

    public Orientation RotationAntihoraire() => RotationHoraire().RotationHoraire().RotationHoraire();
}
/*
 * #region Fields

    private string receiver;
    private string initiator;
    private object command;


    #endregion

    #region Properties

    public string Receiver
    {
        get => receiver;
        set => receiver = value;
    }

    public string Initiator
    {
        get => initiator;
        set => initiator = value;
    }
    public object TypeCommand
    {
        get => command.GetType();

    }
    public object CommandTodo
    {
        get => command;
        set => command = value ?? throw new ArgumentNullException(nameof(value));
    }



    #endregion

    #region Constructors

    public Command()
    {
    }

    public Command(string receiver, string initiator, string command)
    {
        Receiver = receiver;
        Initiator = initiator;
        CommandTodo = command;
    }

    #endregion
 */
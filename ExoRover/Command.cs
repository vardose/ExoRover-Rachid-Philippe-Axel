namespace ExoRover;

public class Command
{

    private readonly string _representation;

    public static Command Avancer = new("A");
    public static Command Reculer = new("R");
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


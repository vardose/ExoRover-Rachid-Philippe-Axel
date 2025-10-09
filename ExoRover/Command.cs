namespace ExoRover;

public class Command
{
    #region Fields

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
}
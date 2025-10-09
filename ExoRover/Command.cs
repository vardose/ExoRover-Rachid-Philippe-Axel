namespace ExoRover;

public class Command
{
    #region Fields

    private string receiver;
    private string initiator;
    private string commandType;
    private string command;

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

    public string CommandType
    {
        get => commandType;
        set => commandType = value;
    }

    public string CommandTodo
    {
        get => command;
        set => command = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion

    #region Constructors

    public Command()
    {
    }

    public Command(string receiver, string initiator, string commandType, string command)
    {
    }

    #endregion
}
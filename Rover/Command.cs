namespace Rover;

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
using ExoRover;
namespace Rover;

public interface IRover
{
    /// <summary>
    /// Process the commands it receives and returns the state of the rover after that.
    /// </summary>
    /// <returns>Returns the Rover's state and position and if there's an obstacle with its position.</returns>
    public object ProcessCommands()
    {
        
    }
}
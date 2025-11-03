namespace ExoRover;

public interface IMap
{
    void addObstacle(IObstacle obstacle);
    bool hasObstacle(int x, int y);
}
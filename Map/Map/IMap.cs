namespace Map;

public interface IMap
{
    void addObstacle(Obstacle obstacle);
    bool hasObstacle(int x, int y);
}
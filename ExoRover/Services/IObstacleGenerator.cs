namespace ExoRover.Services;

public interface IObstacleGenerator
{
    void GenerateObstacles(IMap map, int count);
}
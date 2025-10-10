namespace ExoRover;

public class Map
{
    #region Fields

    private string mapName;
    bool [][] ObstacleMap = new bool [10][] ;

    #endregion

    #region Properties

    public string MapName
    {
        get => mapName;
        set => mapName = value;
    }

   
    
    
    
    #endregion

    #region Constructors

    public Map()
    {
    }

    public Map(string mapName)
    {
    }


    public void AddObstacleMap(Obstacle obstacle)
    {
        ObstacleMap[obstacle.Positions.Longitude][obstacle.Positions.Latitude] = true;

    }
    
    

    #endregion
}
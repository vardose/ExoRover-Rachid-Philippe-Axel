namespace ExoRover;

public class Map
{
    #region Fields

    private string mapName;

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

    #endregion
}
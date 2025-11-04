using System.Text.Json;

namespace Rover;

public class Config
{
    public CommunicationConfig? Communication { get; set; }
    public RoverSettingsConfig? RoverSettings { get; set; }

    public static Config Load(string path = "config.json")
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Fichier de configuration introuvable : {path}");

        string jsonString = File.ReadAllText(path);
        var    config     = JsonSerializer.Deserialize<Config>(jsonString);

        if (config is null)
            throw new Exception("Impossible de désérialiser la configuration.");

        return config;
    }
}

public class CommunicationConfig
{
    public string Host               { get; set; }
    public int    MissionControlPort { get; set; }
    public int    RoverPort          { get; set; }
}

public class RoverSettingsConfig
{
    public string    Orientation        { get; set; }
    public List<int> InitialPosition    { get; set; }
    public bool      isObstacleDetected { get; set; }
}
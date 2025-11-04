using ExoRover;

namespace MissionControl;

class Program
{
    static void Main(string[] args)
    {
        Config config = Config.Load("config.json");
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        MissionControl missionControl = new MissionControl(config);
        missionControl.Run();
    }
}
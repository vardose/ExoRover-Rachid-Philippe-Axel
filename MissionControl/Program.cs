using Rover;

namespace MissionControl;

class Program
{
    static void Main(string[] args)
    {
        Config config = Config.Load();
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        MissionControl missionControl = new MissionControl(config);
        missionControl.Run();
    }
}
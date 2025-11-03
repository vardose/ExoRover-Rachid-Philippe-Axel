namespace ExoRover;

class Program
{
    static void Main(string[] args)
    {
        Config config = Config.Load("config.json");

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        if (args.Length == 0)
        {
            Console.WriteLine("Usage : ExoRover --mode=control OU --mode=rover");
            return;
        }

        string mode = args[0].ToLower();

        if (mode == "--mode=control")
        {
            MissionControl missionControl = new MissionControl(config);
            missionControl.Run();
        }
        else if (mode == "--mode=rover")
        {
            Rover rover = new Rover(config);
            rover.Run();
        }
        else
        {
            Console.WriteLine("Argument inconnu. Utilise --mode=control ou --mode=rover");
        }
    }
}
namespace Rover;

class Program
{
    static void Main(string[] args)
    {
        Config config = Config.Load("config.json");
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Rover rover = new Rover(config);
        rover.Run();
    }
}
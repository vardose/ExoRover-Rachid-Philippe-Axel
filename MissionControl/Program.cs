// See https://aka.ms/new-console-template for more information

using ExoRover;

static void Main()
{
    var missionControl = new MissionControl.MissionControl();

    Console.WriteLine("Entrez une suite de commandes (A, R, G, D) :");
    string input = Console.ReadLine() ?? "";

    try
    {
        Command command = missionControl.ParseUserInput(input);
        Console.WriteLine($"Commande interprétée : {command}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine("Erreur : " + ex.Message);
    }
}

// Execution du programme
Main();
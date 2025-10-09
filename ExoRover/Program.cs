using ExoRover;

static void Main()
{
    var missionControl = new MissionControl();

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


// bool exit = false;
//
// MissionControlClass missionControl = new MissionControlClass();
//
// while (true)
// {
//     if (exit)
//         break;
//     
//     Console.WriteLine("Please enter a number : \n 1 : move up \n 2 : move down \n 3 : move left \n 4 : move right \n 5 : exit");
//     string userInput = Console.ReadLine() ?? string.Empty;
//     Console.Clear();
//     switch (userInput)
//     {
//         case "1":
//             Console.WriteLine("Moving up...");
//             
//             break;
//         case "2":
//             Console.WriteLine("Moving down...");
//             // missionControl.NouvelleCommande(new Command("rover", "missionControl", "move down"));
//             break;
//         case "3":
//             Console.WriteLine("Moving left...");
//             break;
//         case "4":
//             Console.WriteLine("Moving right...");
//             break;
//         case "5":
//             exit = true;
//             break;
//         default:
//             Console.WriteLine("Please don't be dumb...");
//             break;
//     }
// }
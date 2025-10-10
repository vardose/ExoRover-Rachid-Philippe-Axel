using ExoRover;
using System.Threading;

    // Récupération du fichier config
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

using System;

namespace ExoRover
{
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
}

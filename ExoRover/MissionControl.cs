// namespace ExoRover;
//
// public class MissionControl
// {
//     public Command ParseUserInput(string input)
//     {
//         // V�rification que la commande ne soit pas vide
//         if (string.IsNullOrWhiteSpace(input))
//         {
//             throw new ArgumentException("La commande ne peut pas �tre vide.");
//         }
//
//         // Commande nulle au d�but
//         Command? result = null;
//
//         // Pour chaque lettre de la commande, on y associe la commande correspondante
//         foreach (char c in input.ToUpper())
//         {
//
//             Command next = c switch
//             {
//                 'A' => Command.Avancer,
//                 'R' => Command.Reculer,
//                 'G' => Command.TournerAGauche,
//                 'D' => Command.TournerADroite,
//                 _ => throw new ArgumentException($"Commande invalide: {input[0]}")
//             };
//
//             // Incrementation de la commande
//             result = result is null ? next : result + next;
//         }
//
//         return result!;
//     }
// }

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ExoRover
{
    public class MissionControl
    {
        // récupération du fichier config
        private readonly Config _config;

        public MissionControl(Config config)
        {
            _config = config;
        }

        // Connection au reseau
        public TcpListener Start()
        {
            Console.WriteLine("=== Mission Control ===");
            Console.WriteLine($"Connexion à {_config.Communication.Host}:{_config.Communication.MissionControlPort}");
            TcpListener server = new TcpListener(IPAddress.Parse(_config.Communication.Host), _config.Communication.MissionControlPort);
            server.Start();
            return server;
        }

        // Connection au reseau

        public void Run()
        {
            TcpListener server = Start();

            Console.WriteLine("🛰️  Mission Control en attente du rover...");

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("🤖 Rover connecté !");
            NetworkStream stream = client.GetStream();

            while (true)
            {
                Console.Write(
                    "\nCommande à envoyer (ex: A : Avancer, R : Reculer, G : Tourner à gauche, D : Tourner à droite, E : Exit): ");
                string command = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(command))
                    continue;
                if (command.Equals("E"))
                    break;

                foreach (char c in command.ToUpper())
                {
                    if (!new List<char> { 'A', 'R', 'G', 'D' }.Contains(c))
                    {
                        Console.WriteLine($"La commande {c} n'est pas prise en compte et a donc été sautée.");
                        command = command.Replace(c.ToString(), string.Empty);
                    }
                }


                byte[] data = Encoding.UTF8.GetBytes(command);
                stream.Write(data, 0, data.Length);

                byte[] buffer    = new byte[1024];
                int    bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response  = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"[Rover] {response}");
            }
        }
    }
}
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Rover;
using Map;

namespace MissionControl
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
            
            // Création de la carte et génération aléatoire des obstacles
            Map.Map            map       = new Map.Map();
            IObstacleGenerator generator = new RandomObstacleGenerator();
            generator.GenerateObstacles(map, 15);

            // Création du renderer
            MapConsoleRenderer.MapRenderer renderer = new MapConsoleRenderer.MapRenderer();

            // Position initiale du rover (supposons que tu l'as depuis Config)
            int roverX = 0; // ou _config.RoverSettings.InitialPosition[0]
            int roverY = 0; // ou _config.RoverSettings.InitialPosition[1]
            renderer.RoverX = roverX;
            renderer.RoverY = roverY;

            // Affichage initial de la carte
            Console.WriteLine("\nCarte initiale :");
            renderer.Render(map);
            
            // Sérialisation de la carte en JSON
            string mapJson = JsonSerializer.Serialize(map);
            byte[] bytes = Encoding.UTF8.GetBytes(mapJson);
            
            // Envoi de la taille du message en premier (4 octets)
            byte[] lengthPrefix = BitConverter.GetBytes(bytes.Length);
            stream.Write(lengthPrefix, 0, lengthPrefix.Length);

            // Puis envoi des données
            stream.Write(bytes, 0, bytes.Length);

            while (true)
            {
                // Lecture des instructions de l'utilisateur
                Console.Write(
                    "\nCommande à envoyer (ex: A : Avancer, R : Reculer, G : Tourner à gauche, D : Tourner à droite, E : Exit): ");
                string? command = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(command))
                    continue;
                if (command.Equals("E"))
                    break;

                foreach (char c in command.ToUpper())
                {
                    if (new List<char> { 'A', 'R', 'G', 'D' }.Contains(c)) continue;
                    Console.WriteLine($"La commande {c} n'est pas prise en compte et a donc été sautée.");
                    command = command.Replace(c.ToString(), string.Empty);
                }


                // Traitement du retour du rover
                byte[] data = Encoding.UTF8.GetBytes(command);
                stream.Write(data, 0, data.Length);

                byte[] buffer    = new byte[1024];
                int    bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response  = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"[Rover] {response}");
                
                // Recherche la dernière occurrence de "(" et "," pour prendre la vraie position du rover
                int start = response.LastIndexOf('(');
                int comma = response.LastIndexOf(',');

                if (start >= 0 && comma > start)
                {
                    int end = response.IndexOf(')', comma);
                    if (end > comma &&
                        int.TryParse(response.Substring(start + 1, comma - start - 1), out int x) &&
                        int.TryParse(response.Substring(comma + 1, end - comma - 1), out int y))
                    {
                        // Gestion de la sortie de carte pour l'affichage du rover
                        if (x < 0)
                        {
                            renderer.RoverX = 9;
                        }
                        else
                        {
                            renderer.RoverX = x;
                        }

                        if (y < 0)
                        {
                            renderer.RoverY = 9; 
                        }
                        else
                        {
                            renderer.RoverY = y; 
                        }
                    }
                }

                // Affichage de la carte mise à jour
                Console.WriteLine("\nCarte mise à jour :");
                renderer.Render(map);
            }
        }
    }
}
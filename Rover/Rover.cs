using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Linq;
using Map;

namespace Rover;

public class Rover
{
    private readonly Config      _config;
    private          Position    position    = new Position();
    private          Orientation orientation = Orientation.Nord;
    private Map.Map _map;

    public void Run()
    {
        try
        {
            TcpClient client = Initialize();
            Console.WriteLine("🚗 Rover connecté à Mission Control !");
            NetworkStream stream = client.GetStream();
            
            // Lecture de la taille du message (4 octets)
            byte[] lengthPrefix = new byte[4];
            stream.Read(lengthPrefix, 0, 4);
            int length = BitConverter.ToInt32(lengthPrefix, 0);

            // Lecture des données JSON
            byte[] bytes = new byte[length];
            int offset = 0;
            while (offset < length)
            {
                offset += stream.Read(bytes, offset, length - offset);
            }

            string mapJson = Encoding.UTF8.GetString(bytes);
            _map = JsonSerializer.Deserialize<Map.Map>(mapJson);

            while (true)
            {
                // Réception et traitement des instructions
                byte[] buffer          = new byte[1024];
                int    bytesRead       = stream.Read(buffer, 0, buffer.Length);
                string commandReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string response        = string.Empty;

                foreach (char c in commandReceived.ToUpper())
                {
                    Command next = c switch
                    {
                        'A' => Command.Avancer,
                        'R' => Command.Reculer,
                        'G' => Command.TournerAGauche,
                        'D' => Command.TournerADroite,
                        _   => throw new ArgumentException($"Commande invalide: {c}")
                    };

                    response = ExecuteCommand(next);
                }

                Console.WriteLine(
                    $"Position du Rover : \nLongitude : {position.Longitude} \nLatitude : {position.Latitude}");


                byte[] data = Encoding.UTF8.GetBytes(response);
                stream.Write(data, 0, data.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur: {ex.Message}");
        }
    }

    private string ExecuteCommand(Command command)
    {
        Position next = new Position(position.Longitude, position.Latitude);

        // ---- ROTATIONS ----
        if (command.Equals(Command.TournerAGauche))
        {
            orientation = orientation.RotationAntihoraire();
            return $"✅ Position actuelle : ({position.Longitude}, {position.Latitude}, {orientation})";
        }
        
        

        if (command.Equals(Command.TournerADroite))
        {
            orientation = orientation.RotationHoraire();
            return $"✅ Position actuelle : ({position.Longitude}, {position.Latitude}, {orientation})";
        }

        // ---- CALCUL DU DEPLACEMENT ----
        if (command.Equals(Command.Avancer))
            next = orientation.Avancer(next);
        else if (command.Equals(Command.Reculer))
            next = orientation.Reculer(next);

        // ---- DETECTION D’OBSTACLE ----
        if (_map.hasObstacle(next.Longitude, next.Latitude))
        {
            // Position non mis a jour en cas d'obstacle detecté
            return $"⛔ OBSTACLE détecté en ({next.Longitude},{next.Latitude}) — rover arrêté en ({position.Longitude},{position.Latitude})";
        }
        
        position.Longitude = next.Longitude;
        position.Latitude = next.Latitude;

        return $"✅ Position actuelle : ({position.Longitude}, {position.Latitude}, {orientation})";
    }





    // r�cup�ration du fichier config

    public Rover(Config config)
    {
        _config = config;
    }

    // Connection au reseau
    private TcpClient Initialize()
    {
        Console.WriteLine("=== Rover ===");
        Console.WriteLine($"Connexion � {_config.Communication.Host}:{_config.Communication.RoverPort}");
        Console.WriteLine($"Position initiale: {string.Join(",", _config.RoverSettings.InitialPosition)}");
        Console.WriteLine($"Orientation initiale: {_config.RoverSettings.Orientation}");

        return new TcpClient(_config.Communication.Host, _config.Communication.RoverPort);
    }
}
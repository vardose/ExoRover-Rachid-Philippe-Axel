using System.Net.Sockets;
using System.Text;

namespace ExoRover;

public class Rover
{
    private readonly Config _config;
    Position                position    = new Position();
    Orientation             orientation = Orientation.Nord;

    public void Run()
    {
        try
        {
            TcpClient client = Initialize();
            Console.WriteLine("🚗 Rover connecté à Mission Control !");
            NetworkStream stream = client.GetStream();

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
        Point p = new Point(position.Longitude, position.Latitude);
        switch (command.ToString())
        {
            case "A": p           = orientation.Avancer(p); break;
            case "R": p           = orientation.Reculer(p); break;
            case "G": orientation = orientation.RotationAntihoraire(); break;
            case "D": orientation = orientation.RotationHoraire(); break;
        }

        position.Longitude = p.X;
        position.Latitude  = p.Y;

        return $"✅ Position actuelle : ({position.Longitude}, {position.Latitude}, {orientation})";
    }

    // r�cup�ration du fichier config

    public Rover(Config config)
    {
        _config = config;
    }

    // Connection au reseau
    public TcpClient Initialize()
    {
        Console.WriteLine("=== Rover ===");
        Console.WriteLine($"Connexion � {_config.Communication.Host}:{_config.Communication.RoverPort}");
        Console.WriteLine($"Position initiale: {string.Join(",", _config.RoverSettings.InitialPosition)}");
        Console.WriteLine($"Orientation initiale: {_config.RoverSettings.Orientation}");
        
        return new TcpClient(_config.Communication.Host, _config.Communication.RoverPort);
    }
}
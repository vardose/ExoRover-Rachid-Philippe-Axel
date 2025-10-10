// namespace ExoRover;
//
// public class Rover
// {
//     #region Fields
//
//     public enum CardinalPoints
//     {
//         North,
//         South,
//         East,
//         West
//     }
//
//     private Position position;
//     private bool isObstacle;
//     private CardinalPoints orientation;
//
//     #endregion
//
//     #region Properties
//
//     public Position Position
//     {
//         get => position;
//         set => position = value;
//     }
//
//     public bool IsObstacle
//     {
//         get => isObstacle;
//         set => isObstacle = value;
//     }
//
//     public CardinalPoints Orientation
//     {
//         get => orientation;
//         set => orientation = value;
//     }
//
//     #endregion
//
//     #region Constructors
//
//     public Rover(Position position, bool isObstacle, CardinalPoints orientation)
//     {
//
//     }
//
//     #endregion
//     
//     #region Methods
//
//     public void Subscribe(MissionControlClass missionControl)
//     {
//         missionControl.CommandeTransmit += OnMissonControlCommandRecieve;
//
//     }
//
//     private void OnMissonControlCommandRecieve(object sender, Command command)
//     {
//         Console.WriteLine(command);
//     }
//     #endregion
// }

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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

        return $"✅ Position actuelle : ({position.Longitude}, {position.Latitude})";
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
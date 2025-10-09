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

namespace ExoRover
{
    public static class Rover
    {
        static int    x   = 0;
        static int    y   = 0;
        static Random rnd = new Random();

        public static void Run()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 5000);
                Console.WriteLine("🚗 Rover connecté à Mission Control !");
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] buffer    = new byte[1024];
                    int    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string command   = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine($"Commande reçue : {command}");
                    Thread.Sleep(500); // Simule un petit délai

                    string response = ExecuteCommand(command);
                    byte[] data     = Encoding.UTF8.GetBytes(response);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
        }

        private static string ExecuteCommand(string command)
        {
            switch (command.ToUpper())
            {
                case "MOVE NORTH": y--; break;
                case "MOVE SOUTH": y++; break;
                case "MOVE EAST":  x++; break;
                case "MOVE WEST":  x--; break;
            }

            bool obstacle = rnd.Next(0, 10) < 2; // 20% de chance d’obstacle
            if (obstacle)
                return $"⚠️  Obstacle détecté à ({x}, {y}) !";

            return $"✅ Position actuelle : ({x}, {y})";
        }
    }
}

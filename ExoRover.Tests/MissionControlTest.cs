using Xunit;
using Rover;
using System.Net;
using System.Text.Json;

namespace ExoRover.Tests
{
    public class MissionControlTest : IDisposable
    {
        private readonly string _testConfigPath = "test_mission_control_config.json";
        private          Config _testConfig;

        public MissionControlTest()
        {
            // Créer une configuration de test
            var configData = new
            {
                Communication = new
                {
                    Host               = "127.0.0.1",
                    MissionControlPort = 8080,
                    RoverPort          = 8081
                },
                RoverSettings = new
                {
                    Orientation        = "Nord",
                    InitialPosition    = new[] { 0, 0 },
                    isObstacleDetected = false
                }
            };

            var json = JsonSerializer.Serialize(configData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_testConfigPath, json);
            _testConfig = Config.Load(_testConfigPath);
        }

        public void Dispose()
        {
            if (File.Exists(_testConfigPath))
            {
                File.Delete(_testConfigPath);
            }
        }

        [Fact]
        public void MissionControl_Constructor_ShouldInitializeCorrectly()
        {
            // Act
            var missionControl = new MissionControl.MissionControl(_testConfig);

            // Assert
            Assert.NotNull(missionControl);
        }

        [Fact]
        public void MissionControl_Start_ShouldCreateTcpListener()
        {
            // Arrange
            var missionControl = new MissionControl.MissionControl(_testConfig);

            // Act
            var server = missionControl.Start();

            // Assert
            Assert.NotNull(server);
            Assert.True(server.Server.IsBound);

            // Cleanup
            server.Stop();
        }

        [Fact]
        public void MissionControl_Start_ShouldListenOnCorrectEndpoint()
        {
            // Arrange
            var missionControl = new MissionControl.MissionControl(_testConfig);

            // Act
            var server        = missionControl.Start();
            var localEndPoint = server.LocalEndpoint as IPEndPoint;

            // Assert
            Assert.NotNull(localEndPoint);
            Assert.Equal(IPAddress.Parse(_testConfig.Communication.Host), localEndPoint.Address);
            Assert.Equal(_testConfig.Communication.MissionControlPort, localEndPoint.Port);

            // Cleanup
            server.Stop();
        }

        [Fact]
        public void MissionControl_Constructor_WithNullConfig_ShouldThrowException()
        {
            // Act & Assert
            Assert.ThrowsAny<Exception>(() => new MissionControl.MissionControl(null));
        }

        [Fact]
        public void MissionControl_Start_WithInvalidPort_ShouldThrowException()
        {
            // Arrange
            var invalidConfig = new Config
            {
                Communication = new CommunicationConfig
                {
                    Host               = "127.0.0.1",
                    MissionControlPort = -1 // Port invalide
                }
            };
            var missionControl = new MissionControl.MissionControl(invalidConfig);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => missionControl.Start());
        }

        [Fact]
        public void MissionControl_Start_WithInvalidHost_ShouldThrowException()
        {
            // Arrange
            var invalidConfig = new Config
            {
                Communication = new CommunicationConfig
                {
                    Host               = "invalid.host.address",
                    MissionControlPort = 8080
                }
            };
            var missionControl = new MissionControl.MissionControl(invalidConfig);

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => missionControl.Start());
        }

        // Note: Les tests pour la méthode Run() sont plus complexes car ils impliquent 
        // des interactions réseau et des boucles infinies. Ils nécessiteraient des mocks 
        // ou des intégrations tests plus sophistiquées.

        //     [Fact]  
        //     public void MissionControle_Should_Fail_Parse_Commande()
        //     {
        //      
        //         var missionControl = new MissionControl();
        //         var input = "zadfgh";
        //
        //        
        //         Xunit.Assert.ThrowsAny<Exception>(() => missionControl.ParseUserInput(input));
        //     }
        //
        //     [Fact]  
        //     public void MissionControle_Should_Succeed_Parse_Commande()
        //     {
        //         // Arrange
        //         var missionControl = new MissionControl();
        //         var input = "GDRA";
        //
        //         var expected = Command.TournerAGauche
        //                        + Command.TournerADroite
        //                        + Command.Reculer
        //                        + Command.Avancer;
        //
        //         
        //         var result = missionControl.ParseUserInput(input);
        //
        //         
        //         Xunit.Assert.Equal(expected.ToString(), result.ToString());
        //     }
    }
}
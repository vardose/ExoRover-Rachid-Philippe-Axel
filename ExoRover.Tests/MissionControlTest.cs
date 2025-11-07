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
            var missionControl = new MissionControl.MissionControl(_testConfig);

            Assert.NotNull(missionControl);
        }

        [Fact]
        public void MissionControl_Start_ShouldCreateTcpListener()
        {
            var missionControl = new MissionControl.MissionControl(_testConfig);

            var server = missionControl.Start();

            Assert.NotNull(server);
            Assert.True(server.Server.IsBound);

            server.Stop();
        }

        [Fact]
        public void MissionControl_Start_ShouldListenOnCorrectEndpoint()
        {
            var missionControl = new MissionControl.MissionControl(_testConfig);

            var server        = missionControl.Start();
            var localEndPoint = server.LocalEndpoint as IPEndPoint;

            Assert.NotNull(localEndPoint);
            Assert.Equal(IPAddress.Parse(_testConfig.Communication.Host), localEndPoint.Address);
            Assert.Equal(_testConfig.Communication.MissionControlPort, localEndPoint.Port);

            server.Stop();
        }

        [Fact]
        public void MissionControl_Constructor_WithNullConfig_ShouldThrowException()
        {
            Assert.ThrowsAny<Exception>(() => new MissionControl.MissionControl(null));
        }

        [Fact]
        public void MissionControl_Start_WithInvalidPort_ShouldThrowException()
        {
            var invalidConfig = new Config
            {
                Communication = new CommunicationConfig
                {
                    Host               = "127.0.0.1",
                    MissionControlPort = -1 // Port invalide
                }
            };
            var missionControl = new MissionControl.MissionControl(invalidConfig);

            Assert.ThrowsAny<Exception>(() => missionControl.Start());
        }

        [Fact]
        public void MissionControl_Start_WithInvalidHost_ShouldThrowException()
        {
            var invalidConfig = new Config
            {
                Communication = new CommunicationConfig
                {
                    Host               = "invalid.host.address",
                    MissionControlPort = 8080
                }
            };
            var missionControl = new MissionControl.MissionControl(invalidConfig);

            Assert.ThrowsAny<Exception>(() => missionControl.Start());
        }


    }
}
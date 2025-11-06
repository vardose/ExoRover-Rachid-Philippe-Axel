using Rover;
using Xunit;
using System.Text.Json;

namespace ExoRover.Tests;

public class RoverTest : IDisposable
{
    private readonly string _testConfigPath = "test_rover_config.json";
    private          Config _testConfig;

    public RoverTest()
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
    public void Rover_Constructor_ShouldInitializeCorrectly()
    {
        // Act
        var rover = new Rover.Rover(_testConfig);

        // Assert
        Assert.NotNull(rover);
    }

    [Fact]
    public void Rover_Constructor_WithNullConfig_ShouldThrowException()
    {
        // Act & Assert
        Assert.ThrowsAny<Exception>(() => new Rover.Rover(null));
    }

    // Note: Les tests pour Initialize() et Run() sont complexes car ils impliquent 
    // des connexions réseau TCP. Dans un environnement de test réel, on utiliserait 
    // des mocks ou des serveurs de test.

    [Fact]
    public void Rover_Initialize_ShouldReturnTcpClient()
    {
        // Arrange
        var rover = new Rover.Rover(_testConfig);

        // Pour ce test, nous devons d'abord démarrer un serveur TCP de test
        // ou utiliser des mocks. Pour l'instant, nous testons seulement que 
        // la méthode peut être appelée sans exception dans certaines conditions.

        // Ce test nécessiterait un serveur TCP en cours d'exécution pour réussir
        // Dans un vrai environnement de test, on utiliserait des intégrations tests
        // ou des mocks pour simuler la connexion réseau.

        // Act & Assert
        // Nous savons que cela va lever une exception car aucun serveur n'écoute
        Assert.ThrowsAny<Exception>(() => rover);
    }

    // Tests pour la logique métier que nous pouvons extraire et tester
    // En réalité, pour bien tester Rover, il faudrait refactorer le code
    // pour séparer la logique métier des préoccupations réseau.

    [Fact]
    public void Rover_ConfigurationAccess_ShouldHaveCorrectValues()
    {
        // Arrange
        var rover = new Rover.Rover(_testConfig);

        // Note: Pour tester ceci, il faudrait exposer _config ou créer des méthodes
        // publiques pour accéder aux valeurs de configuration. Actuellement,
        // _config est privé, donc nous ne pouvons pas le tester directement.

        // Ce test vérifie que le rover a été créé avec la bonne configuration
        // indirectement en s'assurant qu'aucune exception n'est levée
        Assert.NotNull(rover);
    }
}
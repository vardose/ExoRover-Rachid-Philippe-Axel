using ExoRover;
using Xunit;
using System;
using System.IO;
using System.Reflection;

namespace ExoRover.Tests;

public class ProgramTest
{
    [Fact]
    public void Program_Main_WithNoArguments_ShouldDisplayUsage()
    {
        // Arrange
        string[] args = new string[0];
        
        // Capture console output
        var originalOut = Console.Out;
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        try
        {
            // Act
            // Utiliser la réflexion pour appeler la méthode Main statique
            var programType = typeof(Program);
            var mainMethod = programType.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            mainMethod?.Invoke(null, new object[] { args });

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("Usage", output);
        }
        finally
        {
            // Restore original console output
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void Program_Main_WithInvalidArgument_ShouldDisplayError()
    {
        // Arrange
        string[] args = { "--mode=invalid" };
        
        // Capture console output
        var originalOut = Console.Out;
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        try
        {
            // Act
            var programType = typeof(Program);
            var mainMethod = programType.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            mainMethod?.Invoke(null, new object[] { args });

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("Argument inconnu", output);
        }
        finally
        {
            // Restore original console output
            Console.SetOut(originalOut);
        }
    }

    // Note: Les tests pour --mode=control et --mode=rover sont plus complexes
    // car ils tentent de charger un fichier de configuration et d'établir des connexions réseau.
    // Dans un environnement de test réel, il faudrait :
    // 1. Créer un fichier config.json de test
    // 2. Utiliser des mocks pour les connexions réseau
    // 3. Ou tester ces modes dans des tests d'intégration séparés

    [Fact]
    public void Program_Main_WithControlMode_WithoutConfig_ShouldThrowException()
    {
        // Arrange
        string[] args = { "--mode=control" };

        // Act & Assert
        var programType = typeof(Program);
        var mainMethod = programType.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        
        // Ceci devrait lever une exception car config.json n'existe probablement pas
        Assert.ThrowsAny<Exception>(() => mainMethod?.Invoke(null, new object[] { args }));
    }

    [Fact]
    public void Program_Main_WithRoverMode_WithoutConfig_ShouldThrowException()
    {
        // Arrange
        string[] args = { "--mode=rover" };

        // Act & Assert
        var programType = typeof(Program);
        var mainMethod = programType.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        
        // Ceci devrait lever une exception car config.json n'existe probablement pas
        Assert.ThrowsAny<Exception>(() => mainMethod?.Invoke(null, new object[] { args }));
    }

    [Fact]
    public void Program_Main_CaseInsensitive_ShouldWork()
    {
        // Arrange
        string[] args = { "--MODE=CONTROL" };
        
        // Act & Assert
        var programType = typeof(Program);
        var mainMethod = programType.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        
        // Devrait lever une exception à cause du config manquant, pas à cause de la casse
        Assert.ThrowsAny<Exception>(() => mainMethod?.Invoke(null, new object[] { args }));
    }
}

using ExoRover;
using Xunit;

namespace ExoRover.Tests;

public class OrientationTest
{
    [Fact]
    public void Orientation_StaticInstances_ShouldBeCorrect()
    {
        // Assert
        Assert.NotNull(Orientation.Nord);
        Assert.NotNull(Orientation.Sud);
        Assert.NotNull(Orientation.Est);
        Assert.NotNull(Orientation.Ouest);
    }

    [Fact]
    public void Orientation_Avancer_ShouldMoveForwardCorrectly()
    {
        // Arrange
        var startPoint = new Point(5, 5);

        // Act & Assert
        Assert.Equal(new Point(5, 4), Orientation.Nord.Avancer(startPoint));  // Nord: Y diminue
        Assert.Equal(new Point(5, 6), Orientation.Sud.Avancer(startPoint));   // Sud: Y augmente
        Assert.Equal(new Point(6, 5), Orientation.Est.Avancer(startPoint));   // Est: X augmente
        Assert.Equal(new Point(4, 5), Orientation.Ouest.Avancer(startPoint)); // Ouest: X diminue
    }

    [Fact]
    public void Orientation_Reculer_ShouldMoveBackwardCorrectly()
    {
        // Arrange
        var startPoint = new Point(5, 5);

        // Act & Assert
        Assert.Equal(new Point(5, 6), Orientation.Nord.Reculer(startPoint));  // Nord: Y augmente (inverse)
        Assert.Equal(new Point(5, 4), Orientation.Sud.Reculer(startPoint));   // Sud: Y diminue (inverse)
        Assert.Equal(new Point(4, 5), Orientation.Est.Reculer(startPoint));   // Est: X diminue (inverse)
        Assert.Equal(new Point(6, 5), Orientation.Ouest.Reculer(startPoint)); // Ouest: X augmente (inverse)
    }

    [Fact]
    public void Orientation_RotationHoraire_ShouldRotateClockwise()
    {
        // Act & Assert
        Assert.Equal(Orientation.Est, Orientation.Nord.RotationHoraire());
        Assert.Equal(Orientation.Sud, Orientation.Est.RotationHoraire());
        Assert.Equal(Orientation.Ouest, Orientation.Sud.RotationHoraire());
        Assert.Equal(Orientation.Nord, Orientation.Ouest.RotationHoraire());
    }

    [Fact]
    public void Orientation_RotationAntihoraire_ShouldRotateCounterClockwise()
    {
        // Act & Assert
        Assert.Equal(Orientation.Ouest, Orientation.Nord.RotationAntihoraire());
        Assert.Equal(Orientation.Nord, Orientation.Est.RotationAntihoraire());
        Assert.Equal(Orientation.Est, Orientation.Sud.RotationAntihoraire());
        Assert.Equal(Orientation.Sud, Orientation.Ouest.RotationAntihoraire());
    }

    [Fact]
    public void Orientation_FullRotationHoraire_ShouldReturnToOriginal()
    {
        // Arrange
        var original = Orientation.Nord;

        // Act
        var result = original.RotationHoraire()
                            .RotationHoraire()
                            .RotationHoraire()
                            .RotationHoraire();

        // Assert
        Assert.Equal(original, result);
    }

    [Fact]
    public void Orientation_FullRotationAntihoraire_ShouldReturnToOriginal()
    {
        // Arrange
        var original = Orientation.Est;

        // Act
        var result = original.RotationAntihoraire()
                            .RotationAntihoraire()
                            .RotationAntihoraire()
                            .RotationAntihoraire();

        // Assert
        Assert.Equal(original, result);
    }
}

using Map;
using Xunit;

namespace ExoRover.Tests;

public class OrientationTest
{
    [Fact]
    public void Orientation_StaticInstances_ShouldBeCorrect()
    {
        Assert.NotNull(Orientation.Nord);
        Assert.NotNull(Orientation.Sud);
        Assert.NotNull(Orientation.Est);
        Assert.NotNull(Orientation.Ouest);
    }

    [Fact]
    public void Orientation_Avancer_ShouldMoveForwardCorrectly()
    {
        var startPosition = new Position(5, 5);

        var nordResult = Orientation.Nord.Avancer(startPosition);
        var sudResult = Orientation.Sud.Avancer(startPosition);
        var estResult = Orientation.Est.Avancer(startPosition);
        var ouestResult = Orientation.Ouest.Avancer(startPosition);

        Assert.Equal(5, nordResult.Longitude);
        Assert.Equal(4, nordResult.Latitude);  // Nord: Y diminue
        
        Assert.Equal(5, sudResult.Longitude);
        Assert.Equal(6, sudResult.Latitude);   // Sud: Y augmente
        
        Assert.Equal(6, estResult.Longitude);  // Est: X augmente
        Assert.Equal(5, estResult.Latitude);
        
        Assert.Equal(4, ouestResult.Longitude); // Ouest: X diminue
        Assert.Equal(5, ouestResult.Latitude);
    }

    [Fact]
    public void Orientation_Reculer_ShouldMoveBackwardCorrectly()
    {
        var startPosition = new Position(5, 5);

        var nordResult = Orientation.Nord.Reculer(startPosition);
        var sudResult = Orientation.Sud.Reculer(startPosition);
        var estResult = Orientation.Est.Reculer(startPosition);
        var ouestResult = Orientation.Ouest.Reculer(startPosition);

        Assert.Equal(5, nordResult.Longitude);
        Assert.Equal(6, nordResult.Latitude);  // Nord: Y augmente (inverse)
        
        Assert.Equal(5, sudResult.Longitude);
        Assert.Equal(4, sudResult.Latitude);   // Sud: Y diminue (inverse)
        
        Assert.Equal(4, estResult.Longitude);  // Est: X diminue (inverse)
        Assert.Equal(5, estResult.Latitude);
        
        Assert.Equal(6, ouestResult.Longitude); // Ouest: X augmente (inverse)
        Assert.Equal(5, ouestResult.Latitude);
    }

    [Fact]
    public void Orientation_RotationHoraire_ShouldRotateClockwise()
    {
        Assert.Equal(Orientation.Est, Orientation.Nord.RotationHoraire());
        Assert.Equal(Orientation.Sud, Orientation.Est.RotationHoraire());
        Assert.Equal(Orientation.Ouest, Orientation.Sud.RotationHoraire());
        Assert.Equal(Orientation.Nord, Orientation.Ouest.RotationHoraire());
    }

    [Fact]
    public void Orientation_RotationAntihoraire_ShouldRotateCounterClockwise()
    {
        Assert.Equal(Orientation.Ouest, Orientation.Nord.RotationAntihoraire());
        Assert.Equal(Orientation.Nord, Orientation.Est.RotationAntihoraire());
        Assert.Equal(Orientation.Est, Orientation.Sud.RotationAntihoraire());
        Assert.Equal(Orientation.Sud, Orientation.Ouest.RotationAntihoraire());
    }

    [Fact]
    public void Orientation_FullRotationHoraire_ShouldReturnToOriginal()
    {
        var original = Orientation.Nord;

        var result = original.RotationHoraire()
            .RotationHoraire()
            .RotationHoraire()
            .RotationHoraire();

        Assert.Equal(original, result);
    }

    [Fact]
    public void Orientation_FullRotationAntihoraire_ShouldReturnToOriginal()
    {
        var original = Orientation.Est;

        var result = original.RotationAntihoraire()
            .RotationAntihoraire()
            .RotationAntihoraire()
            .RotationAntihoraire();

        Assert.Equal(original, result);
    }

    [Fact]
    public void Orientation_ToString_ShouldReturnCorrectRepresentation()
    {
        Assert.Equal("Nord", Orientation.Nord.ToString());
        Assert.Equal("Sud", Orientation.Sud.ToString());
        Assert.Equal("Est", Orientation.Est.ToString());
        Assert.Equal("Ouest", Orientation.Ouest.ToString());
    }

    [Fact]
    public void Orientation_WrapAroundMovement_ShouldWorkCorrectly()
    {
        var edgePosition = new Position(0, 0);

        var ouestResult = Orientation.Ouest.Avancer(edgePosition);
        Assert.Equal(9, ouestResult.Longitude);
        Assert.Equal(0, ouestResult.Latitude);

        var nordResult = Orientation.Nord.Avancer(edgePosition);
        Assert.Equal(0, nordResult.Longitude);
        Assert.Equal(9, nordResult.Latitude);
    }
}
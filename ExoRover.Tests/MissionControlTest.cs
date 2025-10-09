using Xunit;
using ExoRover;

namespace ExoRover.Tests
{
    public class MissionControlTest
    {
        [Fact]  // ✅ Marque le test comme unitaire
        public void MissionControle_Should_Fail_Parse_Commande()
        {
            // Arrange
            var missionControl = new MissionControl();
            var input = "zadfgh";

            // Act & Assert
            Xunit.Assert.ThrowsAny<Exception>(() => missionControl.ParseUserInput(input));
        }

        [Fact]  // ✅ Important pour que le runner détecte le test
        public void MissionControle_Should_Succeed_Parse_Commande()
        {
            // Arrange
            var missionControl = new MissionControl();
            var input = "GDRA";

            var expected = Command.TournerAGauche
                           + Command.TournerADroite
                           + Command.Reculer
                           + Command.Avancer;

            // Act
            var result = missionControl.ParseUserInput(input);

            // Assert
            Xunit.Assert.Equal(expected.ToString(), result.ToString());
        }
    }
}
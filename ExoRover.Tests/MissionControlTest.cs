using Xunit;
using ExoRover;

namespace ExoRover.Tests
{
    public class MissionControlTest
    {
        [Fact]  
        public void MissionControle_Should_Fail_Parse_Commande()
        {
         
            var missionControl = new MissionControl();
            var input = "zadfgh";

           
            Xunit.Assert.ThrowsAny<Exception>(() => missionControl.ParseUserInput(input));
        }

        [Fact]  
        public void MissionControle_Should_Succeed_Parse_Commande()
        {
            // Arrange
            var missionControl = new MissionControl();
            var input = "GDRA";

            var expected = Command.TournerAGauche
                           + Command.TournerADroite
                           + Command.Reculer
                           + Command.Avancer;

            
            var result = missionControl.ParseUserInput(input);

            
            Xunit.Assert.Equal(expected.ToString(), result.ToString());
        }
    }
}
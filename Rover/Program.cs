using ExoRover;

bool        exit  = false;
Rover.Rover rover = new Rover.Rover(new Position(1, 1), false, Rover.Rover.CardinalPoints.North);
MissionControl.MissionControlClass missionControl = new MissionControl.MissionControlClass();
rover.Subscribe(missionControl);

while (true)
{
    if (exit)
        break;
    
    Console.WriteLine($"test");

    if (Console.ReadLine() == "q")
        exit = true;
}
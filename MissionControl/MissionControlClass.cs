namespace MissionControl;
using ExoRover;

public class MissionControlClass
{
    public delegate void CommandeTransmitEventHandler(object sender, Command command);
    public event CommandeTransmitEventHandler CommandeTransmit;

    protected virtual void OnCommandeTransmit(Command command)
    {
        CommandeTransmit?.Invoke(this, command);
    }



    public void NouvelleCommande(Command commande)
    {
        OnCommandeTransmit(commande);
        
    }
    
    
    
    
   
    
    
}
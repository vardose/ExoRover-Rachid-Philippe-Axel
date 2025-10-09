namespace ExoRover;

public class MissionControlClass
{
    public delegate void CommandeTransmitEventHandler(object sender, Command command);
    public event CommandeTransmitEventHandler CommandeTransmit;

    protected virtual void OnCommandeTransmit(Command command)
    {
        CommandeTransmit?.Invoke(this, command);
        // Console.WriteLine($"Initiateur de commande : {command.Initiator} \nDestinataire de la commande : {command.Receiver} \nCommande : {command.CommandTodo} \nType de la commande {command.TypeCommand}");
    }

    public void NouvelleCommande(Command commande)
    {
        OnCommandeTransmit(commande);
        
    }
}
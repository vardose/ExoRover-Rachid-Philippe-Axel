namespace ExoRover;

public class MissionControl
{
    // récupération du fichier config
    private readonly Config _config;

    public MissionControl(Config config)
    {
        _config = config;
    }

    // Connection au reseau
    public void Start()
    {
        Console.WriteLine("=== Mission Control ===");
        Console.WriteLine($"Connexion à {_config.Communication.Host}:{_config.Communication.MissionControlPort}");
        // Ici, tu pourrais démarrer un serveur TCP ou une boucle d'écoute
    }

    public Command ParseUserInput(string input)
    {
        // Connection au reseau
        Start();

        // V�rification que la commande ne soit pas vide
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("La commande ne peut pas �tre vide.");
        }

        // Commande nulle au d�but
        Command? result = null;

        // Pour chaque lettre de la commande, on y associe la commande correspondante
        foreach (char c in input.ToUpper())
        {

            Command next = c switch
            {
                'A' => Command.Avancer,
                'R' => Command.Reculer,
                'G' => Command.TournerAGauche,
                'D' => Command.TournerADroite,
                _ => throw new ArgumentException($"Commande invalide: {input}")
            };

            // Incrementation de la commande
            result = result is null ? next : result + next;
        }

        return result!;
    }
}
using ExoRover;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace MissionControl;

public class MissionControl
{
    public Command ParseUserInput(string input)
    {
        // Vérification que la commande ne soit pas vide
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("La commande ne peut pas être vide.");
        }

        // Commande nulle au début
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
                _ => throw new ArgumentException($"Commande invalide: {input[0]}")
            };

            // Incrementation de la commande
            result = result is null ? next : result + next;
        }

        return result!;
    }
}
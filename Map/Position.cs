namespace Map;

public class Position
{
    public int Longitude { get; set; }
    public int Latitude  { get; set; }

    public Position(int longitude = 5, int latitude = 5)
    {
        Longitude = longitude;
        Latitude  = latitude;
    }
}

public class Orientation
{
    private readonly int _vecteurX;
    private readonly int _vecteurY;

    public static Orientation Nord  { get; } = new(0, -1);
    public static Orientation Sud   { get; } = new(0, 1);
    public static Orientation Est   { get; } = new(1, 0);
    public static Orientation Ouest { get; } = new(-1, 0);

    // Taille par défaut de la carte
    private const int MapSize = 10;

    private Orientation(int vecteurX, int vecteurY)
    {
        _vecteurX = vecteurX;
        _vecteurY = vecteurY;
    }

    public Position Avancer(Position p)
    {
        int newX = (p.Longitude + _vecteurX + MapSize) % MapSize;
        int newY = (p.Latitude  + _vecteurY + MapSize) % MapSize;
        return new Position(newX, newY);
    }

    public Position Reculer(Position p)
    {
        int newX = (p.Longitude - _vecteurX + MapSize) % MapSize;
        int newY = (p.Latitude  - _vecteurY + MapSize) % MapSize;
        return new Position(newX, newY);
    }

    public Orientation RotationHoraire()
    {
        if (this == Nord) return Est;
        if (this == Est) return Sud;
        if (this == Sud) return Ouest;
        if (this == Ouest) return Nord;

        throw new InvalidDataException();
    }

    public Orientation RotationAntihoraire() => RotationHoraire().RotationHoraire().RotationHoraire();

    // Convertion en chaine de caractères de l'orientation du rover
    public override string ToString()
    {
        if (this == Nord) return "Nord";
        if (this == Est) return "Est";
        if (this == Sud) return "Sud";
        if (this == Ouest) return "Ouest";
        return "Iconnue";
    }
}
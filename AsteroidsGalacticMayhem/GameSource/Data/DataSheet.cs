using System.Numerics;

namespace AsteroidsGalacticMayhem.GameSource.Data;

public static class DataSheet
{
    public static readonly ShipData ShipGunslinger =
        new(
            "Gunslinger", 10,
            100f, 0f, 50f,
            300, 600, 300, 400);
    
    public static readonly ShipData ShipMarauder =
        new(
            "Marauder", 11,
            100f, 0f, 50f,
            250f, 150f, 300f, 300f);
    
    public static readonly ShipData ShipDevastator =
        new(
            "Devastator", 12,
            100f, 0f, 50f,
            250f, 150f, 300f, 300f);


    public static readonly AsteroidData AsteroidSmall = new("Small", 100, 50f, 0f, 5f);
    public static readonly AsteroidData AsteroidMedium = new("Medium", 101, 100f, 0.05f, 12f);
    public static readonly AsteroidData AsteroidBig = new("Big", 102, 300f, 0.5f, 30f);
}
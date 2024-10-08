using System.Numerics;
using ShapeEngine.Core;

namespace AsteroidsDemo.GameSource.Data;

public static class DataSheet
{
    public static readonly ShipData ShipGunslinger =
        new(
            "Gunslinger", 10,
            100f, 0f, 50f, 150f,
            300, 600, 300, 400, 3f);
    
    public static readonly ShipData ShipMarauder =
        new(
            "Marauder", 11,
            100f, 0f, 50f, 200f,
            250f, 150f, 300f, 300f, 4f);
    
    public static readonly ShipData ShipDevastator =
        new(
            "Devastator", 12,
            100f, 0f, 50f, 100f,
            250f, 150f, 300f, 300f, 2f);


    public   static readonly AsteroidData Floater = 
        new(
            "Floater", 100, 
            1500, 0.5f, 100, 
            50, 0.25f, new ValueRange(15, 60), 
            new ValueRange(5, 25));
    
    
    // public static readonly AsteroidData AsteroidSmall = new("Small", 100, 50f, 0f, 5f);
    // public static readonly AsteroidData AsteroidMedium = new("Medium", 101, 100f, 0.05f, 12f);
    // public static readonly AsteroidData AsteroidBig = new("Big", 102, 300f, 0.5f, 30f);
}
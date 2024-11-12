using System.Numerics;
using AsteroidsDemo.GameSource.Entities.Asteroids;
using ShapeEngine.Core;

namespace AsteroidsDemo.GameSource.Data;

public static class DataSheet
{
    public static readonly ShipData ShipGunslinger =
        new(
            "Gunslinger", 10,
            100f, 0f, 50f, 150f,
            500, 1000, 500, 400, 1.5f);
    
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

    #region Floater
    
    public static readonly AsteroidData FloaterSmall = 
        new(
            "Floater S", 100, 
            1000, 0.25f, 30, 
            10, 1, 
            new ValueRange(25, 30), 2f);
    
    public static readonly AsteroidData FloaterMedium = 
        new(
            "Floater M", 101, 
            1500, 0.35f, 60, 
            20, 3, 
            new ValueRange(20, 25), 2f);
    
    public static readonly AsteroidData FloaterLarge = 
        new(
            "Floater L", 102, 
            2500, 0.5f, 100, 
            35, 5, 
            new ValueRange(15, 20), 2f);
    
    public static readonly AsteroidData FloaterHuge = 
        new(
            "Floater XL", 103, 
            4000, 0.75f, 150, 
            75, 10, 
            new ValueRange(5, 10), 2f);
    
    public static readonly AsteroidData FloaterSmallRich = 
        new(
            "Floater S $$$", 104, 
            2000, 0.25f, 35, 
            10, 5, 
            new ValueRange(20, 25), 2f);
    
    public static readonly AsteroidData FloaterMediumRich = 
        new(
            "Floater M $$$", 105, 
            3000, 0.35f, 70, 
            20, 15, 
            new ValueRange(15, 20), 2f);
    
    public static readonly AsteroidData FloaterLargeRich = 
        new(
            "Floater L $$$", 106, 
            5000, 0.5f, 115, 
            35, 25, 
            new ValueRange(10, 15), 2f);
    
    public static readonly AsteroidData FloaterHugeRich = 
        new(
            "Floater XL $$$", 107, 
            8000, 0.75f, 175, 
            75, 50, 
            new ValueRange(5, 10), 2f);
    
    public static readonly AsteroidData FloaterSmallBarren = 
        new(
            "Floater S $", 104, 
            1500, 0.25f, 35, 
            10, 3, 
            new ValueRange(20, 25), 2f);
    
    public static readonly AsteroidData FloaterMediumBarren = 
        new(
            "Floater M $", 105, 
            2500, 0.35f, 70, 
            20, 8, 
            new ValueRange(15, 20), 2f);
    
    public static readonly AsteroidData FloaterLargeBarren = 
        new(
            "Floater L $", 106, 
            4000, 0.5f, 115, 
            35, 12, 
            new ValueRange(10, 15), 2f);
    
    public static readonly AsteroidData FloaterHugeBarren = 
        new(
            "Floater XL $", 107, 
            6000, 0.75f, 175, 
            75, 20, 
            new ValueRange(5, 10), 2f);
    #endregion

    public static readonly int AsteroidMaxResourceAmount = FindMax();

    private static int FindMax()
    {
        var asteroids = new List<AsteroidData>()
        {
            FloaterSmall,
            FloaterMedium,
            FloaterLarge,
            FloaterHuge,

            FloaterSmallBarren,
            FloaterMediumBarren,
            FloaterLargeBarren,
            FloaterHugeBarren,

            FloaterSmallRich,
            FloaterMediumRich,
            FloaterLargeRich,
            FloaterHugeRich
        };
        
        var maxAmount = 0;
        foreach (var asteroid in asteroids)
        {
            if (asteroid.ResourceAmount > maxAmount)
            {
                maxAmount = (int)asteroid.ResourceAmount;
            }
        }
        return maxAmount;
    }
}
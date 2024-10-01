using System.Numerics;
using ShapeEngine.Core;

namespace AsteroidsGalacticMayhem.GameSource.Data;

public record AsteroidData
(
    string Name,
    int Id,
    float Health,
    float Hardshell,
    float Size,
    float Experience,
    float ResourceChance,
    ValueRange ResourceAmount,
    ValueRange Speed
);
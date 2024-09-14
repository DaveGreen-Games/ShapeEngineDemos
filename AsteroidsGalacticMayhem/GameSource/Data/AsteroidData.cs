using System.Numerics;

namespace AsteroidsGalacticMayhem.GameSource.Data;

public record AsteroidData
(
    string Name,
    int Id,
    float Health,
    float Hardshell,
    float Experience
);
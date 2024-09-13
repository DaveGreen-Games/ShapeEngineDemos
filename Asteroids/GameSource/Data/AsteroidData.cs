using System.Numerics;

namespace Asteroids.GameSource.Data;

public record AsteroidData
(
    string Name,
    int Id,
    float Health,
    float Hardshell,
    float Experience
);
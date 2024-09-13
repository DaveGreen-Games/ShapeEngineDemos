using System.Numerics;

namespace Asteroids.Game.Data;

public record AsteroidData
(
    string Name,
    int Id,
    float Health,
    float Hardshell,
    float Experience
);
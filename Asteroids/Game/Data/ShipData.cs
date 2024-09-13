using System.Numerics;

namespace Asteroids.Game.Data;

public record ShipData
(
    string Name,
    int Id,
    float Health,
    float Hardshell,
    float Size,
    float Speed,
    float Acceleration,
    float Deceleration,
    float TurningSpeed
);
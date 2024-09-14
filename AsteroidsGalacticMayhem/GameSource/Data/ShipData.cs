using System.Numerics;

namespace AsteroidsGalacticMayhem.GameSource.Data;

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
using System.Numerics;

namespace AsteroidsDemo.GameSource.Data;

public record ShipData
(
    string Name,
    int Id,
    float Health,
    float Hardshell,
    float Size,
    float CollectorSize,
    float Speed,
    float Acceleration,
    float Deceleration,
    float TurningSpeed,
    float Drag
);
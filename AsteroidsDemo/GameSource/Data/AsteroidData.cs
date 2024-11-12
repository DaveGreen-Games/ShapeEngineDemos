using System.Numerics;
using ShapeEngine.Core;

namespace AsteroidsDemo.GameSource.Data;

public record AsteroidData
(
    string Name,
    int Id,
    float Health,
    float Hardshell,
    float Size,
    float Experience,
    float ResourceAmount,
    ValueRange Speed,
    float Drag
);
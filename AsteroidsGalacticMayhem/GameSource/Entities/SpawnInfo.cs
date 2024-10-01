using System.Numerics;

namespace AsteroidsGalacticMayhem.GameSource.Entities;

public readonly struct SpawnInfo
{
    public readonly Vector2 Position;
    public readonly Vector2 Direction;

    public SpawnInfo(Vector2 position, Vector2 direction)
    {
        Position = position;
        Direction = direction;
    }
}
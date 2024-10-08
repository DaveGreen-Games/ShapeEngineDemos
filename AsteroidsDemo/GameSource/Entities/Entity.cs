using System.Numerics;
using AsteroidsDemo.GameSource.Entities.Collectibles;
using ShapeEngine.Core;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;

namespace AsteroidsDemo.GameSource.Entities;


public abstract class Entity : CollisionObject
{
    public bool Stunned { get; private set; } = false;


    public abstract void Spawn(SpawnInfo spawnInfo);

    public abstract void BoundsTouched(Intersection surface, Rect bounds);
    // public abstract override Rect GetBoundingBox();
    //
    // public abstract override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui);
    //
    // public abstract override void DrawGame(ScreenInfo game);
    //
    // public abstract override void DrawGameUI(ScreenInfo gameUi);
}
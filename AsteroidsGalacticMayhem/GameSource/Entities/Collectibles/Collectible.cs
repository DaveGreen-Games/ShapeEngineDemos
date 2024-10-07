using System.Linq.Expressions;
using System.Numerics;
using ShapeEngine.Core;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Collectibles;

public abstract class Collectible : CollisionObject // make abstract!
{

    protected float Duration = 2f;
    protected TweenType Tween = TweenType.BOUNCE_OUT;
    protected float F { get; private set; }
    private float timer = 0f;
    
    protected CircleCollider Collider;
    public Collectible(Vector2 pos, float size) : base(pos)
    {
        Transform = new Transform2D(pos, 0f, new Size(size), 1f);
        var c = new CircleCollider(new Transform2D(new(), 0f, new Size(0f), 1f));
        c.CollisionLayer = CollisionLayers.Collectible;
        c.CollisionMask = new BitFlag();
        c.CollisionMask = c.CollisionMask.Add(CollisionLayers.Ships);
        c.ComputeCollision = true;
        c.ComputeIntersections = false;
        c.OnCollision += OnCollision;
        AddCollider(c);
        Collider = c;
    }

    public override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        base.Update(time, game, gameUi, ui);
        timer += time.Delta;
        if (timer >= Duration) timer = 0f;
        F = ShapeTween.Tween(timer / Duration, Tween);
    }

    protected virtual void OnCollision(Collider collider, CollisionInformation info)
    {
        // if (info.Collisions.Count > 0)
        // {
        //     var collision = info.Collisions[0];
        //     if (collision.Other.Parent is ICollector collector)
        //     {
        //         Kill();
        //     }
        // }
    }
}
using System.Linq.Expressions;
using System.Numerics;
using ShapeEngine.Core;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Collectibles;


public abstract class Collectible : CollisionObject
{
    protected CircleCollider Collider;
    protected bool Collected { get; private set; } = false;
    protected float Amount;
    
    protected float EffectDuration = 2f;
    protected float CollectionDuration = 0.2f;
    protected TweenType EffectTweenType = TweenType.BOUNCE_OUT;
    protected TweenType CollectionTweenType = TweenType.BOUNCE_OUT;
    protected float EffectF { get; private set; }
    

    private float collectionTimer = 0f;
    private float effectTimer = 0f;
    private GameObject? collector = null;
    private Vector2 collectionPosition = Vector2.Zero;
    
    
    public Collectible(Vector2 pos, float size, float amount) : base(pos)
    {
        Amount = amount;
        
        Transform = new Transform2D(pos, 0f, new Size(size), 1f);
        var c = new CircleCollider(new Transform2D(new(), 0f, new Size(0f), 1f));
        c.CollisionLayer = CollisionLayers.Collectible;
        c.CollisionMask = new(); // new BitFlag(CollisionLayers.Ships);
        c.ComputeCollision = false;
        c.ComputeIntersections = false;
        // c.OnCollision += OnCollision;
        AddCollider(c);
        Collider = c;
    }

    public override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        base.Update(time, game, gameUi, ui);
        effectTimer += time.Delta;
        if (effectTimer >= EffectDuration) effectTimer = 0f;
        EffectF = ShapeTween.Tween(effectTimer / EffectDuration, EffectTweenType);


        if (collector != null)
        {
            if (collector.IsDead)
            {
                collector = null;
                Collected = false;
                Collider.Enabled = true;
                collectionTimer = 0f;
                // Kill();
            }
            else
            {
                if (collectionTimer > 0f)
                {
                    collectionTimer -= time.Delta;
                    if(collectionTimer <= 0f) collectionTimer = 0f;
                }
                
                var f = 1f - (collectionTimer / CollectionDuration);
                var targetPos = collector.Transform.Position;
                if (f >= 1f)
                {
                    Transform = Transform.SetPosition(targetPos);
                    collector = null;
                    OnCollectorTargetFound();
                }
                else
                {
                    var newPos = ShapeTween.Tween(collectionPosition, targetPos, f, CollectionTweenType);
                    Transform = Transform.SetPosition(newPos);
                }
            }
        }
    }

    public abstract CollectibleType GetCollectibleType();
    public float Collect(GameObject collectorTarget)
    {
        if (Collected) return -1;
        Collected = true;

        Collider.Enabled = false;
        
        collectionPosition = Transform.Position;
        
        OnCollected();

        if (CollectionDuration > 0)
        {
            collectionTimer = CollectionDuration;
            collector = collectorTarget;
        }
        else
        {
            OnCollectorTargetFound();
        }
        
        return Amount;
    }

    protected abstract void OnCollected();
    protected abstract void OnCollectorTargetFound();

    // protected virtual void OnCollision(Collider collider, CollisionInformation info)
    // {
    //     // if (info.Collisions.Count > 0)
    //     // {
    //     //     var collision = info.Collisions[0];
    //     //     if (collision.Other.Parent is ICollector collector)
    //     //     {
    //     //         Kill();
    //     //     }
    //     // }
    // }
}
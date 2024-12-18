using System.Numerics;
using AsteroidsDemo.GameSource.ColorSystem;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;
using ShapeEngine.Random;

namespace AsteroidsDemo.GameSource.Entities.Collectibles;

public class Resource : Collectible
{
    
    public Resource(Vector2 pos, float amount) : base(pos, 10, amount)
    {
        EffectTweenType = TweenType.BOUNCE_OUT;
        EffectDuration = Rng.Instance.RandF(1.75f, 2f);

    }

    public override void DrawGame(ScreenInfo game)
    {
        if (Collected)
        {
            var baseCircle = Collider.GetCircleShape();
            var newRadius = ShapeMath.LerpFloat(baseCircle.Radius * 2, baseCircle.Radius / 4, CollectionF);
            ShapeDrawing.DrawCircleFast(baseCircle.Center, newRadius, Colors.ResourceColor);
        }
        else
        {
            var baseCircle = Collider.GetCircleShape();
            var newRadius = ShapeMath.LerpFloat(baseCircle.Radius / 2, baseCircle.Radius, EffectF);
            ShapeDrawing.DrawCircleFast(baseCircle.Center, newRadius, Colors.ResourceColor);
        }
    }

    public override void DrawGameUI(ScreenInfo gameUi) { }
    public override CollectibleType GetCollectibleType() => CollectibleType.Resource;
    protected override void OnCollected()
    {
        
    }

    protected override void OnCollectorTargetFound()
    {
        Kill();
    }
}
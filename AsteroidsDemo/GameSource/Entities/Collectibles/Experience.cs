using System.Numerics;
using AsteroidsDemo.GameSource.ColorSystem;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;
using ShapeEngine.Random;

namespace AsteroidsDemo.GameSource.Entities.Collectibles;

public class Experience : Collectible
{
    public Experience(Vector2 pos, float amount) : base(pos, 6, amount)
    {
        EffectTweenType = TweenType.BOUNCE_IN;
        EffectDuration = Rng.Instance.RandF(1.25f, 1.5f);
    }

    public override void DrawGame(ScreenInfo game)
    {
        if (Collected)
        {
            var baseCircle = Collider.GetCircleShape();
            var newRadius = ShapeMath.LerpFloat(baseCircle.Radius * 2, baseCircle.Radius / 4, CollectionF);
            ShapeDrawing.DrawCircleFast(baseCircle.Center, newRadius, Colors.ExperienceColor);
        }
        else
        {
            var baseCircle = Collider.GetCircleShape();
            var newRadius = ShapeMath.LerpFloat(baseCircle.Radius / 2, baseCircle.Radius, EffectF);
            ShapeDrawing.DrawCircleFast(baseCircle.Center, newRadius, Colors.ExperienceColor);
        }
    }

    public override void DrawGameUI(ScreenInfo gameUi) { }
    public override CollectibleType GetCollectibleType() => CollectibleType.Experience;
    
    protected override void OnCollected()
    {
        
    }

    protected override void OnCollectorTargetFound()
    {
        Kill();
    }
}
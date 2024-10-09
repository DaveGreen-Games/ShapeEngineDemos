using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Collectibles;

public class Resource : Collectible
{
    
    public Resource(Vector2 pos, float amount) : base(pos, 10, amount)
    {
        EffectTweenType = TweenType.BOUNCE_OUT;
        EffectDuration = 2f;

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
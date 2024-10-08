using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Collectibles;

public class Resource : Collectible
{
    
    public Resource(Vector2 pos, float size, float amount) : base(pos, size, amount)
    {
        EffectTweenType = TweenType.BOUNCE_OUT;
        EffectDuration = 2f;

    }

    public override void DrawGame(ScreenInfo game)
    {
        float thickness = ShapeMath.LerpFloat(2f, 6f, EffectF);
        var baseCircle = Collider.GetCircleShape();
        var newRadius = ShapeMath.LerpFloat(baseCircle.Radius, baseCircle.Radius * 2, EffectF);
        var newCircle = baseCircle.SetRadius(newRadius);
        newCircle.DrawLines(thickness, Colors.ResourceColor);
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
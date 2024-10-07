using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Collectibles;

public class Resource : Collectible
{
    
    public Resource(Vector2 pos, float size) : base(pos, size)
    {
        Tween = TweenType.BOUNCE_OUT;
        Duration = 2f;

    }

    public override void DrawGame(ScreenInfo game)
    {
        float thickness = ShapeMath.LerpFloat(2f, 6f, F);
        var baseCircle = Collider.GetCircleShape();
        var newRadius = ShapeMath.LerpFloat(baseCircle.Radius, baseCircle.Radius * 2, F);
        var newCircle = baseCircle.SetRadius(newRadius);
        newCircle.DrawLines(thickness, Colors.ResourceColor);
    }

    public override void DrawGameUI(ScreenInfo gameUi) { }
}
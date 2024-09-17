using System.Linq.Expressions;
using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using ShapeEngine.Core;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Collectibles;

public class Collectible : CollisionObject // make abstract!
{

    
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
        
    }

    private void OnCollision(Collider collider, CollisionInformation info)
    {
        if (collider.Parent is ICollector collector)
        {
            Kill();
        }
    }


    public override void DrawGame(ScreenInfo game)
    {
        ShapeDrawing.DrawCircleLines(Transform.Position, Transform.ScaledSize.Radius, 2f, Colors.ResourceColor, 4f);
    }

    public override void DrawGameUI(ScreenInfo gameUi)
    {
        
    }
}
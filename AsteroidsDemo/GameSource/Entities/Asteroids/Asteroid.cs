using System.Drawing;
using System.Numerics;
using AsteroidsDemo.GameSource.ColorSystem;
using AsteroidsDemo.GameSource.Data;
using ShapeEngine.Color;
using ShapeEngine.Core.CollisionSystem;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;
using Size = ShapeEngine.Core.Structs.Size;

namespace AsteroidsDemo.GameSource.Entities.Asteroids;

public abstract class Asteroid : Entity
{
    protected AsteroidData Data;

    public Asteroid(AsteroidData data)
    {
        Data = data;
    }
}

public class Floater : Asteroid
{
    private PolyCollider collider;

    private Vector2 dir = Vector2.Zero;
    private float speed = 0f;
    
    public Floater(AsteroidData data) : base(data)
    {
        var points = Polygon.GenerateRelative(12, 0.75f, 1);
        collider = new PolyCollider(new Transform2D(new(), 0f, new Size(0f), 1f), points);
        collider.ComputeCollision = false;
        collider.ComputeIntersections = false;
        collider.CollisionLayer = CollisionLayers.Asteroids;
        AddCollider(collider);

        Drag = Data.Drag;
        // float resourceFactor = Data.ResourceAmount / (float)DataSheet.AsteroidMaxResourceAmount;
        // if (resourceFactor > 0f)
        // {
        //     var maxPoints = (int)(15 * resourceFactor);
        //     var radius = Data.Size * 0.75f;
        //     resourceDots = new(maxPoints);
        //     for (int i = 0; i < maxPoints; i++)
        //     {
        //         
        //     }
        // }
    }


    public override void Spawn(SpawnInfo spawnInfo)
    {
        var position = spawnInfo.Position;
        dir  = spawnInfo.Direction;
        Transform = new Transform2D(position, dir.AngleRad(), new Size(Data.Size), 1f);

        speed = Data.Speed.Rand();
        CurMovementSpeed = speed;
        CurMovementDirection = dir;
    }

    public override void BoundsTouched(CollisionPoint p, Rect bounds)
    { 
        dir =  CurVelocity.Normalize().Flip();
        
        Stun(0.5f, dir, CollisionForce);
    }

    protected override void UpdateMovement(float dt)
    {
        CurMovementSpeed = speed;
        CurMovementDirection = dir;
    }


    public override void DrawGame(ScreenInfo game)
    {
        var poly = collider.GetPolygonShape();
        poly.DrawLines(4f, Colors.AsteroidSpecialColor);

        float resourceFactor = Data.ResourceAmount / (float)DataSheet.AsteroidMaxResourceAmount;
        if (resourceFactor > 0f)
        {
            var scaledPoly = poly.ScaleSizeCopy(0.9f);
            scaledPoly?.DrawLines(4f, Colors.ResourceColor);
        }
    }


    public override void DrawGameUI(ScreenInfo gameUi)
    {
    }
}

/*protected override Vector2 GetMovementDirection(float dt)
    {
        return Vector2.Zero;
    }

    protected override void UpdateMovement(Vector2 movement, float dt)
    {
        CurMovementSpeed = Velocity.Length();
        CurMovementDirection = CurMovementSpeed > 0 ? Velocity / CurMovementSpeed : Vector2.Zero;
    }*/
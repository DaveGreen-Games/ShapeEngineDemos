using System.Drawing;
using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using AsteroidsGalacticMayhem.GameSource.Data;
using ShapeEngine.Color;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;
using Size = ShapeEngine.Core.Structs.Size;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Asteroids;

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
    private Vector2 curDirection = new();
    private float curSpeed = 0f;
    
    public Floater(AsteroidData data) : base(data)
    {
        var points = Polygon.GenerateRelative(12, 0.75f, 1);
        collider = new PolyCollider(new Transform2D(new(), 0f, new Size(0f), 1f), points);
        collider.ComputeCollision = false;
        collider.ComputeIntersections = false;
        collider.CollisionLayer = CollisionLayers.Asteroids;
        AddCollider(collider);
    }

    public override void Spawn(SpawnInfo spawnInfo)
    {
        var position = spawnInfo.Position;
        var direction = spawnInfo.Direction;
        Transform = new Transform2D(position, direction.AngleRad(), new Size(Data.Size), 1f);
        curDirection = direction;
        curSpeed = Data.Speed.Rand();
        
    }

    public override void BoundsTouched(Intersection intersection, Rect bounds)
    {
        var newPos = bounds.ScaleSize(0.9f, new AnchorPoint(0.5f, 0.5f)).GetRandomPointInside();
        Transform = Transform.SetPosition(bounds.Center);
    }

    public override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        base.Update(time, game, gameUi, ui);
        Transform = Transform.ChangePosition(curDirection * curSpeed * time.Delta);
        // collider.Recalculate();
    }

    public override void DrawGame(ScreenInfo game)
    {
        collider.GetPolygonShape().DrawLines(4f, Colors.AsteroidSpecialColor);
        
    }

    
    public override void DrawGameUI(ScreenInfo gameUi) { }
    
}
using System.Numerics;
using System.Text;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using AsteroidsGalacticMayhem.GameSource.Data;
using AsteroidsGalacticMayhem.GameSource.Entities;
using AsteroidsGalacticMayhem.GameSource.Entities.Asteroids;
using AsteroidsGalacticMayhem.GameSource.Entities.Collectibles;
using AsteroidsGalacticMayhem.GameSource.Entities.Ships;
using Raylib_cs;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;
using ShapeEngine.Random;
using ShapeEngine.Screen;

namespace AsteroidsGalacticMayhem.GameSource.Scenes;

public readonly struct GameData
{
    
}

public class UniverseBorder : CollisionObject
{

    private float flashTimerTop = 0f;
    private float flashTimerBottom = 0f;
    private float flashTimerLeft = 0f;
    private float flashTimerRight = 0f;
    
    private float flashDuration = 2f;

    private SegmentCollider wallTop;
    private SegmentCollider wallBottom;
    private SegmentCollider wallLeft;
    private SegmentCollider wallRight;

    private Rect bounds;
    
    public UniverseBorder(Rect bounds)
    {
        this.bounds = bounds;
        var center = bounds.Center;
        var tl = bounds.TopLeft;
        var tr = bounds.TopRight;
        var br = bounds.BottomRight;
        var bl = bounds.BottomLeft;

        var collisionMask = new BitFlag(CollisionLayers.Ships);
        collisionMask = collisionMask.Add(CollisionLayers.Asteroids);
        collisionMask = collisionMask.Add(CollisionLayers.AsteroidBullets);
        collisionMask = collisionMask.Add(CollisionLayers.ShipBullets);
        
        wallTop = new SegmentCollider(new Transform2D(tl - center), (tr - tl).Normalize(), 0f);
        wallTop.CollisionLayer = CollisionLayers.World;
        wallTop.ComputeCollision = true;
        wallTop.ComputeIntersections = false;
        wallTop.CollisionMask = collisionMask;
        wallTop.OnCollision += OnWallCollisionTop;
        
        wallBottom = new SegmentCollider(new Transform2D(bl - center), (br - bl).Normalize(), 0f);
        wallBottom.CollisionLayer = CollisionLayers.World;
        wallBottom.ComputeCollision = true;
        wallBottom.ComputeIntersections = false;
        wallBottom.CollisionMask = collisionMask;
        wallBottom.OnCollision += OnWallCollisionBottom;
        
        wallLeft = new SegmentCollider(new Transform2D(tl - center), (bl - tl).Normalize(), 0f);
        wallLeft.CollisionLayer = CollisionLayers.World;
        wallLeft.ComputeCollision = true;
        wallLeft.ComputeIntersections = false;
        wallLeft.CollisionMask = collisionMask;
        wallLeft.OnCollision += OnWallCollisionLeft;
        
        wallRight = new SegmentCollider(new Transform2D(tr - center), (br - tr).Normalize(), 0f);
        wallRight.CollisionLayer = CollisionLayers.World;
        wallRight.ComputeCollision = true;
        wallRight.ComputeIntersections = false;
        wallRight.CollisionMask = collisionMask;
        wallRight.OnCollision += OnWallCollisionRight;

        Transform = new Transform2D(center, 0f, bounds.Size, 1f);
        
        AddCollider(wallTop);
        AddCollider(wallBottom);
        AddCollider(wallRight);
        AddCollider(wallLeft);
        
        
    }

    private void OnWallCollisionTop(Collider collider, CollisionInformation info)
    {
        foreach (var col in info.Collisions)
        {
            if (col.FirstContact)
            {
                if (col.Other.Parent is Entity e)
                {
                    flashTimerTop = flashDuration;
                    e.BoundsTouched(col.Intersection, bounds);
                }
            }
        }
    }

    private void OnWallCollisionBottom(Collider collider, CollisionInformation info)
    {
        foreach (var col in info.Collisions)
        {
            if (col.FirstContact)
            {
                if (col.Other.Parent is Entity e)
                {
                    flashTimerBottom = flashDuration;
                    e.BoundsTouched(col.Intersection, bounds);
                }
            }
        }
    }
    
    private void OnWallCollisionRight(Collider collider, CollisionInformation info)
    {
        foreach (var col in info.Collisions)
        {
            if (col.FirstContact)
            {
                if (col.Other.Parent is Entity e)
                {
                    flashTimerRight = flashDuration;
                    e.BoundsTouched(col.Intersection, bounds);
                }
            }
        }
    }
    
    private void OnWallCollisionLeft(Collider collider, CollisionInformation info)
    {
        foreach (var col in info.Collisions)
        {
            if (col.FirstContact)
            {
                if (col.Other.Parent is Entity e)
                {
                    flashTimerLeft = flashDuration;
                    e.BoundsTouched(col.Intersection, bounds);
                }
            }
        }
    }
    
    public override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        base.Update(time, game, gameUi, ui);

        if (flashTimerTop > 0f)
        {
            flashTimerTop -= time.Delta;
        }
        if (flashTimerBottom > 0f)
        {
            flashTimerBottom -= time.Delta;
        }
        if (flashTimerLeft > 0f)
        {
            flashTimerLeft -= time.Delta;
        }
        if (flashTimerRight > 0f)
        {
            flashTimerRight -= time.Delta;
        }
    }

    public override void DrawGame(ScreenInfo game)
    {
        if (flashTimerTop > 0f)
        {
            var f = flashTimerTop / flashDuration;
            var thickness = ShapeMath.LerpFloat(4f, 12f, f);
            var color = Colors.BackgroundSpecialColor.Lerp(Colors.HardshellColor, f);
            wallTop.GetSegmentShape().Draw(thickness, color);
        }
        else
        {
            wallTop.GetSegmentShape().Draw(4f, Colors.BackgroundSpecialColor);
        }
        
        if (flashTimerBottom > 0f)
        {
            var f = flashTimerBottom / flashDuration;
            var thickness = ShapeMath.LerpFloat(4f, 12f, f);
            var color = Colors.BackgroundSpecialColor.Lerp(Colors.HardshellColor, f);
            wallBottom.GetSegmentShape().Draw(thickness, color);
        }
        else
        {
            wallBottom.GetSegmentShape().Draw(4f, Colors.BackgroundSpecialColor);
        }
        
        if (flashTimerLeft > 0f)
        {
            var f = flashTimerLeft / flashDuration;
            var thickness = ShapeMath.LerpFloat(4f, 12f, f);
            var color = Colors.BackgroundSpecialColor.Lerp(Colors.HardshellColor, f);
            wallLeft.GetSegmentShape().Draw(thickness, color);
        }
        else
        {
            wallLeft.GetSegmentShape().Draw(4f, Colors.BackgroundSpecialColor);
        }
        
        if (flashTimerRight > 0f)
        {
            var f = flashTimerRight / flashDuration;
            var thickness = ShapeMath.LerpFloat(4f, 12f, f);
            var color = Colors.BackgroundSpecialColor.Lerp(Colors.HardshellColor, f);
            wallRight.GetSegmentShape().Draw(thickness, color);
        }
        else
        {
            wallRight.GetSegmentShape().Draw(4f, Colors.BackgroundSpecialColor);
        }
    }

    public override void DrawGameUI(ScreenInfo gameUi)
    {
        
    }
}
public class GameScene : Scene
{
    private Ship ship;
    private readonly ShapeCamera camera;
    private readonly CameraFollowerSingle cameraFollower;
    private Rect universe;
    private readonly int gridLines;
    private const float GridSpacing = 500f;
    
    
    public GameScene(GameData data)
    {
        camera = new ShapeCamera(new Vector2(0f, 0f), new AnchorPoint(0.5f, 0.5f), 1f, new Dimensions(1920, 1080));
        cameraFollower = new CameraFollowerSingle(100, 0, 500);
        camera.Follower = cameraFollower;
        
        universe = new Rect(new Vector2(0f, 0), new Size(2500, 2500), new AnchorPoint(0.5f, 0.5f));
        gridLines = (int)(universe.Width / GridSpacing);
       
        InitSpawnArea(universe);
        InitCollisionHandler(universe, gridLines, gridLines);
        
        ship =  new ShipGunslinger();

        var universeBorder = new UniverseBorder(universe);
        CollisionHandler?.Add(universeBorder);
        SpawnArea?.AddGameObject(universeBorder);
        
    }
    
    protected override void OnActivate(Scene oldScene)
    {
        Game.CurrentGameInstance.Camera = camera;

        var spawnInfo = new SpawnInfo(new(), new(1, 0));
        ship.Spawn(spawnInfo);
        SpawnArea?.AddGameObject(ship);
        CollisionHandler?.Add(ship);
        
        cameraFollower.SetTarget(ship);
        
        SpawnFloaters(1);
    }

    protected override void OnDeactivate()
    {
        Game.CurrentGameInstance.ResetCamera();
    }

    protected override void OnUpdate(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        cameraFollower.Speed = 300;
        var minSize = game.Area.Size.Min();
        cameraFollower.BoundaryDis = new RangeFloat(minSize * 0.1f, minSize * 0.2f);
    }

    protected override void OnPreDrawGame(ScreenInfo game)
    {
        Game.CurrentGameInstance.BackgroundColorRgba = Colors.BackgroundVeryDarkColor;
        
        // CollisionHandler?.DebugDraw(new ColorRgba(Color.Red), new ColorRgba(Color.Magenta));
        
        if (SpawnArea != null)
        {
            SpawnArea.Bounds.DrawGrid(gridLines, new LineDrawingInfo(3f, Colors.BackgroundDarkColor));
            SpawnArea.Bounds.DrawLines(16f, Colors.BackgroundLightColor);
        }
    }

    protected override void OnDrawGame(ScreenInfo game)
    {
        
        
    }
    private void SpawnFloaters(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var pos = universe.GetRandomPointInside();
            var floater = new Floater(DataSheet.Floater);
            var spawnInfo = new SpawnInfo(pos, ShapeVec.Right().Rotate(Rng.Instance.RandAngleRad()));
            floater.Spawn(spawnInfo);
            SpawnArea?.AddGameObject(floater);
            CollisionHandler?.Add(floater);
        }
    }
    private void SpawnCollectibles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var pos = universe.GetRandomPointInside();
            var size = Rng.Instance.RandF(6, 12);
            var c = new Collectible(pos, size);
            SpawnArea?.AddGameObject(c);
            CollisionHandler?.Add(c);
        }
    }
}
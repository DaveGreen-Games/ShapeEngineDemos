using System.Numerics;
using System.Text;
using AsteroidsDemo.GameSource.ColorSystem;
using AsteroidsDemo.GameSource.Data;
using AsteroidsDemo.GameSource.Entities;
using AsteroidsDemo.GameSource.Entities.Asteroids;
using AsteroidsDemo.GameSource.Entities.Collectibles;
using AsteroidsDemo.GameSource.Entities.Ships;
using ShapeEngine.Audio;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Input;
using ShapeEngine.Lib;
using ShapeEngine.Random;
using ShapeEngine.Screen;

namespace AsteroidsDemo.GameSource.Scenes;

public readonly struct GameData
{
    
}


public class Border : CollisionObject
{
    private float flashDuration = 2f;
    private float flashTimer = 0f;
    private float flashF = 0f;
    
    private RectCollider wall;
    private Rect bounds;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="side">0 is top, 1 = left, 2 = bottom, 3 = right</param>
    public Border(Rect bounds, int side)
    {
        const float wallThickness = 100;
        var collisionMask = new BitFlag(CollisionLayers.Ships);
        collisionMask = collisionMask.Add(CollisionLayers.Asteroids);
        collisionMask = collisionMask.Add(CollisionLayers.AsteroidBullets);
        collisionMask = collisionMask.Add(CollisionLayers.ShipBullets);

        this.bounds = bounds;

        Vector2 p;
        // Vector2 offset;
        Size size;
        AnchorPoint alignment;
        if (side == 0)
        {
            alignment = AnchorPoint.TopCenter;
            p = bounds.GetPoint(alignment);
            // offset = p - bounds.Center;
            size = new Size(bounds.Width, wallThickness);
        }
        else if (side == 1)
        {
            alignment = AnchorPoint.Left;
            p = bounds.GetPoint(alignment);
            // offset = p - bounds.Center;
            size = new Size(wallThickness, bounds.Height - (2f * wallThickness));
        }
        else if (side == 2)
        {
            alignment = AnchorPoint.BottomCenter;
            p = bounds.GetPoint(alignment);
            // offset = p - bounds.Center;
            size = new Size(bounds.Width, wallThickness);
        }
        else//3
        {
            alignment = AnchorPoint.Right;
            p = bounds.GetPoint(alignment);
            // offset = p - bounds.Center;
            size = new Size(wallThickness, bounds.Height - (2f * wallThickness));
        }

        var transform = new Transform2D(new Vector2(0f, 0f), 0f, new Size(0f, 0f), 1f);
        wall = new RectCollider(transform, alignment);
        wall.CollisionLayer = CollisionLayers.World;
        wall.ComputeCollision = true;
        wall.ComputeIntersections = true;
        wall.CollisionMask = collisionMask;
        wall.OnCollision += OnWallCollision;

        AddCollider(wall);
        
        Transform = new Transform2D(p, 0f, size, 1f);
    }
    
    private void OnWallCollision(Collider collider, CollisionInformation info)
    {
        foreach (var col in info.Collisions)
        {
            if (col.FirstContact)
            {
                if (col.Other.Parent is Entity e)
                {
                    flashTimer = flashDuration;
                    e.BoundsTouched(col.Intersection, bounds);
                }
            }
        }
    }
    
    public override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        base.Update(time, game, gameUi, ui);

        if (flashTimer > 0f)
        {
            flashTimer -= time.Delta;
            if (flashTimer <= 0f) flashF = 0f;
            else flashF = flashTimer / flashDuration;
        }
    }
    
    public override void DrawGame(ScreenInfo game)
    {
        var rect = wall.GetRectShape();
        var thickness = ShapeMath.LerpFloat(4f, 8f, flashF);
        var spacing = 35f; //ShapeMath.LerpFloat(50, 25f, flashF);
        var color = Colors.HardshellColor.Lerp(Colors.AsteroidSpecialColor, flashF);
        LineDrawingInfo checkered = new(thickness, color, LineCapType.None, 0);
        LineDrawingInfo outline = new(thickness, color, LineCapType.CappedExtended, 4);
        rect.DrawCheckered(spacing, 45f, checkered, outline, ColorRgba.Clear);
        
        
    }

    public override void DrawGameUI(ScreenInfo gameUi) { }
}

public class GameScene : Scene
{
    private Ship ship;
    private readonly ShapeCamera camera;
    private readonly CameraFollowerSingle cameraFollower;
    private readonly int gridLines;
    private const float GridSpacing = 500f;

    public readonly Rect Universe;
    public GameScene(GameData data)
    {
        
        camera = new ShapeCamera(new Vector2(0f, 0f), new AnchorPoint(0.5f, 0.5f), 1f, new Dimensions(1920, 1080));
        cameraFollower = new CameraFollowerSingle(100, 0, 500);
        camera.Follower = cameraFollower;
        
        Universe = new Rect(new Vector2(0f, 0), new Size(5000, 5000), new AnchorPoint(0.5f, 0.5f));
        gridLines = (int)(Universe.Width / GridSpacing);
       
        InitSpawnArea(Universe);
        InitCollisionHandler(Universe, gridLines, gridLines);
        
        ship =  new ShipGunslinger();

        var borderTop = new Border(Universe, 0);
        var borderLeft = new Border(Universe, 1);
        var borderBottom = new Border(Universe, 2);
        var borderRight = new Border(Universe, 3);
        
        CollisionHandler?.Add(borderTop);
        CollisionHandler?.Add(borderLeft);
        CollisionHandler?.Add(borderBottom);
        CollisionHandler?.Add(borderRight);
        
        SpawnArea?.AddGameObject(borderTop);
        SpawnArea?.AddGameObject(borderLeft);
        SpawnArea?.AddGameObject(borderBottom);
        SpawnArea?.AddGameObject(borderRight);
        
    }
    
    protected override void OnActivate(Scene oldScene)
    {
        Game.CurrentGameInstance.Camera = camera;

        Game.CurrentGameInstance.AudioDevice.PlaylistSwitch(AsteroidsGame.PlaylistGameId);
        

        var spawnInfo = new SpawnInfo(new(), new(1, 0));
        ship.Spawn(spawnInfo);
        SpawnArea?.AddGameObject(ship);
        CollisionHandler?.Add(ship);
        
        cameraFollower.SetTarget(ship);
        
        SpawnFloaters(25);
        SpawnCollectibles(250);
    }

    protected override void OnDeactivate()
    {
        Game.CurrentGameInstance.ResetCamera();
    }

    protected override void OnUpdate(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        cameraFollower.Speed = 300;
        var minSize = game.Area.Size.Min();
        cameraFollower.BoundaryDis = new ValueRange(minSize * 0.1f, minSize * 0.2f);
        
        
        if(ShapeKeyboardButton.ESCAPE.GetInputState().Pressed) Game.CurrentGameInstance.GoToScene(new MainMenu());
    }

    protected override void OnPreDrawGame(ScreenInfo game)
    {
        Game.CurrentGameInstance.BackgroundColorRgba = Colors.BackgroundVeryDarkColor;
        Universe.DrawGrid(gridLines, new LineDrawingInfo(3f, Colors.BackgroundLightColor));
    }

    protected override void OnDrawGame(ScreenInfo game)
    {
        
        
    }
    private void SpawnFloaters(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var pos = Universe.GetRandomPointInside();
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
            var pos = Universe.GetRandomPointInside();
            var value = Rng.Instance.RandF(6, 12);

            if (Rng.Instance.Chance(0.5f))
            {
                var resource = new Resource(pos, value);
                SpawnArea?.AddGameObject(resource);
                CollisionHandler?.Add(resource);
            }
            else
            {
                var experience = new Experience(pos, value);
                SpawnArea?.AddGameObject(experience);
                CollisionHandler?.Add(experience);
            }
            
        }
    }
}
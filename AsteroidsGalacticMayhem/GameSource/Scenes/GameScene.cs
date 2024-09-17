using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using AsteroidsGalacticMayhem.GameSource.Entities.Collectibles;
using AsteroidsGalacticMayhem.GameSource.Entities.Ships;
using ShapeEngine.Core;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;
using ShapeEngine.Random;
using ShapeEngine.Screen;

namespace AsteroidsGalacticMayhem.GameSource.Scenes;

public readonly struct GameData
{
    
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
    }
    
    protected override void OnActivate(Scene oldScene)
    {
        Game.CurrentGameInstance.Camera = camera;
        
        ship.Spawn(new(), new(1, 0));
        SpawnArea?.AddGameObject(ship);
        CollisionHandler?.Add(ship);
        
        cameraFollower.SetTarget(ship);
        
        SpawnCollectibles(100);
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
        if (SpawnArea != null)
        {
            SpawnArea.Bounds.DrawGrid(gridLines, new LineDrawingInfo(3f, Colors.BackgroundDarkColor));
            SpawnArea.Bounds.DrawLines(16f, Colors.BackgroundLightColor);
        }
    }

    protected override void OnDrawGame(ScreenInfo game)
    {
        
        
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
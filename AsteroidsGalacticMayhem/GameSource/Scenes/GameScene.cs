using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using AsteroidsGalacticMayhem.GameSource.Entities.Ships;
using ShapeEngine.Core;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;
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
        ship =  new ShipGunslinger();
        camera = new ShapeCamera(new Vector2(0f, 0f), new AnchorPoint(0.5f, 0.5f), 1f, new Dimensions(1920, 1080));
        cameraFollower = new CameraFollowerSingle(100, 0, 500);
        camera.Follower = cameraFollower;
        
        universe = new Rect(new Vector2(0f, 0), new Size(2500, 2500), new AnchorPoint(0.5f, 0.5f));
        InitSpawnArea(universe);
        InitCollisionHandler(universe, gridLines, gridLines);
        
        gridLines = (int)(universe.Width / GridSpacing);
    }
    
    protected override void OnActivate(Scene oldScene)
    {
        Game.CurrentGameInstance.Camera = camera;
        
        ship.Spawn(new(), new(1, 0));
        SpawnArea?.AddGameObject(ship);
        
        cameraFollower.SetTarget(ship);
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
}
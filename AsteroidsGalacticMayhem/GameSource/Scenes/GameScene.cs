using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using AsteroidsGalacticMayhem.GameSource.Entities.Ships;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsGalacticMayhem.GameSource.Scenes;

public class GameScene : Scene
{
    private Ship ship = new ShipGunslinger();
    
    
    
    protected override void OnActivate(Scene oldScene)
    {
        ship = new ShipGunslinger();
        ship.Spawn(new(), new(1, 0));
        InitSpawnArea(-5000, -5000, 10000, 10000);
        SpawnArea?.AddGameObject(ship);
    }

    protected override void OnDeactivate()
    {
        
    }

    protected override void OnUpdate(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        
    }

    protected override void OnDrawGame(ScreenInfo game)
    {
        Game.CurrentGameInstance.BackgroundColorRgba = Colors.BackgroundDarkColor;
        SpawnArea?.Bounds.DrawLines(12f, Colors.BackgroundSpecialColor);
        
    }
}
using Asteroids.GameSource.ColorSystem;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;

namespace Asteroids.GameSource.Scenes;

public class GameScene : Scene
{
    protected override void OnActivate(Scene oldScene)
    {
        
    }

    protected override void OnDeactivate()
    {
        
    }

    protected override void OnDrawGame(ScreenInfo game)
    {
        Game.CurrentGameInstance.BackgroundColorRgba = Colors.BackgroundDarkColor;
        
    }
}
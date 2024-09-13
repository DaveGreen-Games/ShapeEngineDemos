using System.Drawing;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace Asteroids.Game.Scenes;

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
        game.Area.Draw(new ColorRgba(Color.Maroon));
        game.Area.DrawLines(12f, new ColorRgba(Color.IndianRed));
        game.MousePos.Draw(24f, new ColorRgba(Color.OrangeRed), 36);
    }
}
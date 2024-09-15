using System.Drawing;
using AsteroidsGalacticMayhem.GameSource.Scenes;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsGalacticMayhem;


public static class Program
{
    public static void Main(string[] args)
    {
        var game = new AsteroidsGame(GameSettings.StretchMode, WindowSettings.Default);
        game.Run();
    }
}
public class AsteroidsGame : ShapeEngine.Core.Game
{
    public AsteroidsGame(GameSettings gameSettings, WindowSettings windowSettings) : base(gameSettings, windowSettings)
    {
        
    }


    protected override void LoadContent()
    {
        var newGameScene = new GameScene(new GameData());
        GoToScene(newGameScene);
    }

    protected override void UnloadContent()
    {
        
    }
}





// public static class Program
// {
//     // public static AsteroidsGame GAME;
//     public static void Main(string[] args)
//     {
//         var gameSettings = new GameSettings()
//         {
//             DevelopmentDimensions = new Dimensions(1920, 1080),
//             MultiShaderSupport = false
//         };
//         var windowSettings = new WindowSettings()
//         {
//             Undecorated = false,
//             Focused = true,
//             WindowDisplayState = WindowDisplayState.Normal,
//             WindowBorder = WindowBorder.Resizabled,
//             WindowMinSize = new Dimensions(480, 270),
//             WindowSize = new Dimensions(-1, -1),
//             Monitor = 0,
//             Vsync = false,
//             FrameRateLimit = 60,
//             MinFramerate = 30,
//             MaxFramerate = 240,
//             WindowOpacity = 1f,
//             MouseEnabled = true,
//             MouseVisible = true
//         };
//         var game = new AsteroidsGame(gameSettings, windowSettings);
//         game.Run();
//
//     }
// }
//
//
//
// public class AsteroidsGame : Game
// {
//     public AsteroidsGame(GameSettings gameSettings, WindowSettings windowSettings) : base(gameSettings, windowSettings)
//     {
//     }
//
//     protected override void BeginRun()
//     {
//         
//     }
//     
//     protected override void LoadContent()
//     {
//         
//     }
//
//     protected override void Update(GameTime time, ScreenInfo game, ScreenInfo ui)
//     {
//         
//     }
//
//     protected override void DrawGame(ScreenInfo game)
//     {
//         game.Area.Draw(new ColorRgba(Color.DarkOliveGreen));
//         game.Area.DrawLines(6f, new ColorRgba(Color.OliveDrab));
//         game.MousePos.Draw(24f, new ColorRgba(Color.Lime), 36);
//     }
//
//     protected override void UnloadContent()
//     {
//        
//     }
//
//     protected override void EndRun()
//     {
//         
//     }
// }
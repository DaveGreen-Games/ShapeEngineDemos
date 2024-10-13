using System.Drawing;
using AsteroidsDemo.GameSource;
using AsteroidsDemo.GameSource.Scenes;
using Raylib_cs;
using ShapeEngine.Audio;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsDemo;


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
    // public static AsteroidsGame Instance { get; private set; } = null!;
    
    public static readonly AudioDevice AudioDevice = new AudioDevice();

    public static readonly uint BusSoundId = 1;
    public static readonly uint BusMusicId = 2;
    public static readonly uint BusUiId = 3;
    
    public static readonly uint SoundButtonClick1Id = 10;
    public static readonly uint SoundButtonHover1Id = 11;
    
    public AsteroidsGame(GameSettings gameSettings, WindowSettings windowSettings) : base(gameSettings, windowSettings)
    {
        // Instance = this;
        
        AudioDevice.BusAdd(BusSoundId, 1f);
        AudioDevice.BusAdd(BusMusicId, 1f);
        AudioDevice.BusAdd(BusUiId, 1f);
    }

    protected override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        base.Update(time, game, gameUi, ui);
        AudioDevice.Update(time.Delta, Camera);
    }

    protected override void LoadContent()
    {
        GameContent.Load();
        
        AudioDevice.SFXAdd(SoundButtonClick1Id, GameContent.SoundButtonClick1, 0.5f, 1f, BusSoundId, BusUiId);
        AudioDevice.SFXAdd(SoundButtonHover1Id, GameContent.SoundButtonHover1, 0.5f, 1f, BusSoundId, BusUiId);
        
        GoToScene(new MainMenu());
    }

    protected override void UnloadContent()
    {
        AudioDevice.Close();
        GameContent.Unload();
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
using System.Drawing;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace SavegameDemo;

public static class Program
{
    public static void Main(string[] args)
    {
        var game = new MyGame(GameSettings.StretchMode, WindowSettings.Default);
        game.Run();
    }
}
public class MyGame : Game
{
    public MyGame(GameSettings gameSettings, WindowSettings windowSettings) : base(gameSettings, windowSettings) { }
    protected override void DrawGame(ScreenInfo game)
    {
        game.Area.Draw(new ColorRgba(Color.DarkOliveGreen));
        game.Area.DrawLines(12f, new ColorRgba(Color.AntiqueWhite));
        game.MousePos.Draw(24f, new ColorRgba(Color.Lime), 36);
    }
}
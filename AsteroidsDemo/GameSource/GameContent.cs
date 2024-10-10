using Raylib_cs;
using ShapeEngine.Persistent;

namespace AsteroidsDemo.GameSource;

public static class GameContent
{
    public static Font FontTitle = new();
    public static Font FontLight = new();
    public static Font FontRegular = new();
    public static Font FontBold = new();
    
    public static void Load()
    {
        FontTitle = ContentLoader.LoadFont("resources/fonts/Rubik_Mono_One/RubikMonoOne-Regular.ttf", 250, TextureFilter.Trilinear);
        FontLight = ContentLoader.LoadFont("resources/fonts/Saira_Condensed/SairaCondensed-Light.ttf", 250, TextureFilter.Trilinear);
        FontRegular = ContentLoader.LoadFont("resources/fonts/Saira_Condensed/SairaCondensed-Regular.ttf", 250, TextureFilter.Trilinear);
        FontBold = ContentLoader.LoadFont("resources/fonts/Saira_Condensed/SairaCondensed-Bold.ttf", 250, TextureFilter.Trilinear);
    }

    public static void Unload()
    {
        ContentLoader.UnloadFont(FontTitle);
        ContentLoader.UnloadFont(FontLight);
        ContentLoader.UnloadFont(FontRegular);
        ContentLoader.UnloadFont(FontBold);
    }
}
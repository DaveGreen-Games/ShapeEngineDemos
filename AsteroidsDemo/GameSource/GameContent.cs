using Raylib_cs;
using ShapeEngine.Persistent;
using ShapeEngine.Audio;

namespace AsteroidsDemo.GameSource;

public static class GameContent
{
    public static Font FontTitle = new();
    public static Font FontLight = new();
    public static Font FontRegular = new();
    public static Font FontBold = new();


    public static Sound SoundButtonClick1 = new();
    public static Sound SoundButtonHover1 = new();
    
    public static Music SongCalmInSpace = new();
    public static Music SongANewAdventureBegins = new();
    public static Music SongFollowTheSunlight = new();
    public static Music SongLetsGo = new();
    
    public static void Load()
    {
        FontTitle = ContentLoader.LoadFont("resources/fonts/Rubik_Mono_One/RubikMonoOne-Regular.ttf", 250, TextureFilter.Trilinear);
        FontLight = ContentLoader.LoadFont("resources/fonts/Saira_Condensed/SairaCondensed-Light.ttf", 250, TextureFilter.Trilinear);
        FontRegular = ContentLoader.LoadFont("resources/fonts/Saira_Condensed/SairaCondensed-Regular.ttf", 250, TextureFilter.Trilinear);
        FontBold = ContentLoader.LoadFont("resources/fonts/Saira_Condensed/SairaCondensed-Bold.ttf", 250, TextureFilter.Trilinear);
        
        
        SoundButtonClick1 = ContentLoader.LoadSound("resources/sounds/button-click01.wav");
        SoundButtonHover1 = ContentLoader.LoadSound("resources/sounds/button-hover01.wav");
        
        SongCalmInSpace = ContentLoader.LoadMusicStream("resources/music/calm-in-space.mp3");
        SongANewAdventureBegins = ContentLoader.LoadMusicStream("resources/music/a-new-adventure-begins.mp3");
        SongFollowTheSunlight = ContentLoader.LoadMusicStream("resources/music/follow-the-sunlight.mp3");
        SongLetsGo = ContentLoader.LoadMusicStream("resources/music/lets-go.mp3");
    }

    public static void Unload()
    {
        ContentLoader.UnloadFont(FontTitle);
        ContentLoader.UnloadFont(FontLight);
        ContentLoader.UnloadFont(FontRegular);
        ContentLoader.UnloadFont(FontBold);
        
        ContentLoader.UnloadSound(SoundButtonClick1);
        ContentLoader.UnloadSound(SoundButtonHover1);
    }
}
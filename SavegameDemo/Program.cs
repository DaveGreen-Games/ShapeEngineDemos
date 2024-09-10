using Raylib_cs;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;
using ShapeEngine.Persistent;
using ShapeEngine.Random;
using ShapeEngine.Text;
using Color = System.Drawing.Color;

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
    // public static SavegameFolder SavegameFolder { get; private set; } = 
    //     new(ShapeSavegame.APPLICATION_DATA_PATH, 
    //         "shape-engine-savegame-test", "savegames");
    //
    // public static SavegameFolder ProfileFolder { get; private set; } = 
    //     new(ShapeSavegame.APPLICATION_DATA_PATH, 
    //         "shape-engine-savegame-test", "savegames", "profiles");

    private SavegameBasic savegameBasic;
    private SavegameProfile[] savegameProfiles;
    private SavegameProfile currentSavegameProfile;
    private const int maxProfiles = 3;
    private TextFont textFont;
    
    public static readonly string BasicPath = $"{ShapeSavegame.APPLICATION_DATA_PATH}/savegames";
    public static readonly string ProfilesPath = $"{ShapeSavegame.APPLICATION_DATA_PATH}/savegames/profiles";
    public MyGame(GameSettings gameSettings, WindowSettings windowSettings) : base(gameSettings, windowSettings)
    {
        
        
        Console.WriteLine($"        > Application Folder: {ShapeSavegame.APPLICATION_DATA_PATH}");
        Console.WriteLine($"        > Savegame Folder: {BasicPath}");
        Console.WriteLine($"        > Application Folder: {ProfilesPath}");
        
        // Console.WriteLine($"        > Savegame Folder: {SavegameFolder.Path}");
        // Console.WriteLine($"        > Application Folder: {ProfileFolder.Path}");

        
        
        var basic = ShapeSavegame.Load<SavegameBasic>(BasicPath, "basic.txt");
        savegameProfiles = new SavegameProfile[maxProfiles];
        if (basic == null)
        {
            savegameBasic = CreateSavegameBasic("profile1");
            ShapeSavegame.Save(savegameBasic, BasicPath, savegameBasic.name);
            
            for (int i = 0; i < savegameProfiles.Length; i++)
            {
                var profile = CreateSavegameProfile(i);
                savegameProfiles[i] = profile;
                ShapeSavegame.Save(profile, ProfilesPath, profile.name);
            }
            currentSavegameProfile = savegameProfiles[0];
        }
        else
        {
            savegameBasic = basic;
            for (int i = 0; i < savegameProfiles.Length; i++)
            {
                var profile = ShapeSavegame.Load<SavegameProfile>(ProfilesPath, $"profile{i + 1}.txt");
                if (profile == null)
                {
                    var newProfile = CreateSavegameProfile(i);
                    savegameProfiles[i] = newProfile;
                    if (newProfile.ProfileName == savegameBasic.CurrentProfileName)
                    {
                        currentSavegameProfile = newProfile;
                    }
                }
                else
                {
                    savegameProfiles[i] = profile;
                    if (profile.ProfileName == savegameBasic.CurrentProfileName)
                    {
                        currentSavegameProfile = profile;
                    }
                }
            }
        }

        var font = Raylib.GetFontDefault();
        textFont = new TextFont(font, 32, 5, 0, new ColorRgba(Color.Beige));
    }

    private SavegameBasic CreateSavegameBasic(string profileName)
    {
        return new(profileName, "basic.txt");
    }
    private SavegameProfile CreateSavegameProfile(int index)
    {
        var name = $"Profile{index + 1}";
        var fileName = $"profile{index + 1}.txt";
        var profile = new SavegameProfile(name, name,Rng.Instance.RandI(0, 1000), Rng.Instance.RandI(0, 100), Rng.Instance.RandF() );
        profile.name = fileName;
        profile.version = "1.0.0";
        return profile;
    }
    
    protected override void LoadContent()
    {
        
    }

    protected override void UnloadContent()
    {
        
    }

    protected override void DrawGame(ScreenInfo game)
    {
        
        
        game.Area.Draw(new ColorRgba(Color.DarkOliveGreen));
        game.Area.DrawLines(12f, new ColorRgba(Color.AntiqueWhite));
        game.MousePos.Draw(24f, new ColorRgba(Color.Lime), 36);
    }

    protected override void DrawUI(ScreenInfo ui)
    {
        var center = ui.Area.ApplyMargins(0.1f, 0.1f, 0.1f, 0.1f);
        center.Draw(new ColorRgba(Color.DarkSlateGray));

        int rows = 4;
        var rects = center.SplitV(rows);
        
        var r = rects[0];
        var text = currentSavegameProfile.DisplayName;
        textFont.DrawWord(text, r, new AnchorPoint(0f, 0.5f));
        
        r = rects[1];
        text = $"> Id: {currentSavegameProfile.Id}";
        textFont.DrawWord(text, r, new AnchorPoint(0f, 0.5f));
        
        r = rects[2];
        text = $"> Age: {currentSavegameProfile.Age}";
        textFont.DrawWord(text, r, new AnchorPoint(0f, 0.5f));
        
        r = rects[3];
        var chance = MathF.Round(currentSavegameProfile.Chance * 100f) / 100f;
        text = $"> Chance: {chance}%";
        textFont.DrawWord(text, r, new AnchorPoint(0f, 0.5f));
    }
}

public class SavegameBasic : SavegameObject
{
    public string CurrentProfileName { get; set; }

    public SavegameBasic(string profileName, string fileName)
    {
        CurrentProfileName = profileName;
        name = fileName;
        version = "1.0.0";
    }
}
public class SavegameProfile : SavegameObject
{
    public string ProfileName { get; set; }
    public string DisplayName { get; set; }
    public int Id { get; set; }
    public int Age { get; set; }
    public float Chance { get; set; }

    public SavegameProfile(string profileName, string displayName, int id, int age, float chance)
    {
        ProfileName = profileName;
        DisplayName = displayName;
        Id = id;
        Age = age;
        Chance = chance;
    }
}
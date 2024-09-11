using Raylib_cs;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;
using ShapeEngine.Input;
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
    private int currentSavegameProfileIndex;
    private const int maxProfiles = 3;
    private TextFont textFont;
    
    public static readonly string BasicPath = Path.Combine(ShapeSavegame.ApplicationDocumentsPath, "savegames");
    public static readonly string ProfilesPath = Path.Combine(ShapeSavegame.ApplicationDocumentsPath, "savegames/profiles");   
    public MyGame(GameSettings gameSettings, WindowSettings windowSettings) : base(gameSettings, windowSettings)
    {
        var basic = ShapeSavegame.Load<SavegameBasic>(BasicPath, "basic.txt");
        savegameProfiles = new SavegameProfile[maxProfiles];
        if (basic == null)
        {
            savegameBasic = CreateSavegameBasic("Profile1");
            ShapeSavegame.Save(savegameBasic, BasicPath, savegameBasic.name);
            
            for (int i = 0; i < savegameProfiles.Length; i++)
            {
                var profile = CreateSavegameProfile(i);
                savegameProfiles[i] = profile;
                ShapeSavegame.Save(profile, ProfilesPath, profile.name);
            }
            currentSavegameProfile = savegameProfiles[0];
            currentSavegameProfileIndex = 0;
        }
        else
        {
            savegameBasic = basic;
            for (int i = 0; i < savegameProfiles.Length; i++)
            {
                var fileName = $"profile{i + 1}.txt";
                var profile = ShapeSavegame.Load<SavegameProfile>(ProfilesPath, fileName);
                if (profile == null)
                {
                    var newProfile = CreateSavegameProfile(i);
                    savegameProfiles[i] = newProfile;
                    if (newProfile.ProfileName == savegameBasic.CurrentProfileName)
                    {
                        currentSavegameProfileIndex = i;
                        currentSavegameProfile = newProfile;
                    }
                }
                else
                {
                    savegameProfiles[i] = profile;
                    if (profile.ProfileName == savegameBasic.CurrentProfileName)
                    {
                        currentSavegameProfileIndex = i;
                        currentSavegameProfile = profile;
                    }
                }
            }
        }

        var font = Raylib.GetFontDefault();
        textFont = new TextFont(font, 24, 4, -6, new ColorRgba(Color.Beige));
    }

    private SavegameBasic CreateSavegameBasic(string profileName)
    {
        return new(profileName);
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

    protected override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        if (ShapeKeyboardButton.ESCAPE.GetInputState().Pressed)
        {
            Quit();
            return;
        }
        
        if (ShapeKeyboardButton.SPACE.GetInputState().Pressed) NextProfile();
        if (ShapeKeyboardButton.Q.GetInputState().Pressed) RandomizeCurrentProfile();
        if (ShapeKeyboardButton.R.GetInputState().Pressed) ResetSavegames();
    }

    private void ResetSavegames()
    {
        savegameBasic = CreateSavegameBasic("Profile1");
        ShapeSavegame.Save(savegameBasic, BasicPath, savegameBasic.name);
            
        for (int i = 0; i < savegameProfiles.Length; i++)
        {
            var profile = CreateSavegameProfile(i);
            savegameProfiles[i] = profile;
            ShapeSavegame.Save(profile, ProfilesPath, profile.name);
        }
        currentSavegameProfile = savegameProfiles[0];
        currentSavegameProfileIndex = 0;
    }

    private void RandomizeCurrentProfile()
    {
        // currentSavegameProfile.Age = Rng.Instance.RandI(0, 100);
        currentSavegameProfile.Chance = Rng.Instance.RandF();
        ShapeSavegame.Save(currentSavegameProfile, ProfilesPath, currentSavegameProfile.name);
    }

    private void NextProfile()
    {
        currentSavegameProfileIndex += 1;
        if(currentSavegameProfileIndex >= maxProfiles) currentSavegameProfileIndex = 0;
        currentSavegameProfile = savegameProfiles[currentSavegameProfileIndex];
        savegameBasic.CurrentProfileName = currentSavegameProfile.ProfileName;
        ShapeSavegame.Save(savegameBasic, BasicPath, savegameBasic.name);
    }

    protected override void DrawGame(ScreenInfo game)
    {
        
        
        game.Area.Draw(new ColorRgba(Color.DarkOliveGreen));
        // game.Area.DrawLines(12f, new ColorRgba(Color.AntiqueWhite));
        game.MousePos.Draw(24f, new ColorRgba(Color.Lime), 36);
    }

    protected override void DrawUI(ScreenInfo ui)
    {
        var area = ui.Area.ApplyMargins(0.01f, 0.01f, 0.01f, 0.01f);
        var split = area.SplitV(0.75f);
        var top = split.top;
        var bottom = split.bottom;
        var splitBottom = bottom.SplitV(0.5f);
        
        top.Draw(new ColorRgba(Color.DarkSlateGray));

        top = top.ApplyMargins(0.01f, 0.01f, 0.01f, 0.01f);
        int rows = 4;
        var rects = top.SplitV(rows);
        
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

        var pathRect = splitBottom.top.ApplyMargins(0.01f, 0.01f, 0.01f, 0.01f);
        textFont.DrawWord($"Path: {BasicPath}", pathRect, new AnchorPoint(0.5f, 0.5f));
        
        var inputRect = splitBottom.bottom.ApplyMargins(0.01f, 0.01f, 0.01f, 0.01f);
        textFont.DrawWord("Cycle [Space] | Rand [Q] | Reset [R]", inputRect, new AnchorPoint(0.5f, 1f));
    }
}

public class SavegameBasic : SavegameObject
{
    public string CurrentProfileName { get; set; }

    public SavegameBasic(string currentProfileName)
    {
        CurrentProfileName = currentProfileName;
        name = "basic.txt";
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
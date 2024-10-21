using AsteroidsDemo.GameSource.ColorSystem;
using ShapeEngine.Audio;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Structs;
using ShapeEngine.Input;
using ShapeEngine.Lib;
using ShapeEngine.Text;

namespace AsteroidsDemo.GameSource.Scenes;

public class MainMenu : Scene
{
    private float gappedTimer = 0f;
    private const float GappedDuration = 15f;

    private TextFont titleFont;
    private TextFont subtitleFont;
    private TextFont buttonFont;

    private bool playButtonHovered = false;

    private AudioDevice audioDevice;
    public MainMenu()
    {
        titleFont = new(GameContent.FontTitle, 0f, Colors.TextSpecialColor);
        subtitleFont = new(GameContent.FontBold, 0f, Colors.TextMediumColor);
        buttonFont = new(GameContent.FontRegular, 0f, Colors.TextMediumColor);
        audioDevice = Game.CurrentGameInstance.AudioDevice;
    }
    
    protected override void OnUpdate(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        base.OnUpdate(time, game, gameUi, ui);

        if (gappedTimer > 0f)
        {
            gappedTimer -= time.Delta;
            if (gappedTimer <= 0f)
            {
                var overshoot = gappedTimer * -1;
                gappedTimer = GappedDuration - overshoot;
            }
        }
    }

    protected override void OnDrawUI(ScreenInfo ui)
    {
        Game.CurrentGameInstance.BackgroundColorRgba = Colors.BackgroundVeryDarkColor;
        var thickness = ui.Area.Size.Min() * 0.005f;
        
        var rect = ui.Area.ApplyMargins(0.05f, 0.05f, 0.05f, 0.7f);
        var split = rect.SplitV(0.6f);
        var rectLineInfo = new LineDrawingInfo(thickness, Colors.TextSpecialColor, LineCapType.CappedExtended, 8);
        var f = gappedTimer / GappedDuration;
        f = ShapeTween.CircInOut(f);
        var rectGappedInfo = new GappedOutlineDrawingInfo(12, f, 0.5f);
        split.top.DrawGappedOutline(0f, rectLineInfo, rectGappedInfo);
        titleFont.DrawTextWrapNone("Asteroids", split.top, new AnchorPoint(0.5f, 0.5f));
        
        subtitleFont.DrawTextWrapNone("A Shape Engine Demo", split.bottom, new AnchorPoint(0.5f, 1f));
        
        var buttonRect = ui.Area.ApplyMargins(0.25f, 0.25f, 0.6f, 0.5f);

        var mouseInside = buttonRect.ContainsPoint(ui.MousePos);
        buttonRect.Draw(mouseInside ? Colors.BackgroundLightColor : Colors.BackgroundMediumColor);
        buttonFont.ColorRgba = mouseInside ? Colors.TextLightColor : Colors.TextMediumColor;
        buttonFont.DrawTextWrapNone("Play", buttonRect, new AnchorPoint(0.5f, 0.5f));
        if (mouseInside)
        {
            if (!playButtonHovered)
            {
                playButtonHovered = true;
                audioDevice.SFXPlay(AsteroidsGame.SoundButtonHover1Id, 1f, 1f, 0f);
            }

            var state = ShapeMouseButton.LEFT.GetInputState();
            if (state.Pressed)
            {
                audioDevice.SFXPlay(AsteroidsGame.SoundButtonClick1Id, 1f, 1f, 0f);
            }
            else if (state.Released)
            {
                StartRun();
            }
        }
        else playButtonHovered = false;

    }

    protected override void OnActivate(Scene oldScene)
    {
        gappedTimer = GappedDuration;
        audioDevice.PlaylistSwitch(AsteroidsGame.PlaylistMenuId);
    }

    protected override void OnDeactivate()
    {
        gappedTimer = 0f;
    }

    private void StartRun()
    {
        var newGameScene = new GameScene(new GameData());
        Game.CurrentGameInstance.GoToScene(newGameScene);
    }
}
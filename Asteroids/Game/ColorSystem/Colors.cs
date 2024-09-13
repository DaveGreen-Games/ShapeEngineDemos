using Raylib_cs;
using ShapeEngine.Color;

namespace Asteroids.Game.ColorSystem;

public static class Colors
{
    public static readonly PaletteColor Background = new PaletteColor(new ColorRgba(Color.DarkBlue));           //
    public static readonly PaletteColor ShipMain = new PaletteColor(new ColorRgba(Color.DarkBlue));             //
    public static readonly PaletteColor ShipSecondary = new PaletteColor(new ColorRgba(Color.DarkBlue));        //
    public static readonly PaletteColor ShipSpecial = new PaletteColor(new ColorRgba(Color.DarkBlue));          //
    public static readonly PaletteColor Hardshell = new PaletteColor(new ColorRgba(Color.DarkBlue));            //
    public static readonly PaletteColor AsteroidMain = new PaletteColor(new ColorRgba(Color.DarkBlue));         //
    public static readonly PaletteColor AsteroidSecondary = new PaletteColor(new ColorRgba(Color.DarkBlue));    //
    public static readonly PaletteColor AsteroidSpecial = new PaletteColor(new ColorRgba(Color.DarkBlue));      //
    
    public static ColorRgba BackgroundColor => Background.ColorRgba;
    public static ColorRgba ShipMainColor => ShipMain.ColorRgba;
    public static ColorRgba ShipSecondaryColor => ShipSecondary.ColorRgba;
    public static ColorRgba ShipSpecialColor => ShipSpecial.ColorRgba;
    public static ColorRgba HardshellColor => Hardshell.ColorRgba;
    public static ColorRgba AsteroidMainColor => AsteroidMain.ColorRgba;
    public static ColorRgba AsteroidSecondaryColor => AsteroidSecondary.ColorRgba;
    public static ColorRgba AsteroidSpecialColor => AsteroidSpecial.ColorRgba;

    public static readonly ColorPalette ColorPalette = new
    (
        Background,
        ShipMain,
        ShipSecondary,
        ShipSpecial,
        Hardshell,
        AsteroidMain,
        AsteroidSecondary,
        AsteroidSpecial
    );

    public static readonly ColorScheme Default =
    new(
        Background.Clone(),
        ShipMain.Clone(),
        ShipSecondary.Clone(),
        ShipSpecial.Clone(),
        Hardshell.Clone(),
        AsteroidMain.Clone(),
        AsteroidSecondary.Clone(),
        AsteroidSpecial.Clone()
    );
    
    public static readonly ColorScheme Alternative =
    new(
        Background.Clone(),
        ShipMain.Clone(),
        ShipSecondary.Clone(),
        ShipSpecial.Clone(),
        Hardshell.Clone(),
        AsteroidMain.Clone(),
        AsteroidSecondary.Clone(),
        AsteroidSpecial.Clone()
    );


}
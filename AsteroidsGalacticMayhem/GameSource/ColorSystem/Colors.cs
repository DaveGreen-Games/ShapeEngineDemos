using Raylib_cs;
using ShapeEngine.Color;

namespace AsteroidsGalacticMayhem.GameSource.ColorSystem;

public static class Colors
{

    #region Background

    public static readonly PaletteColor BackgroundVeryDark = new PaletteColor(ColorRgba.FromHex("000302"));
    public static readonly PaletteColor BackgroundDark = new PaletteColor(ColorRgba.FromHex("0A1C1A"));
    public static readonly PaletteColor BackgroundMedium = new PaletteColor(ColorRgba.FromHex("021F1B"));
    public static readonly PaletteColor BackgroundLight = new PaletteColor(ColorRgba.FromHex("0A3D36"));
    public static readonly PaletteColor BackgroundSpecial = new PaletteColor(ColorRgba.FromHex("55c2b3"));

    
    public static ColorRgba BackgroundVeryDarkColor => BackgroundVeryDark.ColorRgba;
    public static ColorRgba BackgroundDarkColor => BackgroundDark.ColorRgba;
    public static ColorRgba BackgroundMediumColor => BackgroundMedium.ColorRgba;
    public static ColorRgba BackgroundLightColor => BackgroundLight.ColorRgba;
    public static ColorRgba BackgroundSpecialColor => BackgroundSpecial.ColorRgba;
    
    #endregion
    
    #region Text
    
    public static readonly PaletteColor TextVeryDark= new PaletteColor(ColorRgba.FromHex("223805"));
    public static readonly PaletteColor TextDark = new PaletteColor(ColorRgba.FromHex("4f800f"));
    public static readonly PaletteColor TextMedium = new PaletteColor(ColorRgba.FromHex("5b9a09"));
    public static readonly PaletteColor TextLight = new PaletteColor(ColorRgba.FromHex("dbfbb1"));
    public static readonly PaletteColor TextSpecial = new PaletteColor(ColorRgba.FromHex("91e326"));
    
    public static ColorRgba TextVeryDarkColor => TextVeryDark.ColorRgba;
    public static ColorRgba TextDarkColor => TextDark.ColorRgba;
    public static ColorRgba TextMediumColor => TextMedium.ColorRgba;
    public static ColorRgba TextLightColor => TextLight.ColorRgba;
    public static ColorRgba TextSpecialColor => TextSpecial.ColorRgba;
    
    #endregion
    
    #region Special

    public static readonly PaletteColor Experience = new PaletteColor(ColorRgba.FromHex("b0ebe4"));
    public static readonly PaletteColor Resource = new PaletteColor(ColorRgba.FromHex("fbb1bb"));
    public static readonly PaletteColor Hardshell = new PaletteColor(ColorRgba.FromHex("e5db8a"));
    public static readonly PaletteColor Evade = new PaletteColor(ColorRgba.FromHex("6aa68e"));
    public static readonly PaletteColor Stun = new PaletteColor(ColorRgba.FromHex("a989cc"));

    public static ColorRgba ExperienceColor => Experience.ColorRgba;
    public static ColorRgba ResourceColor => Resource.ColorRgba;
    public static ColorRgba HardshellColor => Hardshell.ColorRgba;
    public static ColorRgba EvadeColor => Evade.ColorRgba;
    public static ColorRgba StunColor => Stun.ColorRgba;
    
    #endregion

    #region Ship
    
    public static readonly PaletteColor ShipVeryDark = new PaletteColor(ColorRgba.FromHex("0d1031"));
    public static readonly PaletteColor ShipDark = new PaletteColor(ColorRgba.FromHex("151c51"));
    public static readonly PaletteColor ShipMedium = new PaletteColor(ColorRgba.FromHex("212d82"));
    public static readonly PaletteColor ShipLight = new PaletteColor(ColorRgba.FromHex("5466eb"));
    public static readonly PaletteColor ShipSpecial = new PaletteColor(ColorRgba.FromHex("081fcf"));
   
    public static ColorRgba ShipVeryDarkColor => ShipVeryDark.ColorRgba;
    public static ColorRgba ShipDarkColor => ShipDark.ColorRgba;
    public static ColorRgba ShipMediumColor => ShipMedium.ColorRgba;
    public static ColorRgba ShipLightColor => ShipLight.ColorRgba;
    public static ColorRgba ShipSpecialColor => ShipSpecial.ColorRgba;
    
    #endregion
    
    #region Asteroids
    
    public static readonly PaletteColor AsteroidVeryDark = new PaletteColor(ColorRgba.FromHex("301216"));
    public static readonly PaletteColor AsteroidDark = new PaletteColor(ColorRgba.FromHex("4A0A11"));
    public static readonly PaletteColor AsteroidMedium = new PaletteColor(ColorRgba.FromHex("870312"));
    public static readonly PaletteColor AsteroidLight = new PaletteColor(ColorRgba.FromHex("B53847"));
    public static readonly PaletteColor AsteroidSpecial = new PaletteColor(ColorRgba.FromHex("ed4a5d"));
    
    public static ColorRgba AsteroidVeryDarkColor => AsteroidVeryDark.ColorRgba;
    public static ColorRgba AsteroidDarkColor => AsteroidDark.ColorRgba;
    public static ColorRgba AsteroidMediumColor => AsteroidMedium.ColorRgba;
    public static ColorRgba AsteroidLightColor => AsteroidLight.ColorRgba;
    public static ColorRgba AsteroidSpecialColor => AsteroidSpecial.ColorRgba;
    #endregion
    
    

    public static readonly ColorPalette ColorPalette = new
    (
        BackgroundVeryDark,
        BackgroundDark,
        BackgroundMedium,
        BackgroundLight,
        BackgroundSpecial,
        
        TextVeryDark,
        TextDark,
        TextMedium,
        TextLight,
        TextSpecial,
        
        Experience,
        Resource,
        Hardshell,
        Evade,
        Stun,
        
        ShipVeryDark,
        ShipDark,
        ShipMedium,
        ShipLight,
        ShipSpecial,
        
        AsteroidVeryDark,
        AsteroidDark,
        AsteroidMedium,
        AsteroidLight,
        AsteroidSpecial
    );

    public static readonly ColorScheme Default =
    new(
        BackgroundVeryDark.Clone(),
        BackgroundDark.Clone(),
        BackgroundMedium.Clone(),
        BackgroundLight.Clone(),
        BackgroundSpecial.Clone(),
        
        TextVeryDark.Clone(),
        TextDark.Clone(),
        TextLight.Clone(),
        TextMedium.Clone(),
        TextSpecial.Clone(),
        
        Experience.Clone(),
        Resource.Clone(),
        Hardshell.Clone(),
        Evade.Clone(),
        Stun.Clone(),
        
        ShipVeryDark.Clone(),
        ShipDark.Clone(),
        ShipMedium.Clone(),
        ShipLight.Clone(),
        ShipSpecial.Clone(),
        
        AsteroidVeryDark.Clone(),
        AsteroidDark.Clone(),
        AsteroidMedium.Clone(),
        AsteroidLight.Clone(),
        AsteroidSpecial.Clone()
    );


}
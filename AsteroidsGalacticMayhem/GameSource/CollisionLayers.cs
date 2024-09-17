using ShapeEngine.Core.Structs;

namespace AsteroidsGalacticMayhem.GameSource;

public static class CollisionLayers
{
    public static readonly uint LayerPrev1 = 1;
    public static readonly uint LayerPrev2 = 2;
    public static readonly uint LayerPrev3 = 4;
    
    public static readonly uint Ships = 8;
    public static readonly uint Asteroids = 16;
    public static readonly uint Collectible = 32;
    public static readonly uint World = 64;
    public static readonly uint ShipBullets = 128;
    public static readonly uint AsteroidBullets = 256;
    
    public static readonly uint Layer1 = 512;
    public static readonly uint Layer2 = 1024;
    public static readonly uint Layer3 = 2048;
    public static readonly uint Layer4 = 4096;
    public static readonly uint Layer5 = 8192;
    public static readonly uint Layer6 = 16384;
    public static readonly uint Layer7 = 32768;
    public static readonly uint Layer8 = 65536;
    public static readonly uint Layer9 = 131072;
    public static readonly uint Layer10 = 262144;
    public static readonly uint Layer11 = 524288;
    public static readonly uint Layer12 = 1048576;
    public static readonly uint Layer13 = 2097152;
    public static readonly uint Layer14 = 4194304;
    public static readonly uint Layer15 = 8388608;
    public static readonly uint Layer16 = 16777216;
}
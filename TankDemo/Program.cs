using System.Drawing;
using System.Numerics;
using ShapeEngine.Color;
using ShapeEngine.Core;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Input;
using ShapeEngine.Lib;
using Size = ShapeEngine.Core.Structs.Size;

namespace TankDemo;


public static class Program
{
    public static void Main(string[] args)
    {
        var gameSettings = new GameSettings()
        {
            DevelopmentDimensions = new Dimensions(1920, 1080),
            MultiShaderSupport = false
        };
        var game = new TankDemoGame(gameSettings, WindowSettings.Default);
        game.Run();
    }
}

public static class Colors
{
    public static ColorRgba TankBody = new ColorRgba(Color.DarkGreen);
    public static ColorRgba TankTurret = new ColorRgba(Color.Green);
    public static ColorRgba TankTrack = new ColorRgba(Color.SlateGray);
    public static ColorRgba TankTrack2 = new ColorRgba(Color.Gray);
    public static ColorRgba TankOutline = new(Color.Black);
    public static ColorRgba Background = new(Color.SaddleBrown);
    public static ColorRgba PatchColor = new(Color.SandyBrown);
    public static ColorRgba Bounds = new(Color.LimeGreen);
}
public class TankDemoGame : Game
{

    private Tank tank;
    private Rect levelBounds;
    private const int patchCount = 500;
    private List<Rect> grassPatches = new(patchCount);
    
    
    public TankDemoGame(GameSettings gameSettings, WindowSettings windowSettings) : base(gameSettings, windowSettings)
    {
        levelBounds = new Rect(new Vector2(), new Size(2500, 2500), new Vector2(0.5f, 0.5f));
        tank = new(new(new(), 0f, new(150, 250)));
        for (int i = 0; i < patchCount; i++)
        {
            var p = levelBounds.GetRandomPointInside();
            var s = new Size(ShapeRandom.RandF(levelBounds.Width * 0.01f, levelBounds.Width * 0.1f),
                ShapeRandom.RandF(levelBounds.Height * 0.01f, levelBounds.Height * 0.1f));
            var r = new Rect(p, s, new Vector2(0.5f));
            grassPatches.Add(r);
        }
    }

    protected override void BeginRun()
    {
        BackgroundColorRgba = Colors.Background;
    }

    protected override void Update(GameTime time, ScreenInfo game, ScreenInfo ui)
    {
        tank.Update(time.Delta, game.MousePos);


    }

    protected override void DrawGame(ScreenInfo game)
    {
        foreach (var patch in grassPatches)
        {
            patch.Draw(Colors.PatchColor);
        }
        tank.Draw();
        
        levelBounds.DrawLines(levelBounds.Size.Max() * 0.05f, Colors.Bounds);
    }
}

public class Tank
{

    public Transform2D Transform;
    public TankBody Body;
    public TankTurret Turret;
    public TankTrack LeftTrack;
    public TankTrack RightTrack;
    public float AimRotationRad = 0f;

    private float speed = 0f;
    private float accelTimer = 0f;
    private const float maxAccelTime = 2f;
    
    private const float maxSpeed = 600;
    private const float rotSpeedRad = 180 * ShapeMath.DEGTORAD;
    private const float turretRotSpeedRad = 90 * ShapeMath.DEGTORAD;

    public Tank(Transform2D transform)
    {
        Transform = transform;
        Body = new(new Transform2D(new(), 0f, new(0.6f, 1f)));
        Turret = new(new Transform2D(new(0f, -transform.Size.Height * 0.15f), 0f, new(0.75f, 1f)));
        LeftTrack = new(new Transform2D(new Vector2(transform.Size.Width * 0.375f, 0f), 0f, new Size(0.2f, 1f)));
        RightTrack = new(new Transform2D(new Vector2(-transform.Size.Width * 0.375f, 0f), 0f, new Size(0.2f, 1f)));
        
    }

    public void Update(float dt, Vector2 mousePos)
    {
        int rotdir = 0;
        
        if (ShapeKeyboardButton.D.GetInputState().Down)
        {
            rotdir = 1;
        }
        else if (ShapeKeyboardButton.A.GetInputState().Down)
        {
            rotdir = -1;
        }

        Transform = Transform.RotateByRad(rotSpeedRad * rotdir * dt);

        int throttle = 0;
        if (ShapeKeyboardButton.W.GetInputState().Down)
        {
            throttle = 1;
        }
        else if (ShapeKeyboardButton.S.GetInputState().Down)
        {
            throttle = -1;
        }

        // if (throttle == 0)
        // {
        //     if (accelTimer < 0) accelTimer = maxAccelTime / 4;
        //     accelTimer -= dt;
        //     if (accelTimer <= 0f) accelTimer = -1f;
        //     
        //     float f = accelTimer / (maxAccelTime / 4);
        //     speed = ShapeMath.LerpFloat(maxSpeed, 0f, f);
        // }
        // else
        // {
        //     if (accelTimer < 0) accelTimer = maxAccelTime;
        //
        //     accelTimer -= dt;
        //     if (accelTimer <= 0f) accelTimer = -1f;
        //     float f = 1f - (accelTimer / maxAccelTime);
        //     speed = ShapeMath.LerpFloat(0f, maxSpeed, f);
        // }
        speed = maxSpeed;
        

        var movement = new Vector2(speed, 0f).Rotate(Transform.RotationRad + 90 * ShapeMath.DEGTORAD);
        Transform = Transform.MoveBy(movement * dt * throttle);

        int turretRotDir = 0;
        if (ShapeKeyboardButton.RIGHT.GetInputState().Down)
        {
            turretRotDir = 1;
        }
        else if (ShapeKeyboardButton.LEFT.GetInputState().Down)
        {
            turretRotDir = -1;
        }
        // AimRotationRad = (mousePos - Transform.Position).AngleRad() - 90 * ShapeMath.DEGTORAD;
        AimRotationRad += turretRotSpeedRad * turretRotDir * dt * (throttle != 0 ? 0.5f : 1f);
        
        Body.Update(dt, Transform);
        Turret.Update(dt, Transform, AimRotationRad);
        LeftTrack.Update(dt, Transform);
        RightTrack.Update(dt, Transform);
    }

    public void Draw()
    {
        float lt = Transform.Size.Max() * 0.02f;
        LeftTrack.Draw(lt);
        RightTrack.Draw(lt);
        Body.Draw(lt);
        Turret.Draw(lt);
    }
}

public class TankBody
{
    public Transform2D Transform;
    public Transform2D Offset;
    public TankBody(Transform2D offset)
    {
        Offset = offset;
    }
    public void Update(float dt, Transform2D parentTransform)
    {
        float rad = parentTransform.RotationRad + Offset.RotationRad;
        Transform = new Transform2D
        (
            parentTransform.Position + Offset.Position.Rotate(rad),
            rad,
            parentTransform.Size * Offset.Size
        );
    }
    public void Draw(float lineThickness)
    {
        var body =
            new Quad(Transform.Position, Transform.Size, Transform.RotationRad, new(0.5f));

        
        body.Draw(Colors.TankBody);
        body.DrawLines(lineThickness, Colors.TankOutline);
        
    }
}

public class TankTurret
{
    public Transform2D Transform;
    public Transform2D Offset;
    
    public TankTurret(Transform2D offset)
    {
        Offset = offset;
    }
    public void Update(float dt, Transform2D parentTransform, float aimRotationRad)
    {
        float rad = parentTransform.RotationRad + Offset.RotationRad;
        Transform = new Transform2D
        (
            parentTransform.Position + Offset.Position.Rotate(rad),
            aimRotationRad,
            parentTransform.Size * Offset.Size
        );
    }
    public void Draw(float lineThickness)
    {
        var towerSize = Transform.Size;
        var barrelSize = Transform.Size * new Vector2(0.2f, 0.8f);
        
        var tower = new Circle(Transform.Position, towerSize.Width / 2);
        var barrel = new Quad(Transform.Position, barrelSize,Transform.RotationRad, new(0.5f, 0f));

        
        barrel.Draw(Colors.TankTurret);
        barrel.DrawLines(lineThickness, Colors.TankOutline);
        
        tower.Draw(Colors.TankTurret);
        tower.DrawLines(lineThickness * 2, Colors.TankOutline);
    }
}

public class TankTrack
{
    
    public Transform2D Transform;
    public Transform2D Offset;
    
    public TankTrack(Transform2D offset)
    {
        Offset = offset;
    }
    public void Update(float dt, Transform2D parentTransform)
    {
        float rad = parentTransform.RotationRad + Offset.RotationRad;
        Transform = new Transform2D
        (
            parentTransform.Position + Offset.Position.Rotate(rad),
            rad,
            parentTransform.Size * Offset.Size
        );
    }
    public void Draw(float lineThickness)
    {
        // var trackBody =
        //     new Rect(Transform.Position, Transform.Size, new(0.5f, 0.5f))
        //         .Rotate(Transform.RotationRad * ShapeMath.RADTODEG, new Vector2(0.5f, 0.5f));
        var trackBody =
            new Quad(Transform.Position, Transform.Size, Transform.RotationRad, new(0.5f, 0.5f));
        trackBody.Draw(Colors.TankTrack);
        trackBody.DrawLines(lineThickness, Colors.TankOutline);
    }
}
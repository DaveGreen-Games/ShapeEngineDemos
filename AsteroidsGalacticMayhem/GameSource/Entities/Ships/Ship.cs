using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using AsteroidsGalacticMayhem.GameSource.Data;
using AsteroidsGalacticMayhem.GameSource.InputSystem;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Input;
using ShapeEngine.Lib;
using ShapeEngine.Stats;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Ships;

public abstract class Ship : Entity
{
    protected ShipData Data { get; init; }

    protected float CurrentSpeed = 0f;
    protected Vector2 CurrentDirection = new();
    
    private InputAction MoveHorizontal { get; set; } = new InputAction();
    private InputAction MoveVertical { get; set; } = new InputAction();
    
    protected Ship(ShipData data)
    {
        Data = data;
        SetupInput();
    }

    public void Spawn(Vector2 position, Vector2 direction)
    {
        Transform = new Transform2D(position, direction.AngleRad(), new Size(Data.Size), 1f);
        CurrentSpeed = 0f;
        CurrentDirection = direction;
        OnSpawned(position, direction);
    }

    private void SetupInput()
    {
        var moveHorKb = new InputTypeKeyboardButtonAxis(ShapeKeyboardButton.A, ShapeKeyboardButton.D);
        MoveHorizontal = new InputAction(Inputs.ShipMovementTag, moveHorKb);
        
        var moveVerKb = new InputTypeKeyboardButtonAxis(ShapeKeyboardButton.W, ShapeKeyboardButton.S);
        MoveVertical = new InputAction(Inputs.ShipMovementTag, moveVerKb);
    }

    private void OnSpawned(Vector2 position, Vector2 direction)
    {
        
    }

    private void UpdateInput(float dt)
    {
        MoveHorizontal.Update(dt);
        MoveVertical.Update(dt);
    }
    private Vector2 GetMovementInput()
    {
        var h = MoveHorizontal.Consume();
        var v = MoveVertical.Consume();
        return new Vector2(h.AxisRaw, v.AxisRaw);
    }

    private void Move(Vector2 direction, float dt)
    {
        if (direction == Vector2.Zero)
        {
            if (CurrentSpeed > 0f)
            {
                CurrentSpeed -= dt * Data.Deceleration;
                if(CurrentSpeed <= 0f) CurrentSpeed = 0f;
            }
        }
        else
        {
            // CurrentDirection = direction.Normalize();
            CurrentDirection = CurrentDirection.ExpDecayLerp(direction.Normalize(), 0.95f, dt);
            if (CurrentSpeed < Data.Speed)
            {
                CurrentSpeed += dt * Data.Acceleration;
                if(CurrentSpeed >= Data.Speed) CurrentSpeed = Data.Speed;
            }
        }

        if (CurrentSpeed > 0f)
        {
            Transform = Transform.ChangePosition(CurrentDirection * CurrentSpeed * dt);
            Transform = Transform.SetRotationRad(CurrentDirection.AngleRad());
        }
        
    }
    
    // public override Rect GetBoundingBox()
    // {
    //     throw new NotImplementedException();
    // }
    //
    public override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        UpdateInput(time.Delta);
        var movementInput = GetMovementInput();
        Move(movementInput, time.Delta);
    }
    //
    // public override void DrawGame(ScreenInfo game)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override void DrawGameUI(ScreenInfo gameUi)
    // {
    //     throw new NotImplementedException();
    // }
}

public class ShipGunslinger() : Ship(DataSheet.ShipGunslinger)
{
    public override Rect GetBoundingBox()
    {
        throw new NotImplementedException();
    }

    public override void DrawGame(ScreenInfo game)
    {
        var a = Transform.Position + CurrentDirection * Data.Size * 0.5f;
        var back = (Transform.Position - CurrentDirection * Data.Size * 0.5f) - Transform.Position;
        var b = Transform.Position + back.RotateDeg(-45);
        var c = Transform.Position + back.RotateDeg(45);
        
        var tri = new Triangle(a, b, c);
        tri.Draw(Colors.ShipMediumColor);
        
        var drawInfo = new LineDrawingInfo(Data.Size * 0.05f, Colors.ShipLightColor, LineCapType.CappedExtended, 4);
        tri.DrawLines(drawInfo);
        
        Transform.Position.Draw(Data.Size * 0.1f, Colors.ShipSpecialColor, 16);
    }

    public override void DrawGameUI(ScreenInfo gameUi)
    {
        throw new NotImplementedException();
    }
}
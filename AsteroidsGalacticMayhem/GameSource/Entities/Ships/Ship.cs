using System.Numerics;
using AsteroidsGalacticMayhem.GameSource.ColorSystem;
using AsteroidsGalacticMayhem.GameSource.Data;
using AsteroidsGalacticMayhem.GameSource.InputSystem;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Interfaces;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Input;
using ShapeEngine.Lib;
using ShapeEngine.Stats;

namespace AsteroidsGalacticMayhem.GameSource.Entities.Ships;

public abstract class Ship : Entity, ICameraFollowTarget
{
    protected ShipData Data { get; init; }

    protected float CurrentSpeed = 0f;
    protected Vector2 CurrentDirection = new();
    
    protected CircleCollider CollectionCircle { get; init; }
    
    
    private InputAction MoveHorizontal { get; set; } = new InputAction();
    private InputAction MoveVertical { get; set; } = new InputAction();
    
    protected Ship(ShipData data)
    {
        Data = data;
        
        CollectionCircle = new CircleCollider(new Transform2D(new(), 0f, new Size(0f), 1f));
        CollectionCircle.ComputeCollision = false;
        CollectionCircle.ComputeIntersections = false;
        CollectionCircle.CollisionLayer = CollisionLayers.Ships;
        AddCollider(CollectionCircle);
        
        SetupInput();
    }

    public override void Spawn(SpawnInfo spawnInfo)
    {
        var position = spawnInfo.Position;
        var direction = spawnInfo.Direction;
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
            var curAngleRad = CurrentDirection.AngleRad();
            var targetAngleRad = direction.AngleRad();
            if (Math.Abs(targetAngleRad - curAngleRad) > 0.0001f)
            {
                var shortestAngleRad = ShapeMath.GetShortestAngleRad(curAngleRad, targetAngleRad);
                var angleSign = Math.Sign(shortestAngleRad);
                var maxTurningSpeedRad = MathF.Abs(shortestAngleRad);
                var turningSpeedRad = Data.TurningSpeed * ShapeMath.DEGTORAD;
                var turnAmountRad = turningSpeedRad * dt;
                if(turnAmountRad > maxTurningSpeedRad) turnAmountRad = maxTurningSpeedRad;
                var newAngleRad = curAngleRad + turnAmountRad * angleSign;
                CurrentDirection = ShapeVec.VecFromAngleRad(newAngleRad);
            }
            
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
        base.Update(time, game, gameUi, ui);
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
    public void FollowStarted()
    {
        
    }

    public void FollowEnded()
    {
        
    }

    public Vector2 GetCameraFollowPosition() => Transform.Position;
}

public class ShipGunslinger() : Ship(DataSheet.ShipGunslinger)
{
    // public override Rect GetBoundingBox()
    // {
    //     return new Rect(Transform.Position, new Size(Data.Size), new AnchorPoint(0.5f, 0.5f));
    // }

    
    public override void DrawGame(ScreenInfo game)
    {
        
        foreach (var col in Colliders)
        {
            if(col is CircleCollider cCol) ShapeDrawing.DrawCircleLines(
                cCol.CurTransform.Position, 
                cCol.CurTransform.ScaledSize.Radius, 
                3f,
                Colors.ShipLightColor, 
                4f);
        }

        var size = Transform.ScaledSize.Length;
        var a = Transform.Position + CurrentDirection * size / 2;
        var back = (Transform.Position - CurrentDirection * size / 2) - Transform.Position;
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
        
    }
}
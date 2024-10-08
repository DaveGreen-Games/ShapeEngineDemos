using System.Numerics;
using AsteroidsDemo.GameSource.ColorSystem;
using AsteroidsDemo.GameSource.Data;
using AsteroidsDemo.GameSource.Entities.Collectibles;
using AsteroidsDemo.GameSource.InputSystem;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Interfaces;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Input;
using ShapeEngine.Lib;
using ShapeEngine.Stats;

namespace AsteroidsDemo.GameSource.Entities.Ships;

public abstract class Ship : Entity, ICameraFollowTarget, ICollector
{
    public static readonly float BoundsCollisionForce = 1500;
    
    
    protected ShipData Data { get; init; }

    protected float CurrentSpeed = 0f;
    protected Vector2 CurrentDirection = new();
    
    protected CircleCollider CollectionCircle { get; init; }
    
    
    private InputAction MoveHorizontal { get; set; } = new InputAction();
    private InputAction MoveVertical { get; set; } = new InputAction();

    
    protected Ship(ShipData data)
    {
        Data = data;
        
        CollectionCircle = new CircleCollider(new Transform2D(new(), 0f, new Size(data.CollectorSize), 1f));
        CollectionCircle.Scales = false;
        CollectionCircle.ComputeCollision = true;
        CollectionCircle.ComputeIntersections = false;
        // CollectionCircle.CollisionLayer = CollisionLayers.Ships;
        CollectionCircle.CollisionMask = new BitFlag(CollisionLayers.Collectible);
        CollectionCircle.OnCollision += OnCollection;
        AddCollider(CollectionCircle);
        
        SetupInput();

        Drag = data.Drag;
    }

    private void OnCollection(Collider collider, CollisionInformation info)
    {
        
        foreach (var col in info.Collisions)
        {
            if (col.Other.Parent is Collectible collectible)
            {
                collectible.Collect(this);
            }
        }
        
        
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

    public override void BoundsTouched(Intersection intersection, Rect bounds)
    {
        var dir = CurrentDirection.Flip(); // CurrentDirection.Reflect(intersection.CollisionSurface.Normal.Flip());
        Velocity += BoundsCollisionForce * dir;
        
        
        // Velocity += Data.Speed * 5 * intersection.CollisionSurface.Normal.Flip();
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
    
    public bool CanFollow() => !IsDead;
    public Vector2 GetFollowPosition() => Transform.Position;
    public void ReceiveCollectible(float amount, CollectibleType type)
    {
        
    }
}

public class ShipGunslinger() : Ship(DataSheet.ShipGunslinger)
{
    // public override Rect GetBoundingBox()
    // {
    //     return new Rect(Transform.Position, new Size(Data.Size), new AnchorPoint(0.5f, 0.5f));
    // }

    
    public override void DrawGame(ScreenInfo game)
    {
        // foreach (var col in Colliders)
        // {
        //     if(col is CircleCollider cCol) ShapeDrawing.DrawCircleLines(
        //         cCol.CurTransform.Position, 
        //         cCol.CurTransform.ScaledSize.Radius, 
        //         1.5f,
        //         Colors.ShipLightColor, 
        //         4f);
        // }

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





/*
private class Ship : ICameraFollowTarget
   {
       // public Circle Hull { get; private set; }
       private Pathfinder pathfinder;
       private Vector2 chasePosition;
       private int lastTraversableNodeIndex = -1;
       private Triangle hull;
       private readonly float shipSize;
       private Vector2 pivot;
       private Vector2 movementDir;
       private float angleRad;
       private float stopTimer = 0f;
       private float accelTimer = 0f;
       private const float AccelTime = 0.25f;
       private const float StopTime = 0.5f;
       public const float Speed = 750;
   
       
       private readonly PaletteColor hullColor = Colors.PcCold;
   
       private InputAction iaMoveHor;
       private InputAction iaMoveVer;
       
       
       private void SetupInput()
       {
           var moveHorKb = new InputTypeKeyboardButtonAxis(ShapeKeyboardButton.A, ShapeKeyboardButton.D);
           var moveHorGp = new InputTypeGamepadAxis(ShapeGamepadAxis.LEFT_X, 0.1f, ModifierKeyOperator.Or, GameloopExamples.ModifierKeyGamepadReversed);
           // var moveHorMW = new InputTypeMouseWheelAxis(ShapeMouseWheelAxis.HORIZONTAL, 0.2f, ModifierKeyOperator.Or, GameloopExamples.ModifierKeyMouseReversed);
           // var moveHorM = new InputTypeMouseAxis(ShapeMouseAxis.HORIZONTAL, 0.2f, ModifierKeyOperator.Or, GameloopExamples.ModifierKeyMouseReversed);
           iaMoveHor = new(moveHorKb, moveHorGp);
           
           var moveVerKb = new InputTypeKeyboardButtonAxis(ShapeKeyboardButton.W, ShapeKeyboardButton.S);
           var moveVerGp = new InputTypeGamepadAxis(ShapeGamepadAxis.LEFT_Y, 0.1f, ModifierKeyOperator.Or, GameloopExamples.ModifierKeyGamepadReversed);
           // var moveVerMW = new InputTypeMouseWheelAxis(ShapeMouseWheelAxis.VERTICAL, 0.2f, ModifierKeyOperator.Or, GameloopExamples.ModifierKeyMouseReversed);
           // var moveVerM = new InputTypeMouseAxis(ShapeMouseAxis.VERTICAL, 0.2f, ModifierKeyOperator.Or, GameloopExamples.ModifierKeyMouseReversed);
           iaMoveVer = new(moveVerKb, moveVerGp);
       }
       public Ship(Vector2 pos, float shipSize, Pathfinder pathfinder)
       {
           this.pathfinder = pathfinder;
           this.shipSize = shipSize;
           CreateHull(pos, shipSize);
           chasePosition = hull.A;
           SetupInput();
       }

       public Polygon GetCutShape(float minSize)
       {
           var s = MathF.Max(minSize, shipSize);
           return Polygon.Generate(pivot, 12, s, s * 2);
       }

       public bool Overlaps(Polygon poly)
       {
           return hull.OverlapShape(poly);
       }
       private void CreateHull(Vector2 pos, float size)
       {
           var a = pos + new Vector2(size, 0);
           var b = pos + new Vector2(-size, -size * 0.75f);
           var c = pos + new Vector2(-size, size * 0.75f);
           pivot = pos;
           hull = new Triangle(a, b, c);
       }
       
       public string GetInputDescription(InputDeviceType inputDeviceType)
       {
           if (inputDeviceType == InputDeviceType.Mouse)
           {
               return "Move Horizontal [Mx] Vertical [My]";
           }
           
           string hor = iaMoveHor.GetInputTypeDescription(inputDeviceType, true, 1, false, false);
           string ver = iaMoveVer.GetInputTypeDescription(inputDeviceType, true, 1, false, false);
           return $"Move Horizontal [{hor}] Vertical [{ver}]";
       }
       public void Reset(Vector2 pos, float size)
       {
           CreateHull(pos, size);
           chasePosition = hull.A;
           movementDir = new(0, 0);
           angleRad = 0f;
       }

       private void Move(float dt, Vector2 dir, float speed)
       {
           movementDir = dir; // amount.Normalize();
           var newAngle = movementDir.AngleRad();
           var angleDif = ShapeMath.GetShortestAngleRad(angleRad, newAngle);
           var movement = movementDir * speed * dt;

           hull = hull.ChangePosition(movement);
           pivot += movement;

           var angleMovement = MathF.Sign(angleDif) * dt * MathF.PI * 4f;
           if (MathF.Abs(angleMovement) > MathF.Abs(angleDif))
           {
               angleMovement = angleDif;
           }
           angleRad += angleMovement;
           hull = hull.ChangeRotation(angleMovement, pivot);
       }
       public void Update(float dt)
       {
           iaMoveHor.Gamepad = GAMELOOP.CurGamepad;
           iaMoveHor.Update(dt);
           
           iaMoveVer.Gamepad = GAMELOOP.CurGamepad;
           iaMoveVer.Update(dt);
           
           
           if (ShapeInput.CurrentInputDeviceType == InputDeviceType.Mouse)
           {
               // var mousePos = GAMELOOP.GameScreenInfo.MousePos;
               // var camera = GAMELOOP.Camera;
               // var dir = mousePos - camera.BasePosition;// hull.GetCentroid();
               //
               // float minDis = 100 * camera.ZoomFactor;
               // float maxDis = 350 * camera.ZoomFactor;
               // float minDisSq = minDis * minDis;
               // float maxDisSq = maxDis * maxDis;
               // var lsq = dir.LengthSquared();
               // if (lsq <= minDisSq) dir = new();
               // else if (lsq >= maxDisSq) dir = dir.Normalize();
               // else
               // {
               //     var f = (lsq - minDisSq) / (maxDisSq - minDisSq);
               //     dir = dir.Normalize() * f;
               // }

               var dir = CalculateMouseMovementDirection(GAMELOOP.GameScreenInfo.MousePos, GAMELOOP.Camera);
               if (dir.LengthSquared() > 0f)
               {
                   stopTimer = 0f;

                   float accelF = 1f;
                   if (accelTimer <= AccelTime)
                   {
                       accelTimer += dt;
                       accelF = accelTimer / AccelTime;
                   }
               
                   Move(dt, dir, Speed * accelF * accelF);
               }
               else
               {
                   accelTimer = 0f;
                   if (stopTimer <= StopTime)
                   {
                       stopTimer += dt;
                       float stopF = 1f - (stopTimer / StopTime);
                       Move(dt, movementDir, Speed * stopF * stopF);
                   }
                   else movementDir = new();
               }
           }
           else
           {
               Vector2 dir = new(iaMoveHor.State.AxisRaw, iaMoveVer.State.AxisRaw);
               float lsq = dir.LengthSquared();
               if (lsq > 0f)
               {
                   stopTimer = 0f;

                   float accelF = 1f;
                   if (accelTimer <= AccelTime)
                   {
                       accelTimer += dt;
                       accelF = accelTimer / AccelTime;
                   }
               
                   Move(dt, dir.Normalize(), Speed * accelF * accelF);
               }
               else
               {
                   accelTimer = 0f;
                   if (stopTimer <= StopTime)
                   {
                       stopTimer += dt;
                       float stopF = 1f - (stopTimer / StopTime);
                       Move(dt, movementDir, Speed * stopF * stopF);
                   }
                   else movementDir = new();
               }
           }
           
           
           

           var nodeIndex = pathfinder.GetIndex(hull.A);
           if (lastTraversableNodeIndex != nodeIndex && pathfinder.IsTraversable(nodeIndex))
           {
               lastTraversableNodeIndex = nodeIndex;
               var r = pathfinder.GetRect(nodeIndex);
               chasePosition = r.Center;
           }
       }
       public void Draw()
       {
           // var rightThruster = movementDir.RotateDeg(-25);
           // var leftThruster = movementDir.RotateDeg(25);
           // ShapeDrawing.DrawCircle(Hull.Center - rightThruster * Hull.Radius, Hull.Radius / 6, outlineColor.ColorRgba, 12);
           // ShapeDrawing.DrawCircle(Hull.Center - leftThruster * Hull.Radius, Hull.Radius / 6, outlineColor.ColorRgba, 12);
           // Hull.Draw(hullColor.ColorRgba);
           // ShapeDrawing.DrawCircle(Hull.Center + movementDir * Hull.Radius * 0.66f, Hull.Radius * 0.33f, cockpitColor.ColorRgba, 12);
           //
           hull.DrawLines(4f, hullColor.ColorRgba);
       }
       
       public void FollowStarted()
       {
           
       }
       public void FollowEnded()
       {
           
       }

       public Vector2 GetPosition() => hull.A;
       public Vector2 GetChasePosition() => chasePosition; //GetCentroid();
       public Vector2 GetPredictedChasePosition(float seconds = 1f)
       {
           if (movementDir.LengthSquared() <= 0f) return GetChasePosition();
           
           
           return hull.GetCentroid() + movementDir * Speed * seconds;
       }

       public Vector2 GetCameraFollowPosition()
       {
           return hull.GetCentroid();
       }
   }

*/
using System.Numerics;
using AsteroidsDemo.GameSource.Entities.Collectibles;
using ShapeEngine.Core;
using ShapeEngine.Core.Collision;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;
using ShapeEngine.Lib;

namespace AsteroidsDemo.GameSource.Entities;

/*

- Entities have to check for collision with bounds and call a function on the bounds class!
- Otherwise the normals of the collision are wrong and facing the bounds instead of facing the entity.
- Circumvent drag for stunned period -> velocity should slowly go towards the movement direction/speed -> fraction of how far along the stun is could be used as a factor for blending

*/

public abstract class Entity : CollisionObject
{
    protected float CollisionForce = 1500;
    public bool Stunned => StunTimer > 0f;
    
    public float StunDuration { get; private set; } = 0;
    public float StunTimer { get; private set; } = 0;
    public float StunF => StunDuration <= 0 ? 0 : 1f - (StunTimer / StunDuration);
    public float StunResistance { get; protected set; } = 1f; //0 = cant be stunned, 1 = fully stunned, > 1 = stunned for longer

    public float MaxStunDuration { get; protected set; } = 2f;
    public Vector2 CurVelocity => Velocity + (CurMovementDirection * CurMovementSpeed);// Stunned ? Velocity : CurMovementDirection * CurMovementSpeed;
    public Vector2 CurMovementDirection { get; protected set; } = Vector2.Zero;
    public float CurMovementSpeed { get; protected set; } = 0f;


    public bool Stun(float duration, Vector2 direction, float magnitude)
    {
        var stunDuration = duration * StunResistance;
        if (stunDuration <= 0) return false;

        if (magnitude > 0)
        {
            var force = direction * magnitude * StunResistance;
            if(Velocity.X > 0 || Velocity.Y > 0) force += Velocity.Flip() * 0.25f;
            Velocity = Vector2.Zero;
            AddImpulse(force);
        }

        StunTimer += stunDuration;
        if (StunTimer > MaxStunDuration) StunTimer = MaxStunDuration;
        StunDuration = StunTimer;
        
        CurMovementDirection = Vector2.Zero;
        CurMovementSpeed = 0f;
        return true;
    }

    public override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui)
    {
        base.Update(time, game, gameUi, ui);
        UpdateStunTimer(time.Delta);
        Move(time.Delta);
    }


    public abstract void Spawn(SpawnInfo spawnInfo);
    public abstract void BoundsTouched(Intersection surface, Rect bounds);
    protected abstract void UpdateMovement(float dt);


    private void Move(float dt)
    {
        UpdateMovement(dt);

        if (Stunned)
        {
            CurMovementSpeed *= StunF;
        }

        
        if (CurMovementSpeed > 0f)
        {
            Transform = Transform.ChangePosition(CurMovementDirection * CurMovementSpeed * dt);
            Transform = Transform.SetRotationRad(CurMovementDirection.AngleRad());
        }
    }


    private void UpdateStunTimer(float dt)
    {
        if (StunTimer > 0)
        {
            StunTimer -= dt;
            if (StunTimer <= 0)
            {
                StunTimer = 0;
                StunDuration = 0;
            }
        }
    }
}


/*public Vector2 CurMovementVelocity => Stunned ? Velocity : CurMovementDirection * CurMovementSpeed;
   protected Vector2 CurMovementDirection { get; set; } = Vector2.Zero;
   protected float CurMovementSpeed { get; set; } = 0;


   protected abstract Vector2 GetMovementDirection(float dt);
   protected abstract void UpdateMovement(Vector2 movement, float dt);



   protected void Move(float dt)
   {
       if (Stunned)
       {
           var speed = Velocity.Length();
           var heading = speed > 0 ? Velocity / speed : Vector2.Zero;

           CurMovementDirection = heading;
           CurMovementSpeed = speed;

           return;
       }

       var movementDirection = GetMovementDirection(dt);
       UpdateMovement(movementDirection, dt);


       if (CurMovementSpeed > 0f)
       {
           Transform = Transform.ChangePosition(CurMovementDirection * CurMovementSpeed * dt);
           Transform = Transform.SetRotationRad(CurMovementDirection.AngleRad());
       }
   }*/
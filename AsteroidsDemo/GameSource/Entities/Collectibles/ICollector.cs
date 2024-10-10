using System.Numerics;

namespace AsteroidsDemo.GameSource.Entities.Collectibles;

public interface ICollector
{
    public bool CanFollow();
    public Vector2 GetFollowPosition();
    public void ReceiveCollectible(float amount, CollectibleType type);
}
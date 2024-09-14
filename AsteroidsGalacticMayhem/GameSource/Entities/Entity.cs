using ShapeEngine.Core;
using ShapeEngine.Core.Shapes;
using ShapeEngine.Core.Structs;

namespace AsteroidsGalacticMayhem.GameSource.Entities;

public abstract class Entity : GameObject
{
    public bool Stunned { get; private set; } = false;
    
    
    public abstract override Rect GetBoundingBox();

    public abstract override void Update(GameTime time, ScreenInfo game, ScreenInfo gameUi, ScreenInfo ui);

    public abstract override void DrawGame(ScreenInfo game);

    public abstract override void DrawGameUI(ScreenInfo gameUi);
}
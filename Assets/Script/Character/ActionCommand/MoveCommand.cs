using UnityEngine;

public class MoveCommand: ActionCommand
{
    private readonly Vector2 _direction;
    
    public MoveCommand(Vector2 direction)
    {
        _direction = direction;
    }

    public override void Perform(ActionCommandController controller)
    {
        
    }
}
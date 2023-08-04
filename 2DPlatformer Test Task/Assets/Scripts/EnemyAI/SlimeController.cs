using UnityEngine;

public class SlimeController : EnemyControllerBase
{
    protected override void Start()
    {
        base.Start();
        movementVector = Vector2.right;
    }
    
    //just inherits base behaviour
}

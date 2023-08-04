using UnityEngine;

public class SlimeController : EnemyControllerBase
{
    protected override void Start()
    {
        base.Start();
        movementVector = Vector2.right * (transform.localScale.x > 0 ? -1 : 1);
    }
    
    //just inherits base behaviour
}

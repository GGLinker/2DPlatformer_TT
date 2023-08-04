using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MushroomController : EnemyControllerBase
{
    [SerializeField] private CollisionDetection triggerSphere;

    [SerializeField] private string IsRunningAnimatorParameterName;
    [SerializeField] private string IsDeathAnimatorParameterName;
    
    [SerializeField] private AnimationClip deathAnimationClip;

    private BoxCollider2D collider;
    private Transform playerAsTarget;
    private Coroutine chaseCoroutineHandler;

    private bool destroying;

    protected override void Start()
    {
        base.Start();
        collider = transform.GetComponent<BoxCollider2D>();
        triggerSphere.OnCollisionChanged += ChasePlayer;
        playerAsTarget = null;
    }

    protected override void MovementVectorUpdated()
    {
        enemyAnimator.SetBool(IsRunningAnimatorParameterName, movementVector != Vector2.zero);
    }
    
    protected override void DetectPrecipice(bool groundDetected, GameObject other)
    {
        //do nothing
    }
    protected override void DetectObstacle(bool groundDetected, GameObject other)
    {
        if (playerAsTarget != null)
        {
            base.DetectObstacle(groundDetected, other);
        }
    }
    private void ChasePlayer(bool groundDetected, GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (groundDetected)
            {
                playerAsTarget = other.transform;
                chaseCoroutineHandler = StartCoroutine(UpdateMovementVector());
            }
            else
            {
                playerAsTarget = null;
                movementVector = Vector2.zero;
                if (chaseCoroutineHandler != null)
                {
                    StopCoroutine(chaseCoroutineHandler);
                }
            }
        }
    }

    private IEnumerator UpdateMovementVector()
    {
        movementVector = (transform.position.x - playerAsTarget.position.x) > 0 ? Vector2.left : Vector2.right;
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator OnCollisionEnter2D(Collision2D col)
    {
        if (!destroying &&
            col.gameObject.CompareTag("Player") &&
            !col.collider.isTrigger &&
            col.gameObject.transform.position.y > transform.position.y &&
            Mathf.Abs(col.gameObject.transform.position.x - transform.position.x) <= collider.size.x * 2f)
        {
            destroying = true;
            //death
            movementVector = Vector2.zero;
            enemyAnimator.SetBool(IsDeathAnimatorParameterName, true);
            yield return new WaitForSeconds(deathAnimationClip.length);
            
            Destroy(gameObject);
        }
    }
}

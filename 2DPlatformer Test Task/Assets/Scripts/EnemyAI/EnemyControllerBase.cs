using UnityEngine;

public class EnemyControllerBase : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private CollisionDetection precipiceDetector;
    [SerializeField] private CollisionDetection obstacleDetector;

    protected Animator enemyAnimator;
    protected Rigidbody2D rigidbodyComponent;

    private Vector2 _movementVector;
    protected Vector2 movementVector
    {
        get => _movementVector;
        set
        {
            _movementVector = value;
            MovementVectorUpdated();
        }
    }
    protected virtual void MovementVectorUpdated() {} 

    protected virtual void Start()
    {
        enemyAnimator = transform.GetComponent<Animator>();
        rigidbodyComponent = transform.GetComponent<Rigidbody2D>();

        precipiceDetector.OnCollisionChanged += DetectPrecipice;
        obstacleDetector.OnCollisionChanged += DetectObstacle;
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = movementVector * movementSpeed;
    }

    protected virtual void DetectPrecipice(bool groundDetected, GameObject other)
    {
        if (other.CompareTag("Player")) return;
        if (!groundDetected) TurnAround();
    }
    protected virtual void DetectObstacle(bool groundDetected, GameObject other)
    {
        if (other.CompareTag("Player")) return;
        if (!other.CompareTag("Player") && groundDetected) TurnAround();
    }

    protected virtual void TurnAround()
    {
        movementVector *= -1;
        transform.localScale = new Vector3(
            transform.localScale.x * -1f,
            transform.localScale.y, 
            transform.localScale.z
        );
    }
}

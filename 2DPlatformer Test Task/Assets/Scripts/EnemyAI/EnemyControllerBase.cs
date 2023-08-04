using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControllerBase : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private CollisionDetection precipiceDetector;
    [SerializeField] private CollisionDetection obstacleDetector;

    [SerializeField] private Animator GameOverAnimator;
    private static readonly int GameOverParameterName = Animator.StringToHash("bGameOver");
    
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
        rigidbodyComponent.velocity = new Vector2(movementVector.x * movementSpeed, rigidbodyComponent.velocity.y);
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
    
    protected virtual IEnumerator OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") &&
            !col.collider.isTrigger)
        {
            GameOverAnimator.gameObject.SetActive(true);
            GameOverAnimator.SetBool(GameOverParameterName, true);
            yield return new WaitForSecondsRealtime(4f);

            SceneManager.LoadScene(0);
        }
    }
}

using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public delegate void CollisionChanged(bool newValue, GameObject other);
    public event CollisionChanged OnCollisionChanged;
    
    private void OnTriggerStay2D(Collider2D col)
    {
        OnCollisionChanged?.Invoke(true, col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnCollisionChanged?.Invoke(false, other.gameObject);
    }
}

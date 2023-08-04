using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public delegate void GroundCollisionChanged(bool newValue, GameObject other);
    public event GroundCollisionChanged OnGroundCollisionChanged;
    
    private void OnTriggerStay2D(Collider2D col)
    {
        OnGroundCollisionChanged?.Invoke(true, col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnGroundCollisionChanged?.Invoke(false, other.gameObject);
    }
}

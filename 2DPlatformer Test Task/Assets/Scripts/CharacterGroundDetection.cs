using UnityEngine;

public class CharacterGroundDetection : MonoBehaviour
{
    public delegate void GroundCollisionChanged(bool newValue);
    public event GroundCollisionChanged OnGroundCollisionChanged;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        OnGroundCollisionChanged?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnGroundCollisionChanged?.Invoke(false);
    }
}

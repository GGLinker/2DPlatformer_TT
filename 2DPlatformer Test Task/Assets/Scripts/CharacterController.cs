using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float airSpeedScale;
    [SerializeField] private float jumpHeight;

    [SerializeField] private CollisionDetection groundDetection;
    [SerializeField] private CollisionDetection lootDetection;

    [SerializeField] private TextMeshProUGUI lootingTipWidget;
    [SerializeField] private InventoryWidget inventoryWidget;
    
    [SerializeField] private string isRunningAnimatorParameterName;
    [SerializeField] private string verticalDirectionAnimatorParameterName;

    private Animator characterAnimator;
    private Rigidbody2D characterRigidbody2D;

    private readonly float velocityDetectionThreshold = 0.05f;
    
    private float horizontalVelocity;
    private bool secondJumpAllowed = true;
    private bool isInAir;

    private Item nearestItem = null;

    void Start()
    {
        characterAnimator = transform.GetComponent<Animator>();
        characterRigidbody2D = transform.GetComponent<Rigidbody2D>();

        groundDetection.OnCollisionChanged += (newValue, other) =>
        {
            isInAir = !newValue;
            if (!isInAir)
            {
                secondJumpAllowed = true;
            }
        };
        lootDetection.OnCollisionChanged += (newValue, other) =>
        {
            var item = other.transform.GetComponentInParent<Item>();
            if (item != null)
            {
                nearestItem = newValue ? item : null;
                lootingTipWidget.gameObject.SetActive(newValue);
            }
        };
    }

    private void Update()
    {
        horizontalVelocity = Input.GetAxisRaw("Horizontal");
        transform.localScale = new Vector3(
            horizontalVelocity == 0 ? 
                transform.localScale.x : 
                Mathf.Abs(transform.localScale.x) * (horizontalVelocity >= 0 ? 1 : -1),
            transform.localScale.y,
            transform.localScale.z);
    }

    private void FixedUpdate()
    {
        var velocity = characterRigidbody2D.velocity = new Vector2(horizontalVelocity * (isInAir ? movementSpeed * airSpeedScale : movementSpeed), characterRigidbody2D.velocity.y);
        characterAnimator.SetBool(isRunningAnimatorParameterName, Mathf.Abs(velocity.x) > velocityDetectionThreshold);
        characterAnimator.SetFloat(verticalDirectionAnimatorParameterName, Mathf.Abs(velocity.y) < velocityDetectionThreshold ? 0 : (velocity.y > 0 ? 1 : -1));
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && (!isInAir || secondJumpAllowed))
        {
            if (isInAir)
            {
                secondJumpAllowed = false;
            }

            characterRigidbody2D.velocity += Vector2.up * jumpHeight;
        }
    }
    
    public void OnLoot(InputValue value)
    {
        if (nearestItem != null)
        {
            ItemContent content = nearestItem.GetContent();
            lootingTipWidget.gameObject.SetActive(false);
            if (inventoryWidget.UpdateSlotContent(content))
            {
                nearestItem.LootItem();
            };
        }
    }
}

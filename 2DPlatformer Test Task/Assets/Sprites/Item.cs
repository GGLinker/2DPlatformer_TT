using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Item : MonoBehaviour
{
    [SerializeField] private string itemID;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string lootedAnimatorParameterName;
    
    private ItemContent itemContent;
    private Animator animator;

    private void Awake()
    {
        itemContent.ID = itemID;
        itemContent.sprite = spriteRenderer.sprite;
        animator = transform.GetComponent<Animator>();
    }

    public ItemContent LootItem()
    {
        animator.SetBool(lootedAnimatorParameterName, true);
        return itemContent;
    }
}

public struct ItemContent
{
    public string ID;
    public Sprite sprite;
}
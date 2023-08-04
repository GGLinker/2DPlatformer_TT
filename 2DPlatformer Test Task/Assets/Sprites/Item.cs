using System;
using System.Collections;
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

    public ItemContent GetContent()
    {
        return itemContent;
    }
    public void LootItem()
    {
        StartCoroutine(Loot());
    }

    private IEnumerator Loot()
    {
        animator.SetBool(lootedAnimatorParameterName, true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

public struct ItemContent
{
    public string ID;
    public Sprite sprite;
}
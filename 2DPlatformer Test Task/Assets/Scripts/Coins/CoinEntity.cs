using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CoinEntity : MonoBehaviour
{
    [SerializeField] private int coinWorth;
    [SerializeField] private string IsCollectedAnimatorParameterName;
    [SerializeField] private AnimationClip collectionAnimationClip;

    private Animator animator;
    private bool collected;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    
    private IEnumerator OnTriggerStay2D(Collider2D col)
    {
        var collector = col.transform.GetComponentInParent<CoinCollector>();
        if (!collected && collector != null)
        {
            collected = true;
            collector.CollectCoins(coinWorth);
            animator.SetBool(IsCollectedAnimatorParameterName, true);
            yield return new WaitForSeconds(collectionAnimationClip.length + 1f);
            
            Destroy(gameObject);
        }
    }
}

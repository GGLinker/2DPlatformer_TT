using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CoinEntity : MonoBehaviour
{
    [SerializeField] private readonly int coinWorth;
    [SerializeField] private string IsCollectedAnimatorParameterName;
    [SerializeField] private AnimationClip collectionAnimationClip;

    private Animator animator;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    
    private IEnumerator OnTriggerStay2D(Collider2D col)
    {
        var collector = col.transform.GetComponentInParent<CoinCollector>();
        if (collector != null)
        {
            collector.CollectCoins(coinWorth);
            animator.SetBool(IsCollectedAnimatorParameterName, true);
            yield return new WaitForSeconds(collectionAnimationClip.length + 1f);
            
            Destroy(gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class SlimeController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    
    [SerializeField] private Transform detectorsParent;
    
    [SerializeField] private GroundDetection precipiceDetector;
    [SerializeField] private GroundDetection obstacleDetector;

    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidbody;
    private Vector2 movementVector = Vector2.right;

    void Start()
    {
        enemyAnimator = transform.GetComponent<Animator>();
        enemyRigidbody = transform.GetComponent<Rigidbody2D>();

        precipiceDetector.OnGroundCollisionChanged += DetectPrecipice;
        obstacleDetector.OnGroundCollisionChanged += DetectObstacle;
    }

    private void FixedUpdate()
    {
        enemyRigidbody.velocity = movementVector * movementSpeed;
    }

    private void DetectPrecipice(bool groundDetected, GameObject other)
    {
        if (other.CompareTag("Player")) return;
        if (!groundDetected) TurnAround();
    }
    private void DetectObstacle(bool groundDetected, GameObject other)
    {
        if (other.CompareTag("Player")) return;
        if (!other.CompareTag("Player") && groundDetected) TurnAround();
    }

    private void TurnAround()
    {
        movementVector *= -1;
        transform.localScale = new Vector3(
            transform.localScale.x * -1f,
            transform.localScale.y, 
            transform.localScale.z
        );
    }
}

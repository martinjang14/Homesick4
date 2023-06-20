using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    public Animator anim;
    private GameObject player;
    private float moveSpeed;
    private float jumpForce;
    private float detectionRadius;
    private Rigidbody2D rb;
    public bool isJumping = false;
    public bool isMovingTowardsPlayer = false;

    private float moveX;
    private bool facingRight = true;

    private void Start()
    {
        EnemyAttributes EA = GetComponent<EnemyAttributes>();
        this.detectionRadius = EA.detectionRadius;
        this.jumpForce = EA.jumpForce;
        this.moveSpeed = EA.moveSpeed;
        this.player = EA.player;

        rb = GetComponent<Rigidbody2D>();
    }

    private void setanims()
    {
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isRunning", isMovingTowardsPlayer);
    }

    private void Update()
    {
        setanims();

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            isMovingTowardsPlayer = true;

            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            if (player.transform.position.y - transform.position.y > 1 && !isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }
        else
        {
            isMovingTowardsPlayer = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        moveX = (player.transform.position - transform.position).normalized.x;
        if (moveX > 0 && !facingRight)
            Flip();
        else if (moveX < 0 && facingRight)
            Flip();

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Animator anim;
    private PlayerAttributes PA;

    private float moveSpeed;
    private float sprintSpeedMultiplier;

    private float jumpForce;
    private int maxJumps;
    private int currentJumps = 0;

    public bool isJumping = false;
    public bool isSprinting = false;

    private float speed;
    private float moveX;
    
    private Rigidbody2D rb;

    private bool facingRight = true;

    private void Start()
    {
        PA = GetComponent<PlayerAttributes>();
        rb = GetComponent<Rigidbody2D>();

        this.moveSpeed = PA.moveSpeed;
        this.sprintSpeedMultiplier = PA.sprintSpeedMultiplier;
        this.jumpForce = PA.jumpForce;
        this.maxJumps = PA.maxJumps;
    }

    private void addanims()
    {
        Debug.Log(Mathf.Abs(moveX * speed));
        anim.SetFloat("runSpeed", Mathf.Abs(moveX * speed));
        anim.SetBool("isJumping", isJumping);
    }

    private void Update()
    {
        addanims();
        moveX = Input.GetAxis("Horizontal");

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        // Horizontal movement
        speed = isSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
        Vector2 movement = new Vector2(moveX * speed, rb.velocity.y);
        rb.velocity = movement;

        if (moveX > 0 && !facingRight)
            Flip();
        else if (moveX < 0 && facingRight)
            Flip();

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(jumpForce);
            if (currentJumps++ < maxJumps && isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            else if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }
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
            currentJumps = 0;
        }
    }
}
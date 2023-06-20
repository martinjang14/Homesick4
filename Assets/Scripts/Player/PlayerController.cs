using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private PlayerAttributes PA;

    private float moveSpeed;
    private float sprintSpeedMultiplier;
    private float jumpForce;
    private int maxJumps;
    private int currentJumps = 0;
    public bool isJumping = false;
    private bool isSprinting = false;
    private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        PA = GetComponent<PlayerAttributes>();
        rb = GetComponent<Rigidbody2D>();

        this.moveSpeed = PA.moveSpeed;
        this.sprintSpeedMultiplier = PA.moveSpeed;
        this.jumpForce = PA.jumpForce;
        this.maxJumps = PA.maxJumps;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

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

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            currentJumps = 0;
        }
    }
}
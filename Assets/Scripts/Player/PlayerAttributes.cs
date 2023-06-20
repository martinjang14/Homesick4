using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // Character attributes
    public int maxHealth = 100;
    public int attackDamage = 10;

    public float moveSpeed = 5f;
    public float sprintSpeedMultiplier = 1.5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;

    // Current character state
    private int currentHealth;

    // Public property to access the current health
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    // Example usage of character attributes
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Character took " + damage + " damage. Current health: " + currentHealth);
    }
}

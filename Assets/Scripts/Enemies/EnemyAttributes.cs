using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    public float detectionRadius = 5f;
    public float projectileRadius = 8f;
    public float meleeRadius = 1f;

    public int meleeDamage;
    public int projectileDamage;

    public GameObject player;
    public float posCheckFrequency;
    public float meleeFrequency;
    public float projectileFrequency;

    public int maxHealth = 100;
    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}
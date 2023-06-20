using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAttributes : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    public float detectionRadius = 5f;
    public float projectileRadius = 8f;
    public float meleeRadius = 1f;

    public int meleeDamage = 10;
    public int projectileDamage = 10;

    public GameObject player;
    public float meleeFrequency = 1;
    public float projectileFrequency = 0.5f;

    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    private void Update()
    {
        text.text = "Health: " + currentHealth;
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);
    }
}
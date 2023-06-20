using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttributes : MonoBehaviour
{
    public Camera camera;
    public GameObject HealthText;
    public GameObject EnemiesLeft;

    public GameObject projectilePrefab;

    public int maxHealth = 100;

    public int meleeDamage = 10;
    public int projectileDamage = 8;

    public float projectileSpeed = 10;
    public float projectileCooldownTime = 1;

    public float meleeRange = 10f;
    public float meleeCooldownTime = 0.2f; 

    public float moveSpeed = 5f;
    public float sprintSpeedMultiplier = 1.5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;

    public int multiplier = 1;

    private int currentHealth;

    private void Update()
    {
        HealthText.GetComponent<TextMeshProUGUI>().text = "Health: " + currentHealth;
        EnemiesLeft.GetComponent<TextMeshProUGUI>().text = "Enemies Left: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);
    }
}

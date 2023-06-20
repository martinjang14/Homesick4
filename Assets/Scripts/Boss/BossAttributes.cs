using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttributes : MonoBehaviour
{
    public PlayerAttributes playerAttributes;

    public GameObject projectilePrefab;
    public GameObject warningPadPrefab;

    public int maxHealth = 500;

    public int damage;

    public float attackFrequency;

    public float attackWidth;

    private int currentHealth;

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

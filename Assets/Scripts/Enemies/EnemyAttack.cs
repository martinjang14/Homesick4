using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAttributes EA;

    private float meleeRadius;
    private int meleeDamage;
    private float projectileRadius;
    private int projectileDamage;
    
    private GameObject player;
    private float posCheckFrequency = 5;
    private float meleeFrequency;
    private float projectileFrequency;

    private float distanceToPlayer;
    public bool isMeleeAttacking = false;
    public bool isProjectileAttacking = false;

    private void Start()
    {
        EA = GetComponent<EnemyAttributes>();

        this.meleeRadius = EA.meleeRadius;
        this.projectileRadius = EA.projectileRadius;
        this.meleeFrequency = EA.meleeFrequency;
        this.projectileFrequency = EA.projectileFrequency;
        this.player = EA.player;
        this.meleeDamage = EA.meleeDamage;
        this.projectileDamage = EA.projectileDamage;

        StartCoroutine("CheckingForPlayer");
    }

    private IEnumerator CheckingForPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/posCheckFrequency);

            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        }
    }

    private void Update()
    {
        if (distanceToPlayer < meleeRadius)
        {
            if(isProjectileAttacking)
            {
                StopCoroutine("ProjectileAttacking");
                isProjectileAttacking = false;
            }

            if (!isMeleeAttacking)
            {
                isMeleeAttacking = true;
                StartCoroutine("MeleeAttacking");
            }
        }
        else if (distanceToPlayer < projectileRadius)
        {
            if(isMeleeAttacking)
            {
                StopCoroutine("MeleeAttacking");
                isMeleeAttacking = false;
            }        

            if (!isProjectileAttacking)
            {
                StartCoroutine("ProjectileAttacking");
                isProjectileAttacking = true;
            }
        }
        else
        {
            StopCoroutine("MeleeAttacking");
            isMeleeAttacking = false;
            StopCoroutine("ProjectileAttacking");
            isProjectileAttacking = false;
        }
    }

    private IEnumerator MeleeAttacking()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / meleeFrequency);

            PlayerAttributes PA = player.GetComponent<PlayerAttributes>();
            PA.takeDamage(meleeDamage);

            Debug.Log("Enemy Melee Attack. Damage: " + meleeDamage);
        }
    }

    private IEnumerator ProjectileAttacking()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / projectileFrequency);

            Debug.Log("Enemy Projectile Attack");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, projectileRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeRadius);
    }
}
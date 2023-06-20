using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAttributes EA;

    private float meleeRadius;
    private float projectileRadius;
    
    private GameObject player;
    private float posCheckFrequency;
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
        this.posCheckFrequency = EA.posCheckFrequency;
        this.meleeFrequency = EA.meleeFrequency;
        this.projectileFrequency = EA.projectileFrequency;
        this.player = EA.player;

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

            Debug.Log("Melee Attack with Damage of " + EA.meleeDamage);
        }
    }

    private IEnumerator ProjectileAttacking()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / projectileFrequency);

            Debug.Log("Projectile Attack with Damage of " + EA.projectileDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, projectileRadius);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    private Camera camera;

    private GameObject projectilePrefab;

    private PlayerAttributes PA;

    private int meleeDamage;
    private int projectileDamage;

    private float projectileSpeed;

    private float meleeRange;
    private List<GameObject> enemiesWithinMeleeRange = new List<GameObject>();
    private float enemyCheckFrequency = 5;

    private float meleeCooldownTime;
    private float projectileCooldownTime;

    private bool canMeleeAttack = true;
    private bool canProjectileAttack = true;

    void Start()
    {
        StartCoroutine("CheckingForEnemies");

        PA = GetComponent<PlayerAttributes>();

        this.projectilePrefab = PA.projectilePrefab;
        this.camera = PA.camera;

        this.meleeDamage = PA.meleeDamage;
        this.projectileDamage = PA.projectileDamage;
        this.meleeRange = PA.meleeRange;
        this.meleeCooldownTime = PA.meleeCooldownTime;
        this.projectileCooldownTime = PA.projectileCooldownTime;
        this.projectileSpeed = PA.projectileSpeed;
    }
    void Update()
    {
        if(Input.GetButtonDown("Quick Melee"))
        { 
            foreach (GameObject enem in enemiesWithinMeleeRange)
            {
                EnemyAttributes enemyAttributes = enem.GetComponent<EnemyAttributes>();
                BossAttributes bossAttributes = enem.GetComponent<BossAttributes>();

                if (canMeleeAttack)
                {
                    anim.SetTrigger("melee");
                    if (enemyAttributes)
                    {
                        MeleeAttack(enemyAttributes);
                    }
                    else if (bossAttributes)
                    {
                        MeleeAttack(bossAttributes);
                    }
                    
                }
            }
        }
        if (Input.GetButtonDown("Projectile Fire"))
        {
            if(canProjectileAttack)
            {
                ProjectileAttack();
            }
        }
    }

    private IEnumerator CheckingForEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / enemyCheckFrequency);

            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, meleeRange);
            enemiesWithinMeleeRange.Clear();

            foreach (Collider2D col in cols)
            {
                if(col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Boss"))
                {
                    enemiesWithinMeleeRange.Add(col.gameObject);
                }
            }
        }
    }

    private void MeleeAttack(EnemyAttributes enemy)
    {
        enemy.takeDamage(meleeDamage);

        canMeleeAttack = false;
        Invoke("ResetMeleeAttack", meleeCooldownTime);
    }

    private void MeleeAttack(BossAttributes boss)
    {
        boss.takeDamage(meleeDamage);

        canMeleeAttack = false;
        Invoke("ResetMeleeAttack", meleeCooldownTime);
    }

    private void ProjectileAttack()
    {
        anim.SetTrigger("projectile");

        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(pos);

        Vector3 direction = pos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, transform);
        if(PA.multiplier == 1)
        {
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        } else
        {
            projectile.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }
        

        Projectile proj = projectile.GetComponent<Projectile>();
        proj.damage = projectileDamage;
        proj.speed = projectileSpeed;



        canProjectileAttack = false;
        Invoke("ResetProjectileAttack", projectileCooldownTime);
    }

    private void ResetMeleeAttack()
    {
        canMeleeAttack = true;
    }

    private void ResetProjectileAttack()
    {
        canProjectileAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}

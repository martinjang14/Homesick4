using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyNum { get; private set; }
    public WinLossManager WLM;

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyNum = enemies.Length;

        if (enemyNum <= 0)
        {
            WLM.spawnBoss();
        }

        foreach (GameObject enem in enemies)
        {
            EnemyAttributes enemyAttributes = enem.GetComponent<EnemyAttributes>();
            if(enemyAttributes.CurrentHealth <= 0)
            {
                Destroy(enem);
            }
        }
    }
}

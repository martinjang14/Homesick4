using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private BossAttributes BA;
    private PlayerAttributes PA;
    private float attackFrequency;
    private GameObject projectilePrefab;
    private GameObject warningPadPrefab;
    private float attackWidth;

    void Start()
    {
        BA = GetComponent<BossAttributes>();

        attackFrequency = BA.attackFrequency;
        projectilePrefab = BA.projectilePrefab;
        warningPadPrefab = BA.warningPadPrefab;
        PA = BA.playerAttributes;
        attackWidth = BA.attackWidth;

        StartCoroutine("AttackingPlayer");
    }

    private IEnumerator AttackingPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / attackFrequency);

            throwRocks();
        }
    }

    private void throwRocks()
    {
        Vector3 displacement = new Vector3(attackWidth / 2, 0, 0);
        
        Vector3 pos = PA.transform.position;
        Vector3 posLeft = pos - displacement;
        Vector3 posRight = pos + displacement;

        GameObject mid = Instantiate(projectilePrefab, pos, Quaternion.identity);
        GameObject left = Instantiate(projectilePrefab, posLeft, Quaternion.identity);
        GameObject right = Instantiate(projectilePrefab, posRight, Quaternion.identity);

        mid.GetComponent<Rigidbody2D>().gravityScale = 10;
        left.GetComponent<Rigidbody2D>().gravityScale = 10;
        right.GetComponent<Rigidbody2D>().gravityScale = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

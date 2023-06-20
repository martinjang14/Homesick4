using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;

    private float secondsElapsed;

    private void UpdateTimer()
    {
        secondsElapsed += Time.deltaTime;
    }

    private void Update()
    {
        UpdateTimer();

        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(secondsElapsed >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAttributes enem = collision.gameObject.GetComponent<EnemyAttributes>();
            enem.takeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}

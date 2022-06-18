using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhisicAttack : MonoBehaviour
{
    public int damage = 10;
    float liveTime = 0;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        liveTime += Time.deltaTime;

        if (liveTime >= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

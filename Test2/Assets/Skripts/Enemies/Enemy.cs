using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float mana;
    public float xp;
    public float moveSpeed;
    public int positionOfPatrol;
    public Transform point;
    Transform player;
    public float stopingDistance;
    public Animator animator;
    private Vector2 direction;

    private float speed;

    bool chill = false;
    bool angry = false;
    bool goBack = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, player.position) < stopingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }
        
        if (Vector2.Distance(transform.position, player.position) > stopingDistance)
        {
            goBack = true;
            angry = false;
        }

        if (chill == true)
        {
            
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }

        //смена значений полей Horizontal, Vertical и Speed в аниматоре, для смены анимаций
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", speed);
    }

    public virtual void Chill()
    {
        direction.x = 0;
        direction.y = 0;
        speed = 0;
    }

    public virtual void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if ((player.position.x - transform.position.x) > 1)
        {
            direction.x = 1;
        }
        else if ((player.position.x - transform.position.x) < -1)
        {
            direction.x = -1;
        }
        else
        {
            direction.x = (player.position.x - transform.position.x) % 1;
        }
        if ((player.position.y - transform.position.y) > 1)
        {
            direction.y = 1;
        }
        else if ((player.position.y - transform.position.y) < -1)
        {
            direction.y = -1;
        }
        else
        {
            direction.y = (player.position.y - transform.position.y) % 1;
        }
        speed = moveSpeed * Time.deltaTime;
    }

    public virtual void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);

        if ((point.position.x - transform.position.x) > 1)
        {
            direction.x = 1;
        }
        else if ((point.position.x - transform.position.x) < -1)
        {
            direction.x = -1;
        }
        else
        {
            direction.x = (point.position.x - transform.position.x) % 1;
        }
        if ((point.position.y - transform.position.y) > 1)
        {
            direction.y = 1;
        }
        else if ((point.position.y - transform.position.y) < -1)
        {
            direction.y = -1;
        }
        else
        {
            direction.y = (point.position.y - transform.position.y) % 1;
        }
        speed = moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

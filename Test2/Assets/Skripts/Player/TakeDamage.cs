using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TakeDamage : MonoBehaviour
{
    public GameObject player;
    private float damage = 0.1f;
    PlayerInfo hp;
    private bool takeDamage = false;

    public Image hpBar;
    public Image manaBar;

    private void Start()
    {
        hp = FindObjectOfType<PlayerInfo>();
    }
    void CheckHp()
    {
        float newScale = hp.hp / hp.maxHp;
        hpBar.fillAmount = newScale;
        if (hp.hp == 0)
        {
            Death();
        }
    }

    void Death()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void DamageValue()
    {
        if ((player.transform.position.x < -4) && (player.transform.position.x > -5))
        {
            if ((player.transform.position.y < 1.5) && (player.transform.position.y > 0.5))
            {
                hp.hp -= damage;
                takeDamage = true;
            }
            else
            {
                takeDamage = false;
            }
        }
        else
        {
            takeDamage = false;
        }
        if (hp.hp < 0)
        {
            hp.hp = 0;
        }
    }

    void HpRegen()
    {
        if (takeDamage == true)
        {
            return;
        }
        if (hp.hp < hp.maxHp)
        {
            hp.hp += 0.005f;
            if (hp.hp > hp.maxHp)
            {
                hp.hp = hp.maxHp;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DamageValue();
        HpRegen();
        CheckHp();
    }
}

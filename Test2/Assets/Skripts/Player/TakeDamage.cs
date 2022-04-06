using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TakeDamage : MonoBehaviour
{
    public GameObject player;
    private float damage = 0.1f;
    public float hp;
    public float maxHp;
    private bool takeDamage = false;

    public Image hpBar;
    public Image manaBar;

    void CheckHp()
    {
        float newScale = hp / maxHp;
        hpBar.fillAmount = newScale;
        if (hp == 0)
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
                hp -= damage;
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
        if (hp < 0)
        {
            hp = 0;
        }
    }

    void HpRegen()
    {
        if (takeDamage == true)
        {
            return;
        }
        if (hp < maxHp)
        {
            hp += 0.005f;
            if (hp > maxHp)
            {
                hp = maxHp;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TakeDamage : MonoBehaviour
{
    public GameObject player;
    private float damage = 0.1f;
    PlayerInfo playerStats;
    private bool takeDamage = false;

    public Image hpBar;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerInfo>();
    }
    void CheckHp()
    {
        float newScale = playerStats.hp / playerStats.maxHp;
        hpBar.fillAmount = newScale;
        if (playerStats.hp == 0)
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
                playerStats.hp -= damage;
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
        if (playerStats.hp < 0)
        {
            playerStats.hp = 0;
        }
    }

    void HpRegen()
    {
        if (takeDamage == true)
        {
            return;
        }
        if (playerStats.hp < playerStats.maxHp)
        {
            playerStats.hp += 0.005f;
            if (playerStats.hp > playerStats.maxHp)
            {
                playerStats.hp = playerStats.maxHp;
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

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "DamageFences")
        {
            takeDamage = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "DamageFences")
        {
            takeDamage = false;
        }
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
        if (takeDamage == true)
        {
            playerStats.hp -= damage;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillHeal : MonoBehaviour
{
    public GameObject player;
    PlayerInfo playerStats;

    public float maxCooldown;
    float cooldown;
    public float healCount;
    public float manaCost;

    public Image healSkill;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCooldown();

        if (Input.GetKeyDown(KeyCode.H))
        {
            UseSkill();
        }
    }

    void CheckCooldown()
    {
        if (cooldown < maxCooldown)
        {
            cooldown += Time.deltaTime;
            float newScale = cooldown / maxCooldown;
            healSkill.fillAmount = newScale;
        }
    }

    void UseSkill()
    {
        if ((cooldown < maxCooldown) || (playerStats.mana < manaCost))
        {
            return;
        }

        cooldown = 0f;

        playerStats.mana -= manaCost;
        if (playerStats.mana < 0f)
        {
            playerStats.mana = 0f;
        }
        playerStats.hp += healCount;
        if (playerStats.hp > playerStats.maxHp)
        {
            playerStats.hp = playerStats.maxHp;
        }
    }
}

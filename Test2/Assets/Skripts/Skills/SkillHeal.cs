using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillHeal : MonoBehaviour
{
    public GameObject player;
    PlayerInfo hp;

    public float maxCooldown;
    float cooldown;
    public float healCount;

    public Image healSkill;

    // Start is called before the first frame update
    void Start()
    {
        hp = FindObjectOfType<PlayerInfo>();
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
        if (cooldown < maxCooldown)
        {
            return;
        }

        cooldown = 0f;
        hp.hp += healCount;
        if (hp.hp > hp.maxHp)
        {
            hp.hp = hp.maxHp;
        }
        Debug.Log(hp.hp);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float hp = 10.0f;
    public float maxHp = 10.0f;
    public float mana = 10.0f;
    public float maxMana = 10.0f;

    //заготовочка на будущее)
    public int hpStat = 10;
    public int manaStat = 10;
    public int stengthStat = 10;
    public int agilityStat = 10;
    public int magicStat = 10;

    //мана реген, пока пусть будет тут. ’з куда его ещЄ засунуть)
    public Image manaBar;
    public void ManaRegen()
    {
        if (mana < maxMana)
        {
            mana += 0.005f;
            if (mana > maxMana)
            {
                mana = maxMana;
            }
            float newScale = mana / maxMana;
            manaBar.fillAmount = newScale;
        }
    }

    public void FixedUpdate()
    {
        ManaRegen();
    }
}

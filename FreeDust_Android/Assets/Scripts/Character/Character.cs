using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    
    bool m_bEnemy;

    public Hp m_hp;

    private void Awake()
    {
        m_hp = GameObject.Instantiate((Resources.Load("Prefab/Character/Character_Hp") as GameObject)).GetComponent<Hp>();
        m_hp.transform.SetParent(transform.Find("Hp"), false);

        m_hp.Set_Hp(Global.PLAYER_HP);
    }

    public void Set_Character(bool bEnemy)
    {
        m_bEnemy = bEnemy;

        
    }

    public bool Set_Damage(int iDamage)
    {
        return m_hp.Set_Damage(iDamage);
    }
    
    public void Set_Revive()
    {
        m_hp.Set_Hp(1);
    }

}

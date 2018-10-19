using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour {

    int m_iMaxHp;
    public int m_iCurHp;

    public UISprite m_fill;

    public UILabel m_hpLabel;


    public void Set_Hp(int iHp)
    {
        m_iMaxHp = iHp;
        m_iCurHp = iHp;

        Set_UI();
    }

    public void Set_CurHp(int iHp)
    {
        m_iCurHp = iHp;

        Set_UI();
    }

    public bool Set_Damage(int iDamage)
    {
        m_iCurHp += iDamage;

        m_iCurHp = Mathf.Max(0, m_iCurHp);

        Set_UI();

        if (m_iCurHp <= 0)
            return true;

        return false;
    }

    void Set_UI()
    {
        m_hpLabel.text = m_iCurHp.ToString();

        StartCoroutine(HP_Animation());
    }

    IEnumerator HP_Animation()
    {
        float fRatio = m_iCurHp / (float)m_iMaxHp;

        float fStart = m_fill.fillAmount;
        float fDest = fRatio;

        float fTick = fDest - fStart;

        float fTime = 1f;

        while (true)
        {
            fTime -= Time.deltaTime;

            m_fill.fillAmount += fTick * Time.deltaTime;

            if (fTime < 0f)
                break;

            yield return null;
        }

        m_fill.fillAmount = fRatio;
    }
}

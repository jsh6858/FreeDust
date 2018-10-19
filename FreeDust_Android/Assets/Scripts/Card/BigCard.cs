using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCard : Card {

    readonly string[] WEAPON_NAME = new string[] { "오지고 지리는 대검", "기가 막히는 방패", "만병통치약 그 자체", "오지고 지리는 대검"};

    #region Varibles

    public UISprite _sprItem;
    public GameObject _objTypeSelect;

    // Explain
    public GameObject m_itemExplain;
    public UILabel m_name;
    public UILabel[] m_labelInfo;

    #endregion

    public override void SetCard(int iNum, CARD_TYPE cardType = CARD_TYPE.END)
    {
        base.SetCard(iNum, cardType);

        m_name.text = WEAPON_NAME[(int)cardType];
        _sprItem.spriteName = Global.STR_ATTRIBUTE[(int)m_cardType];
    }

    public void OpenChangeType(bool b)
    {
        _objTypeSelect.SetActive(b);
    }

    public void Push_SelectType(GameObject obj)
    {
        _objTypeSelect.SetActive(false);

        int iSelect = int.Parse(obj.name);

        m_cardType = (CARD_TYPE)iSelect;

        SetCard(m_iPower, m_cardType);
    }

    public void AddEvent_typeSelect(EventDelegate newEvent)
    {
        EventDelegate.Add(_sprType.GetComponent<UIButton>().onClick, newEvent);
    }

    public void Show_Info()
    {
        int iCurPower = m_iPower;

        if(GameManager.Instance.m_inGameManager._BattleField.m_randType == m_cardType)
            iCurPower++;

        float fDamage = Card_Manager.Instance.Get_Damage(m_cardType, iCurPower, Global.WEAPON_POWER, false);
        float fCounterDamage = Card_Manager.Instance.Get_Damage(m_cardType, iCurPower, Global.WEAPON_POWER, true);

        if(m_cardType == CARD_TYPE.ATTACK)
        {
            m_labelInfo[0].text = string.Format("[0f0526]{0}[000000]의 [59afff]{1}", (int)fDamage, "일반 공격" );
            m_labelInfo[1].text = string.Format("[ff4000]{0}", "Defeat" );
            m_labelInfo[2].text = string.Format("[236fff]{0}[000000]의 [59afff]{1}", (int)fCounterDamage, "카운터 공격" );
        }
        else if(m_cardType == CARD_TYPE.SHIELD)
        {
            m_labelInfo[0].text = string.Format("[236fff]{0}[000000]의 [59afff]{1}", (int)fCounterDamage, "카운터 공격" );
            m_labelInfo[1].text = string.Format("[ff4000]{0}", "Defeat" );
            m_labelInfo[2].text = string.Format("[0f0526]{0}[000000]의 [59afff]{1}", (int)fDamage, "일반 공격" );
        }
        else
        {
            m_labelInfo[0].text = string.Format("[ff4000]{0}", "Defeat" );
            m_labelInfo[1].text = string.Format("[236fff]{0}[000000]의 [59afff]{1}", (int)fCounterDamage / 2, "공격 및 힐" );
            m_labelInfo[2].text = string.Format("[0f0526]{0}[000000]의 [59afff]{1}", (int)fDamage, "힐" );
        }
        
        
    }
}

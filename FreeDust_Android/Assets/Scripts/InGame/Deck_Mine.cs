using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck_Mine : Deck {

    // 변경된 스몰카드 (카드 변경 중에서)
    public List<KeyValuePair<SmallCard, Card_Info>> Changed_SmallCard = new List<KeyValuePair<SmallCard, Card_Info>>();

    void Awake()
    {
        Initialzie();

        for(int i=0; i<m_smallCard.Length; ++i)
        {
			// 이벤트 넣어주기
            EventDelegate newEvent = new EventDelegate(this, "OnClick_SmallCard");
            newEvent.parameters[0] = UsefulFunction.MakeParameter(m_smallCard[i], typeof(SmallCard));
            m_smallCard[i].AddEvent_ShowBigCard(newEvent);
        }
    }

	public void OnClick_SmallCard(SmallCard card)
	{
        if (Global._gameMode == GAME_MODE.PLAY_CARD)
        {
            // Show BigCard
            for (int i = 0; i < m_smallCard.Length; ++i)
            {
                if (m_smallCard[i] == card)
                {
                    m_smallCard[i].Set_Selected(true);
                }
                else
                {
                    m_smallCard[i].Set_Selected(false);
                }
            }
        }
        else if(Global._gameMode == GAME_MODE.CHANGE_CARD)
        {
            // Show card select
            for (int i = 0; i < m_smallCard.Length; ++i)
            {
                if (m_smallCard[i] == card)
                {
                    m_smallCard[i].OpenChangeType(true);
                }
                else
                {
                    m_smallCard[i].OpenChangeType(false);
                }
            }
        }
	}

    public Card Get_SelectedCard()
    {
        for (int i = 0; i < m_smallCard.Length; ++i)
        {
            if(m_smallCard[i].m_bSelected)
                return m_smallCard[i];
        }

        // 선택된 카드가 없으면 랜덤으로 아무거나 냄
        return Submit_RandomCard();
    }

    public void Reset_CardChange()
    {
        Changed_SmallCard.Clear();

        for(int i=0; i<m_smallCard.Length; ++i)
        {
            m_smallCard[i].Emphasize(false);
        }
    }
}

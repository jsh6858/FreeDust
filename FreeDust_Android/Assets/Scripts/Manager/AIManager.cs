using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {
    
    void Awake()
    {
        Singleton.aiManager = this;
    }

    public Card Choose_Random(Card[] cards)
    {
        Card card;

        while(true)
        {
            card = cards[Random.Range(0, cards.Length)];

            if(card._bUsed == false)
                break;
        }

        return card;
    }

    public CARD_TYPE[] Get_EnemyTypeRandom() // 5개 카드속성 return
    {
        CARD_TYPE[] type = new CARD_TYPE[Constants.DECK_MAX_COUNT];

        for(int i=0; i<type.Length; ++i)
            type[i] = (CARD_TYPE)(Random.Range(0, (int)CARD_TYPE.END)); 

        return type;
    }

    public Card Get_CardToEnhance(Card[] cards) // 강화할 카드
    {
        for(int i=0; i<cards.Length; ++i)
        {
            if(false == cards[i]._bEnhanced) // 강화할 카드가 하나라도 있으면,
            {
                Card card = null;

                while(true)
                {
                    card = cards[Random.Range(0, cards.Length)];

                    if(card._bEnhanced == false)
                        break;
                }

                return card;
            }
        }

        return null;
    }

    public bool EnemyUseSkill(Deck deck, UI_Enemy enemy)
    {
        if (enemy._skill._blackSprite.activeSelf)
            return false;

        Card[] cards = deck.Get_Deck();

        int count = 0;
        for(int i=0; i<cards.Length; ++i)
        {
            if (false == cards[i]._bUsed)
                count++;
        }

        if (Random.Range(0, count + 1) == 0)
            return true;
        return false;
    }
    
}

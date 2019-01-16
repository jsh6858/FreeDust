﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {
    
    void Awake()
    {
        Singleton.aiManager = this;
    }

    public Card Choose_Random(Card[] cards)
    {
        return cards[Random.Range(0, cards.Length)];
    }

    public CARD_TYPE[] Get_EnemyTypeRandom()
    {
        CARD_TYPE[] type = new CARD_TYPE[Constants.DECK_MAX_COUNT];

        for(int i=0; i<type.Length; ++i)
            type[i] = (CARD_TYPE)(Random.Range(0, (int)CARD_TYPE.END)); 

        return type;
    }
}
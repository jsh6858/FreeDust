using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeck : Deck {
    
    void Awake()
    {
        Set_Deck(new CARD_TYPE[] { CARD_TYPE.ATTACK, CARD_TYPE.DEFEND,
            CARD_TYPE.DEFEND, CARD_TYPE.DEFEND, CARD_TYPE.HEAL,});
    }
}

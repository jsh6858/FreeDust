using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDeck : Deck {

	void Awake ()
    {
        Set_Deck(new CARD_TYPE[] { CARD_TYPE.ATTACK, CARD_TYPE.DEFEND,
            CARD_TYPE.DEFEND, CARD_TYPE.DEFEND, CARD_TYPE.HEAL,});

        for(int i=0; i<_cards.Length; ++i)
        {
            EventDelegate selectEvent = new EventDelegate(this, "Select_Card");
            selectEvent.parameters[0].value = i;
            EventDelegate.Add(_cards[i].GetComponent<UIButton>().onClick, selectEvent);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDeck : Deck {

	void Awake ()
    {
    }

    public override void Set_Deck(CARD_TYPE[] cardTypes)
    {
        base.Set_Deck(cardTypes);

        for(int i=0; i<_cards.Length; ++i)
        {
            EventDelegate selectEvent = new EventDelegate(this, "Select_Card");
            selectEvent.parameters[0].value = i;
            EventDelegate.Add(_cards[i].GetComponent<UIButton>().onClick, selectEvent);
        }
    }
    

    void Start()
    {
        Singleton.inGameManager._myDeck = this;
    }
}

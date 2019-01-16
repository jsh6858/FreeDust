using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDeck : Deck {

    float _fTime = 0f;

    void Start()
    {
        Singleton.inGameManager._myDeck = this;
    }

    public override void Set_Deck(CARD_TYPE[] cardTypes)
    {
        base.Set_Deck(cardTypes);

        for(int i=0; i<_cards.Length; ++i)
        {
            EventDelegate selectEvent = new EventDelegate(this, "Select_Card");
            selectEvent.parameters[0].value = i;

            EventDelegate.Add(_cards[i].GetComponent<UIEventTrigger>().onClick, selectEvent);

            EventDelegate.Add(_cards[i].GetComponent<UIEventTrigger>().onPress, Press);
            EventDelegate.Add(_cards[i].GetComponent<UIEventTrigger>().onDragOut, Release);
        }
    }
    
    public void Select_Card(int index) // select myCard
    {
        for(int i=0; i<_cards.Length; ++i)
        {
            if(i == index)
            {
                _cards[i].OnSelected();

                Singleton.inGameManager._uiPlayer.SetCardView(_cards[i]._cardType); // CardView
            }
            else
            {
                _cards[i].onDeSelected();
            }
        }
    }

    public void Press()
    {
        Debug.Log("Press!");

        _fTime = 0.5f;
    }
    public void Release()
    {
        Debug.Log("Release!");

        _fTime = -1f;
    }

    void Update()
    {
        if(_fTime > 0f)
        {
            _fTime -= Time.deltaTime;

            if(_fTime < 0f)
            {
                Debug.Log("Show CardInfo");

                _fTime = -1f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDeck : Deck {

    int _iSelectedCard;

    CardInfo _cardInfo;
    CardInfo cardInfo
    {
        get
        {
            if(null == _cardInfo)
            {
                GameObject obj = Instantiate(Resources.Load("Prefab/CardInfo") as GameObject, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(transform, false);
                obj.layer = (int)LAYER.Card;

                _cardInfo = obj.GetComponent<CardInfo>();
            }
            return _cardInfo;
        }
    }

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

            selectEvent = new EventDelegate(this, "Press");
            selectEvent.parameters[0].value = i;

            EventDelegate.Add(_cards[i].GetComponent<UIEventTrigger>().onHoverOver, selectEvent);

            selectEvent = new EventDelegate(this, "Release");
            selectEvent.parameters[0].value = i;

            EventDelegate.Add(_cards[i].GetComponent<UIEventTrigger>().onHoverOut, selectEvent);
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


    public void Press(int index)
    {
        //Debug.Log("Press!");

        _iSelectedCard = index;

        _fTime = 0.5f;
    }
    public void Release(int index)
    {
        //Debug.Log("Release!");

        _iSelectedCard = -1;

        _fTime = -1f;
    }

    void Update()
    {
        if(_fTime > 0f)
        {
            _fTime -= Time.deltaTime;

            if(_fTime < 0f)
            {
                Debug.Log("Show CardInfo " + _iSelectedCard);

                ShowCardInfo();

                _fTime = -1f;
            }
        }
    }

    void ShowCardInfo()
    {
        cardInfo.gameObject.SetActive(true);

        cardInfo.transform.localPosition = new Vector2(_cards[_iSelectedCard].transform.localPosition.x, 260f);

        
    }
}

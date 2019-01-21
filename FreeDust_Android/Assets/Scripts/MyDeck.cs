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
                obj.layer = LayerMask.NameToLayer("Card");
                obj.transform.SetParent(transform, false);

                _cardInfo = obj.GetComponent<CardInfo>();
            }
            return _cardInfo;
        }
    }

    public BoxCollider _boxCollider;

    float _fTime = 0f;

    void Start()
    {
        Singleton.inGameManager._myDeck = this;

        _boxCollider = transform.Find("Collider").GetComponent<BoxCollider>();

        Singleton.inGameManager.gameChanged += delegate(GAME_STATE state)
        {
            if(state != GAME_STATE.BATTLE)
            {
                _boxCollider.enabled = false;
            }
            else
            {
                _boxCollider.enabled = true;
            }
        };
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

                Singleton.inGameManager._uiPlayer._SelectCardView.SetType(_cards[i]._cardType); // CardView
                Singleton.inGameManager._uiPlayer._BlackSprite.SetActive(false); // OkButton
            }
            else
            {
                _cards[i].onDeSelected();
            }
        }
    }

    public void Press(int index)
    {
        //LogManager.Log("Press!");

        _iSelectedCard = index;

        _fTime = 0.5f;
    }
    public void Release(int index)
    {
        //LogManager.Log("Release!");

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
                LogManager.Log("Show CardInfo " + _iSelectedCard);

                cardInfo.SetCardInfo(_cards[_iSelectedCard]);

                _fTime = -1f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour {

    Card[] _cards;
    Card[] cards
    {
        get
        {
            if(null == _cards)
                InitializeCard();
            return _cards;
        }
    }

    public UICenterOnChild _centerChild;

    int _iSelected = -1;

    private void Awake()
    {
        InitializeCard();
        _centerChild.onCenter += CenterCallBack;
    }

    void InitializeCard()
    {
        _cards = new Card[5];

        GameObject prefab = Resources.Load("Prefab/Proto/MyCard") as GameObject;

        Transform trParent = transform.Find("Cards/Grid");

        for (int i = 0; i < _cards.Length; ++i)
        {
            GameObject temp = Instantiate(prefab, Vector3.zero, Quaternion.identity, trParent);

            temp.layer = 20; // Popup Layer

            _cards[i] = temp.GetComponent<Card>();
            _cards[i].Set_CardType(CARD_TYPE.END);

            EventDelegate selectEvent = new EventDelegate(this, "OnClickCard");
            selectEvent.parameters[0].value = i;
            EventDelegate.Add(_cards[i].GetComponent<UIButton>().onClick, selectEvent);
        }
        trParent.GetComponent<UIGrid>().Reposition();
    }

    public void OnClickCard(int index)
    {
        if(!_centerChild.gameObject.activeSelf)
            _centerChild.gameObject.SetActive(true);

        for (int i = 0; i < cards.Length; ++i)
        {
            if (i == index) // Select
            {
                cards[i].OnSelected();
                _iSelected = i;

                if(cards[i]._cardType == CARD_TYPE.END)
                    CenterCallBack(_centerChild.centeredObject);
                else
                    _centerChild.CenterOn(_centerChild.transform.GetChild((int)cards[i]._cardType));
            }
            else // Deselect
            {
                cards[i].onDeSelected();
            }
        }
    }

    public void CenterCallBack(GameObject centeredObject)
    {
        if(-1 == _iSelected || null == centeredObject)
            return;

        switch(centeredObject.name)
        {
            case "Attack":
                cards[_iSelected].Set_CardType(CARD_TYPE.ATTACK);
                break;
            case "Defend":
                cards[_iSelected].Set_CardType(CARD_TYPE.DEFEND);
                break;
            case "Heal":
                cards[_iSelected].Set_CardType(CARD_TYPE.HEAL);
                break;
        }

        ActivateButton();
    }

    void ActivateButton()
    {
        for(int i=0; i<cards.Length; ++i)
        {
            if(cards[i]._cardType == CARD_TYPE.END)
                return;
        }

        transform.Find("Button").GetComponent<BoxCollider>().enabled = true;
        transform.Find("Button/BlackSprite").gameObject.SetActive(false);
    }

    public void OnClickOKButton()
    {
        Singleton.inGameManager.Game_Start(cards);        

        gameObject.SetActive(false);
    }
}

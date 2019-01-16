using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public GameObject _PrefabCard; // prefab

    protected Card[] _cards;

    UIGrid _grid;
    UIGrid grid
    {
        get
        {
            if(null == _grid)
                _grid = transform.Find("Cards").GetComponent<UIGrid>();
            return _grid;
        }
    }


    public Card[] Get_Deck()
    {
        return _cards;
    }

    public virtual void Set_Deck(CARD_TYPE[] cardTypes)
    {
        if (null == _cards)
        {
            _cards = new Card[5];

            Transform trParent = transform.Find("Cards");

            for (int i = 0; i < cardTypes.Length; ++i)
            {
                GameObject temp = Instantiate(_PrefabCard, Vector3.zero, Quaternion.identity, trParent);

                _cards[i] = temp.GetComponent<Card>();
                _cards[i].Set_CardType(cardTypes[i]);
            }
            grid.Reposition();
            grid.animateSmoothly = true;
        }
    }

    public Card Get_SelectedCard()
    {
        for(int i=0; i<_cards.Length; ++i)
        {
            if(_cards[i]._bSelected)
                return _cards[i];
        }

        return null;
    }

    public Card Get_LeftmostCard()
    {
        for(int i=0; i<_cards.Length; ++i)
        {
            if(!_cards[i]._bUsed)
                return _cards[i];
        }

        return null;
    }

    public void UseCard(Card card)
    {
        card._bUsed = true;
        card.gameObject.SetActive(false);
        _grid.Reposition();
    }
}

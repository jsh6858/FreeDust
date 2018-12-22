using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    public GameObject _PrefabCard; // prefab

    protected Card[] _cards;

    public void Set_Deck(CARD_TYPE[] cardTypes)
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
            trParent.GetComponent<UIGrid>().Reposition();
        }
    }

    public void Select_Card(int index)
    {
        for(int i=0; i<_cards.Length; ++i)
        {
            if(i == index)
            {
                _cards[i].OnSelected();
            }
            else
            {
                _cards[i].onDeSelected();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour {

    Card[] _cards;

   

    private void Awake()
    {
        _cards = new Card[5];

        GameObject prefab = Resources.Load("Prefab/Proto/MyCard") as GameObject;

        Transform trParent = transform.Find("Cards/Grid");

        for (int i = 0; i < _cards.Length; ++i)
        {
            GameObject temp = Instantiate(prefab, Vector3.zero, Quaternion.identity, trParent);

            temp.layer = 20;

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
        for (int i = 0; i < _cards.Length; ++i)
        {
            if (i == index)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    CardSelect _cardSelect;
    public CardSelect cardSelect
    {
        set
        {
            _cardSelect = value;
        }
        get
        {
            if (null == _cardSelect)
                _cardSelect = GameObject.Instantiate((Resources.Load("Prefab/CardSelect") as GameObject)).GetComponent<CardSelect>();
            return _cardSelect;
        }
    }

    void Awake()
    {
        Sound_Manager.GetInstance().Initialize();

        Start_CardSelect();
    }

    public void Start_CardSelect()
    {
        Global._gameMode = GAME_MODE.CARD_SELECT;

        Sound_Manager.GetInstance().Stop_Sound();
        if (Random.Range(0, 2) == 0)
            Sound_Manager.GetInstance().PlaySound("Hearthstone");
        else
            Sound_Manager.GetInstance().PlaySound("Hearthstone2");

        DestroyAll();

        cardSelect.Start_CardSelect();
    }

    void DestroyAll()
    {
        Destroy(cardSelect.gameObject);
        cardSelect = null;
        /*
        Destroy(myDeck.gameObject);
        myDeck = null;
        Destroy(yourDeck.gameObject);
        yourDeck = null;
        Destroy(battleField.gameObject);
        battleField = null;
        */
    }
}

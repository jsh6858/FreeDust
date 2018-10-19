using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    public CardSelect _cardSelect;

    public BattleField _BattleField;

    void Awake()
    {
        Start_CardSelect();
    }

    void Initialize()
    {
        GameManager.Instance.m_inGameManager = this;

        Sound_Manager.GetInstance().Initialize();

        _cardSelect = GameObject.Instantiate((Resources.Load("Prefab/CardSelect") as GameObject)).GetComponent<CardSelect>();
        _BattleField = GameObject.Instantiate((Resources.Load("Prefab/BattleField") as GameObject)).GetComponent<BattleField>();
    }

    // 카드 선택 시작
    public void Start_CardSelect()
    {
        RemoveAll();
        Initialize();

        CloseAll();
        _cardSelect.gameObject.SetActive(true);
        Global._gameMode = GAME_MODE.CARD_SELECT;

        Sound_Manager.GetInstance().Stop_Sound();
        if (Random.Range(0, 2) == 0)
            Sound_Manager.GetInstance().PlaySound("Hearthstone");
        else
            Sound_Manager.GetInstance().PlaySound("Hearthstone2");

        _cardSelect.Start_CardSelect();
    }

    public void Start_Game()
    {
        CloseAll();
        _BattleField.gameObject.SetActive(true);
        Global._gameMode = GAME_MODE.BATTLE;

        _BattleField.Start_Battle();
    }

    void CloseAll()
    {
        _cardSelect.gameObject.SetActive(false);
        _BattleField.gameObject.SetActive(false);
    }

    void RemoveAll()
    {   
        if(_cardSelect != null)
            Destroy(_cardSelect.gameObject);
        if(_BattleField != null)
            Destroy(_BattleField.gameObject);

    }
}

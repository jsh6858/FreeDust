using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    public UI_Player _uiPlayer;
    public UI_Enemy _uiEnemy;
    public UI_Round _uiRound;

    public MyDeck _myDeck;
    public EnemyDeck _enemyDeck;

    public GAME_STATE _gameState = GAME_STATE.END;

    int _round;
    CARD_TYPE _attribute;

    void Awake()
    {
        Singleton.inGameManager = this;

        _uiPlayer = transform.Find("InGameUI/UI_Player").GetComponent<UI_Player>();
        _uiEnemy = transform.Find("InGameUI/UI_Enemy").GetComponent<UI_Enemy>();
        _uiRound = transform.Find("InGameUI/UI_Round").GetComponent<UI_Round>();

        _round = 0;
        _attribute = CARD_TYPE.END;
    }

    // 전투 Anim 시작
    public void Start_Battle()
    {
        if(_gameState != GAME_STATE.CARD_SELECT)
            return;
        _gameState = GAME_STATE.BATTLE;

        Debug.Log("GameState : BATTLE");

        Card myCard = _myDeck.Get_SelectedCard();
        Card enemyCard = _enemyDeck.Get_SelectedCard();

        if(myCard == null) // 아무것도 선택하지 않고 timeout된 경우
            myCard = _myDeck.Get_LeftmostCard();

        if(enemyCard == null)
            enemyCard = Singleton.aiManager.Choose_Random(_enemyDeck.Get_Deck());

        CARD_TYPE myType = myCard._cardType;
        CARD_TYPE enemyType = enemyCard._cardType;

        Debug.Log("my : " + myType + "\nenemy : " + enemyType);

        // 스킬 확인


        // CardSelect        
        _uiPlayer.Set_CardSelect_CardType(myType);
        _uiEnemy.Set_CardSelect_CardType(enemyType);

        
    }

    // 5개 카드 선택
    public void SetMyCard(Card[] myCard)
    {
        CARD_TYPE[] type = new CARD_TYPE[myCard.Length];

        for(int i=0; i<myCard.Length; ++i)
            type[i] = myCard[i]._cardType;

        _myDeck.Set_Deck(type);

        Singleton.popUpManager.systemMesssage.ShowMessage("Ready", 1f, false);
        Singleton.popUpManager.systemMesssage.SetCallBackFunc(gameObject, "GameStart");
    }

    // 게임 시작!
    public void GameStart()
    {
        _gameState = GAME_STATE.CARD_SELECT;

        _attribute = (CARD_TYPE)Random.Range(0, (int)CARD_TYPE.END);
        _uiRound.SetRound(_round, (int)_attribute);

        _enemyDeck.Set_Deck(Singleton.aiManager.Get_EnemyTypeRandom()); // 적 카드 (PVE)
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //Singleton.popUpManager.systemMesssage.ShowMessage("Ready", 1f, true);

            _myDeck.UseCard(_myDeck.Get_LeftmostCard());
        }
    }
}

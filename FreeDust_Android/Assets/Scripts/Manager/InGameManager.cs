using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    public delegate void StateChange(GAME_STATE state);
    public StateChange gameChanged = delegate(GAME_STATE state){};

    public UI_Player _uiPlayer;
    public UI_Enemy _uiEnemy;
    public UI_Round _uiRound;

    public MyDeck _myDeck;
    public EnemyDeck _enemyDeck;

    public MyPlayer _myPlayer;
    public EnemyPlayer _enemyPlayer;
    public Effect_Attack _effectAttack;

    GAME_STATE gameState;
    public GAME_STATE _gameState
    {
        set
        {
            gameState = value;
            gameChanged(gameState);
        }
        get
        {
            return gameState;
        }
    
    }

    public int _quarter;
    public CARD_TYPE _attribute;

    void Awake()
    {
        Singleton.inGameManager = this;

        _uiPlayer = transform.Find("InGameUI/UI_Player").GetComponent<UI_Player>();
        _uiEnemy = transform.Find("InGameUI/UI_Enemy").GetComponent<UI_Enemy>();
        _uiRound = transform.Find("InGameUI/UI_Round").GetComponent<UI_Round>();

        _myPlayer = transform.Find("Player/AnchorL/Player_01").GetComponent<MyPlayer>();
        _enemyPlayer = transform.Find("Player/AnchorR/Player_02").GetComponent<EnemyPlayer>();

        _effectAttack = transform.Find("InGameUI/Ef").GetComponent<Effect_Attack>();

        _quarter = 1;
        _attribute = CARD_TYPE.END;

        _gameState = GAME_STATE.END;
    }

    // 전투 Anim 시작
    public void Start_Battle()
    {
        if(_gameState != GAME_STATE.CARD_SELECT)
            return;
        _gameState = GAME_STATE.BATTLE;

        LogManager.Log("GameState : BATTLE");

        Card myCard = _myDeck.Get_SelectedCard();
        Card enemyCard = _enemyDeck.Get_SelectedCard();

        if(myCard == null) // 아무것도 선택하지 않고 timeout된 경우
            myCard = _myDeck.Get_LeftmostCard();
        myCard._bUsed = true;
        myCard.onDeSelected();

        if(enemyCard == null)
            enemyCard = Singleton.aiManager.Choose_Random(_enemyDeck.Get_Deck());
        enemyCard._bUsed = true;

        CARD_TYPE myType = myCard._cardType;
        CARD_TYPE enemyType = enemyCard._cardType;

        LogManager.Log("my : " + myType + "\nenemy : " + enemyType);

        // 스킬 확인




        // Select View     
        _uiPlayer._SelectCardView.SetType(myType);
        _uiPlayer._SelectCardView.SetFront(false);
        _uiPlayer._SelectCardView.SetSelectCardMode(false);
        _uiEnemy._SelectCardView.SetType(enemyType);
        _uiEnemy._SelectCardView.SetFront(false);

        // Damage
        float damageToMe = BattleManager.Get_Damage(enemyCard, _uiEnemy, myType);
        float damageToEnemy = BattleManager.Get_Damage(myCard, _uiPlayer, enemyType);

        float resultToMe = damageToMe;
        float resultToEnemy = damageToEnemy;

        if(enemyType == CARD_TYPE.HEAL)
        {
            resultToMe -= damageToMe / 2f;
            resultToEnemy -= damageToMe / 2f;
        }
        if(myType == CARD_TYPE.HEAL)
        {
            resultToEnemy -= damageToEnemy / 2f;
            resultToMe -= damageToEnemy / 2f;
        }

        _uiPlayer._hpDamaged = -resultToMe;
        _uiEnemy._hpDamaged = -resultToEnemy;

        // Use Card
        _myDeck.UseCard(myCard);
        _enemyDeck.UseCard(enemyCard);

        // Anim
        StartCoroutine(Play_Anim(BattleManager.Get_BattleResult(myType, enemyType)));
    }

    IEnumerator Play_Anim(BATTLE_RESULT result)
    {
        // 달려가기
        StartCoroutine(_myPlayer.PlayAnim("At_Dr_Ready"));
        yield return StartCoroutine(_enemyPlayer.PlayAnim("At_Dr_Ready"));

        // 이펙트
        yield return StartCoroutine(_effectAttack.Play_Anim("Ef_Attack_01"));

        // 카드 오픈
        StartCoroutine(_uiPlayer._SelectCardView.PlayAnim("Open"));
        yield return StartCoroutine(_uiEnemy._SelectCardView.PlayAnim("Open"));

        // 캐릭터 뒤로가기
        if(result == BATTLE_RESULT.WIN)
        {
            StartCoroutine(_myPlayer.PlayAnim("At_Result_Win_1"));
            StartCoroutine(_enemyPlayer.PlayAnim("At_Result_Lose"));
        }
        else if(result == BATTLE_RESULT.DRAW)
        {
            StartCoroutine(_myPlayer.PlayAnim("At_Dr_Back"));
            StartCoroutine(_enemyPlayer.PlayAnim("At_Dr_Back"));
        }
        else if(result == BATTLE_RESULT.LOSE)
        {
            StartCoroutine(_myPlayer.PlayAnim("At_Result_Lose"));
            StartCoroutine(_enemyPlayer.PlayAnim("At_Result_Win_1"));
        }

        // 깎일 데미지
        StartCoroutine(_uiPlayer.PlayDamagedAnim("Damaged_Count"));
        yield return StartCoroutine(_uiEnemy.PlayDamagedAnim("Damaged_Count"));

        // 체력 감소
        StartCoroutine(_uiPlayer.PlayHpAnim());
        StartCoroutine(_uiEnemy.PlayHpAnim());

        AnimFinish();

        yield return null;
    }

    // 애니매이션 종료
    void AnimFinish()
    {
        if(_gameState != GAME_STATE.BATTLE)
            return;
        _gameState = GAME_STATE.CARD_SELECT;

        _attribute = (CARD_TYPE)Random.Range(0, (int)CARD_TYPE.END);
        _uiRound.SetRound(_quarter, (int)_attribute);

        // Select View
        _uiPlayer._SelectCardView.SetType(CARD_TYPE.END);
        _uiPlayer._SelectCardView.SetFront(true);
        _uiEnemy._SelectCardView.SetType(CARD_TYPE.END);
        _uiEnemy._SelectCardView.SetFront(false);

        if(_myDeck.LeftCount() == 1)
            CardChangeMode();
        else if(_myDeck.LeftCount() == 0)
            EnhanceCardMode();
    }

    // 카드 변경
    void CardChangeMode()
    {
        _uiPlayer._SelectCardView.SetSelectCardMode(true);
        _uiPlayer._BlackSprite.SetActive(false);

        gameChanged += delegate(GAME_STATE state) // 경기 시작할 때 적 카드 변경
        {
            if(state == GAME_STATE.BATTLE)
            {
                _enemyDeck.Get_LeftmostCard().Set_CardType((CARD_TYPE)Random.Range(0, (int)CARD_TYPE.END));

                gameChanged -= gameChanged.GetInvocationList()[gameChanged.GetInvocationList().GetLength(0) - 1] as StateChange;
            }
        };
    }

    // 카드 강화
    void EnhanceCardMode()
    {
        _gameState = GAME_STATE.ROUND_READY;

        _myDeck.Reset();
        _enemyDeck.Reset();

        // 강화할 카드가 없으면 시작버튼 활성화
        if(Singleton.aiManager.Get_CardToEnhance(_myDeck.Get_Deck()) == null) 
            _uiPlayer._BlackSprite.SetActive(false);
    }

    public void NextQuarter()
    {
        // Next Quarter
        _quarter++;
        _attribute = (CARD_TYPE)Random.Range(0, (int)CARD_TYPE.END);
        _uiRound.SetRound(_quarter, (int)_attribute);

        // Select View
        _uiPlayer._SelectCardView.SetType(CARD_TYPE.END);
        _uiPlayer._SelectCardView.SetFront(true);
        _uiEnemy._SelectCardView.SetType(CARD_TYPE.END);
        _uiEnemy._SelectCardView.SetFront(false);

        // 내 카드 강화
        Card myCard = _myDeck.Get_SelectedCard();
        if(null != myCard)
        {
            myCard.Enhance(true);
            myCard.onDeSelected();
        }

        // 적 카드 강화
        Card enemyCard = Singleton.aiManager.Get_CardToEnhance(_enemyDeck.Get_Deck());
        if (null != enemyCard) 
            enemyCard.Enhance(true);

        // Card Select
        _gameState = GAME_STATE.CARD_SELECT;
    }


    // 5개 카드 선택
    public void SetMyCard(Card[] myCard)
    {
        CARD_TYPE[] type = new CARD_TYPE[myCard.Length];

        for(int i=0; i<myCard.Length; ++i)
            type[i] = myCard[i]._cardType;

        _myDeck.Set_Deck(type);

        (Singleton.popUpManager.Get_PopUp("SystemMessage") as SystemMessage).ShowMessage("Ready", 1f, false);
        (Singleton.popUpManager.Get_PopUp("SystemMessage") as SystemMessage).SetCallBackFunc(gameObject, "GameStart");
    }

    // 게임 시작!
    public void GameStart()
    {
        _gameState = GAME_STATE.CARD_SELECT;

        _attribute = (CARD_TYPE)Random.Range(0, (int)CARD_TYPE.END);
        _uiRound.SetRound(_quarter, (int)_attribute);

        _enemyDeck.Set_Deck(Singleton.aiManager.Get_EnemyTypeRandom()); // 적 카드 (PVE)
    }

#if UNITY_EDITOR
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //Singleton.popUpManager.systemMesssage.ShowMessage("Ready", 1f, true);

        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            //SceneChangeManager.Change_Scene("Proto");

            
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            _myDeck.UseCard(_myDeck.Get_LeftmostCard());
            _enemyDeck.UseCard(_enemyDeck.Get_LeftmostCard());
        }
    }
#endif
}

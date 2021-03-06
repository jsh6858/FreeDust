﻿using System.Collections;
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

    bool _bDuece;

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

        _bDuece = false;

        _gameState = GAME_STATE.END;
    }


    // 전투 Anim 시작
    void Battle_Animation()
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

        LogManager.Log("my : " + myCard._cardType + "\nenemy : " + enemyCard._cardType);

        // Use Card
        _myDeck.UseCard(myCard);
        _enemyDeck.UseCard(enemyCard);

        // 스킬 확인
        if(_uiPlayer._skill._bActivated)
        {
            _uiPlayer._skill.UseSkill();
            UsefulFunction.SwapCard(ref myCard, ref enemyCard);

            LogManager.Log("나 스킬 사용");
        }
        if(Singleton.aiManager.EnemyUseSkill(_enemyDeck, _uiEnemy))
        {
            _uiEnemy._skill.UseSkill();
            UsefulFunction.SwapCard(ref myCard, ref enemyCard);

            LogManager.Log("적 스킬 사용");
        }

        // Select View     
        _uiPlayer._SelectCardView.SetType(myCard._cardType, myCard._bEnhanced);
        _uiPlayer._SelectCardView.SetFront(false);
        _uiPlayer._SelectCardView.SetSelectCardMode(false);
        _uiEnemy._SelectCardView.SetType(enemyCard._cardType, enemyCard._bEnhanced);
        _uiEnemy._SelectCardView.SetFront(false);

        // Damage
        float damageToMe = BattleManager.Get_Damage(enemyCard, _uiEnemy, myCard._cardType);
        float damageToEnemy = BattleManager.Get_Damage(myCard, _uiPlayer, enemyCard._cardType);

        float resultToMe = damageToMe;
        float resultToEnemy = damageToEnemy;

        if (enemyCard._cardType == CARD_TYPE.HEAL)
        {
            resultToMe -= damageToMe / 2f;
            resultToEnemy -= damageToMe / 2f;
        }
        if(myCard._cardType == CARD_TYPE.HEAL)
        {
            resultToEnemy -= damageToEnemy / 2f;
            resultToMe -= damageToEnemy / 2f;
        }

        _uiPlayer._hpDamaged = -resultToMe;
        _uiEnemy._hpDamaged = -resultToEnemy;
        
        // Anim
        StartCoroutine(Play_Anim(BattleManager.Get_BattleResult(myCard._cardType, enemyCard._cardType)));
    }

    public void NextQuarter()
    {
        // Card Select
        _gameState = GAME_STATE.CARD_SELECT;

        // Next Quarter
        _quarter++;
        _attribute = (CARD_TYPE)Random.Range(0, (int)CARD_TYPE.END);
        _uiRound.SetRound(_quarter, (int)_attribute);

        // Select View
        Initialize_SelectView();

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
    }

    // OK 버튼 누르거나, 시간 다 되었을때
    public void Start_Battle()
    {
        if(_gameState == GAME_STATE.CARD_SELECT) // 배틀 Anim 시작!
            Battle_Animation();
        if(_gameState == GAME_STATE.ROUND_READY) // 다음 쿼터로
            NextQuarter();
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

        yield return new WaitUntil(()=> (_uiPlayer._hpDamaged == 0) && (_uiEnemy._hpDamaged == 0));

        if((_uiPlayer._hp <= 0f && _uiEnemy._hp <= 0f)) // 듀스
        {
            if(!_bDuece) (Singleton.popUpManager.Get_PopUp("SystemMessage") as SystemMessage).ShowMessage("듀스", 0.5f, false);

            StartCoroutine(PlayDeuce());
        }
        else if(_uiPlayer._hp <= 0f)
        {
            (Singleton.popUpManager.Get_PopUp("SystemMessage") as SystemMessage).ShowMessage("LOSE", 1f, true);
            Singleton.popUpManager.Get_PopUp("SystemMessage").SetCallBackFunc(gameObject, "GoToMain");
        }
        else if(_uiEnemy._hp <= 0f)
        {
            (Singleton.popUpManager.Get_PopUp("SystemMessage") as SystemMessage).ShowMessage("WIN", 1f, true);
            Singleton.popUpManager.Get_PopUp("SystemMessage").SetCallBackFunc(gameObject, "GoToMain");
        }
        else
        {
            if(_bDuece)
                StartCoroutine(PlayDeuce());
            else
                AnimFinish();
        }
            
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
        Initialize_SelectView();

        if(_myDeck.LeftCount() == 1)
            CardChangeMode();
        else if(_myDeck.LeftCount() == 0)
            EnhanceCardMode();
    }

    // 카드 변경
    void CardChangeMode()
    {
        if(!_bDuece) (Singleton.popUpManager.Get_PopUp("SystemMessage") as SystemMessage).ShowMessage("카드를\n 교체하세요", 1f);

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
        (Singleton.popUpManager.Get_PopUp("SystemMessage") as SystemMessage).ShowMessage("카드를\n 강화하세요", 1f);

        _gameState = GAME_STATE.ROUND_READY;

        _myDeck.Reset();
        _enemyDeck.Reset();

        // 강화할 카드가 없으면 시작버튼 활성화
        if(Singleton.aiManager.Get_CardToEnhance(_myDeck.Get_Deck()) == null) 
            _uiPlayer._BlackSprite.SetActive(false);
    }

    IEnumerator PlayDeuce()
    {
        _bDuece = true;

        _uiPlayer.SetHp(1);
        _uiEnemy.SetHp(1);

        _myDeck.Reset();
        _enemyDeck.Reset();

        while(true)
        {
            if(_myDeck.LeftCount() == 1)
                break;

             yield return new WaitForSeconds(1f);

            _myDeck.UseCard(_myDeck.Get_LeftmostCard());
            _enemyDeck.UseCard(_enemyDeck.Get_LeftmostCard());
        }

        Initialize_SelectView();

        AnimFinish();

        yield break;
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

    public void GoToMain()
    {
        SceneChangeManager.Change_Scene("Main");
    }

    void Initialize_SelectView()
    {
        _uiPlayer._SelectCardView.SetType(CARD_TYPE.END);
        _uiPlayer._SelectCardView.SetFront(true);
        _uiEnemy._SelectCardView.SetType(CARD_TYPE.END);
        _uiEnemy._SelectCardView.SetFront(false);
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

        if(Input.GetKeyDown(KeyCode.L))
        {
            _myDeck.UseCard(_myDeck.Get_LeftmostCard());
            _enemyDeck.UseCard(_enemyDeck.Get_LeftmostCard());
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            _myDeck.UseCard(_myDeck.Get_RightmostCard());
            _enemyDeck.UseCard(_enemyDeck.Get_RightmostCard());
        }

        
    }
#endif

}

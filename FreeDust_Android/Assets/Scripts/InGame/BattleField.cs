using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour {
	readonly string[] OBJECTS_STRINGS = new string[] { "Timer", "RandomType", "ProgressButton", "Info", "BigCard"};
	Dictionary<string, GameObject> m_dicObjects;

	// Deck
	public Deck_Mine m_deckMine;
	Deck_Enemy m_deckEnemy;

	// Card Anim
	BigCard m_bigCardMine;
	BigCard m_bigCardEnemy;

	// Rand Type
	public CARD_TYPE m_randType;
	public UISprite m_sprRandomType;

	// Timer
	public UILabel m_labelTime;
	float m_fTime;

	// Round
	int m_iRound;

	// Info (Quarter Time)
	public UILabel m_labelInfo;

	void Awake()
	{
		m_deckMine = GameObject.Instantiate((Resources.Load("Prefab/Deck_Mine") as GameObject)).GetComponent<Deck_Mine>();
		m_deckEnemy = GameObject.Instantiate((Resources.Load("Prefab/Deck_Enemy") as GameObject)).GetComponent<Deck_Enemy>();

		m_deckMine.transform.SetParent(transform.Find("Deck"));
		m_deckEnemy.transform.SetParent(transform.Find("Deck"));

		m_dicObjects = new Dictionary<string, GameObject>();
		for(int i=0; i<OBJECTS_STRINGS.Length; ++i)
			m_dicObjects.Add(OBJECTS_STRINGS[i], transform.Find(OBJECTS_STRINGS[i]).gameObject);

		m_iRound = 0;
	}

	public void Start_Battle()
	{
        // Cards
		Set_MyDeck();
		Set_AIDeck();
		
        // Skills
		m_deckMine.Set_Skill(Resources.Load("Prefab/Skill/Card_Change") as GameObject);
		m_deckEnemy.Set_Skill(Resources.Load("Prefab/Skill/Card_Change") as GameObject);

        // Character
        m_deckMine.Set_Character(Resources.Load("Prefab/Character/Monster01") as GameObject, false);
        m_deckEnemy.Set_Character(Resources.Load("Prefab/Character/Monster02") as GameObject, true);

		// Big Card (for battle)
		m_bigCardMine = (GameObject.Instantiate(Resources.Load("Prefab/BigCard") as GameObject, Vector3.zero, Quaternion.identity)).GetComponent<BigCard>();
		m_bigCardMine.transform.SetParent(transform.Find("BigCard/Mine"), false);
		m_bigCardMine.m_itemExplain.SetActive(true);

		m_bigCardEnemy = (GameObject.Instantiate(Resources.Load("Prefab/BigCard") as GameObject, Vector3.zero, Quaternion.identity)).GetComponent<BigCard>();
		m_bigCardEnemy.transform.SetParent(transform.Find("BigCard/Enemy"), false);
		m_bigCardEnemy.m_itemExplain.SetActive(true);

		Round_Start();
    }

	// 새 라운드 시작 (카드 선택)
	public void Round_Start()
	{
		Global._gameMode = GAME_MODE.PLAY_CARD;
		Close_All();

		m_iRound++;
		m_fTime = 30f;

		m_dicObjects["Timer"].SetActive(true);
		m_dicObjects["RandomType"].SetActive(true);
		m_dicObjects["ProgressButton"].SetActive(true);

		Set_RandomType();
	}

	// 카드 선택 한 이후 or 카드 교체 완료
	public void Push_OkButton()
    {
        if (Global._gameMode == GAME_MODE.PLAY_CARD)
        {
            Global._gameMode = GAME_MODE.BATTLE_RESULT;
            Close_All();

            m_dicObjects["RandomType"].SetActive(true);

            StartCoroutine(Start_BattleAnim());
        }
		else if(Global._gameMode == GAME_MODE.CHANGE_CARD)
		{
			// Small Card Selection이 있는 동안에 버튼 안눌리게 예외처리..
			for(int i=0; i<m_deckMine.m_smallCard.Length; ++i)
			{
				if(m_deckMine.m_smallCard[i]._objTypeSelect.activeSelf)
					return;
			}

			m_iRound = 0;
			m_deckMine.Reset_CardChange();

			Change_AICard();
			Round_Start();
		}
    }

	IEnumerator Start_BattleAnim()
	{
		Debug.Log("전투 Animation 시작");

		// 싸울 카드들 
		Card mine = m_deckMine.Get_SelectedCard();
		Card enemy = m_deckEnemy.Submit_RandomCard();

		(mine as SmallCard).Set_Activate(false);
		(mine as SmallCard).Set_Selected(false);
		(enemy as SmallCard).Set_Activate(false);
		(enemy as SmallCard).Set_Selected(false);
		
		/* 
		Debug.Log("========== 전투 결과 창 ==========");
		Debug.Log("라운드 : " + m_iRound);
		Debug.Log("내 카드 파워 : " + mine._txtNum.text + " 속성 : " + mine.m_cardType);
		Debug.Log("적 카드 파워 : " + enemy._txtNum.text + " 속성 : " + enemy.m_cardType);
		Debug.Log("중앙 랜덤 속성 : " + m_randType);
		Debug.Log("========== 전투 결과 창 ==========");
		*/
		
		m_bigCardMine.SetCard(mine.m_iPower, mine.m_cardType);
		m_bigCardMine.Show_Info();
		m_bigCardEnemy.SetCard(enemy.m_iPower, enemy.m_cardType);
		m_bigCardEnemy.Show_Info();

		// Bic Card Animation
		m_dicObjects["BigCard"].SetActive(true);

		float fTime = 10f;

		while(true)
		{
			fTime -= Time.deltaTime;

			if(fTime < 0f)
				break;
			if(Input.GetMouseButton(0))
				break;	

			yield return null;
		}

		Card_Manager.Instance.Damage_Result(m_deckMine, mine, m_deckEnemy, enemy, m_randType);

		int iMyCurHp = m_deckMine.m_character.m_hp.m_iCurHp;
		int iEnemyCurHp = m_deckEnemy.m_character.m_hp.m_iCurHp;

		yield return new WaitForSeconds(1f);

		// 듀스
		if(iMyCurHp <= 0 && iEnemyCurHp <= 0)
		{
			m_deckMine.m_character.m_hp.Set_CurHp(1);
			m_deckEnemy.m_character.m_hp.Set_CurHp(1);
		}
		// 전투 종료
		else if(iMyCurHp <= 0 || iEnemyCurHp <= 0)
		{
			Close_All();

			m_dicObjects["Info"].SetActive(true);
			m_labelInfo.text = "전투 종료! \n 6초 뒤 다시 시작합니다";

			yield return new WaitForSeconds(6f);

			GameManager.Instance.m_inGameManager.Start_CardSelect();
		}

		// next round
		if(m_iRound == 5)
			Change_Card();
		else
			Round_Start();
	}

	// 5 라운드 종료 후 카드 교체의 시간
	void Change_Card()
	{
		Debug.Log("카드 교체!");

		Global._gameMode = GAME_MODE.CHANGE_CARD;
		Close_All();

		m_deckMine.Activate_Cards();
		m_deckEnemy.Activate_Cards();

		m_fTime = 60f;

		m_dicObjects["Info"].SetActive(true);
		m_dicObjects["Timer"].SetActive(true);
		m_dicObjects["ProgressButton"].SetActive(true);
	}

	// AI가 카드를 최대 2개까지 바꿈
	void Change_AICard()
	{
		int iRand = Random.Range(0, 3); // 카드 교체할 수

		if(iRand == 0)
			return;
		if(iRand == 1)
		{
			SmallCard[] smallCard = m_deckEnemy.m_smallCard;

			iRand = Random.Range(0, smallCard.Length);

			smallCard[iRand].ChangeType_Random();
		}
		else if(iRand == 2)
		{
			SmallCard[] smallCard = m_deckEnemy.m_smallCard;

			List<int> list = UsefulFunction.Get_Random_Numbers(2, new int[]{1,2,3,4,5});

			smallCard[list[0]].ChangeType_Random();
			smallCard[list[1]].ChangeType_Random();
		}
	}

	void Set_MyDeck()
	{
		BigCard[] bigCards = GameManager.Instance.m_inGameManager._cardSelect.selectCard;

		for(int i=0; i<5; ++i)
		{
			m_deckMine.Set_Card(i, i + 1, bigCards[i].m_cardType, false);
		}
	}
	void Set_AIDeck()
	{
		int[] iArr = {1, 2, 3, 4, 5};
		UsefulFunction.Shuffle(iArr);

		for(int i=0; i<5; ++i)
		{
			m_deckEnemy.Set_Card(i, iArr[i], (CARD_TYPE)Random.Range(0, 3), true);
		}
	}

	// 매 턴 중앙 타입
	public void Set_RandomType()
	{
		m_randType = (CARD_TYPE)Random.Range(0,3);

		m_sprRandomType.spriteName = Global.STR_ATTRIBUTE[(int)m_randType];
	}

	void Update()
	{
		if(m_dicObjects["Timer"].activeSelf)
		{
			m_fTime -= Time.deltaTime;
			m_labelTime.text = (int)m_fTime + "s";

			if(m_fTime < 0f)
            {
                if (Global._gameMode == GAME_MODE.PLAY_CARD)
                    Push_OkButton();
                if (Global._gameMode == GAME_MODE.CHANGE_CARD)
                {
                    m_iRound = 0;
                    Round_Start();
                }
            }
		}

		if(Input.GetKeyDown(KeyCode.B))
			GameManager.Instance.m_inGameManager.Start_CardSelect();
	}

	void Close_All()
	{
		foreach(KeyValuePair<string, GameObject> pair in m_dicObjects)
		{
			pair.Value.SetActive(false);
		}
	}
}

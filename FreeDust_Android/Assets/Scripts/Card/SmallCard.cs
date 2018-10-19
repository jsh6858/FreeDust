using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCard : Card {

	bool m_bEnemy;

	public GameObject m_Mine;
	public GameObject m_Enemy;
	
	public BigCard m_bigCard; // for show

	public UISprite _sprEnemyType;

	public bool m_bActivated;

	// 현재 선택된 카드
	public bool m_bSelected;

	public GameObject m_darkCover;

    public GameObject _objTypeSelect;
	
	void Awake()
	{
		m_bigCard = (GameObject.Instantiate(Resources.Load("Prefab/BigCard") as GameObject, Vector3.zero, Quaternion.identity)).GetComponent<BigCard>();
		m_bigCard.m_itemExplain.SetActive(true);
		m_bigCard.transform.SetParent(transform.Find("BigCard"), false);

		Set_Activate(true);
		Set_Selected(false);
	}

	public override void SetCard(int iNum, CARD_TYPE cardType = CARD_TYPE.END)
    {
        base.SetCard(iNum, cardType);

		_sprEnemyType.spriteName = Global.STR_ATTRIBUTE[(int)m_cardType];

		m_bigCard.SetCard(iNum, cardType); // small card가 들고있는 big card
    }

	public void Set_Enemy(bool b)
	{
		m_bEnemy = b;

		m_Enemy.SetActive(m_bEnemy);
		m_Mine.SetActive(!m_bEnemy);
	}

	public void Set_Activate(bool b)
	{
		m_bActivated = b;

		m_darkCover.SetActive(!b);
	}

	public void Set_Selected(bool b)
	{
		m_bSelected = b;

		m_bigCard.gameObject.SetActive(b);

		if(b)
			m_bigCard.Show_Info();
	}

	public void AddEvent_ShowBigCard(EventDelegate newEvent)
	{
		UIButton button = transform.Find("bgSprite").GetComponent<UIButton>();
		
		EventDelegate.Add(button.onClick, newEvent);
	}

	public void Push_SelectType(GameObject obj)
    {
        _objTypeSelect.SetActive(false);

		List<KeyValuePair<SmallCard, Card_Info>> list = GameManager.Instance.m_inGameManager._BattleField.m_deckMine.Changed_SmallCard;

		int iSelect = int.Parse(obj.name);

		if(m_cardType == (CARD_TYPE)iSelect)
			return;

		// 변경 이전을 저장
		if(list.Count == 0)
		{
			Add_List(list, new KeyValuePair<SmallCard, Card_Info>(this, new Card_Info(m_iPower, m_cardType)));
		}
		else if(list.Count == 1)
		{
			if(list[0].Key != this)
				Add_List(list, new KeyValuePair<SmallCard, Card_Info>(this, new Card_Info(m_iPower, m_cardType)));
        }
        else
        {
            if (list[0].Key != this && list[1].Key != this)
            {
                list[0].Key.SetCard(list[0].Value._iPower, list[0].Value._type);
                list[0].Key.Emphasize(false);
                list.RemoveAt(0);

				Add_List(list, new KeyValuePair<SmallCard, Card_Info>(this, new Card_Info(m_iPower, m_cardType)));
            }
		}

        m_cardType = (CARD_TYPE)iSelect;

        SetCard(m_iPower, m_cardType);
		
    }

	void Add_List(List<KeyValuePair<SmallCard, Card_Info>> list, KeyValuePair<SmallCard, Card_Info> pair)
	{
		list.Add(pair);
		Emphasize(true);
	}

	public void OpenChangeType(bool b)
    {
        _objTypeSelect.SetActive(b);
    }

	public void Emphasize(bool b)
	{
		if(b)
			transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
		else
			transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// 다른 속성으로 랜덤 변환
	public void ChangeType_Random()
	{
		List<CARD_TYPE> list = UsefulFunction.Get_OtherType(m_cardType);

		SetCard(m_iPower, list[(int)Random.Range(0, 2)]);
	}
}

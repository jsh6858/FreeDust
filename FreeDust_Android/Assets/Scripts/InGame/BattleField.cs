using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour {

	Deck_Mine m_deckMine;
	Deck_Enemy m_deckEnemy;

	void Awake()
	{
		m_deckMine = GameObject.Instantiate((Resources.Load("Prefab/Deck_Mine") as GameObject)).GetComponent<Deck_Mine>();
		m_deckEnemy = GameObject.Instantiate((Resources.Load("Prefab/Deck_Enemy") as GameObject)).GetComponent<Deck_Enemy>();

		m_deckMine.transform.SetParent(transform.Find("Deck"));
		m_deckEnemy.transform.SetParent(transform.Find("Deck"));
	}

	public void Start_Battle()
	{
		Set_MyDeck();
		Set_AIDeck();
		
		m_deckMine.Set_Skill(GameObject.Instantiate(Resources.Load("Prefab/Skill/Card_Change") as GameObject));
		m_deckEnemy.Set_Skill(GameObject.Instantiate(Resources.Load("Prefab/Skill/Card_Change") as GameObject));
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
}

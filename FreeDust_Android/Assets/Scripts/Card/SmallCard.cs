using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCard : Card {

	bool bEnemy;

	public GameObject m_Mine;
	public GameObject m_Enemy;
	
	public UISprite _sprEnemyType;

	void Awake()
	{
		
	}

	public override void SetCard(int iNum, CARD_TYPE cardType = CARD_TYPE.END)
    {
        base.SetCard(iNum, cardType);

		_sprEnemyType.spriteName = _strAttribute[(int)m_cardType];

		
    }

	public void Set_Enemy(bool b)
	{
		bEnemy = b;

		m_Enemy.SetActive(bEnemy);
		m_Mine.SetActive(!bEnemy);
	}


}

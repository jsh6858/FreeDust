using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
	public SmallCard[] m_smallCard;
	public UIGrid m_grid;

    // Skill

    // Ani
    public Character m_character;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            m_character.Set_Damage(-1000);
        }
    }

    protected void Initialzie()
	{
		m_smallCard = new SmallCard[5];

		GameObject smallCard = Resources.Load("Prefab/SmallCard") as GameObject;
		Transform parent = m_grid.transform;

		for(int i=0; i<5; ++i)
		{
			GameObject obj = GameObject.Instantiate(smallCard, Vector3.zero, Quaternion.identity);
			
			m_smallCard[i] = obj.GetComponent<SmallCard>();
			obj.transform.SetParent(parent);
		}

		m_grid.Reposition();
	}
    public void Set_Card(int iIndex, int iNum, CARD_TYPE cardType, bool bEnemy)
	{
		m_smallCard[iIndex].SetCard(iNum, cardType);
		m_smallCard[iIndex].Set_Enemy(bEnemy);
	}

	public void Set_Skill(GameObject skill)
	{
		GameObject obj = GameObject.Instantiate(skill, Vector3.zero, Quaternion.identity); 
		obj.transform.SetParent(transform.Find("Skill"), false );
	}

	public void Set_Character(GameObject character, bool bEnemy)
	{
        m_character = (GameObject.Instantiate(character, Vector3.zero, Quaternion.identity)).GetComponent<Character>();
        m_character.transform.SetParent(transform.Find("Character"), false);

        m_character.Set_Character(bEnemy);
    }

	// TimeOver, AI
    public Card Submit_RandomCard()
    {
        List<int> listInt = new List<int>();
        for(int i=0; i<m_smallCard.Length; ++i)
        {
            if(m_smallCard[i].m_bActivated)
                listInt.Add(i);
        }

        if(0 == listInt.Count)
            return null;

        int iRand = Random.Range(0, listInt.Count);

        return m_smallCard[listInt[iRand]];
    }

    public bool Set_Damage(int iDamage)
    {
        return m_character.Set_Damage(iDamage);
    }

    public void Activate_Cards()
    {
        for(int i=0; i<m_smallCard.Length; ++i)
        {
            m_smallCard[i].Set_Activate(true);
        }
    }
}

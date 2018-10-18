using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
	public SmallCard[] m_smallCard;
	public UIGrid m_grid;

    // Skill

    // Ani
    Character m_character;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            m_character.Set_Damage(-1000);
        }
    }

    void Awake()
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
}

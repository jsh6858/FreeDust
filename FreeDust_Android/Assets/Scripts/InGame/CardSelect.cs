using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour {

    BigCard[] _selectCard;
	public BigCard[] selectCard
	{
		get
		{
			if(null == _selectCard)
				_selectCard = new BigCard[5];

			return _selectCard;
		}
	}

	public void Start_CardSelect()
	{
        Debug.Log("Start CardSelect");

		gameObject.SetActive(true);

		GameObject card = Resources.Load("Prefab/BigCard") as GameObject;
		Transform grid = transform.Find("AnchorC/Grid");

		for(int i=0; i<selectCard.Length; ++i)
		{
			GameObject temp = GameObject.Instantiate(card, Vector3.zero, Quaternion.identity);
			temp.transform.SetParent(grid);

			selectCard[i] = temp.GetComponent<BigCard>();
			selectCard[i].SetCard(i+1, CARD_TYPE.ATTACK);

			// 이벤트 넣어주기
            EventDelegate newEvent = new EventDelegate(this, "OnPush_OpenSelection");
            newEvent.parameters[0] = UsefulFunction.MakeParameter(selectCard[i], typeof(BigCard));
            selectCard[i].AddEvent_typeSelect(newEvent);
        }
    }

    public int Get_SelectedCardCount()
	{
		int iCount = 0;

		for(int i=0; i<selectCard.Length; ++i)
		{
			if(selectCard[i].m_cardType != CARD_TYPE.END)
				iCount++;
		}

		return iCount;
	}

    public void OnPush_OpenSelection(BigCard bigcard)
    {
        for(int i=0; i<selectCard.Length; ++i)
        {
            selectCard[i].OpenChangeType(selectCard[i] == bigcard);
        }
    }

	public void Play_Game()
	{
		GameManager.Instance.m_inGameManager.Start_Game();
	}
}

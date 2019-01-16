using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Parent : MonoBehaviour {

	public SelectCardView _SelectCardView;

	public void SetCardView(CARD_TYPE type)
	{
		_SelectCardView.SetType(type);
	}

	public void Set_CardSelect_CardType(CARD_TYPE type)
	{
		_SelectCardView.SetType(type);

		_SelectCardView.PlayAnim();
	}
}

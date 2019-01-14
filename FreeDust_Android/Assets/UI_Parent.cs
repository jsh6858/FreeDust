using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Parent : MonoBehaviour {

	SelectCardView _SelectCardView;
	SelectCardView SelectCardView
	{
		get
		{
			if(null == _SelectCardView)
				_SelectCardView = transform.Find("Card_Select").GetComponent<SelectCardView>();
			return _SelectCardView;
		}
	}

	public void Set_CardSelect_CardType(CARD_TYPE type)
	{
		SelectCardView.SetType(type);

		SelectCardView.PlayAnim();
	}
}

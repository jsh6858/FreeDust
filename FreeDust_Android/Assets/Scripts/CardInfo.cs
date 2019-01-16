using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour {

	UILabel _label;

	UILabel[] _damageExpcected;

	UISprite _image;

	readonly string[] SpriteName = new string[3] {"Ui_Card_Info_At", "Ui_Card_Info_Df","Ui_Card_Info_Heal",};

	void Awake()
	{
		_label = transform.Find("Title").GetComponent<UILabel>();

		_damageExpcected = new UILabel[(int)CARD_TYPE.END];

		_damageExpcected[0] = transform.Find("Attribute/Attack/Label").GetComponent<UILabel>();
		_damageExpcected[1] = transform.Find("Attribute/Defense/Label").GetComponent<UILabel>();
		_damageExpcected[2] = transform.Find("Attribute/Heal/Label").GetComponent<UILabel>();

		_image = transform.Find("Type").GetComponent<UISprite>();
	}

	public void OnTouchScreen()
	{
		gameObject.SetActive(false);
	}

	public void SetCardInfo(Card card)
	{
		CARD_TYPE type = card._cardType;

		_label.text = Constants.ATTRIBUTE_NAME[(int)type];
		_image.spriteName = SpriteName[(int)type];

		
	}
}

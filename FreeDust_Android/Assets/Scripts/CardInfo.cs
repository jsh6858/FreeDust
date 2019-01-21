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
		gameObject.SetActive(true);

        transform.localPosition = new Vector2(
        Mathf.Clamp(card.transform.localPosition.x, -230f, 230f), 260f);

		CARD_TYPE type = card._cardType;

		_label.text = Constants.ATTRIBUTE_NAME[(int)type];
		_image.spriteName = SpriteName[(int)type];

		float fDamage = 0f;

		for(int i=0; i<(int)CARD_TYPE.END; ++i)
		{
			fDamage = BattleManager.Get_Damage
			(card,
				Singleton.inGameManager._uiPlayer,
				(CARD_TYPE)i
			);

			_damageExpcected[i].text = fDamage.ToString();
			_damageExpcected[i].color = (fDamage == 0f) ? Color.red : Color.white;

			if(card._cardType == Singleton.inGameManager._attribute && fDamage != 0f)
				_damageExpcected[i].color = Color.green;
		}
	}

	void LateUpdate()
	{
		if(Singleton.inGameManager._gameState != GAME_STATE.CARD_SELECT)
			OnTouchScreen();
	}
}

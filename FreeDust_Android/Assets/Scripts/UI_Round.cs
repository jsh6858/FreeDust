using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Round : MonoBehaviour {

	UILabel _timer;

	UILabel _roundCount;

	UISprite[] _roundAttribute;

	readonly Color32[] ATTRIBUTE_COLOR = new Color32[3] { new Color32(209, 32, 3, 255), new Color32(32, 3, 209, 255), new Color32(3, 209, 32, 255)};

	bool _bStart = false;

	int _remainingTime;
	float fTime;

	void Awake()
	{
		_timer = transform.Find("AnchorC/Timer").GetComponent<UILabel>();
		_roundCount = transform.Find("AnchorC/Round_Count").GetComponent<UILabel>();

		_roundAttribute = new UISprite[2];
		_roundAttribute[0] = transform.Find("AnchorC/Sign_Attribute_01").GetComponent<UISprite>();
		_roundAttribute[1] = transform.Find("AnchorC/Sign_Attribute_02").GetComponent<UISprite>();
	}

	public void SetRound(int iRound, int attribute)
	{
		_roundCount.text = iRound.ToString();

		_remainingTime = 30;
		_timer.text = _remainingTime.ToString();

		fTime = 1f;

		_roundAttribute[0].color = ATTRIBUTE_COLOR[attribute];
		_roundAttribute[1].color = ATTRIBUTE_COLOR[attribute];
	}

	void Update()
	{
		if(Singleton.inGameManager._gameState == GAME_STATE.CARD_SELECT || // 시간이 흐르는 조건
			Singleton.inGameManager._gameState == GAME_STATE.ROUND_READY)	
		{
			fTime -= Time.deltaTime;
			if(fTime < 0f)
			{
				fTime = 1f;
				_remainingTime--;

				_timer.text = _remainingTime.ToString();

				if(_remainingTime == 0)
					Singleton.inGameManager.Start_Battle();
			}
		}
	}
}

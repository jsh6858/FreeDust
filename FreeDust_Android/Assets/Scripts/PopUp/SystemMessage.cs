using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMessage : PopUpBase {

	UILabel _label;
	UIButton _button;

	void Awake()
	{
		_label = transform.Find("Label").GetComponent<UILabel>();
		_button = transform.Find("Sprite").GetComponent<UIButton>();
	}

	public void ShowMessage(string text, float fTime = -1f, bool isButton = false)
	{
		gameObject.SetActive(true);

		_label.text = text;

		if(fTime == -1f)
		{
			_button.enabled = isButton;
		}
		else
		{
			if(!isButton)
			{
				_button.enabled = false;
				Invoke("OnClickScreen", fTime);
			}
			else
			{
				_button.enabled = false;
				Invoke("EnableButton", fTime);
			}
		}
	}

	void EnableButton()
	{
		_button.enabled = true;
	}

	public void OnClickScreen()
	{
		gameObject.SetActive(false);

		if(_callBackFunc != null && _callBackObj != null)
		{
			_callBackObj.SendMessage(_callBackFunc);

			_callBackObj = null;
			_callBackFunc = null;
		}
	}

}

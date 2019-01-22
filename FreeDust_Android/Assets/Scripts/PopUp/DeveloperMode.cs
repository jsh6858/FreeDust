using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperMode : PopUpBase {

	GameObject[] _Edits;

	void Awake()
	{
		Transform trEdit = transform.Find("Edit");

		_Edits = new GameObject[trEdit.childCount];

		for(int i=0; i<_Edits.Length; ++i)
		{
			_Edits[i] = trEdit.GetChild(i).gameObject;
		}
	}

	public void OnSubmit(UIInput input)
	{
		LogManager.Log(input.label.text);
	}



	public void OnClickSave()
	{
		float[] value = new float[_Edits.Length];

		for(int i=0; i<_Edits.Length; ++i)
		{
			UIInput input = _Edits[i].transform.Find("Input").GetComponent<UIInput>();

			if(input.value == "")
				value[i] = -1000f;
			else
			{
				value[i] = float.Parse(input.value);
			}

			//LogManager.Log(_Edits[i].name + " : " + value[i]);
		}

		ParserManager.DeveloperSetting(value);

		SceneChangeManager.Change_Scene("Proto");
	}

	public void OnClickExit()
	{
		gameObject.SetActive(false);
	}	
}

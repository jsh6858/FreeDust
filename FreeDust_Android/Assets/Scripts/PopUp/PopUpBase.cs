using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBase : MonoBehaviour {

	protected GameObject _callBackObj;
	protected string _callBackFunc;

	public void SetCallBackFunc(GameObject obj, string str)
	{
		_callBackObj = obj;
		_callBackFunc = str;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour {

	void Awake()
	{	
		Singleton.popUpManager = this;
	}

	SystemMessage _systemMesssage;
	public SystemMessage systemMesssage
	{
		get
		{
			if(null == _systemMesssage)
			{
				GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/PopUp/SystemMessage") as GameObject, Vector3.zero, Quaternion.identity);
				obj.transform.SetParent(transform, false);

				_systemMesssage = obj.GetComponent<SystemMessage>();
			}
			return _systemMesssage;
		}
	}
}

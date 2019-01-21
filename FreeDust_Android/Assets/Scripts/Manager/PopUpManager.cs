using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour {

	Dictionary<string, PopUpBase> _popUp = new Dictionary<string, PopUpBase>();

	void Awake()
	{	
		Singleton.popUpManager = this;
	}

	public PopUpBase Get_PopUp(string name)
	{
		if(_popUp.ContainsKey(name))
			return _popUp[name];

		GameObject obj = GameObject.Instantiate(Resources.Load(string.Format("Prefab/PopUp/{0}", name)) as GameObject, Vector3.zero, Quaternion.identity);
		obj.transform.SetParent(transform, false);
		obj.name = name;

		PopUpBase basePopUp = obj.GetComponent<PopUpBase>();
		_popUp.Add(name, basePopUp);

		return basePopUp;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardView : MonoBehaviour {

	Animator _animator;
	Animator animator
	{
		get
		{
			if(null == _animator)
				_animator = GetComponent<Animator>();
			return _animator;
		}
	}

	GameObject[] _objType;
    GameObject[] objType
    {
        get
        {
            if(null == _objType)
            {
                _objType = new GameObject[(int)CARD_TYPE.END];

                _objType[(int)CARD_TYPE.ATTACK] = transform.Find("Front/Option/Attack").gameObject;
                _objType[(int)CARD_TYPE.DEFEND] = transform.Find("Front/Option/Defend").gameObject;
                _objType[(int)CARD_TYPE.HEAL] = transform.Find("Front/Option/Heal").gameObject;
            }
            return _objType;
        }
    }

	public void SetType(CARD_TYPE type)
	{
		for(int i=0; i<objType.Length; ++i)
		{
			if(i == (int)type)
				_objType[i].SetActive(true);
			else
				_objType[i].SetActive(false);
		}
	}

	public void PlayAnim()
	{
		animator.Play("Open");
	}
}

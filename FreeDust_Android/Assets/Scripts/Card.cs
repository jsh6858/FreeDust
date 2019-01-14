using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public CARD_TYPE _cardType;

    GameObject[] _objType;
    GameObject[] objType
    {
        get
        {
            if(null == _objType)
            {
                _objType = new GameObject[(int)CARD_TYPE.END];

                _objType[(int)CARD_TYPE.ATTACK] = transform.Find("Image/Attack").gameObject;
                _objType[(int)CARD_TYPE.DEFEND] = transform.Find("Image/Defend").gameObject;
                _objType[(int)CARD_TYPE.HEAL] = transform.Find("Image/Heal").gameObject;
            }
            return _objType;
        }
    }

    public GameObject _objSelect;

    public bool _bSelected = false;
    public bool _bUsed = false;
    
    void Awake ()
    {
		
	}

    public void Set_CardType(CARD_TYPE type)
    {
        _cardType = type;

        for(int i=0; i<objType.Length;++i)
        {
            if(i == (int)type)
                objType[i].SetActive(true);
            else
                objType[i].SetActive(false);
        }
    }

    public virtual void OnSelected()
    {
        if(false == _bSelected)
        {
            _objSelect.SetActive(true);
            _bSelected = true;
        }
        else
        {
            Singleton.inGameManager.Start_Battle();
        }

    }

    public virtual void onDeSelected()
    {
        _objSelect.SetActive(false);
        _bSelected = false;
    }
}

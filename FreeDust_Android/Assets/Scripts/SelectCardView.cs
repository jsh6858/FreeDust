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

	GameObject _back;
	GameObject back
	{
		get
		{
			if(null == _back)
				_back = transform.Find("Back").gameObject;
			return _back;
		}
		
	}
	GameObject _front;
	GameObject front
	{
		get
		{
			if(null == _front)
				_front = transform.Find("Front").gameObject;
			return _front;
		}
	}

	GameObject _scrollView;
	GameObject scrollView
	{
		get
		{
			if(null == _scrollView)
			{
				_scrollView = transform.Find("Scrollview").gameObject;

				_scrollView.transform.Find("Grid").GetComponent<UICenterOnChild>().onCenter += CenterCallBack;
			}
			return _scrollView;
		}
	}

	GameObject _enhance;
	GameObject enhance
	{
		get
		{
			if(null == _enhance)
			{
				_enhance = transform.Find("Front/Enhance").gameObject;
			}
			return _enhance;
		}
	}
	
	void Start()
	{
	}

	public void SetFront(bool b)
	{
		front.SetActive(b);
		back.SetActive(!b);	
	}
	
	public void SetType(CARD_TYPE type, bool bEnhance = false)
	{
		for(int i=0; i<objType.Length; ++i)
		{
			if(i == (int)type)
				_objType[i].SetActive(true);
			else
				_objType[i].SetActive(false);
		}
		enhance.SetActive(bEnhance);
	}
	public IEnumerator PlayAnim(string anim)
	{
		animator.enabled = true;
		animator.Play(anim);

		yield return new WaitForEndOfFrame();

		while(true)
		{
			if(animator.GetCurrentAnimatorStateInfo(0).IsName(anim))
			{
				yield return null;
			}
			else
				break;
		}

		animator.enabled = false;
		yield break;
	}

	public void SetSelectCardMode(bool b)
	{
		scrollView.SetActive(b);

		if(b == true)
		{
			SetFront(false);

			Card lastCard = Singleton.inGameManager._myDeck.Get_LeftmostCard();
			scrollView.transform.Find("Grid").GetComponent<UICenterOnChild>().CenterOn(scrollView.transform.Find("Grid").GetChild((int)lastCard._cardType));
		}
	}


	void CenterCallBack(GameObject centeredObject)
    {
        Card card = Singleton.inGameManager._myDeck.Get_LeftmostCard();
        if(card == null)
            return;

        switch(centeredObject.name)
        {
            case "Attack":
                card.Set_CardType(CARD_TYPE.ATTACK);
                break;
            case "Defend":
                card.Set_CardType(CARD_TYPE.DEFEND);
                break;
            case "Heal":
                card.Set_CardType(CARD_TYPE.HEAL);
                break;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Parent : MonoBehaviour {

	public float _hp;
	public float _ad;
	public SelectCardView _SelectCardView;

	public Animator _damaged;

	public float _hpDamaged;

	public UILabel _damagedHp;
	public UILabel _curHp;

	void Awake()
	{
		_hp = ParserManager.HP;
		_ad = ParserManager.AD;

		_curHp.text = ((int)_hp).ToString();
	}

	public IEnumerator PlayDamagedAnim(string anim)
	{
		_damagedHp.text = ((int)_hpDamaged).ToString();

		_damagedHp.color = (_hpDamaged >= 0f) ? Color.green : Color.red;

		_damaged.Play(anim);
		yield return new WaitForEndOfFrame();

		while(true)
		{
			if(_damaged.GetCurrentAnimatorStateInfo(0).IsName(anim))
			{
				yield return null;
			}
			else
				break;
		}

		yield break;
	}

	public IEnumerator PlayHpAnim()
	{
		int hpCur = (int)_hp;
		int hpDamaged = (int)_hpDamaged;

		if(hpDamaged == 0)
			yield break;

		float fTime = 0f;

		while(true)
		{
			fTime += Time.deltaTime;
			if(fTime > 1f)
				break;

			_hp = hpCur + hpDamaged * fTime;
			_curHp.text = ((int)_hp).ToString();

			yield return null;
		}

		_hp = hpCur + hpDamaged;
		_curHp.text = ((int)_hp).ToString();

		yield break;
	}

	
}

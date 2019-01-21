using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {
	public Animator _animator;

	public IEnumerator Play_Anim(string anim)
	{
		gameObject.SetActive(true);
		_animator.Play(anim);

		yield return new WaitForEndOfFrame();
		while(true)
		{

			if(_animator.GetCurrentAnimatorStateInfo(0).IsName(anim))
			{
				yield return null;
			}
			else
				break;
		}

		gameObject.SetActive(false);
		yield break;
	}
}

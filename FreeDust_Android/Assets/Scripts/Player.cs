using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Animator _animator;

	public IEnumerator PlayAnim(string anim)
	{
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

		yield break;
	}
}

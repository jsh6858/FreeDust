using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void OnClickOKButton()
	{
		SceneManager.LoadSceneAsync("Proto");
	}
}

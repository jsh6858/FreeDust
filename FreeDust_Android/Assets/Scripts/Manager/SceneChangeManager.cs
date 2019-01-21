using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChangeManager {

	public static void Change_Scene(string scene)
	{
		SceneManager.LoadSceneAsync(scene);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogManager{

	public static void Log(object log)
	{
#if UNITY_EDITOR
		Debug.Log(log);
#endif
	}
}

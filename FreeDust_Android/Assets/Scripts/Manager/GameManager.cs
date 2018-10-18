using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {
	static GameManager _Instance;
	public static GameManager Instance
	{
		get
		{
			if(null == _Instance)
				_Instance = new GameManager();
			return _Instance;
		}
	}
    
	public InGameManager m_inGameManager;
	
}

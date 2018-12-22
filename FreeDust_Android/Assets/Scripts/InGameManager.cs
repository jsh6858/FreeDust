using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {
    
    public static InGameManager _Instance;
    public static InGameManager Instance
    {
        get
        {
            if (null == _Instance)
                _Instance = new InGameManager();
            return _Instance;
        }
    }

    public UI_Player _uiPlayer;
    public UI_Enemy _uiEnemy;

    public void Start_Battle(CARD_TYPE myType, CARD_TYPE enemyType)
    {

    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Player : UI_Parent {

    public GameObject _BlackSprite;



    void Start()
    {
        _BlackSprite = transform.Find("AnchorRB/OkButton/BlackSprite").gameObject;

        Singleton.inGameManager.gameChanged += delegate(GAME_STATE state)
        {
            if(state != GAME_STATE.CARD_SELECT)
            {
                _BlackSprite.SetActive(true);
            }
        };

        
    }

    public void OnClickOkButton()
    {
        _BlackSprite.SetActive(true);
        
        Singleton.inGameManager.Start_Battle();
    }
}

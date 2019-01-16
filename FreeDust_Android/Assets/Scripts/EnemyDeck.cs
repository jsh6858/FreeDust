using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeck : Deck {
    
    void Awake()
    {
        //Set_Deck(Singleton.aiManager.Get_EnemyTypeRandom());
    }

    void Start()
    {
        Singleton.inGameManager._enemyDeck = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    void Awake()
    {
        Singleton.inGameManager = this;
    }

    public UI_Player _uiPlayer;
    public UI_Enemy _uiEnemy;

    public MyDeck _myDeck;
    public EnemyDeck _enemyDeck;

    public void Start_Battle()
    {
        Debug.Log("Battle Start!");

        Card myCard = _myDeck.Get_SelectedCard();
        Card enemyCard = _enemyDeck.Get_SelectedCard();

        if(enemyCard == null)
            enemyCard = Singleton.aiManager.Choose_Random(_enemyDeck.Get_Deck());

        CARD_TYPE myType = myCard._cardType;
        CARD_TYPE enemyType = enemyCard._cardType;

        Debug.Log("my " + myType + "enemy " + enemyType);

        // 스킬 확인


        // CardSelect        
        _uiPlayer.Set_CardSelect_CardType(myType);
        _uiEnemy.Set_CardSelect_CardType(enemyType);

        
    }

    public void Game_Start(Card[] myCard)
    {
        CARD_TYPE[] type = new CARD_TYPE[myCard.Length];

        for(int i=0; i<myCard.Length; ++i)
            type[i] = myCard[i]._cardType;

        _myDeck.Set_Deck(type);
    }
}

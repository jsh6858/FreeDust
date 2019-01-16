public enum PLAYER_STATE
{
    IDLE,
    
}

public enum CARD_TYPE
{
    ATTACK,
    DEFEND,
    HEAL,
    END
}

public enum GAME_STATE
{
    READY,

    CARD_SELECT,

    BATTLE,

    CHANGE_CARD,

    END,
}

public enum LAYER
{
    NGUI = 8,

    MainUI = 10,

    Back = 11,

    Player = 12,

    Card = 13,

    PopUp = 20,
}
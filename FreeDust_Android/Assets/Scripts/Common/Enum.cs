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

    CARD_SELECT, // 카드 고르기

    BATTLE, // 전투 Animation

    ROUND_READY, // 카드 강화

    END,
}

public enum BATTLE_RESULT
{
    WIN,
    DRAW,
    LOSE,
    END
}
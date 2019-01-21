using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleManager {

	public static float Get_Damage(Card card, UI_Parent player, CARD_TYPE enemyType)
	{
		float damage = 0f;
		CARD_TYPE myType = card._cardType;
		BATTLE_RESULT result = Get_BattleResult(myType, enemyType);

		// Character
		float AD = player._ad;
		float TC = ParserManager.TC[(int)myType][(int)result];
		float TE = (card._bEnhanced) ? ParserManager.TE[(int)myType] : 1f;

		// Item
		float AP = 0f;

		// Stage
		float FC = (Singleton.inGameManager._attribute == myType) ? ParserManager.FC : 1f;
		float QC = ParserManager.QC[Singleton.inGameManager._quarter];

		damage = (AD + AP) * TC * TE * FC * QC;
		damage = Mathf.Round(damage);

		// Log

		LogManager.Log("Type Counter : " + TC + " Type Enhanced : " + TE + " Field Control : " + FC + " Quarter Control : " + QC + " Total Damage : " + damage);

		return damage;
	}

	public static BATTLE_RESULT Get_BattleResult(CARD_TYPE myType, CARD_TYPE enemyType)
	{
		BATTLE_RESULT result = BATTLE_RESULT.END;

		if(myType == CARD_TYPE.ATTACK)
		{
			if(enemyType == CARD_TYPE.ATTACK)
				result = BATTLE_RESULT.DRAW;
			else if(enemyType == CARD_TYPE.DEFEND)
				result = BATTLE_RESULT.LOSE;
			else if(enemyType == CARD_TYPE.HEAL)
				result = BATTLE_RESULT.WIN;
		}
		else if(myType == CARD_TYPE.DEFEND)
		{
			if(enemyType == CARD_TYPE.ATTACK)
				result = BATTLE_RESULT.WIN;
			else if(enemyType == CARD_TYPE.DEFEND)
				result = BATTLE_RESULT.DRAW;
			else if(enemyType == CARD_TYPE.HEAL)
				result = BATTLE_RESULT.LOSE;
		}
		else if(myType == CARD_TYPE.HEAL)
		{
			if(enemyType == CARD_TYPE.ATTACK)
				result = BATTLE_RESULT.LOSE;
			else if(enemyType == CARD_TYPE.DEFEND)
				result = BATTLE_RESULT.WIN;
			else if(enemyType == CARD_TYPE.HEAL)
				result = BATTLE_RESULT.DRAW;
		}

		return result;
	}
}

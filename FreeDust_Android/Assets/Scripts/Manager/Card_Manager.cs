using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Manager {

	static Card_Manager _Instance;
	public static Card_Manager Instance
	{
		get
		{
			if(null == _Instance)
				_Instance = new Card_Manager();
			return _Instance;
		}
	}

	public float Get_Damage(CARD_TYPE cardType, int iPower, int iWeaponDamage, bool bCounter)
	{
		float fResult = 0f;

		if(bCounter)
			fResult = iWeaponDamage + (Mathf.Pow(iWeaponDamage, (1.2f + iPower * 0.01f))) * Global.SUPERIOR[(int)cardType];
		else
			fResult = iWeaponDamage + (Mathf.Pow(iWeaponDamage, (1.2f + iPower * 0.01f)));

		return fResult;
	}

	public void Damage_Result(Deck myDeck, Card myCard, Deck enemyDeck, Card enemyCard, CARD_TYPE randType)
	{
		CARD_TYPE myType = myCard.m_cardType;
		CARD_TYPE yourType = enemyCard.m_cardType;

		int myPower = myCard.m_iPower;
		int yourPower = enemyCard.m_iPower;

		if(randType == myType)
			myPower++;
		if(randType == yourType)
			yourPower++;

        if (myType == CARD_TYPE.ATTACK)
        {

            if (yourType == CARD_TYPE.ATTACK)
            {
				myDeck.Set_Damage( -(int)Get_Damage(yourType, yourPower, Global.WEAPON_POWER, false) );

				enemyDeck.Set_Damage( -(int)Get_Damage(myType, myPower, Global.WEAPON_POWER, false) );
            }

            else if (yourType == CARD_TYPE.SHIELD)
            {
				myDeck.Set_Damage( -(int)Get_Damage(yourType, yourPower, Global.WEAPON_POWER, true) );

				//enemyDeck.Set_Damage( (int)Get_Damage(myType, myPower, Global.WEAPON_POWER, false) );

            }

            else if (yourType == CARD_TYPE.HEAL)
            {
				//myDeck.Set_Damage( (int)Get_Damage(yourType, yourPower, Global.WEAPON_POWER, false) );

				enemyDeck.Set_Damage( -(int)Get_Damage(myType, myPower, Global.WEAPON_POWER, true) );

            }

        }

        else if (myType == CARD_TYPE.SHIELD)
        {

			if (yourType == CARD_TYPE.ATTACK)
            {
				//myDeck.Set_Damage( (int)Get_Damage(yourType, yourPower, Global.WEAPON_POWER, false) );

				enemyDeck.Set_Damage( -(int)Get_Damage(myType, myPower, Global.WEAPON_POWER, true) );

            }

            else if (yourType == CARD_TYPE.SHIELD)
            {


            }

            else if (yourType == CARD_TYPE.HEAL)
            {
				myDeck.Set_Damage( -(int)Get_Damage(yourType, yourPower, Global.WEAPON_POWER, true) / 2 );

				enemyDeck.Set_Damage( +(int)Get_Damage(myType, myPower, Global.WEAPON_POWER, true) / 2 );

            }

        }

        else if (myType == CARD_TYPE.HEAL)
        {

			if (yourType == CARD_TYPE.ATTACK)
            {
				myDeck.Set_Damage( -(int)Get_Damage(yourType, yourPower, Global.WEAPON_POWER, true) );

				//enemyDeck.Set_Damage( -(int)Get_Damage(myType, myPower, Global.WEAPON_POWER, false) );

            }

            else if (yourType == CARD_TYPE.SHIELD)
            {
				myDeck.Set_Damage( +(int)Get_Damage(yourType, yourPower, Global.WEAPON_POWER, true) / 2 );

				enemyDeck.Set_Damage( -(int)Get_Damage(myType, myPower, Global.WEAPON_POWER, true) / 2 );

            }

            else if (yourType == CARD_TYPE.HEAL)
            {
				myDeck.Set_Damage( +(int)Get_Damage(yourType, yourPower, Global.WEAPON_POWER, false) );

				enemyDeck.Set_Damage( +(int)Get_Damage(myType, myPower, Global.WEAPON_POWER, false) );

            }
        }
	}

}

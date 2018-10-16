using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    protected readonly string[] _strAttribute = { "Attack", "Shield", "Heal", "Attack" };

    public CARD_TYPE m_cardType = CARD_TYPE.END;

    public int m_iPower = 0;
}

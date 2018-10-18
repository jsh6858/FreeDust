using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    protected readonly string[] _strAttribute = { "Attack", "Shield", "Heal", "Attack" };
    protected readonly Color[] _color = {Color.red, Color.blue, Color.green, Color.red};
    public CARD_TYPE m_cardType = CARD_TYPE.END;
    public int m_iPower = 0;
    public UILabel _txtNum; // x3
    public UISprite _sprType;

    public virtual void SetCard(int iNum, CARD_TYPE cardType = CARD_TYPE.END)
    {
        m_iPower = iNum;
        _txtNum.text = "x" + m_iPower;
        _txtNum.color = _color[(int)cardType];

        m_cardType = cardType;
        _sprType.spriteName = _strAttribute[(int)cardType];
    }
}

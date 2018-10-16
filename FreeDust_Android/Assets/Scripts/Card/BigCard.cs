using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCard : Card {
    
    #region Varibles
    
    public UILabel _txtNum; // x3

    public UISprite _sprItem; 
    public UISprite _sprType;

    public GameObject _objTypeSelect;

    #endregion

    public void SetCard(int iNum, CARD_TYPE cardType = CARD_TYPE.END)
    {
        m_iPower = iNum;
        _txtNum.text = "x" + m_iPower;

        m_cardType = cardType;
        _sprType.spriteName = _strAttribute[(int)m_cardType];
    }

    public void OpenChangeType(bool b)
    {
        _objTypeSelect.SetActive(b);
    }

    public void Push_SelectType(GameObject obj)
    {
        Debug.Log("Card Selected!");

        _objTypeSelect.SetActive(false);

        int iSelect = int.Parse(obj.name);

        m_cardType = (CARD_TYPE)iSelect;

        _sprType.spriteName = _strAttribute[(int)m_cardType];
        _sprItem.spriteName = _strAttribute[(int)m_cardType];
    }

    public void AddEvent_typeSelect(EventDelegate newEvent)
    {
        EventDelegate.Add(_sprType.GetComponent<UIButton>().onClick, newEvent);
    }
}

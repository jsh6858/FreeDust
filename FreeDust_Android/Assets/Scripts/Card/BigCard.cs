using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCard : Card {

    #region Varibles

    public UISprite _sprItem;
    public GameObject _objTypeSelect;

    #endregion

    public override void SetCard(int iNum, CARD_TYPE cardType = CARD_TYPE.END)
    {
        base.SetCard(iNum, cardType);
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

        base.SetCard(m_iPower, m_cardType);
        _sprItem.spriteName = _strAttribute[(int)m_cardType];
    }

    public void AddEvent_typeSelect(EventDelegate newEvent)
    {
        EventDelegate.Add(_sprType.GetComponent<UIButton>().onClick, newEvent);
    }


}

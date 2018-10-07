using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCard : MonoBehaviour {

    readonly string[] _strAttribute = { "Attack", "Shield", "Heal", "Attack" };

    #region Varibles

    public int _iNum = 0;
    public CARD_TYPE _cardType = CARD_TYPE.END;

    public UILabel _txtNum; // x3

    public UISprite _sprItem; 
    public UISprite _sprType;

    public GameObject _objTypeSelect;

    #endregion

    public void SetCard(int iNum, CARD_TYPE cardType = CARD_TYPE.END)
    {
        _iNum = iNum;
        _txtNum.text = "x" + _iNum;

        _cardType = cardType;
        _sprType.spriteName = _strAttribute[(int)_cardType];
    }

    public void Push_ChangeType()
    {
        _objTypeSelect.SetActive(true);


    }

    public void Push_SelectType(GameObject obj)
    {
        Debug.Log("Card Selected!");

        _objTypeSelect.SetActive(false);

        int iSelect = int.Parse(obj.name);

        _cardType = (CARD_TYPE)iSelect;

        _sprType.spriteName = _strAttribute[(int)_cardType];
        _sprItem.spriteName = _strAttribute[(int)_cardType];
    }

}

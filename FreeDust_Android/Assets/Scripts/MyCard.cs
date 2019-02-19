using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCard : Card {
    
    public delegate void showToolTip(MyCard card, bool state);
    public showToolTip toolTip;

    Animator _animator;

    public GameObject _blackSprite;
    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _blackSprite = transform.Find("Image/BlackSprite").gameObject;
    }

    void Start()
    {
        // 강화된 카드는 또 강화 불가 (선택 불가)
        Singleton.inGameManager.gameChanged += delegate(GAME_STATE state)
        {
            if(state == GAME_STATE.ROUND_READY)
            {
                if(_bEnhanced)
                    _blackSprite.SetActive(true);
                else
                    _blackSprite.SetActive(false);
            }
            else
            {
                _blackSprite.SetActive(false);
            }
        };
    }
    
    public override void OnSelected()
    {
        base.OnSelected();

        if(gameObject.activeSelf)
        {
            _animator.enabled = true;
            _animator.Play("Card_Push");
        }
    }

    public override void onDeSelected()
    {
        base.onDeSelected();

        if(gameObject.activeSelf)
        {
            _animator.Rebind();
            _animator.enabled = false;
        }
    }

    public void OnTooltip(bool state)
    {
        if(null != toolTip)
            toolTip(this, state);
    }
}

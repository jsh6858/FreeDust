using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCard : Card {
    
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
}

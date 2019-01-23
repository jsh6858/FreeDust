using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Change : MonoBehaviour {

    public UILabel _coolDown;
    public GameObject _blackSprite;
    public GameObject _Activated;

    public bool _bActivated;
    float _fTime;

    public int _cool;

    private void Start()
    {
        Singleton.inGameManager.gameChanged += delegate (GAME_STATE state)
        {
            if (!_blackSprite.activeSelf)
                return;

            if(state == GAME_STATE.CARD_SELECT)
            {
                _cool--;
                _coolDown.text = _cool.ToString();

                if(_cool == 0)
                {
                    _blackSprite.SetActive(false);
                    _coolDown.cachedGameObject.SetActive(false);
                }
            }
        };
    }

    public void UseSkill()
    {
        _blackSprite.SetActive(true);
        _coolDown.cachedGameObject.SetActive(true);

        _bActivated = false;
        _Activated.transform.localScale = Vector2.one;

        _cool = 5;
        _coolDown.text = _cool.ToString();
    }

	public void OnclickSkill()
    {
        _bActivated = !_bActivated;

        if (_bActivated == false)
            _Activated.transform.localScale = Vector2.one;
    }

    private void Update()
    {
        if (!_bActivated)
            return;

        _fTime += Time.deltaTime * Mathf.PI * 2f;

        float value = (Mathf.Cos(_fTime) + 20) / 21f;
        _Activated.transform.localScale = new Vector2(value, value);
    }
}

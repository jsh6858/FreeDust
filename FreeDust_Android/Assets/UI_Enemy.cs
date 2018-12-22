using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Enemy : MonoBehaviour {

    private void Awake()
    {
        InGameManager.Instance._uiEnemy = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Player : MonoBehaviour {

    private void Awake()
    {
        InGameManager.Instance._uiPlayer = this;
    }
}

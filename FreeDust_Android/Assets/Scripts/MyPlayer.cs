using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player {

    

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B))
        {
            _animator.Play("At_Dr_Ready");
        }

		if(Input.GetKeyDown(KeyCode.C))
        {
            _animator.Play("At_Dr_Back");
        }
	}
}


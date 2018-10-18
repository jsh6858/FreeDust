using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulFunction : MonoBehaviour {

	public static void Shuffle(int[] iArr)
	{
		int iTemp = 0;

		for(int i=0; i<100; ++i)
		{
			int iRand1 = Random.Range(0, iArr.Length);
			int iRand2 = Random.Range(0, iArr.Length);

			iTemp = iArr[iRand1];
			iArr[iRand1] = iArr[iRand2];
			iArr[iRand2] = iTemp;
		}
	}

	public static EventDelegate.Parameter MakeParameter(Object _value, System.Type _type)
    {
        EventDelegate.Parameter param = new EventDelegate.Parameter();  // 이벤트 parameter 생성.
        param.obj = _value;                                              // 이벤트 함수에 전달하고 싶은 값.
        param.expectedType = _type;                                     // 값의 타입.

        return param;
    }
}

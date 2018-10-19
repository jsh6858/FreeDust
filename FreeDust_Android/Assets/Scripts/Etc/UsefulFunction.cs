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

	public static List<CARD_TYPE> Get_OtherType(CARD_TYPE type)
	{
		List<CARD_TYPE> list = new List<CARD_TYPE>();

		for(int i=0; i< (int)CARD_TYPE.END; ++i)
		{
			if(type == (CARD_TYPE)i)
				continue;

			list.Add((CARD_TYPE)i);
		}

		return list;
	}

	// count : 2, list [1,2,3,4,5]
	public static List<int> Get_Random_Numbers(int iCount, int[] iArr)
	{
		List<int> result = new List<int>();

		int iMax = iArr.Length;

		while(true)
		{
			if(result.Count == iCount)
				break;

			int iRand = iArr[Random.Range(0, iMax)];

			if(result.Contains(iRand))
				continue;

			result.Add(iRand);
		}

		return result;
	}
}

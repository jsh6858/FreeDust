using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UsefulFunction {

	public static int FindNearest(float point, float[] area)
    {
        int result = 0;

        float smallest = Mathf.Abs(point - area[0]);

        for(int i=1; i<area.Length; ++i)
        {
            float temp = Mathf.Abs(point - area[i]);

            if (temp < smallest)
            {
                result = i;
                smallest = temp;
            }
        }

        return result;
    }

    public static void SwapCard(ref Card obj1, ref Card obj2)
    {
        Card temp = obj1;
        obj1 = obj2;
        obj2 = temp;
    }
}

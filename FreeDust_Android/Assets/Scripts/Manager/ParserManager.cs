using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParserManager {

	static ParserManager()
	{
		LogManager.Log("h");
		Initialize();
	}

#region Character ID
	public static float HP;

	public static float AD; // Attack Damage

	public static float[][] TC; // Type Counter	

	public static float[] TE; // Type Enchant
#endregion

#region STAGE

	public static float FC; // Field Condition

	public static float[] QC; // Quarter Condition

#endregion

	public static void Initialize()
	{
		HP = 2000f;
		AD = 100f;
		TC = new float[3][] { new float[] { 1.3f, 1f, 0f}, new float[] { 1.5f, 0f, 0f},new float[] { 1.6f, 1f, 0f},};
		TE = new float[3] { 1.7f, 1.7f, 1.7f};

		FC = 1.15f;
		QC = new float[] { 1f, 1.15f, 1.3f, 1.45f, 1.6f, 1.75f, 1.9f, 2.05f, 2.20f, 2.5f};
	}

	public static void DeveloperSetting(float [] value)
	{
		SetValue(ref HP, value[0]);

		SetValue(ref TC[0][0], value[1]);
		SetValue(ref TC[1][0], value[2]);
		SetValue(ref TC[2][0], value[3]);

		SetValue(ref TE[0], value[4]);
		SetValue(ref TE[1], value[4]);
		SetValue(ref TE[2], value[4]);

		SetValue(ref FC, value[5]);

		for(int i=0; i<10; ++i)
			SetValue(ref QC[i], value[6 + i]);
	}

	static void SetValue(ref float dst, float src)
	{
		if(src == -1000f)
			return;

		dst = src;
	}
}

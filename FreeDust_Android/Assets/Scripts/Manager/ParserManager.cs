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

		FC = 1.15f;
		QC = new float[] { 1f, 1.15f, 1.3f, 1.45f, 1.6f, 1.75f, 1.9f, 2.05f, 2.20f, 2.5f};
	}
}

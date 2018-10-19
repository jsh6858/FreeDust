using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global {

	public static GAME_MODE _gameMode;
	
	public static readonly string[] STR_ATTRIBUTE = { "Attack", "Shield", "Heal", "Attack" };


    public static readonly int PLAYER_HP = 5000;
	public static readonly int WEAPON_POWER = 100;

	public static readonly float[] SUPERIOR = new float[] {1.5f, 2f, 2f};

	public static readonly float ATT_SUPERIOR = 1.5f;
	public static readonly float DEF_SUPERIOR = 2f;
	public static readonly float HEAL_SUPERIOR = 2f;

	public static readonly int[] RAND_NUMBERS = new int[] {21, 22 ,23 ,24 ,25 ,26 ,27 ,28 ,29 ,30};

	public static readonly string[] EDIT_STRINGS = new string[] { "MyHp", "YourHp", "Att_Superior", "Def_Superior", "Heal_Superior", "WaitingTime"};
	public static readonly float[] EDIT_DEFAULTS = new float[] { 2500f, 2500f, 3.0f, 3.5f, 2.5f, 15f};


}

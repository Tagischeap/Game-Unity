using UnityEngine;
using System.Collections;

[System.Serializable]
public class Monster{

	public string species;
	public string name;
	public Type type;
	public float baseHP;
	public float curHP;
	public float baseAtk;
	public float curAtk;
	public float baseDef;
	public float curDef;
	public float baseSpd;
	public float curSpd;

	public enum Type {
		Grass,
		Fire,
		Water
	};

}

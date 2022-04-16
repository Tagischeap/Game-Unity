using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int func1;
	public int func2;
	public int func3;
	public int func4;
	public int func5;
	public ItemType itemType;

	public enum ItemType 
	{
		Weapon,
		Tool,
		Cunsumable,
		Quest,
		Material
	}
	
}

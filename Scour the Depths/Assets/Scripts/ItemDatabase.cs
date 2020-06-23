using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
	[SerializeField] private Item[] items = null;
	[SerializeField] private Dictionary<Item, int> itemToID = null;

	void OnAfterDeserialize()
	{
	}

	public int GetID(Item item)
	{
		if(itemToID == null)
		{
			itemToID = new Dictionary<Item, int>();
			for(int x = 0; x < items.Length; x++)
			{
				itemToID.Add(items[x], x);
			}
		}
		return itemToID[item];
	}
}

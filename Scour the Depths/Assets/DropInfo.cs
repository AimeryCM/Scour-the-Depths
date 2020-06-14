using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InformationHolders", menuName = "Monster Drop Info")]
public class DropInfo : ScriptableObject
{
	private List<ItemDropInfo> infos;

	public class ItemDropInfo
	{
		private Item item;
		private Dictionary<int, float> rarities;
		public ItemDropInfo(Item item, Dictionary<int, float> rarities)
		{
			this.item = item;
			this.rarities = rarities;
		}

		public Item GetItem()
		{
			return item;
		}

		public int DetermineDropAmount()
		{
			float randomValue = Random.Range(0f, 1f);
			float sumPrev = 0f;
			foreach (KeyValuePair<int, float> pair in rarities)
			{
				if(pair.Value > sumPrev && pair.Value < randomValue)
					return pair.Key;
				else
					sumPrev += pair.Value;
			}
			return 0;
		}
	}

	public List<Item> GetDrops()
	{
		List<Item> result = new List<Item>();
		foreach (ItemDropInfo idInfo in infos)
		{
			for(int x = 0; x < idInfo.DetermineDropAmount(); x++)
				result.Add(idInfo.GetItem());
		}
		return result;
	}

}



﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InformationHolders", menuName = "Monster Drop Info")]
public class DropInfo : ScriptableObject
{
	[SerializeField] private List<ItemRarity> rarityInfo = null;

	[System.Serializable]
	public struct QuantityRarity
	{
		public int quantity;
		[Range(0,1)] public float rarity;
	}

	[System.Serializable]
	public struct ItemRarity
	{
		public GameObject item;
		public List<QuantityRarity> rarities;
	}

	public struct ItemQuantity
	{
		public GameObject item;
		public int quantity;
	}

	/*
	 * Given an ItemRarity struct, goes through the rarities associated with each value and returns a quantity
	 */
	private int DetermineDropAmount(ItemRarity values)
	{
		float randomValue = Random.Range(0f, 1f);
		float sumPrev = 0f;
		foreach (QuantityRarity qrare in values.rarities)
		{
			//Debug.Log("sumPrev: " + sumPrev + " Rarity: " + qrare.rarity + " randomValue: " + randomValue);
			if(randomValue >= sumPrev && (qrare.rarity + sumPrev) >= randomValue){
				//Debug.Log("Returning " + qrare.quantity);
				return qrare.quantity;
			}
			else
				sumPrev += qrare.rarity;
		}
		return 0;
	}
	
	/*
	 * Returns all of the random drops dropped by this enemy
	 */
	public List<ItemQuantity> GetDrops()
	{
		List<ItemQuantity> result = new List<ItemQuantity>();
		int val;
		foreach (ItemRarity itemRarity in rarityInfo)
		{
			if((val = DetermineDropAmount(itemRarity)) != 0)
			{
				ItemQuantity quant = new ItemQuantity();
				quant.item = itemRarity.item;
				quant.quantity = val;
				result.Add(quant);
			}
		}
		return result;
	}

}
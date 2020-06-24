﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryBoxManager : MonoBehaviour, IDropHandler
{
	public InventoryOwner owner = InventoryOwner.Player;
	[SerializeField] private Image iconSpot = null;
	[SerializeField] private Sprite defaultSprite = null;
	private int slotID = -1;

	public void UpdateIcon(Sprite icon)
	{
		iconSpot.sprite = icon;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if(eventData.pointerDrag != null)
		{
			if(!eventData.pointerDrag.GetComponent<Image>().sprite.Equals(defaultSprite))
			{
				InventoryBoxManager otherBox = eventData.pointerDrag.GetComponentInParent<InventoryBoxManager>();
				Sprite temp = iconSpot.sprite;
				UpdateIcon(eventData.pointerDrag.GetComponent<Image>().sprite);
				eventData.pointerDrag.GetComponent<Image>().sprite = temp;
				if(owner == InventoryOwner.Player && otherBox.owner == InventoryOwner.Player)
				{
					InventoryToolbox.instance.GetGlobalComponent(owner).Swap(slotID, otherBox.GetID());
				}
				if(owner == InventoryOwner.Blacksmith && otherBox.owner == InventoryOwner.Player)
				{
					Debug.Log("Dropping from player to blacksmith");
					ProjectUtil.SwapBetweenInventories(InventoryToolbox.instance.GetGlobalComponent(owner), slotID, InventoryToolbox.instance.GetGlobalComponent(otherBox.owner), otherBox.GetID());
				}
				if(owner == InventoryOwner.Player && otherBox.owner == InventoryOwner.Blacksmith)
				{
					Debug.Log("Dropping from Blacksmith into player");
					ProjectUtil.SwapBetweenInventories(InventoryToolbox.instance.GetGlobalComponent(owner), slotID, InventoryToolbox.instance.GetGlobalComponent(otherBox.owner), otherBox.GetID());
				}
			}
		}
	}

	public void SetID(int num)
	{
		if(slotID != -1)
			Debug.LogWarning("Attempting to set inventory box ID multiple times");
		else
			slotID = num;
	}

	public int GetID()
	{
		return slotID;
	}
}
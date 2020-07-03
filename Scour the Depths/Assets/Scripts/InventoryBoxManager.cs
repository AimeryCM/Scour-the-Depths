using System.Collections;
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

	public void SetIconToDefault()
	{
		iconSpot.sprite = defaultSprite;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if(eventData.pointerDrag != null)
		{
			if(!eventData.pointerDrag.GetComponent<Image>().sprite.Equals(defaultSprite))
			{
				InventoryBoxManager otherBox = eventData.pointerDrag.GetComponentInParent<InventoryBoxManager>();
				if(owner == InventoryOwner.Player && otherBox.owner == InventoryOwner.Player)
				{
					if(InventoryToolbox.instance.GetGlobalComponent(owner).Swap(slotID, otherBox.GetID()))
					{
						Sprite temp = iconSpot.sprite;
						UpdateIcon(eventData.pointerDrag.GetComponent<Image>().sprite);
						eventData.pointerDrag.GetComponent<Image>().sprite = temp;
					}
				}
				if((owner == InventoryOwner.Blacksmith && otherBox.owner == InventoryOwner.Player) || (owner == InventoryOwner.Player && otherBox.owner == InventoryOwner.Blacksmith))
				{
					if(ProjectUtil.Sell(InventoryToolbox.instance.GetGlobalComponent(otherBox.owner), otherBox.GetID(), InventoryToolbox.instance.GetGlobalComponent(owner), slotID))
					{
						Sprite temp = iconSpot.sprite;
						UpdateIcon(eventData.pointerDrag.GetComponent<Image>().sprite);
						eventData.pointerDrag.GetComponent<Image>().sprite = temp;
					}
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryBoxManager : MonoBehaviour, IDropHandler
{
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
				Sprite temp = iconSpot.sprite;
				UpdateIcon(eventData.pointerDrag.GetComponent<Image>().sprite);
				eventData.pointerDrag.GetComponent<Image>().sprite = temp;
				PlayerInventoryManager.instance.Swap(slotID, eventData.pointerDrag.GetComponentInParent<InventoryBoxManager>().GetID());
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

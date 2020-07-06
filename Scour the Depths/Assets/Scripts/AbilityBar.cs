using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityBar : MonoBehaviour, IDropHandler
{
	[SerializeField] private Image iconSpot = null;
	[SerializeField] private Sprite defaultSprite = null;

	private int boxID = -1;

	public void OnDrop(PointerEventData eventData)
	{
		if(eventData.pointerDrag != null)
		{
			if(!eventData.pointerDrag.GetComponent<Image>().sprite.Equals(defaultSprite))
			{
				if(eventData.pointerDrag.GetComponentInParent<AbilityBar>() != null)	//source is another ability bar
				{
					if(AbilityManager.instance.Swap(boxID, eventData.pointerDrag.GetComponentInParent<AbilityBar>().GetID()))
					{
						Sprite temp = iconSpot.sprite;
						UpdateIcon(eventData.pointerDrag.GetComponent<Image>().sprite);
						eventData.pointerDrag.GetComponent<Image>().sprite = temp;
					}
				}
				else
				{
					InventoryBoxManager otherBox = eventData.pointerDrag.GetComponentInParent<InventoryBoxManager>();
					if(otherBox != null)
					{
						if(ProjectUtil.IsWeaponSlot(otherBox.GetID()))
						{
							Debug.LogWarning("Dragging between a weapon and the ability bar is unimplemented");
						}
						else if(ProjectUtil.IsTrinketSlot(otherBox.GetID()))
						{
							if(AbilityManager.instance.SetAbility(boxID, otherBox.GetID()))
							{
								UpdateIcon(eventData.pointerDrag.GetComponent<Image>().sprite);
							}
						}
					}
				}
				//if it is from a trinket or equipment slot, then get the necessary info from the equipped item

			}
		}
	}

	public void UpdateIcon(Sprite icon)
	{
		iconSpot.sprite = icon;
	}

	public void SetIconToDefault()
	{
		iconSpot.sprite = defaultSprite;
	}

	public void SetID(int num)
	{
		if(boxID != -1)
			Debug.LogWarning("Attempting to set ability bar box ID multiple times");
		else
			boxID = num;
	}

	public int GetID()
	{
		return boxID;
	}
}

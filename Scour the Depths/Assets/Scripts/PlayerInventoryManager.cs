using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInventoryManager : MonoBehaviour, IInventoryManager
{
	//public short hotbarSize = 4;
	//public float hotbarXOffset = 220;
	//public float hotbarYOffset = -20;
	public Inventory inventory = null;
	public GameObject[] hotbarBoxes = null;
	public GameObject[] inventoryBoxes = null;
	public TextMeshProUGUI coinsText = null;
	[SerializeField] private GameObject inventoryUI = null;
	//[SerializeField] private GameObject hotbarBox;
	//private GameObject[] hotbarSlots;

/*
	//for generating the inventory UI
	[SerializeField] private Image inventoryBackground;
	[SerializeField] private GameObject inventoryBox;
	public float gapSize = 10f;
	private GameObject[] inventorySlots;
*/

	void Start()
	{
		/*
		for(int x = 0; x < hotbarSlots.Length; x++)
		{
			hotbarSlots[x] = Instantiate(hotbarBox, Vector3.zero, Quaternion.identity);
			hotbarSlots[x].GetComponent<RectTransform>().SetParent(canvas.transform);
			hotbarSlots[x].GetComponent<RectTransform>().localPosition = new Vector3(x * ((hotbarXOffset * 2)/(hotbarSize - 1)) - hotbarXOffset, hotbarYOffset, 0);
		}
		*/
		inventory.Setup();
		InventoryToolbox.instance.AddGlobalComponent(InventoryOwner.Player, this);
		SetBoxIDs();
		for(int x = inventoryBoxes.Length - 1; x + hotbarBoxes.Length >= inventory.size && x >= 0; x--)
		{
			inventoryBoxes[x].SetActive(false);
		}
		//GenerateInventoryUI();
		inventory.ResetCoins();
		coinsText.SetText("Coins: " + inventory.GetCoins());
		InputHandler.instance.inventoryActions["Inventory"].performed += ctx => ToggleInventory();
	}

	private void SetBoxIDs()
	{
		for(int x = 0; x < hotbarBoxes.Length; x++)
		{
			hotbarBoxes[x].GetComponent<InventoryBoxManager>().SetID(x);
		}
		for(int x = hotbarBoxes.Length; x < inventoryBoxes.Length; x++)
		{
			inventoryBoxes[x - hotbarBoxes.Length].GetComponent<InventoryBoxManager>().SetID(x);
		}
	}

	public void ManageCoins(int change)
	{
		inventory.ManageCoins(change);
		coinsText.SetText("Coins: " + inventory.GetCoins());
		Debug.Log(inventory.GetCoins() + " coins in the inventory");
	}

	public int GetCoins()
	{
		return inventory.GetCoins();
	}

	public bool AddToInventory(Item item)
	{
		int index = inventory.Add(item);
		if(index != -1)
		{
			if(index < hotbarBoxes.Length)
			{
				hotbarBoxes[index].GetComponent<InventoryBoxManager>().UpdateIcon(item.icon);
			}
			else
			{
				inventoryBoxes[index - hotbarBoxes.Length].GetComponent<InventoryBoxManager>().UpdateIcon(item.icon);
			}
			return true;
		}
		return false;
	}

	public Inventory.InventoryInfo Remove(int slot)
	{
		return inventory.Remove(slot);
	}

	public Inventory.InventoryInfo Replace(int slot, Inventory.InventoryInfo info)
	{
		return inventory.Replace(slot, info);
	}

	public bool Swap(int slot1, int slot2)
	{
		return inventory.Swap(slot1, slot2);
	}

/*
	private void GenerateInventoryUI()
	{
		//creates the appropriate sized inventory background
		int width = hotbarSize, height = inventory.size % hotbarSize != 0 ? (inventory.size / hotbarSize) + 1: (inventory.size / hotbarSize);
		inventoryBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (width * inventoryBox.GetComponent<RectTransform>().rect.width) + ((width + 1) * gapSize));
		inventoryBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (height * inventoryBox.GetComponent<RectTransform>().rect.height) + ((height + 1) * gapSize));

		//creates inventorySize number of boxes using the previously calculated width and height
		for(int y = 0; y < height; y++)
		{
			for(int x = 0; x < width; x++)
			{
				int slotNum = x + y * width;
				if(slotNum >= inventory.size)
					break;
				inventorySlots[slotNum] = Instantiate(inventoryBox, Vector3.zero, Quaternion.identity);
				inventorySlots[slotNum].GetComponent<RectTransform>().SetParent(inventoryBackground.transform);
				float xPos = (gapSize * (x + 1)) + (x * inventoryBox.GetComponent<RectTransform>().rect.width);
				float yPos = 0 - ((gapSize * (y + 1)) + (y * inventoryBox.GetComponent<RectTransform>().rect.height));
				inventorySlots[slotNum].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
				inventorySlots[slotNum].name = "(" + xPos + "," + yPos + ")";
			}
		}
		inventoryUI.SetActive(false);
	}
*/
	public void ToggleInventory()
	{
		Debug.Log("Toggling Inventory");
		inventoryUI.SetActive(!inventoryUI.activeSelf);
	}
}

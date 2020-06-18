using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
	
	private int coins = 0;
	public short inventorySize = 16;
	public short hotbarSize = 4;
	public float hotbarXOffset = 220;
	public float hotbarYOffset = -20;
	[SerializeField] private GameObject hotbarBox;
	[SerializeField] private Canvas canvas;
	private Item[] itemList;
	private GameObject[] hotbarSlots;

	//for generating the inventory UI
	[SerializeField] private GameObject inventory;
	[SerializeField] private Image inventoryBackground;
	[SerializeField] private GameObject inventoryBox;
	public float gapSize = 10f;
	private GameObject[] inventorySlots;

	void Awake()
	{
		if(instance != null)
			Debug.LogWarning("More than one Inventory instance detected");
		instance = this;

		itemList = new Item[inventorySize];
		hotbarSlots = new GameObject[hotbarSize];
		inventorySlots = new GameObject[inventorySize];
		for(int x = 0; x < hotbarSlots.Length; x++)
		{
			hotbarSlots[x] = Instantiate(hotbarBox, Vector3.zero, Quaternion.identity);
			hotbarSlots[x].GetComponent<RectTransform>().SetParent(canvas.transform);
			hotbarSlots[x].GetComponent<RectTransform>().localPosition = new Vector3(x * ((hotbarXOffset * 2)/(hotbarSize - 1)) - hotbarXOffset, hotbarYOffset, 0);
		}
		GenerateInventoryUI();
	}
	public void ManageCoins(int change)
	{
		coins = Mathf.Clamp(coins + change, 0, int.MaxValue);
		Debug.Log(coins + " coins in the inventory");
	}

	public int GetCoins()
	{
		return coins;
	}

	public bool AddToInventory(Item item)
	{
		bool added = false;
		for(int x = 0; x < inventorySize && !added; x++)
		{
			if(itemList[x] == null)
			{
				itemList[x] = item;
				inventorySlots[x].GetComponent<HotbarManager>().UpdateIcon(item.icon);
				added = true;
				if(x < hotbarSize)
				{
					hotbarSlots[x].GetComponent<HotbarManager>().UpdateIcon(item.icon);
				}
			}
		}
		return added;
	}

	private void GenerateInventoryUI()
	{
		//creates the appropriate sized inventory background
		int width = hotbarSize, height = inventorySize % hotbarSize != 0 ? (inventorySize / hotbarSize) + 1: (inventorySize / hotbarSize);
		inventoryBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (width * inventoryBox.GetComponent<RectTransform>().rect.width) + ((width + 1) * gapSize));
		inventoryBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (height * inventoryBox.GetComponent<RectTransform>().rect.height) + ((height + 1) * gapSize));

		//creates inventorySize number of boxes using the previously calculated width and height
		for(int y = 0; y < height; y++)
		{
			for(int x = 0; x < width; x++)
			{
				int slotNum = x + y * width;
				if(slotNum >= inventorySize)
					break;
				inventorySlots[slotNum] = Instantiate(inventoryBox, Vector3.zero, Quaternion.identity);
				inventorySlots[slotNum].GetComponent<RectTransform>().SetParent(inventoryBackground.transform);
				float xPos = (gapSize * (x + 1)) + (x * inventoryBox.GetComponent<RectTransform>().rect.width);
				float yPos = 0 - ((gapSize * (y + 1)) + (y * inventoryBox.GetComponent<RectTransform>().rect.height));
				inventorySlots[slotNum].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
				inventorySlots[slotNum].name = "(" + xPos + "," + yPos + ")";
			}
		}
	}

	public void ToggleInventory()
	{
		Debug.Log("Toggling Inventory");
		inventory.SetActive(!inventory.activeSelf);
	}
}

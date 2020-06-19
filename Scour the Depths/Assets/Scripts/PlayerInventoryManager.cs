using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager instance;
	
	public short hotbarSize = 4;
	public float hotbarXOffset = 220;
	public float hotbarYOffset = -20;
	public Inventory inventory;
	[SerializeField] private GameObject hotbarBox;
	[SerializeField] private Canvas canvas;
	private GameObject[] hotbarSlots;

	//for generating the inventory UI
	[SerializeField] private GameObject inventoryUI;
	[SerializeField] private Image inventoryBackground;
	[SerializeField] private GameObject inventoryBox;
	public float gapSize = 10f;
	private GameObject[] inventorySlots;

	void Awake()
	{
		if(instance != null)
			Debug.LogWarning("More than one Inventory instance detected");
		instance = this;
	}

	void Start()
	{
		hotbarSlots = new GameObject[hotbarSize];
		inventorySlots = new GameObject[inventory.size];

		for(int x = 0; x < hotbarSlots.Length; x++)
		{
			hotbarSlots[x] = Instantiate(hotbarBox, Vector3.zero, Quaternion.identity);
			hotbarSlots[x].GetComponent<RectTransform>().SetParent(canvas.transform);
			hotbarSlots[x].GetComponent<RectTransform>().localPosition = new Vector3(x * ((hotbarXOffset * 2)/(hotbarSize - 1)) - hotbarXOffset, hotbarYOffset, 0);
		}
		GenerateInventoryUI();
		inventory.ResetCoins();
	}

	public void ManageCoins(int change)
	{
		inventory.ManageCoins(change);
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
			inventorySlots[index].GetComponent<HotbarManager>().UpdateIcon(item.icon);
			if(index < hotbarSize)
			{
				hotbarSlots[index].GetComponent<HotbarManager>().UpdateIcon(item.icon);
			}
			return true;
		}
		return false;
	}

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

	public void ToggleInventory()
	{
		Debug.Log("Toggling Inventory");
		inventoryUI.SetActive(!inventoryUI.activeSelf);
	}
}

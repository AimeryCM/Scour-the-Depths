using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInventoryManager : MonoBehaviour, IInventoryManager
{
	public PlayerInventory inventory = null;
	public GameObject[] hotbarBoxes = null;
	public GameObject[] inventoryBoxes = null;
	public GameObject[] equipmentBoxes = null;
	public GameObject[] trinketBoxes = null;
	public TextMeshProUGUI coinsText = null;
	private GameObject[] weaponObjects = null;
	[SerializeField] private GameObject inventoryUI = null;


	void Start()
	{
		inventory.Setup();
		InventoryToolbox.instance.AddGlobalComponent(InventoryOwner.Player, this);
		SetBoxIDs();
		for(int x = inventoryBoxes.Length - 1; x + hotbarBoxes.Length >= inventory.size && x >= 0; x--)
		{
			inventoryBoxes[x].SetActive(false);
		}
		inventory.ResetCoins();
		coinsText.SetText("Coins: " + inventory.GetCoins());
		InputHandler.instance.inventoryActions["Inventory"].performed += ctx => ToggleInventory();
	}

	public void Setup(List<Inventory.InventoryInfo> items)
	{
		if(items == null)
			inventory.Setup();
		else
			inventory.Setup(items);
		UpdateWeapons();
		UpdateSprites();
	}

	private void UpdateSprites()
	{
		for(int x = 0; x < inventory.size; x++)
		{
			GameObject box;
			if(x < hotbarBoxes.Length)
				box = hotbarBoxes[x];
			else
				box = inventoryBoxes[x - hotbarBoxes.Length];
			
			if(inventory.GetItem(x).occupied)
				box.GetComponent<InventoryBoxManager>().UpdateIcon(inventory.GetItem(x).item.icon);
			else
				box.GetComponent<InventoryBoxManager>().SetIconToDefault();
		}
	}

	private void UpdateWeapons()
	{
		if(weaponObjects == null)
			weaponObjects = new GameObject[GlobalVariables.weaponSlots];
		
		for(int x = 0; x < weaponObjects.Length; x++)
		{
			UpdateWeapons(x);
		}
	}

	private void UpdateWeapons(int slot)
	{
		if(weaponObjects == null)
			weaponObjects = new GameObject[GlobalVariables.weaponSlots];

		if(slot >=0 && slot < weaponObjects.Length)
		{
			Weapon temp = inventory.GetWeapon(slot);
			if(weaponObjects[slot] == null)
			{
				if(temp != null)
				{
					weaponObjects[slot] = Instantiate(temp.weaponPrefab, Vector3.zero, Quaternion.identity);
					weaponObjects[slot].transform.SetParent(GameObject.FindGameObjectsWithTag("Player")[0].transform);
					weaponObjects[slot].transform.localPosition = Vector3.zero;
				}
			}
			else if(temp == null)
			{
				Destroy(weaponObjects[slot]);
				weaponObjects[slot] = null;
			}
			else if(!ProjectUtil.ItemsEqual(temp, weaponObjects[slot].GetComponent<ItemPrefabInfo>().parentItem, inventory.database))
			{
				Destroy(weaponObjects[slot]);
				weaponObjects[slot] = Instantiate(temp.weaponPrefab, Vector3.zero, Quaternion.identity);
				weaponObjects[slot].transform.SetParent(GameObject.FindGameObjectsWithTag("Player")[0].transform);
				weaponObjects[slot].transform.localPosition = Vector3.zero;
			}
		}
	}

	private void SetBoxIDs()
	{
		for(int x = 0; x < GlobalVariables.totalPlayerInventorySlots; x++)
		{
			if(x < GlobalVariables.hotbarSlots)
			{
				hotbarBoxes[x].GetComponent<InventoryBoxManager>().SetID(x);
			}
			else if(x < GlobalVariables.hotbarSlots + GlobalVariables.playerInventorySlots)
			{
				inventoryBoxes[x - GlobalVariables.hotbarSlots].GetComponent<InventoryBoxManager>().SetID(x);
			}
			else if(x < GlobalVariables.hotbarSlots + GlobalVariables.playerInventorySlots + GlobalVariables.weaponSlots)
			{
				equipmentBoxes[x - GlobalVariables.hotbarSlots - GlobalVariables.playerInventorySlots].GetComponent<InventoryBoxManager>().SetID(x);
			}
			else{
				trinketBoxes[x - GlobalVariables.hotbarSlots - GlobalVariables.playerInventorySlots - GlobalVariables.weaponSlots].GetComponent<InventoryBoxManager>().SetID(x);
			}
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
		Inventory.InventoryInfo result = inventory.Remove(slot);
		if(ProjectUtil.IsEquipmentSlot(slot))
			UpdateWeapons(slot - inventory.size);
		return result;
	}

	public Inventory.InventoryInfo Replace(int slot, Inventory.InventoryInfo info)
	{
		Inventory.InventoryInfo result = inventory.Replace(slot, info);
		if(ProjectUtil.IsEquipmentSlot(slot))
			UpdateWeapons(slot - inventory.size);
		return result;
	}

	public bool Swap(int slot1, int slot2)
	{
		bool result = inventory.Swap(slot1, slot2);
		if(result && (ProjectUtil.IsEquipmentSlot(slot1) || ProjectUtil.IsEquipmentSlot(slot2)))
			UpdateWeapons();
		return result;
	}

	public int GetCost(int index)
	{
		Inventory.InventoryInfo itemInfo = inventory.GetItem(index);
		if(itemInfo.occupied)
			return itemInfo.item.cost * itemInfo.quantity;
		Debug.LogWarning("Getting cost of an empty item slot");
		return 0;
	}

	public void ToggleInventory()
	{
		Debug.Log("Toggling Inventory");
		Debug.Log("Player Inventory:\n" + inventory.ToString());
		inventoryUI.SetActive(!inventoryUI.activeSelf);
	}

	public GameObject GetWeaponObject(int slot)
	{
		if(weaponObjects == null)
			weaponObjects = new GameObject[GlobalVariables.weaponSlots];

		if(slot < 0 || slot >= weaponObjects.Length)
		{
			return null;
		}
		return weaponObjects[slot];
	}
}


		/*
		for(int x = 0; x < hotbarSlots.Length; x++)
		{
			hotbarSlots[x] = Instantiate(hotbarBox, Vector3.zero, Quaternion.identity);
			hotbarSlots[x].GetComponent<RectTransform>().SetParent(canvas.transform);
			hotbarSlots[x].GetComponent<RectTransform>().localPosition = new Vector3(x * ((hotbarXOffset * 2)/(hotbarSize - 1)) - hotbarXOffset, hotbarYOffset, 0);
		}
		*/

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
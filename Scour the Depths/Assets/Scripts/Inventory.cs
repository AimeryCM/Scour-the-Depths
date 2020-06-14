using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

	void Awake()
	{
		if(instance != null)
			Debug.LogWarning("More than one Inventory instance detected");
		instance = this;
	}
	
	private int coins = 0;

	public void ManageCoins(int change)
	{
		coins = Mathf.Clamp(coins + change, 0, int.MaxValue);
	}

	public int GetCoins()
	{
		return coins;
	}
}

using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Coin")
		{
			Destroy(collision.gameObject);
			Inventory.instance.ManageCoins(1);
			Debug.Log(Inventory.instance.GetCoins() + " coins");
		}
	}
}

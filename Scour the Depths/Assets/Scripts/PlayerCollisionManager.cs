﻿using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Item")
		{
			collision.gameObject.GetComponent<ItemManager>().Pickup();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
	[SerializeField] private Image iconSpot;

	public void UpdateIcon(Sprite icon)
	{
		iconSpot.enabled = true;
		iconSpot.sprite = icon;
	}
}

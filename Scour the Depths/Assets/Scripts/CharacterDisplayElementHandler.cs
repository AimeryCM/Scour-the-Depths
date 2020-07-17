using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterDisplayElementHandler : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI nameText = null;
	[SerializeField] private TextMeshProUGUI levelText = null;
	[SerializeField] private TextMeshProUGUI attackText = null;
	[SerializeField] private TextMeshProUGUI magicText = null;
	[SerializeField] private TextMeshProUGUI defenseText = null;
	[SerializeField] private TextMeshProUGUI speedText = null;
	[SerializeField] private Image characterIcon = null;
	public Button button = null;
	private int buttonID = -1;

	public void SetName(string name)
	{
		nameText.text = name;
	}

	public void SetLevel(int level)
	{
		levelText.text = "Level " + level.ToString();
	}

	public void SetSprite(Sprite sprite)
	{
		characterIcon.sprite = sprite;
	}

	public void SetStats(int attack, int magic, float resistance, int move)
	{
		attackText.text = attack.ToString();
		magicText.text = magic.ToString();
		defenseText.text = Mathf.FloorToInt(resistance * 100).ToString();
		speedText.text = move.ToString();
	}

	public int GetID()
	{
		return buttonID;
	}

	public void SetID(int num)
	{
		buttonID = num;
	}
}

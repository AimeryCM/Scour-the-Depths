using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterDisplayElementHandler : MonoBehaviour
{
	public TextMeshProUGUI nameText = null;
	public TextMeshProUGUI levelText = null;
	public TextMeshProUGUI attackText = null;
	public TextMeshProUGUI magicText = null;
	public TextMeshProUGUI defenseText = null;
	public TextMeshProUGUI speedText = null;
	public Image characterIcon = null;
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

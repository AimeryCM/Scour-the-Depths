using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BartenderManager : MonoBehaviour
{
	private List<PlayerStats> characterList = null;
	public GameObject characterDisplayElement = null;
	public GameObject uiElement = null;
	public GameObject layoutManager = null;
	public List<Sprite> classSymbols = null;

	void Start()
	{
		TestPopulate();
		HideUI();
	}

	public void OnInteract()
	{
		ShowUI();
	}

	public void OnLeave()
	{
		HideUI();
	}

	public void HideUI()
	{
		uiElement.SetActive(false);
	}

	public void ShowUI()
	{
		uiElement.SetActive(true);
	}

	public void TestPopulate()
	{
		for(int x = 0; x < 10; x++)
		{
			GameObject displayElement = Instantiate(characterDisplayElement, Vector3.zero, Quaternion.identity);
			displayElement.transform.SetParent(layoutManager.transform);
			CharacterDisplayElementHandler currentHandler = displayElement.GetComponent<CharacterDisplayElementHandler>();
			currentHandler.SetLevel(x);
			currentHandler.SetID(x);
			currentHandler.button.onClick.AddListener(delegate{ButtonClicked(currentHandler.GetID());});
		}
	}

	public void Populate(List<PlayerStats> playerStats)
	{
		foreach(PlayerStats stats in playerStats)
		{
			GameObject displayElement = Instantiate(characterDisplayElement, Vector3.zero, Quaternion.identity);
			displayElement.transform.parent = layoutManager.transform;
		}
	}

	void ButtonClicked(int index)
	{
		Debug.Log("Button " + index + " has been clicked");
	}
}

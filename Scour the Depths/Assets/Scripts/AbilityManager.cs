using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
	public static AbilityManager instance = null;
	public GameObject[] abilityBoxes = null;
	private AbilityInfo[] abilityInfos = null;
	private GameObject character = null;
	[SerializeField] private PlayerInventoryManager playerInventoryManager = null;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		SetIDs();
		abilityInfos = new AbilityInfo[abilityBoxes.Length];
		InputHandler.instance.abilityActions["FirstAbility"].performed += ctx => AbilityTriggered(0);
		InputHandler.instance.abilityActions["SecondAbility"].performed += ctx => AbilityTriggered(1);
		InputHandler.instance.abilityActions["ThirdAbility"].performed += ctx => AbilityTriggered(2);
		InputHandler.instance.abilityActions["FourthAbility"].performed += ctx => AbilityTriggered(3);
		SetPlayer();
	}

	void SetIDs()
	{
		if(abilityBoxes != null)
		{
			for(int x = 0; x < abilityBoxes.Length; x++)
			{
				abilityBoxes[x].GetComponent<AbilityBar>().SetID(x);
			}
		}
	}

	public bool Swap(int x, int y)
	{
		if(x < abilityInfos.Length && x >= 0 && y < abilityInfos.Length && y >= 0)
		{
			AbilityInfo temp = abilityInfos[x];
			abilityInfos[x] = abilityInfos[y];
			abilityInfos[y] = temp;
			return true;
		}
		return false;
	}

	public void SetPlayer()
	{
		character = GameObject.FindGameObjectsWithTag("Player")[0];
	}

	public bool SetAbility(int index, int sourceIndex)
	{
		if(index < abilityInfos.Length && index >= 0)
		{
			abilityInfos[index] = ((Trinket) playerInventoryManager.inventory.GetItem(sourceIndex).item).abilityInfo;
			return true;
		}
		return false;
	}

	public bool SetAbility(int index, AbilityInfo info)
	{
		if(index < abilityInfos.Length && index >= 0)
		{
			abilityInfos[index] = info;
			return true;
		}
		return false;
	}

	public void AbilityTriggered(int abilityNumber)
	{
		Debug.Log("Ability " + abilityNumber + " has been triggered");
		if(abilityInfos != null && abilityInfos[abilityNumber].type != Ability.Default)
		{
			UseAbility(abilityInfos[abilityNumber]);
		}
	}

	public void UseAbility(AbilityInfo ability)
	{
		switch (ability.type)
		{
			case Ability.Invisibility:
				StartCoroutine(Invisibility(ability.duration));
				break;
			default:
				break;
		}
	}

	IEnumerator Invisibility(float duration)
	{
		float elapsedTime = 0f;
		Color temp = character.GetComponent<SpriteRenderer>().color;
		float originalAlpha = temp.a;
		temp.a = 0f;
		character.GetComponent<SpriteRenderer>().color = temp;
		while(elapsedTime < duration)
		{
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		Color second = character.GetComponent<SpriteRenderer>().color;
		second.a = originalAlpha;
		character.GetComponent<SpriteRenderer>().color = second;
		Debug.Log("Invisibility Fades");
	}
}
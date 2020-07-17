using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
	//This is where all of the functions from the save thing will be given and then distributed

	[SerializeField] private BartenderManager bartenderManager = null;

	//These variables are for testing purposes only, used for building test material
	public CharacterClassStats characterClassStats = null;

	void Start()
	{
		//call the save manager read function and get all of the save info

		//pass the info from the save manager to the BartenderManager's populate function
		HandleBartender();

		//pass save info to all shops

		//pass info to inventory box (in town)
	}

	private void HandleBartender()
	{
		//handle all of the converting save manager info into a list of PlayerStats

		//Temporary test stuff
		List<PlayerStats> result = new List<PlayerStats>();
		for(int x = 0; x < 4; x++)
			result.Add(new PlayerStats(characterClassStats, new Item[0], new LinkedList<Item>(), new int[GlobalVariables.visibleTraitCount]));
		bartenderManager.Populate(result);
	}
}

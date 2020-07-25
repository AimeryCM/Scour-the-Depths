using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "ProceduralGeneration/Room")]
public class Room : ScriptableObject
{
	public int width = 1;
	public int height = 1;
	public bool[] doors = null;	//start at left most door on the Northern side of the room, go clockwise
	public GameObject roomObject = null;

	public Room()
	{
		doors = new bool[GetDoorCount()];
	}

	public Room(int wide, int high)
	{
		width = wide;
		height = high;
		doors = new bool[GetDoorCount()];
	}

	public Room(int wide, int high, GameObject room) : this(wide, high)
	{
		roomObject = room;
	}

	private int GetDoorCount()
	{
		return (width + height) * 2;
	}

	public GameObject SetRoom(GameObject newRoom)
	{
		GameObject oldRoom = roomObject;
		roomObject = newRoom;
		return oldRoom;
	}

	public void AddDoor(int location)
	{
		if(location < doors.Length && location >= 0)
		{
			doors[location] = true;
		}
		else
		{
			Debug.LogWarning("Attempting to add a door out of the range of the room");
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectUtil
{
	public static bool ItemsEqual(Item first, Item second, ItemDatabase database)
	{
		return database.GetID(first) == database.GetID(second);
	}
}

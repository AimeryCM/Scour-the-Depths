using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectScreen : MonoBehaviour
{
	public GameObject panelPrefab;
	public short panelCount = 3;
	private List<GameObject> panels;

	void Start()
	{
		for(int x = 0; x < panelCount; x++)
		{
			GameObject current = Instantiate(panelPrefab, Vector3.zero, Quaternion.identity);
			RectTransform rTrans = current.GetComponent<RectTransform>();
			//rTrans.
			panels.Add(current);

		}
	}

}
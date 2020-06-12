using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[Range (0,1)] public float cameraAcceleration;
	public Vector3 offset;
	public float cameraYStickyMod;
	private Transform characterPosition;
	private float characterBaseY;
	private Vector3 velocity = Vector3.zero;

    void Start()
    {
        GameObject[] characterList = GameObject.FindGameObjectsWithTag("Player");
		if(characterList.Length < 0)
			Debug.LogError("CameraMovement script could not find an object with the character tag");
		else
			characterPosition = characterList[0].GetComponent<Transform>();
		characterBaseY = characterPosition.position.y;
    }

    void FixedUpdate()
    {
		Vector3 targetPosition = new Vector3(characterPosition.position.x, (characterPosition.position.y * cameraYStickyMod + characterBaseY) / (1 + cameraYStickyMod), characterPosition.position.z);
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition + offset, ref velocity, cameraAcceleration);
    }
}

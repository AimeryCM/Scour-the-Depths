using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
	[SerializeField] private CircleCollider2D circleCollider;
	public UnityEvent onInteractEvent;

	private bool playerInRange = false;

	void Start()
	{
		InputHandler.instance.playerActions["Interact"].performed += ctx => AttemptInteract();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			playerInRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			playerInRange = false;
		}
	}

	private void AttemptInteract()
	{
		if(playerInRange)
		{
			Debug.Log("Interacting");
			onInteractEvent.Invoke();
		}
	}
}
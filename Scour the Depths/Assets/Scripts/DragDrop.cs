using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	private Canvas canvas = null;
	[SerializeField] private float UIScaleFactor = 9;
	[SerializeField] private CanvasGroup canvasGroup = null;
	private RectTransform rectTransform = null;

	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		Canvas[] c = GetComponentsInParent<Canvas>();
		canvas = c[c.Length - 1];
	}

	public void ResetPosition()
	{
		rectTransform.anchoredPosition = Vector3.zero;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = false;
		GetComponent<Canvas>().sortingOrder = 2;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		ResetPosition();
		canvasGroup.blocksRaycasts = true;
		GetComponent<Canvas>().sortingOrder = 1;
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += (eventData.delta / UIScaleFactor) / canvas.scaleFactor;
	}
	
	public void OnPointerDown(PointerEventData eventData)
	{
		
	}
}

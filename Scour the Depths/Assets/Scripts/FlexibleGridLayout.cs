using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
	public int rows = 0;
	public int columns = 0;
	public Vector2 cellSize = Vector2.zero;

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();

		float sqrRt = Mathf.Sqrt(transform.childCount);
		rows = Mathf.CeilToInt(sqrRt);
		columns = Mathf.CeilToInt(sqrRt);

		float parentWidth = rectTransform.rect.width;
		float parentHeight = rectTransform.rect.height;

		cellSize.x = parentWidth / (float)columns;
		cellSize.y = parentHeight / (float)rows;

		int columnCount = 0, rowCount = 0;

		for(int x = 0; x < rectChildren.Count; x++)
		{
			rowCount = x / columns;
			columnCount = x % columns;

			var xPos = (cellSize.x * columnCount);
			var yPos = (cellSize.y * rowCount);

			SetChildAlongAxis(rectChildren[x], 0, xPos, cellSize.x);
			SetChildAlongAxis(rectChildren[x], 1, yPos, cellSize.y);
		}
	}

	public override void CalculateLayoutInputVertical()
	{
		
	}

	public override void SetLayoutHorizontal()
	{

	}

	public override void SetLayoutVertical()
	{

	}
}

using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	[SerializeField]
	private int totalColumns;

	[SerializeField]
	private int totalRows;

	private readonly Color normalColor = Color.grey;

	//private readonly Color selectedColor = Color.yellow;

	public int TotalColumns
	{
		get{return totalColumns;}
		set{totalColumns = value;}
	}

	public int TotalRows
	{
		get{return totalRows;}
		set{totalRows = value;}
	}
		

	public float gridSize = 2.25f;


	private void GridFrameGizmo(int cols, int rows) 
	{
		Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, rows * gridSize, 0));
		Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(cols * gridSize, 0, 0));
		Gizmos.DrawLine(new Vector3(cols * gridSize, 0, 0), new Vector3(cols * gridSize, rows * gridSize, 0));
		Gizmos.DrawLine(new Vector3(0, rows * gridSize, 0), new Vector3(cols * gridSize, rows * gridSize, 0));
	} 

	private void GridGizmo(int cols, int rows) 
	{
		for (int i = 1 ; i < cols ; i++) 
		{
			Gizmos.DrawLine(new Vector3(i * gridSize, 0, 0), new Vector3(i * gridSize, rows * gridSize, 0));
		}
		for (int j = 1 ; j < rows ; j++) 
		{
			Gizmos.DrawLine(new Vector3(0, j * gridSize, 0), new Vector3(cols * gridSize, j * gridSize, 0));
		}
	}

	public Vector3 WorldToGridCoordinates(Vector3 point) 
	{
		Vector3 gridPoint = new Vector3((int)((point.x - transform.position.x) / gridSize) ,
			(int)((point.y - transform.position.y) / gridSize), 0.0f);
		return gridPoint;
	}

	public Vector3 GridToWorldCoordinates(int col, int row) 
	{
		Vector3 worldPoint = new Vector3(transform.position.x + (col * gridSize + gridSize / 2.0f),
			transform.position.y + (row * gridSize + gridSize / 2.0f),0.0f);
		return worldPoint;
	}
	public bool IsInsideGridBounds(Vector3 point) 
	{
		float minX = transform.position.x;
		float maxX = minX + totalColumns * gridSize;
		float minY = transform.position.y;
		float maxY = minY + totalRows * gridSize;
		return (point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY);
	}

	public bool IsInsideGridBounds(int col, int row) 
	{
		return (col >= 0 && col < totalColumns && row >= 0 && row < totalRows);
	}

	private void OnDrawGizmos() 
	{
		Color oldColor = Gizmos.color;
		Matrix4x4 oldMatrix = Gizmos.matrix; 
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = normalColor;
		GridGizmo(totalColumns, totalRows);
		GridFrameGizmo(totalColumns, totalRows);
		Gizmos.color = oldColor;
		Gizmos.matrix = oldMatrix;
	}
}

using UnityEngine;
//using UnityEditor;


[ExecuteInEditMode]
public class SnapToGridTest: MonoBehaviour 
{
	private Level instance;

	private void Awake()
	{
		instance = gameObject.transform.parent.GetComponent<Level>();
	}

	private void Update () 
	{
		Vector3 gridCoord = instance.WorldToGridCoordinates(transform.position);
		transform.position = instance.GridToWorldCoordinates((int)gridCoord.x,(int) gridCoord.y);
	}

}
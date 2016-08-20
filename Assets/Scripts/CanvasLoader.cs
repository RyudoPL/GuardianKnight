using UnityEngine;
using System.Collections;

public class CanvasLoader : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}

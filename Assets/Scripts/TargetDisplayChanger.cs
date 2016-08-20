using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TargetDisplayChanger : MonoBehaviour {

	void OnLevelWasLoaded()
	{
		if(SceneManager.GetActiveScene().name == "Arena" || SceneManager.GetActiveScene().name == "Arcade")
		{
			gameObject.GetComponent<Canvas>().targetDisplay = 0;
		}
		else
		{
			gameObject.GetComponent<Canvas>().targetDisplay = 1;
		}
	}
}

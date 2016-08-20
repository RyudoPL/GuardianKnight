using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour 
{
	//function that pauses the game
	public void PauseGame()
	{
		if(GameObject.Find("QuickTipPanel") == null || GameObject.Find("QuickTipPanel").activeSelf == false)
		{
			Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
		}
	}

}

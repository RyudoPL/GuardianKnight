using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour {


	public void Restart()
	{
		GameObject.Find("GameOverPanel").SetActive(false);
		Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
		if(SceneManager.GetActiveScene().name == "Arena")
		{
			GameObject.FindObjectOfType<GameManager>().level = 0;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

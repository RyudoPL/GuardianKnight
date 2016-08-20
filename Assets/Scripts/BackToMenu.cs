using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {

	public GameObject confirmQuit;
	public GameObject gameOverPanel; 

	public void ConfirmQuitting(bool confirm)
	{
		gameOverPanel.SetActive(!confirm);
		confirmQuit.SetActive(confirm);
	}


	public void ReturnToMenu()
	{
		AndroidGoogleAdsExample.Instance.B2Hide();
		LoadingManager loadingManager = GameObject.FindObjectOfType<LoadingManager>();
		loadingManager.loadScene = false;
		loadingManager.scene = 1;
		Time.timeScale = (Time.timeScale == 1) ? 0 : 1; 
		SceneManager.LoadScene(5);
	}
}

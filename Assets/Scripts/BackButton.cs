using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {

	public void OnClick()
	{
		/*Destroy(GameObject.Find("Knight"));
		Destroy(GameObject.Find("Canvas"));
		Destroy(GameObject.Find("AchievementCanvas"));
		Destroy(GameObject.Find("EarnCanvas"));
		Destroy(GameObject.Find("AchievementManager"));*/
		AndroidGoogleAdsExample.Instance.B3Hide();
		LoadingManager loadingManager = GameObject.FindObjectOfType<LoadingManager>();
		loadingManager.loadScene = false;
		loadingManager.scene = 1;
		SceneManager.LoadScene(5);
	}
}

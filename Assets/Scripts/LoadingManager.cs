using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour 
{
	public bool loadScene = true;

	public int scene;

	// Updates once per frame
	void Update() 
	{
		if (loadScene == false) {
			// ...set the loadScene boolean to true to prevent loading a new scene more than once...
			loadScene = true;

			// ...and start a coroutine that will load the desired scene.
			StartCoroutine(LoadNewScene());
		}

	}
		
	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	public IEnumerator LoadNewScene() 
	{
		yield return new WaitForSeconds(0.1f);

		if(scene == 1)
		{
			Destroy(GameObject.Find("Knight"));
			Destroy(GameObject.Find("Canvas"));
			Destroy(GameObject.Find("AchievementManager"));
			Destroy(GameObject.Find("LoadingManager"));
			Destroy(GameObject.Find("LocalizationManager"));
			Destroy(GameObject.Find("MusicManager"));

		}
		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(scene);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) 
		{
			yield return null;
		}
	}
}


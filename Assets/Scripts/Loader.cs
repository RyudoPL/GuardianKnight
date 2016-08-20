using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject gameManager;


	// Use this for initialization
	void Awake () //bo inaczek klociloby sie z DontSaveOnLoad
	{
		if(gameManager != null)
		{
		if (GameManager.instance == null)
			Instantiate (gameManager);
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour 
{
	public void OnClick()
	{
		SceneManager.LoadScene("HowToPlay");
	}

}

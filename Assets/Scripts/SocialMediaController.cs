using UnityEngine;
using System.Collections;

public class SocialMediaController : MonoBehaviour {

	public void ShareFB() 
	{
		StartCoroutine(PostFBScreenshot());
	}

	public void ShareTwitterScreehshot() 
	{
		StartCoroutine(PostScreenshot());
	}


	private IEnumerator PostFBScreenshot() {


		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();

		AndroidSocialGate.StartShareIntent("Share your score!", "My score: " + Bonuses.Instance.Score.ToString(), tex,"facebook.katana");

		Destroy(tex);

	}

	private IEnumerator PostScreenshot() 
	{
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();

		AndroidSocialGate.StartShareIntent("Share your score!", "My score: " + Bonuses.Instance.Score.ToString(), tex,"twi");

		Destroy(tex);

	}
}

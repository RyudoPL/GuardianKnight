using UnityEngine;
using System.Collections;

public class CanvasManager : MonoBehaviour {

	//reference to GameObject which is storing all the canvases
	public GameObject[] canvasHolder;

	//reference to the canvas which should be shown first
	public  int  currentCanvasIndex = 0;

	//reference to a previous canvas
	public GameObject previousCanvas;

	void Awake()
	{
		AndroidGoogleAdsExample.Instance.B3Show();
	}

	//function that "flips" howtoplay page forward
	public void nextInfo()
	{
		if(currentCanvasIndex < 5)
		{
			previousCanvas.SetActive(!previousCanvas.activeSelf);
			currentCanvasIndex += 1;
			canvasHolder[currentCanvasIndex].SetActive(!canvasHolder[currentCanvasIndex].activeSelf);
			previousCanvas = canvasHolder[currentCanvasIndex];
		}
		else 
			return;
	}

	//function that "flips" howtoplay page back
	public void previousInfo()
	{
		if(currentCanvasIndex >= 1)
		{
			previousCanvas.SetActive(!previousCanvas.activeSelf);
			currentCanvasIndex -= 1;
			canvasHolder[currentCanvasIndex].SetActive(!canvasHolder[currentCanvasIndex].activeSelf);
			previousCanvas = canvasHolder[currentCanvasIndex];
		}
		else
			return;
	}

}

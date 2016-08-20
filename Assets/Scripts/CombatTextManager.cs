using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour {

	//singleton
	private static CombatTextManager instance; 

	public static CombatTextManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<CombatTextManager>();
			}
			return instance;
		}
	}

	//reference to a text prefab
	public GameObject textPrefab;

	//how long it will take for a text to fade out
	public float fadeTime;

	//how fast will text move
	public float speed;

	//direction of the moving text
	public Vector3 direction;

	void Awake()
	{

	}

	//function that creates a combat text
	public void CreateText(Vector3 position,string text, Color color, bool crit, RectTransform canvasTransform) 
	{
		GameObject scrollingCombatText = (GameObject)Instantiate (textPrefab, position, Quaternion.identity);
		scrollingCombatText.transform.SetParent (canvasTransform);
		scrollingCombatText.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		scrollingCombatText.GetComponent<CombatText> ().Initialize (speed, direction, fadeTime, crit);
		scrollingCombatText.GetComponent<Text> ().text = text;
		scrollingCombatText.GetComponent<Text> ().color = color;
	}
}

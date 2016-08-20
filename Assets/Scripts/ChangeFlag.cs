using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;


public class ChangeFlag : MonoBehaviour 
{
	public Dictionary<string, Sprite> languages = new Dictionary<string,Sprite>();
	public Sprite[] flags = new Sprite[3];
	private string language = "English";
	private Language localizationManager;
	public Sprite[] gameLogo;

	void Awake()
	{
		languages.Add("Japanese",flags[0]);
		languages.Add("English",flags[1]);
		languages.Add("Polish",flags[2]);
		localizationManager = GameObject.Find("LocalizationManager").GetComponent<Language>();
	}

	void Start()
	{
		OnLevelWasLoaded();
	}

	void OnLevelWasLoaded()
	{
		if(PlayerPrefs.GetString("Language") != null)
		{
			language = PlayerPrefs.GetString("Language");
		}
		else
		{
			PlayerPrefs.SetString("Language",language);
		}

		switch (language) 
		{
		case "Japanese":
			gameObject.GetComponent<Image>().sprite = flags[0];
			localizationManager.SetLanguage(language);
			GameObject.Find("TitleImage").GetComponent<Image>().sprite = gameLogo[1];
			break;

		case "English":
			gameObject.GetComponent<Image>().sprite = flags[1];
			localizationManager.SetLanguage(language);
			GameObject.Find("TitleImage").GetComponent<Image>().sprite = gameLogo[0];
			break;
		
		case "Polish":
			gameObject.GetComponent<Image>().sprite = flags[2];
			localizationManager.SetLanguage(language);
			GameObject.Find("TitleImage").GetComponent<Image>().sprite = gameLogo[0];
			break;

		default:
			break;
		}
	}

	public void ChangeFlags()
	{
		Sprite currentFlag = GameObject.Find("Language").GetComponent<Image>().sprite;
		if(currentFlag.name == "Japan")
		{
			if(languages.TryGetValue("Polish",out flags[2]))
			{
				gameObject.GetComponent<Image>().sprite = flags[2];
			}
			GameObject.Find("TitleImage").GetComponent<Image>().sprite = gameLogo[0];
			language = "Polish";

		}
		else if(currentFlag.name == "Poland")
		{
			if(languages.TryGetValue("English",out flags[1]))
			{
				gameObject.GetComponent<Image>().sprite = flags[1];
			}
			GameObject.Find("TitleImage").GetComponent<Image>().sprite = gameLogo[0];
			language = "English";
		}
		else if(currentFlag.name == "english")
		{
			if(languages.TryGetValue("Japanese",out flags[0]))
			{
				gameObject.GetComponent<Image>().sprite = flags[0];
			}
			GameObject.Find("TitleImage").GetComponent<Image>().sprite = gameLogo[1];
			language = "Japanese";
		}
		localizationManager.SetLanguage(language);
	}
}

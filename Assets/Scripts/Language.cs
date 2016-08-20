using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using UnityEngine.SceneManagement;

public class Language : MonoBehaviour 
{
	public static Language instance = null;
	public Lang languageManager;
	public TextAsset xmlLang;

	public static string currentLanguage = "English";

	private Text connectionText;
	private Text controlsText;
	private Text playerControl;
	private Text tapSpell;
	private Text useSpell;
	private Text attackDescription;
	private Text defenseDescription;
	private Text slowDescription;
	private Text recoveryText;
	private Text blueFireText;
	private Text devilAwaitText;
	private Text electricBallText;
	private Text waterBallText;
	private Text dizzyText;
	private Text explosionText;
	private Text arcadeText;
	private Text arenaText;
	private Text zombieText;
	private Text skeletonText;
	private Text gollemText;
	private Text vampireText;
	private Text sfxVolumeText;
	private Text musicVolumeText;
	private Text statusesText;
	private Text enemiesText;
	private Text modesText;
	private Text spellsText;
	private Text soundOptionsText;
	private Text uxText;
	private Text soundText;
	private Text translationText;
	private Text creditsText;

	void Awake()
	{
		PlayerPrefs.SetString("Language",currentLanguage);
		connectionText = GameObject.Find("ConnectionText").GetComponent<Text>();
		sfxVolumeText = GameObject.Find("SfxVolText").GetComponent<Text>();
		musicVolumeText  = GameObject.Find("MusicVolText").GetComponent<Text>();
		soundOptionsText  = GameObject.Find("OptionsText").GetComponent<Text>();
	}

	public void OnEnable()
	{
		xmlLang = Resources.Load("lang",typeof(TextAsset)) as TextAsset; 
		//languageManager = new Lang(Path.Combine(Application.dataPath, "lang.xml"), currentLanguage, false);
		languageManager = new Lang(xmlLang.text, currentLanguage,false);
	}

	void OnLevelWasLoaded()
	{
		currentLanguage =  PlayerPrefs.GetString("Language");

		if(SceneManager.GetActiveScene().name == "Menu")
		{
			connectionText = GameObject.Find("ConnectionText").GetComponent<Text>();
			sfxVolumeText = GameObject.Find("SfxVolText").GetComponent<Text>();
			musicVolumeText  = GameObject.Find("MusicVolText").GetComponent<Text>();
			soundOptionsText  = GameObject.Find("OptionsText").GetComponent<Text>();
		}

		if(SceneManager.GetActiveScene().name == "HowToPlay")
		{
			spellsText = GameObject.Find("SpellsText").GetComponent<Text>();
			enemiesText = GameObject.Find("EnemiesText").GetComponent<Text>();
			modesText = GameObject.Find("ModesText").GetComponent<Text>();
			statusesText = GameObject.Find("StatusesText").GetComponent<Text>();
			controlsText = GameObject.Find("ControlsText").GetComponent<Text>();
			playerControl = GameObject.Find("MoveDescription").GetComponent<Text>();
			tapSpell = GameObject.Find("SpellDescription").GetComponent<Text>();
			attackDescription = GameObject.Find("AttackDescription").GetComponent<Text>();
			defenseDescription = GameObject.Find("DefenseDescription").GetComponent<Text>();
			slowDescription = GameObject.Find("SlowDescription").GetComponent<Text>();
			recoveryText = GameObject.Find("RecoveryText").GetComponent<Text>();
			blueFireText = GameObject.Find("BlueFireText").GetComponent<Text>();
			devilAwaitText = GameObject.Find("DevilAwaitText").GetComponent<Text>();
			electricBallText = GameObject.Find("ElectricBallText").GetComponent<Text>();
			explosionText = GameObject.Find("ExplosionText").GetComponent<Text>();
			waterBallText = GameObject.Find("WaterBallText").GetComponent<Text>();
			dizzyText = GameObject.Find("DizzyText").GetComponent<Text>();
			arcadeText = GameObject.Find("ArcadeText").GetComponent<Text>();
			arenaText = GameObject.Find("ArenaText").GetComponent<Text>();
			skeletonText = GameObject.Find("SkeletonDescription").GetComponent<Text>();
			zombieText = GameObject.Find("ZombieDescription").GetComponent<Text>();
			vampireText = GameObject.Find("VampireDescription").GetComponent<Text>();
			gollemText = GameObject.Find("GollemDescription").GetComponent<Text>();
			useSpell = GameObject.Find("useDescription").GetComponent<Text>();
			uxText = GameObject.Find("UXTitle").GetComponent<Text>();
			soundText = GameObject.Find("SoundTitle").GetComponent<Text>();
			translationText = GameObject.Find("TranslationTitle").GetComponent<Text>();
			creditsText = GameObject.Find("CreditsText").GetComponent<Text>();
		}

		SetLanguage(currentLanguage);
	}

	void FixedUpdate() 
	{
		if(SceneManager.GetActiveScene().name == "Menu")
		{
			if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) 
			{
				connectionText.text = languageManager.getString("disconnect_google");
			}
			else
			{
				connectionText.text = languageManager.getString("connect_google");
			}
		}
	}

	public void SetLanguage(string language)
	{
		currentLanguage = language;
		//languageManager.setLanguage(Path.Combine(Application.dataPath, "lang.xml"), currentLanguage);
		languageManager.setLanguage(xmlLang.text, currentLanguage);
		if(SceneManager.GetActiveScene().name == "Menu")
		{
			/*if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) 
			{
				connectionText.text = languageManager.getString("disconnect_google");
			}
			else
			{
				connectionText.text = languageManager.getString("connect_google");
			}*/
			connectionText.text = languageManager.getString("connect_google");
			sfxVolumeText.text = languageManager.getString("sfx_volume");
			musicVolumeText.text = languageManager.getString("music_volume");
			soundOptionsText.text = languageManager.getString("sound_options");

		}
		if(SceneManager.GetActiveScene().name == "HowToPlay")
		{
			controlsText.text = languageManager.getString("controls");
			playerControl.text = languageManager.getString("player_control");
			tapSpell.text = languageManager.getString("tap_spell");
			attackDescription.text = languageManager.getString("attack");
			defenseDescription.text = languageManager.getString("defense");
			slowDescription.text = languageManager.getString("slow");
			recoveryText.text = languageManager.getString("recovery");
			blueFireText.text = languageManager.getString("blue_fire");
			devilAwaitText.text = languageManager.getString("devil_await");
			electricBallText.text = languageManager.getString("electric_ball");
			waterBallText.text = languageManager.getString("water_ball");
			dizzyText.text = languageManager.getString("dizzy");
			explosionText.text = languageManager.getString("explosion");
			arcadeText.text = languageManager.getString("arcade");
			arenaText.text = languageManager.getString("arena");
			skeletonText.text = languageManager.getString("skeleton");
			zombieText.text = languageManager.getString("zombie");
			vampireText.text = languageManager.getString("vampire");
			gollemText.text = languageManager.getString("gollem");
			statusesText.text = languageManager.getString("statuses");
			spellsText.text = languageManager.getString("spells");
			enemiesText.text = languageManager.getString("enemies");
			modesText.text = languageManager.getString("modes");
			useSpell.text = languageManager.getString("use_spell");
			uxText.text = languageManager.getString("ux");
			soundText.text = languageManager.getString("sound");
			translationText.text = languageManager.getString("translation");
			creditsText.text = languageManager.getString("credits");

		}

		PlayerPrefs.SetString("Language",currentLanguage);
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//time after which the level will start
	public float levelStartDelay = 4f; 
	public static GameManager instance = null;

	//reference to the tiles
	public GameObject[] tileManager;

	//reference to the text informing which day is it ex. Day one
	private Text levelText;

	//reference to black screen which is being shown before loading a level
	private GameObject levelImage;

	//refence to UI Canvas
	public GameObject uiCanvas;

	//variable which store level's number
	public int level = 0;

	//reference to enemies' prefabs
	public GameObject[] enemyTiles;

	//reference to spawn points
	public Transform[] spawnPoints;

	//bool that lets enemies to be spawned
	private bool isSpawning = true;

	//reference to current level being used
	private GameObject levelActive;

	//bool that checks whether a physcial attack was used (for achievement purposes)
	public bool physicalAttackUsed = false;

	//reference to the previous score achieved by player
	private int previousScore = 0;

	//bool that checks whether Game Manager should load new level
	private bool toNextLevel = false;

	// Use this for initialization
	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (this.gameObject);//aby nie skasowac tego skryptu przy ladowaniu kolejnej sceny
	
	}


	private void OnLevelWasLoaded(int index)
	{
		Bonuses.Instance.Score = previousScore;
		isSpawning = true;
		InitGame ();
	}

	void InitGame()
	{
		levelImage = GameObject.Find("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		level++;
		levelText.text = "Day " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);
		StartCoroutine("InstantiateEnemy",level);
	}

	public void GameOver()
	{	
		if(level == 1)
		{
			AchievementManager.Instance.EarnAchievement("Loser");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQDA");

		}
	}
		
	//function that hides black screen
	private void HideLevelImage()
	{
		levelActive = null;
		levelImage.SetActive (false);
		uiCanvas.SetActive(true);
		levelActive = Instantiate(tileManager[Random.Range(0,tileManager.Length)],Camera.main.ViewportToWorldPoint(new Vector3(0.065f,0.0028f)),Quaternion.identity) as GameObject;
		levelActive.SetActive(true);
	}

	// Update is called once per frame
	void Update () 
	{
		if(Bonuses.Instance.enemiesInGame.Count == 0 && isSpawning == false)
		{
			if(CastleHealth.Instance.currentHealth == CastleHealth.Instance.startingHealth)
			{
				AchievementManager.Instance.EarnAchievement("Untouchable");
				GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQDQ");
				AchievementManager.Instance.EarnAchievement("Gold Helmet");
			}
			if(EnergyManager.Instance.currentMana == EnergyManager.Instance.startingMana)
			{
				AchievementManager.Instance.EarnAchievement("Energy-saver");
				GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQCg");
			}
			if(physicalAttackUsed == false)
			{
				AchievementManager.Instance.EarnAchievement("Magician");
				GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQCw");
				AchievementManager.Instance.EarnAchievement("Moon Helmet");
			}
			Bonuses.Instance.enemiesInGame.Clear();
			toNextLevel = true;
			Invoke("Restart",levelStartDelay);
		}

		if(level == 10)
		{
			AchievementManager.Instance.EarnAchievement("Survivalist");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQBg");
			AchievementManager.Instance.EarnAchievement("Blue Boots");
		}

		if(level == 25)
		{
			AchievementManager.Instance.EarnAchievement("Perseverance");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQBw");
			AchievementManager.Instance.EarnAchievement("Gold Boots");
		}

		if(level == 50)
		{
			AchievementManager.Instance.EarnAchievement("Fearless");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQCA");
		}

		if(level == 100)
		{
			AchievementManager.Instance.EarnAchievement("Darkness is my friend");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQCQ");
			AchievementManager.Instance.EarnAchievement("Moon Armor");
		}
	}

	//function that spawns enemies
	private IEnumerator InstantiateEnemy(int index)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");;
		foreach(GameObject enemy in enemies)
		{
			Destroy(enemy);
		}
		Bonuses.Instance.enemiesInGame.Clear();
		//starts generating enemies

		int enemyCount = level * 2;//(int)Mathf.Log (level, 2f);
		for(int i = 0; i < enemyCount; i++)
		{
			GameObject enemy = Instantiate(enemyTiles[Random.Range(0,enemyTiles.Length)],spawnPoints[Random.Range(0,spawnPoints.Length)].position,Quaternion.identity) as GameObject;
			Bonuses.Instance.enemiesInGame.Add(enemy);
			yield return new WaitForSeconds(1);
		}
		//stops genereting enemies
		isSpawning = false;
	}

	//function which restarts a level
	private void Restart()
	{
		AndroidGoogleAdsExample.Instance.B2Hide();
		if(toNextLevel == true)
		{
			toNextLevel = false;
			//restart current level
			if(Bonuses.Instance.enemiesInGame.Count == 0 && isSpawning == false)
			{
				previousScore = Bonuses.Instance.Score;
				Bonuses.Instance.StopBonusAnimation();
				ParticleSystem[] toDestroy = GameObject.FindObjectsOfType<ParticleSystem>();
				foreach(ParticleSystem particle in toDestroy)
				{
					Destroy(particle);
				}
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}
}

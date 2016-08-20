using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CastleHealth : MonoBehaviour {

	//reference to the slider
	public Slider slider;

	//reference to the attacking Enemy
	private EnemyController enemy;

	//castle's health
	public float startingHealth = 500;

	// The color the health bar will be when on full health.
	public Color m_FullHealthColor = Color.green;  

	// The color the health bar will be when on no health.
	public Color m_ZeroHealthColor = Color.red;         

	//castle's current health
	public float currentHealth;

	//reference to slider's image
	public Image fillImage;

	//timer which checks how much time passed without receiving any damage
	private float timer = 0.0f;

	//variable that stores current health before the timer going on
	private float previousHealth;

	//reference to gameOverCanvas
	public GameObject gameOverCanvas;

	//singleton
	private static CastleHealth instance; 

	//public access to the singleton
	public static CastleHealth Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<CastleHealth>();
			}
			return CastleHealth.instance;
		}

		set
		{
			instance = value;
		}
	}


	void Awake()
	{
	}

	// Use this for initialization
	void Start () 
	{
		currentHealth = startingHealth;
		SetHealthUI();
		previousHealth = currentHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//checks whether current scene is Arcade
		if(SceneManager.GetActiveScene().name == "Arcade" )
		{
			//starts the timer
			timer += Time.deltaTime;
			//if the timer passed 60 seconds
			if(timer >= 60f)	
			{
				if(currentHealth == previousHealth)
				{
					AchievementManager.Instance.EarnAchievement("Not scared of you!");
					GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQAw");
					AchievementManager.Instance.EarnAchievement("Viscious Sword");
				}
				else
				{
					timer = 0.0f;
					previousHealth = currentHealth;
				}
			}
		}
	}

	//function that decrease castle's health
	public void ReceiveDamage(float _damage)
	{
		currentHealth -= _damage;

		SetHealthUI();

		if(CheckIfDead() == true)
		{
			GameOver();
		}
	}

	//function that sets castle's health UI
	private void SetHealthUI()
	{
		slider.value = currentHealth;

		fillImage.color = Color.Lerp(m_ZeroHealthColor,m_FullHealthColor,currentHealth / startingHealth);

	}

	//function that checks whether player is dead
	public bool CheckIfDead()
	{
		if(currentHealth <= 0)
		{
			if(Bonuses.Instance.Score == 0)
			{
				AchievementManager.Instance.EarnAchievement("Points are not everything");
				GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQEA");
			}
			return true;
		}
		else 
			return false;
	}

	//function that handles Game Over state
	public void GameOver()
	{
		Bonuses.Instance.StopBonusAnimation();

		if(SceneManager.GetActiveScene().name == "Arena")
		{
			GameManager.instance.GameOver();
		}

		if(gameOverCanvas != null)
		{
			gameOverCanvas.SetActive(true);
			AndroidGoogleAdsExample.Instance.B2Show();
			gameOverCanvas.transform.GetChild(5).GetComponent<Text>().text = "SCORE: " + Bonuses.Instance.Score.ToString();
			GooglePlayManager.Instance.SubmitScoreById("CgkIkti4j78fEAIQAQ", Bonuses.Instance.Score);
			gameOverCanvas.transform.GetChild(6).GetComponent<Text>().text = "BEST SCORE: " + Bonuses.Instance.CheckIfBestScore(Bonuses.Instance.Score);
			Bonuses.Instance.SetTotalScore();
			if(PlayerPrefs.GetInt("TotalScore") > 100000)
			{
				AchievementManager.Instance.EarnAchievement("Annihilator");
				GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQEg");
			}
			Time.timeScale = 0;
		}
	}
}

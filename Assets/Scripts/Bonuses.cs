using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Bonuses : MonoBehaviour {

	//list of in-game enemies
	public List<GameObject> enemiesInGame;

	private static Bonuses instance; //singleton

	public static Bonuses Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<Bonuses>();
			}
			return Bonuses.instance;
		}
	}

	//referance to the score text
	public Text scoreText;

	//reference to current score
	private int score;

	public Text bonusText;

	//reference to the quicktip prefab
	public GameObject quickTip;

	//reference to player's best score
	private int bestScore;

	//number which will store player's total score
	int totalScore;

	public int Score
	{
		get{return score;}
		set{score = value;}
	}

	void Awake()
	{
		quickTip = GameObject.Find("QuickTipButton");
		bonusText = GameObject.Find("BonusText").GetComponent<Text>();
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		bestScore = PlayerPrefs.GetInt("Score");
		totalScore = PlayerPrefs.GetInt("TotalScore");
		score = 0;
	}

	void OnLevelWasLoaded(int index)
	{
	}

	// Use this for initialization
	void Start () 
	{
		scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(score >= 1000)
		{
			AchievementManager.Instance.EarnAchievement("Destroyer");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQEQ");
		}
	}

	//function that updates the total score
	public void UpdateScore(int _enemyScore)
	{
		score = _enemyScore + score;
		scoreText.text = score.ToString();
	}

	//coroutine which handles Bonus Damage Text ex. Amazing! animation
	public IEnumerator BonusDamage()
	{
		quickTip.SetActive(false);
		bonusText.text = "Amazing!";
		bonusText.GetComponent<Animation>().Play();

		yield return new WaitForSeconds(bonusText.GetComponent<Animation>().clip.length);

		bonusText.text = string.Empty;
		quickTip.SetActive(true);
	}

	//function that checks whether achieved score is bigger than best score
	public int CheckIfBestScore(int score)
	{
		if(score > bestScore)
		{
			bestScore = score;
			PlayerPrefs.SetInt("Score",bestScore);
			return bestScore;
		}
		else
		{
			PlayerPrefs.SetInt("Score",bestScore);
			return bestScore;
		}

	}

	//function that stops Bonus Damage Text ex. Amazing! animation 
	public void StopBonusAnimation()
	{
		StopCoroutine("BonusDamage");
		if(quickTip.activeInHierarchy == false)
		{
			quickTip.SetActive(true);
		}
	}

	public void SetTotalScore()
	{
		totalScore += score;
		PlayerPrefs.SetInt("TotalScore",totalScore);
	}
		
}

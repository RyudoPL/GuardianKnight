using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Lottery : MonoBehaviour {

	//array of skills' images
	public Sprite[] skillsImage;

	//array of lottery's images
	public Image[] lotteryImage;

	//bool that checks whether the lottery is spinning
	private bool isSpinning;

	//array of skill slots
	public Skill[] skills;
	public AnimationCurve ac;

	//reference to lottery's neutral box
	public Sprite boxNormal;

	public bool IsSpinning
	{
		get{return isSpinning;}
		set{isSpinning = value;}
	}


	void Awake()
	{
		//default -> lottery is not spinning
		isSpinning = false;
	}

	// Use this for initialization
	void Start () 
	{
		//function sets the first three skills
		skills[0].Sprite.sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
		skills[1].Sprite.sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
		skills[2].Sprite.sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
	}

	void OnLevelWasLoaded()
	{
		skills[0].Sprite.sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
		skills[1].Sprite.sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
		skills[2].Sprite.sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
	}

	// Update is called once per frame
	void Update () 
	{
		for(int i = 0; i < skills.Length; i++)
		{
			//check whether any of skill slots has no skill image attached
			if(skills[i].Sprite.sprite.name == "level no stars")
			{
				//lottery should start spinning
				isSpinning = true;

				//functions for spinning the lottery
				StartCoroutine(Spin(skills[i],0.20f));
			}
		}
		
	}

	//function for spinning the lottery
	public IEnumerator Spin(Skill _skill,float _time)
	{
		//set the timer to 0
		float _timer = 0.0f;


		while(_timer < _time)
		{
			float wait = 0.3f;

			bool _isFirstSpin = true;
			//if it is a first spin then set skill images to every lottery slot
			if(_isFirstSpin)
			{
				lotteryImage[0].sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
				lotteryImage[1].sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
				lotteryImage[2].sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];
				_isFirstSpin = false;
			}

			lotteryImage[2].sprite = lotteryImage[0].sprite;
			lotteryImage[0].sprite = lotteryImage[1].sprite;
			lotteryImage[1].sprite = skillsImage[UnityEngine.Random.Range(0,skillsImage.Length)];

			_skill.Sprite.sprite = lotteryImage[0].sprite;
			_timer += Time.deltaTime;

			yield return new WaitForSeconds(wait);

		}
		_skill.Sprite.sprite = lotteryImage[0].sprite;
		lotteryImage[0].sprite = boxNormal;
		lotteryImage[1].sprite = boxNormal;
		lotteryImage[2].sprite = boxNormal;

		//bool stops spinning
		isSpinning = false;

		AchievementManager.Instance.EarnAchievement("Spin it!");
		GooglePlayManager.Instance.IncrementAchievementById("CgkIkti4j78fEAIQEw",1);
	}

}

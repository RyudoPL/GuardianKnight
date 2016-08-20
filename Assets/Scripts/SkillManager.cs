using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {

	//bool that checks whether an enemy is on fire
	public bool isOnFire;

	//dictionary of spell prefabs
	private Dictionary<string,GameObject> database = new Dictionary<string,GameObject>();

	//singleton
	public static SkillManager instance = null;

	//array of skills' prefabs
	public GameObject[] skillPrefabs;

	//variable used for checking whether each spell was used
	int counter = 0;

	public Dictionary<string,GameObject> Database
	{
		get{return database;}
	}

	void Awake()
	{

		//add skill prefab's to the dictionary
		database.Add("BlueFire",skillPrefabs[0]);
		database.Add("DevilAwait",skillPrefabs[1]);
		database.Add("ElectricBall",skillPrefabs[2]);
		database.Add("WaterBall",skillPrefabs[3]);
		database.Add("Explosion",skillPrefabs[4]);
		database.Add("Recovery",skillPrefabs[5]);
		database.Add("Dizzy",skillPrefabs[6]);

		for(int i = 0; i < skillPrefabs.Length; i++)
		{
			skillPrefabs[i].GetComponent<Spell>().wasUsed = false;
		}
	}
	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		//add 1 to the variable each time a new spell was used
		for(int i = 0; i < skillPrefabs.Length; i++)
		{
			if(skillPrefabs[i].GetComponent<Spell>().wasUsed == true)
			{
				counter += 1;
				if(counter >= 7)
				{
					AchievementManager.Instance.EarnAchievement("This is just the beginning");
					GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQDg");
					AchievementManager.Instance.EarnAchievement("Gold Armor");
				}
			}
			else
				counter = 0;
		}
	}
}

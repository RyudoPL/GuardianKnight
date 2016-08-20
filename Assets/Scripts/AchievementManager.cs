using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour 
{
	public Dictionary<string,Achievement> achievements = new Dictionary<string, Achievement>();

	private static AchievementManager instance; //singleton

	public static AchievementManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<AchievementManager>();
			}
			return AchievementManager.instance;
		}
	}

	// Use this for initialization
	void Start () {

		CreateAchievement("Engulfed in flames"); //100 monsters engulfed in flames
		CreateAchievement("I am Thor!"); //8 electrified monsters
		CreateAchievement("This is just the beginning"); // all skills
		CreateAchievement("Loot!"); //equipment found
		CreateAchievement("Hoarder"); // all equipment found
		CreateAchievement("Darkness is my friend"); // level 100
		CreateAchievement("Survivalist"); //level 10
		CreateAchievement("Energy-saver"); // 0 mana used
		CreateAchievement("Moon Helmet"); //only magic
		CreateAchievement("Points are not everything"); // Score 0
		CreateAchievement("Destroyer");  // Score 1000
		CreateAchievement("Annihilator"); // Score > 100000
		CreateAchievement("Gold Helmet"); //no damage
		CreateAchievement("Moon Armor");  //level 100
		CreateAchievement("Gold Armor");  // all skills
		CreateAchievement("Blue Boots");  //level 10
		CreateAchievement("Gold Boots"); //level 25
		CreateAchievement("Moon Shield"); //8 electrified monsters
		CreateAchievement("Gold Shield"); //help section read
		CreateAchievement("Moon Sword"); //Score 1000
		CreateAchievement("Viscious Sword"); //60 seconds
		CreateAchievement("Loser"); //dead at Level 1
		CreateAchievement("Can I go?"); //dizzu spell used twice on the same enemy
		CreateAchievement("Untouchable"); // no damage
		CreateAchievement("Spin it!"); //100 spins
		CreateAchievement("Magician"); //only magic
		CreateAchievement("Not scared of you!"); //60 seconds
		CreateAchievement("Scholar"); //help section read
		CreateAchievement("Perseverance"); //level 25
		CreateAchievement("Fearless"); //level 50
	}

	void Update()
	{
		if(GameObject.Find("BonusSystem") != null)
		{
			if(Bonuses.Instance.Score >= 1000)
			{
				EarnAchievement("Destroyer");
				GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQEQ");
				EarnAchievement("Moon Sword");
			}
		}
	
		//check if any equipment was earned
		if(achievements["Moon Helmet"].Unlocked || achievements["Moon Armor"].Unlocked || achievements["Blue Boots"].Unlocked
			|| achievements["Moon Shield"].Unlocked || achievements["Moon Sword"].Unlocked || achievements["Gold Helmet"].Unlocked
			|| achievements["Gold Armor"].Unlocked || achievements["Gold Boots"].Unlocked || achievements["Gold Shield"].Unlocked
			|| achievements["Viscious Sword"].Unlocked == true)
		{
			EarnAchievement("Loot!");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQBA");
		}

		//check if all equipment parts were earned
		if(achievements["Moon Helmet"].Unlocked && achievements["Moon Armor"].Unlocked && achievements["Blue Boots"].Unlocked
			&& achievements["Moon Shield"].Unlocked && achievements["Moon Sword"].Unlocked && achievements["Gold Helmet"].Unlocked
			&& achievements["Gold Armor"].Unlocked && achievements["Gold Boots"].Unlocked && achievements["Gold Shield"].Unlocked
			&& achievements["Viscious Sword"].Unlocked == true)
		{
			EarnAchievement("Hoarder");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQBQ");
		}

		if(GameObject.Find("CreditsCanvas") != null)
		{
			EarnAchievement("Scholar");
			GooglePlayManager.Instance.UnlockAchievementById("CgkIkti4j78fEAIQAg");
			EarnAchievement("Gold Shield");
		}
		else return;  
	}

	//function that instantiate the achievement
	public void EarnAchievement(string title)
	{
		achievements[title].EarnAchievement();
	}

	//function that creates an achievement
	public void CreateAchievement(string title)
	{
		Achievement newAchievement = new Achievement (title);

		achievements.Add (title, newAchievement); //add achievement to achievement list

	}
}

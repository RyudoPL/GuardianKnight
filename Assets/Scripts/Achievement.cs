using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Achievement
{
	private string name; //achievement' name

	public string Name
	{
		get{return name;}
		set{name = value;}
	}

	private bool unlocked; //check whether an achievement is unlocked

	public bool Unlocked
	{
		get{ return unlocked;}
		set{ unlocked = value;}
	}

	public Achievement(string name) //constructor
	{
		this.name = name;
		this.unlocked = false;
		LoadAchievement ();
	}

	public bool EarnAchievement()
	{
		if (!unlocked) 
			//if achievement is not unlocked yet
		{
			SaveAchievement(true); //save achievement
			return true;
		}
		return false;
	}

	//function that saves the achievement
	public void SaveAchievement(bool value)
	{
		unlocked = value; //if achievement is unlocked, it has value of 1

		PlayerPrefs.SetInt (Name, value ? 1 : 0); //save achievement if it has a value of 1/is unlocked
		PlayerPrefs.Save (); //save data
	}

	public void LoadAchievement()
	{
		unlocked = PlayerPrefs.GetInt (Name) == 1 ? true : false; //load those achievements which have value of 1
	}
}

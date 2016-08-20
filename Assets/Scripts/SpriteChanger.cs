using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour 
{
	//array of helmets
	public Sprite[] headSprite;

	//array of shields
	public Sprite[] shieldSprite;

	//array of swords
	public Sprite[] swordSprite;

	//array of armors
	public Sprite[] armorSprite;

	//array which stores all parts of sprite's name
	private string[] objectName;

	//reference to attack power text
	public Text attackText;

	//reference to mana text
	public Text manaText;

	//reference to health text
	public Text healthText;

	//index for the array of helmets
	private int headIndex = 0;

	//index for the array of shields
	private int shieldIndex = 0;

	//index for the array of swords
	private int swordIndex = 0;

	//index for the array of armors
	private int armorIndex = 0;

	//base attack power
	private int totalAttackPower = 10;

	//base health
	private int totalHealth = 500;

	//base mana
	private int totalMana = 100;

	//function that changes the sprite
	public void ChangeSprite(GameObject name)
	{
		switch(name.name) 
		{
		case "Helmet":
			if(CheckIfOutOfArray(headIndex))
			{
				headIndex = -1;
			}
			headIndex += 1;
			if(CheckIfEarned(headSprite[headIndex]) == false)
			{
				name.GetComponent<Image>().color = Color.black;
				GameObject.Find("Back").GetComponent<Button>().interactable = false;
			}
			else
			{
				name.GetComponent<Image>().color = new Color(255,255,255,255);
				GameObject.Find("Back").GetComponent<Button>().interactable = true;
				ChangePlayerSprite(headSprite[headIndex]);
			}
			name.gameObject.GetComponent<Image>().sprite = headSprite[headIndex];

			if(headIndex == 0)
			{
				if(CheckIfEarned(headSprite[2]) == true)
				DecreaseStats(headSprite[2]);
			}
			else
			{
				if(CheckIfEarned(headSprite[headIndex - 1]) == true)
				DecreaseStats(headSprite[headIndex - 1]);
			}
			break;

		case "Chest":
			if(CheckIfOutOfArray(armorIndex))
			{
				armorIndex = -1;
			}
			armorIndex += 1;
			if(CheckIfEarned(armorSprite[armorIndex]) == false)
			{
				name.GetComponent<Image>().color = Color.black;
				GameObject.Find("Back").GetComponent<Button>().interactable = false;
			}
			else
			{
				name.GetComponent<Image>().color = new Color(255,255,255,255);
				GameObject.Find("Back").GetComponent<Button>().interactable = true;
				ChangePlayerSprite(armorSprite[armorIndex]);
			}
			name.gameObject.GetComponent<Image>().sprite = armorSprite[armorIndex];

			if(armorIndex == 0)
			{
				if(CheckIfEarned(armorSprite[2]) == true)
				DecreaseStats(armorSprite[2]);
			}
			else
			{
				if(CheckIfEarned(armorSprite[armorIndex - 1]) == true)
				DecreaseStats(armorSprite[armorIndex - 1]);
			}
			break;

		case "Shield":
			if(CheckIfOutOfArray(shieldIndex))
			{
				shieldIndex = -1;
			}
			shieldIndex += 1;

			if(CheckIfEarned(shieldSprite[shieldIndex]) == false)
			{
				name.GetComponent<Image>().color = Color.black;
				GameObject.Find("Back").GetComponent<Button>().interactable = false;
			}
			else
			{
				name.GetComponent<Image>().color = new Color(255,255,255,255);
				GameObject.Find("Back").GetComponent<Button>().interactable = true;
				ChangePlayerSprite(shieldSprite[shieldIndex]);
			}
			name.gameObject.GetComponent<Image>().sprite = shieldSprite[shieldIndex];
			if(shieldIndex == 0)
			{
				if(CheckIfEarned(shieldSprite[2]) == true)
				DecreaseStats(shieldSprite[2]);
			}
			else
			{
				if(CheckIfEarned(shieldSprite[shieldIndex - 1]) == true)
				DecreaseStats(shieldSprite[shieldIndex - 1]);
			}
			break;

		case "Sword":
			if(CheckIfOutOfArray(swordIndex))
			{
				swordIndex = -1;
			}
			swordIndex += 1;

			if(CheckIfEarned(swordSprite[swordIndex]) == false)
			{
				name.GetComponent<Image>().color = Color.black;
				GameObject.Find("Back").GetComponent<Button>().interactable = false;
			}
			else
			{
				name.GetComponent<Image>().color = new Color(255,255,255,255);
				GameObject.Find("Back").GetComponent<Button>().interactable = true;
				ChangePlayerSprite(swordSprite[swordIndex]);
			}
			name.gameObject.GetComponent<Image>().sprite = swordSprite[swordIndex];
			if(swordIndex == 0)
			{
				if(CheckIfEarned(swordSprite[2]) == true)
				DecreaseStats(swordSprite[2]);
			}
			else
			{
				if(CheckIfEarned(swordSprite[swordIndex - 1]) == true)
				DecreaseStats(swordSprite[swordIndex - 1]);
			}
			break;
		}
		Image[] childsColor = GameObject.Find("EquipmentPanel").GetComponentsInChildren<Image>();
		for(int i = 0; i < childsColor.Length; i ++)
		{
			if(childsColor[i].color == Color.black)
			{
				GameObject.Find("Back").GetComponent<Button>().interactable = false;
				return;
			}
		}
		UpdateStats(name);

	}


	//update all the stats
	public void UpdateStats(GameObject item)
	{
		//split name of the object to parts 
		objectName = item.gameObject.GetComponent<Image>().sprite.name.Split(',');

		//add attack power
		totalAttackPower += System.Int32.Parse(objectName[2]);

		//add hp
		totalHealth = totalHealth + System.Int32.Parse(objectName[1]);

		//add mana
		totalMana += System.Int32.Parse(objectName[3]) ;

		attackText.text = "ATTACK: " + totalAttackPower;
		healthText.text = "HP: " + totalHealth;
		manaText.text = "MANA: " + totalMana;

		if(GameObject.FindObjectOfType<CastleHealth>() != null)
		{
			CastleHealth castleHealth = GameObject.FindObjectOfType<CastleHealth>();
			CastleHealth.Instance.startingHealth = totalHealth;
			CastleHealth.Instance.currentHealth = totalHealth;
			castleHealth.GetComponentInChildren<Slider>().maxValue = totalHealth;
			castleHealth.GetComponentInChildren<Slider>().value = totalHealth;

		}
		if(GameObject.FindObjectOfType<EnergyManager>() != null)
		{
			EnergyManager energyManager = GameObject.FindObjectOfType<EnergyManager>();
			EnergyManager.Instance.startingMana = totalMana;
			EnergyManager.Instance.currentMana = totalMana;
			energyManager.GetComponentInChildren<Slider>().maxValue = totalMana;
			energyManager.GetComponentInChildren<Slider>().value = totalMana;
		}
		if(GameObject.FindObjectOfType<PlayerController>() != null)
		{
			PlayerController.Instance.attackPower = totalAttackPower;
		}
	}

	//function that decreases variables 
	public void DecreaseStats(Sprite item)
	{
		//split name of the object to parts
		objectName = item.name.Split(',');

		totalAttackPower -= System.Int32.Parse(objectName[2]);
		totalHealth -= System.Int32.Parse(objectName[1]);
		totalMana -= System.Int32.Parse(objectName[3]) ;
	}

	//function that changes player's look
	private void ChangePlayerSprite(Sprite item)
	{
		//check which part of equipment is it
		if(item.name.Contains("Helmet"))
		{
			GameObject.Find("Knight").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item;
			GameObject.Find("KnightMenu").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item;
		}
		if(item.name.Contains("Chest"))
		{
			GameObject.Find("Knight").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = item;
			GameObject.Find("KnightMenu").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = item;
		}
		if(item.name.Contains("Shield"))
		{
			GameObject.Find("Knight").transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = item;
			GameObject.Find("KnightMenu").transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = item;

		}
		if(item.name.Contains("Sword"))
		{
			GameObject.Find("Knight").transform.GetChild(7).GetComponent<SpriteRenderer>().sprite = item;
			GameObject.Find("KnightMenu").transform.GetChild(7).GetComponent<SpriteRenderer>().sprite = item;
		}
	}

	//function that checks whether an achievement connected with equipment was unlocked
	private bool CheckIfEarned(Sprite item)
	{
		string[] itemName = item.name.Split(',');
		if(itemName[0].Contains("Sun"))
		{
			return true;
		}
		else if(AchievementManager.Instance.achievements[itemName[0]].Unlocked)
		{
			return true;
		}
		else 
			return false;
	}

	//function that checks whether index is out of array
	private bool CheckIfOutOfArray(int index)
	{
		if(index > 1)
		{
			return true;
		}
		return false;
	}
}

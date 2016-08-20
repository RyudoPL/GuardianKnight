using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {


	//reference to the slider
	public Slider slider;

	//player's mana
	public float startingMana = 100;

	public Color m_FullManaColor = Color.green;       // The color the mana bar will be when on full mana.
	public Color m_ZeroManaColor = Color.red;         // The color the mana bar will be when on no mana.

	//castle's current mana
	public float currentMana;

	//reference to slider's image
	public Image fillImage;

	private static EnergyManager instance; //singleton

	public static EnergyManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<EnergyManager>();
			}
			return EnergyManager.instance;
		}
	}

	// Use this for initialization
	void Start () 
	{
		currentMana = startingMana;
		SetManaUI();
	}

	//function that sets castle's mana UI
	public void SetManaUI()
	{
		slider.value = currentMana;

		fillImage.color = Color.Lerp(m_ZeroManaColor,m_FullManaColor,currentMana / startingMana);
	}

	//checks whether player has any mana left
	public bool CheckIfNoMana(int energyCost)
	{
		if(energyCost >= currentMana)
		{
			return true;
		}
		else 
			return false;
	}
}


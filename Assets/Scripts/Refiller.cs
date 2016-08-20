using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Refiller : MonoBehaviour {


	void OnLevelWasLoaded()
	{
		CastleHealth castleHealth = GameObject.FindObjectOfType<CastleHealth>();
		CastleHealth.Instance.currentHealth = CastleHealth.Instance.startingHealth;
		castleHealth.GetComponentInChildren<Slider>().maxValue = CastleHealth.Instance.startingHealth;;
		castleHealth.GetComponentInChildren<Slider>().value = CastleHealth.Instance.startingHealth;;
		castleHealth.fillImage.color = castleHealth.m_FullHealthColor;

		EnergyManager energyManager = GameObject.FindObjectOfType<EnergyManager>();
		EnergyManager.Instance.startingMana = energyManager.startingMana;
		EnergyManager.Instance.currentMana = energyManager.startingMana;
		energyManager.GetComponentInChildren<Slider>().maxValue = energyManager.startingMana;
		energyManager.GetComponentInChildren<Slider>().value = energyManager.startingMana;
		energyManager.fillImage.color = energyManager.m_FullManaColor;
	}
}

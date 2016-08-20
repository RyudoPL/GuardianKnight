using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject quickTipPanel;						//Store a reference to the Game Object QuickTipPanel 
	public GameObject equipmentPanel;						//Store a reference to the Game Object EquipmentPanel
	public GameObject gameModePanel;						//Store a reference to the Game Object GameModePanel

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}

	public void ShowQuickTipPanel()
	{
		quickTipPanel.SetActive(true);
		Time.timeScale = 0;
		GameObject.Find("QuickTipText").GetComponent<Text>().text = gameObject.GetComponent<QuickTipManager>().tips
			[Random.Range(0,gameObject.GetComponent<QuickTipManager>().tips.Length - 1)];
	

		AndroidGoogleAdsExample.Instance.B1Show();
	}

	public void HideQuickTipPanel()
	{
		GameObject.Find("QuickTipText").GetComponent<Text>().text = string.Empty;
		AndroidGoogleAdsExample.Instance.B1Hide();
		quickTipPanel.SetActive(false);
		Time.timeScale = 1;
	}

	public void ShowEquipmentPanel()
	{
		equipmentPanel.SetActive(true);
	}

	public void HideEquipmentPanel()
	{
		equipmentPanel.SetActive(false);
	}

	public void ShowGameModePanel()
	{
		gameModePanel.SetActive(true);
	}

	public void HideGameModePanel()
	{
		gameModePanel.SetActive(false);
	}
}

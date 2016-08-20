using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour 
{
	public AudioMixer mainMixer;   //Used to hold a reference to the AudioMixer mainMixer
	public Slider musicSlider;
	public Slider sfxSlider;
	private GameObject musicManager;
	public GameObject[] enemyPrefabs;


	void OnEnable()
	{
		musicManager = GameObject.Find("MusicManager");
		musicSlider = GameObject.Find("MusicVolSliderOptions").GetComponent<Slider>();
		sfxSlider = GameObject.Find("SfxVolSliderOptions").GetComponent<Slider>();
	}

	//Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
	public void SetMusicLevel(float musicLvl)
	{
		mainMixer.SetFloat("musicVol", musicLvl);
		PlayerPrefs.SetFloat("MusicLvl", musicLvl);
		PlayerPrefs.Save();
		if(musicManager != null)
			musicManager.GetComponent<AudioSource>().volume = musicLvl;
	}

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
		PlayerPrefs.SetFloat("SfxLvl", sfxLevel);
		PlayerPrefs.Save();
		foreach(GameObject enemy in enemyPrefabs)
		{
			AudioSource[] sfxAudio = enemy.GetComponents<AudioSource>();
			foreach(AudioSource sfx in sfxAudio)
			{
				sfx.volume = sfxLevel;
			}
		}
	}

	private void OnLevelWasLoaded()
	{
		SetMusicLevel(PlayerPrefs.GetFloat("MusicLvl"));
		SetSfxLevel(PlayerPrefs.GetFloat("SfxLvl"));
		if(musicSlider != null && sfxSlider != null)
		{
			musicSlider.value = PlayerPrefs.GetFloat("MusicLvl");
			musicManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicLvl");
			sfxSlider.value = PlayerPrefs.GetFloat("SfxLvl");
			foreach(GameObject enemy in enemyPrefabs)
			{
				AudioSource[] sfxAudio = enemy.GetComponents<AudioSource>();
				foreach(AudioSource sfx in sfxAudio)
				{
					sfx.volume = PlayerPrefs.GetFloat("SfxLvl");
				}
			}
		}
	}
}

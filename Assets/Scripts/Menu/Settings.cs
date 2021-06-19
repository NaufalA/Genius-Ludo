using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InGame;

public class Settings : MonoBehaviour
{
	// TODO: variables and methods for global settings
	public GameObject sfxOn;
	public GameObject sfxOff;
	public GameObject bgmOn;
	public GameObject bgmOff;
	GameObject globalAudioObject;
	GlobalAudio globalAudio;


	private void Awake()
	{
		if (GlobalSettings.settings.sfx == 1f)
		{
			sfxOn.gameObject.SetActive(true);
			sfxOff.gameObject.SetActive(false);
		}
		else if (GlobalSettings.settings.sfx == 0f)
		{
			sfxOn.gameObject.SetActive(false);
			sfxOff.gameObject.SetActive(true);
		}
		if (GlobalSettings.settings.bgm == 1f)
		{
			bgmOn.gameObject.SetActive(true);
			bgmOff.gameObject.SetActive(false);
		}
		else if (GlobalSettings.settings.bgm == 0f)
		{
			bgmOn.gameObject.SetActive(false);
			bgmOff.gameObject.SetActive(true);
		}
	}

	private void Start()
	{

		globalAudioObject = GameObject.FindGameObjectsWithTag("GlobalAudio")[0];
		globalAudio = globalAudioObject.GetComponent<GlobalAudio>();
	}

	public void ToggleSfx(float value)
	{
		if (value == 0f)
		{
			sfxOn.gameObject.SetActive(false);
			sfxOff.gameObject.SetActive(true);
		}
		else if (value == 1f)
		{
			sfxOn.gameObject.SetActive(true);
			sfxOff.gameObject.SetActive(false);
		}
		GlobalSettings.settings.sfx = value;
		SaveSystem.SaveSettings();
	}

	public void ToggleBgm(float value)
	{
		if (value == 0f)
		{
			globalAudio.music.Stop();
			bgmOn.gameObject.SetActive(false);
			bgmOff.gameObject.SetActive(true);
		}
		else if (value == 1f)
		{
			globalAudio.music.Play();
			bgmOn.gameObject.SetActive(true);
			bgmOff.gameObject.SetActive(false);
		}
		GlobalSettings.settings.bgm = value;
		SaveSystem.SaveSettings();
	}
}

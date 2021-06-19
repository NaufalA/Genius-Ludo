using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAudio : MonoBehaviour
{
	public AudioSource music;
	public AudioSource click;
	private static GlobalAudio instance = null;
	public static GlobalAudio Instance
	{
		get { return instance; }
	}

	private void Start()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(transform.gameObject);

		if (GlobalSettings.settings.bgm == 1f)
		{
			music.Play();
		}
		else if (GlobalSettings.settings.bgm == 0f)
		{
			music.Stop();
		}
	}

	public void ClickSound()
	{
		click.Play();
	}
}

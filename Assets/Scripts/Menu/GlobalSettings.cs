using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
	public float sfx;
	public float bgm;

	public SettingsData()
	{

		sfx = 1f;
		bgm = 1f;
	}
}

public static class GlobalSettings
{
	public static SettingsData settings = new SettingsData();

	public static int previousScene;
}

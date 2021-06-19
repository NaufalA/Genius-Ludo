using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InGame;

public class MainMenu : MonoBehaviour
{
	private void Start()
	{
		SaveSystem.LoadAchievments();
		SaveSystem.LoadSettings();
	}

	public void ContinueGame(string sceneName)
	{
		if (SaveSystem.CheckSave("savegame.geniusludo"))
		{
			SceneManager.LoadScene(sceneName);
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

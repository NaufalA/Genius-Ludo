using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InGame;

public class NewGame : MonoBehaviour
{
	private void Start()
	{
		SavedSettings.players = new PlayerEntity[] {
		new PlayerEntity(Team.GREEN, "Player Green"),
		new PlayerEntity(Team.YELLOW,"Player Yellow" ),
		new PlayerEntity(Team.BLUE, "Player Blue"),
		new PlayerEntity(Team.RED, "Player Red")
	};
	}
	public void StartGame(string sceneName)
	{
		SaveSystem.ClearSave("savegame.geniusludo");
		SceneManager.LoadScene(sceneName);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("MainMenuScene");
	}
}

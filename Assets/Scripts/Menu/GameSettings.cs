using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InGame;

public class GameSettings : MonoBehaviour
{
	public GameObject playerTypeDropdown;
	public GameObject playerNameInput;

	private void Start()
	{
		playerNameInput.SetActive(false);
	}

	public void SetPlayerType(int index)
	{
		if (playerTypeDropdown.GetComponent<Dropdown>().value == 0)
		{
			playerNameInput.SetActive(false);
			SavedSettings.players[index].playerType = PlayerType.AI;
		}
		else
		{
			playerNameInput.SetActive(true);
			SavedSettings.players[index].playerType = PlayerType.HUMAN;
		}
	}

	public void SetPlayerName(int index)
	{
		SavedSettings.players[index].playerName = playerNameInput.GetComponent<InputField>().text;
	}
}

public static class SavedSettings
{
	public static PlayerEntity[] players = {
		new PlayerEntity(Team.GREEN, "Player Green"),
		new PlayerEntity(Team.YELLOW,"Player Yellow" ),
		new PlayerEntity(Team.BLUE, "Player Blue"),
		new PlayerEntity(Team.RED, "Player Red")
	};

	public static PlayerRecord[] finalRecord = { new PlayerRecord(), new PlayerRecord(), new PlayerRecord(), new PlayerRecord() };
}

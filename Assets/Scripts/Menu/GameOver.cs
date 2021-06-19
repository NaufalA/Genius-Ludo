using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InGame;

public class GameOver : MonoBehaviour
{
	[System.Serializable]
	public class PlayerRecordDisplay
	{
		public Text playerName;
		public Text drawCount;
		public Text drawNumberCount;
		public Text travelCount;
		public Text travelBackCount;
		public Text killCount;
		public Text killedCount;
		public Text extraMoveCount;
		public Text goBackCount;
		public Text instaDeathCount;
	}

	public List<PlayerRecordDisplay> displays = new List<PlayerRecordDisplay>();

	private void Start()
	{
		checkWinner();
		for (int i = 0; i < SavedSettings.finalRecord.Length; i++)
		{
			PlayerRecord currentRecord = SavedSettings.finalRecord[i];

			if (currentRecord.playerType == InGame.PlayerType.AI)
			{
				displays[i].playerName.text = $"{currentRecord.playerName.ToString()} (AI)";
			}
			else
			{
				displays[i].playerName.text = $"{currentRecord.playerName.ToString()}";
			}

			displays[i].drawCount.text = $": {currentRecord.drawPerformed.ToString()}";
			displays[i].drawNumberCount.text = $": {currentRecord.drawAccumulated.ToString()}";
			displays[i].travelCount.text = $": {currentRecord.nodesTravelled.ToString()}";
			displays[i].travelBackCount.text = $": {currentRecord.nodesTravelledBackwards.ToString()}";
			displays[i].killCount.text = $": {currentRecord.pawnKills.ToString()}";
			displays[i].killedCount.text = $": {currentRecord.pawnKilled.ToString()}";
			displays[i].extraMoveCount.text = $": {currentRecord.extraMove.ToString()}";
			displays[i].goBackCount.text = $": {currentRecord.goBack.ToString()}";
			displays[i].instaDeathCount.text = $": {currentRecord.instaDeath.ToString()}";
		}
	}

	void checkWinner()
	{
		Debug.Log(SavedSettings.finalRecord[0].playerType);
		if (AchievmentRecord.achievments[0].achieved == false)
		{
			PlayerRecord winner = SavedSettings.finalRecord[0];
			if (winner.playerType == PlayerType.HUMAN)
			{
				AchievmentRecord.achievments[0].achieved = true;
			}
		}
	}

	public void Rematch(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}

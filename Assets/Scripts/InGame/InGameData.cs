using System.Collections;
using System.Collections.Generic;
using System;

namespace InGame
{
	[System.Serializable]
	public class InGameData
	{
		public List<PlayerEntity> players = new List<PlayerEntity>();
		public GlobalState globalState;
		public int activePlayer;
		public bool changingPlayer;
		public bool turnPossible;
	}

	[System.Serializable]
	public class SavedPawn
	{
		public Team pawnTeam;
		public int currentPos;
		public bool isActive;
		public bool isPlaying;
		public bool isImmune;
		public int drawNumber;
		public int steps;
	}

	[System.Serializable]
	public class SavedPlayerEntity
	{
		public string playerName;
		public PlayerType playerType;
		public Team playerTeam;
		public List<SavedPawn> pawns = new List<SavedPawn>();
		public bool isPlaying;
		public bool hasWon;
		public PlayerRecord record;
	}

	[System.Serializable]
	public class SavedGameData
	{
		public List<SavedPlayerEntity> players = new List<SavedPlayerEntity>();
		public GlobalState globalState;
		public int activePlayer;
		public bool changingPlayer;
		public bool turnPossible;

		public SavedGameData(InGameData inGameData)
		{
			for (int i = 0; i < inGameData.players.Count; i++)
			{
				List<SavedPawn> savePawns = new List<SavedPawn>();
				for (int j = 0; j < inGameData.players[i].pawns.Count; j++)
				{
					savePawns.Add(new SavedPawn
					{
						pawnTeam = inGameData.players[i].pawns[j].pawnTeam,
						currentPos = inGameData.players[i].pawns[j].currentPos,
						isActive = inGameData.players[i].pawns[j].isActive,
						isPlaying = inGameData.players[i].pawns[j].isPlaying,
						isImmune = inGameData.players[i].pawns[j].isImmune,
						drawNumber = inGameData.players[i].pawns[j].drawNumber,
						steps = inGameData.players[i].pawns[j].steps,
					});
				}
				players.Add(new SavedPlayerEntity
				{
					playerName = inGameData.players[i].playerName,
					playerType = inGameData.players[i].playerType,
					playerTeam = inGameData.players[i].playerTeam,
					isPlaying = inGameData.players[i].isPlaying,
					hasWon = inGameData.players[i].hasWon,
					pawns = savePawns,
					record = inGameData.players[i].record
				});
			}

			globalState = inGameData.globalState;
			activePlayer = inGameData.activePlayer;
			changingPlayer = inGameData.changingPlayer;
			turnPossible = inGameData.turnPossible;
		}

		// For use when there's no save data found
		public SavedGameData()
		{
			List<SavedPlayerEntity> savePlayers = new List<SavedPlayerEntity>();
			for (int i = 0; i < 4; i++)
			{
				List<SavedPawn> savePawns = new List<SavedPawn>();
				for (int j = 0; j < 4; j++)
				{
					savePawns.Add(new SavedPawn
					{
						pawnTeam = SavedSettings.players[i].playerTeam,
						currentPos = -1,
						isActive = false,
						isPlaying = false,
						isImmune = false,
						drawNumber = 0,
						steps = 0,
					});
				}

				savePlayers.Add(new SavedPlayerEntity
				{
					playerName = SavedSettings.players[i].playerName,
					playerType = SavedSettings.players[i].playerType,
					playerTeam = SavedSettings.players[i].playerTeam,
					isPlaying = false,
					hasWon = false,
					pawns = savePawns,
					record = new PlayerRecord(SavedSettings.players[i].playerName, SavedSettings.players[i].playerType, SavedSettings.players[i].playerTeam),
				});
			}
			globalState = GlobalState.DRAW_CARD;
			activePlayer = new Random().Next(0, 4);
			changingPlayer = false;
			turnPossible = true;
			players = savePlayers;
		}
	}
}

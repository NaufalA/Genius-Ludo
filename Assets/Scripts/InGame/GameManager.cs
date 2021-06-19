using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InGame
{
	public enum GlobalState
	{
		WAITING,
		DRAW_CARD,
		CHANGE_PLAYER,
		GAME_OVER,
	}

	[System.Serializable]
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;
		public InGameData gameData;

		[Header("HUMAN INPUT")]
		public GameObject drawButton;
		public int drawNumber;
		public GameObject[] pawnSelectors;

		[Header("INFO WINDOWS")]
		public Info infoWindow;
		public Info cardInfoWindow;

		[Header("ENVIRONMENT")]
		public GameObject activeCamera;
		public GameObject[] cameras;

		[Header("MISC")]
		public Quiz Quiz;
		public FlashVideo flashVideo;
		public AudioSource[] audio;


		private void Awake()
		{
			Instance = this;
			gameData.turnPossible = true;
		}

		private void Start()
		{
			AudioListener.volume = GlobalSettings.settings.sfx;
			LoadGameData();
			ActivateButton(false);
			infoWindow.ShowMessage("Game Starts");
		}

		private void Update()
		{
			switch (gameData.globalState)
			{
				case GlobalState.WAITING:
					// IDLE, WAIT FOR ANIMATION/INPUT
					break;
				case GlobalState.DRAW_CARD:
					{
						infoWindow.ShowMessage($"{gameData.players[gameData.activePlayer].playerName}'s Turn");
						cardInfoWindow.ShowMessage("DRAW CARD");
						if (gameData.players[gameData.activePlayer].playerType == PlayerType.AI)
						{
							if (gameData.turnPossible)
							{
								SaveGameData();
								StartCoroutine(DrawCardWait());
								gameData.globalState = GlobalState.WAITING;
							}
						}
						else if (gameData.players[gameData.activePlayer].playerType == PlayerType.HUMAN)
						{
							if (gameData.turnPossible)
							{
								SaveGameData();
								ActivateButton(true);
								gameData.globalState = GlobalState.WAITING;
							}
						}
					}
					break;
				case GlobalState.CHANGE_PLAYER:
					if (gameData.turnPossible)
					{
						gameData.players[gameData.activePlayer].hasWon = winCondition();
						StartCoroutine(ChangePlayer());
						gameData.globalState = GlobalState.WAITING;
					}
					break;
				case GlobalState.GAME_OVER:
					for (int i = 0; i < gameData.players.Count; i++)
					{
						if (!gameData.players[i].hasWon)
						{
							SavedSettings.finalRecord[gameData.players.Count - 1] = gameData.players[i].record.getFinalRecord();
						}
					}
					SaveSystem.ClearSave("savegame.geniusludo");
					SceneManager.LoadScene("GameOverScene");
					break;
			}
		}

		void DrawCard()
		{
			int drawNumber = Random.Range(1, 7);
			cardInfoWindow.ShowMessage($"{drawNumber}");
			gameData.players[gameData.activePlayer].record.countDraw(drawNumber);

			if (drawNumber == 6)
			{
				CheckStartNode(drawNumber);
			}

			if (drawNumber < 6)
			{
				MovePawn(drawNumber);
			}
		}

		IEnumerator DrawCardWait()
		{
			yield return new WaitForSeconds(1f);
			audio[0].Play();
			DrawCard();
		}

		void CheckStartNode(int drawNumber)
		{
			bool startNodeTaken = false;

			// Check start node availability
			for (int i = 0; i < gameData.players[gameData.activePlayer].pawns.Count; i++)
			{
				if (gameData.players[gameData.activePlayer].pawns[i].currentNode == gameData.players[gameData.activePlayer].pawns[i].startNode)
				{
					startNodeTaken = true;
					break;
				}
			}

			if (startNodeTaken)
			{
				MovePawn(drawNumber);
			}

			else
			{
				// Check inactive pawns
				for (int i = 0; i < gameData.players[gameData.activePlayer].pawns.Count; i++)
				{
					if (!gameData.players[gameData.activePlayer].pawns[i].GetStatus())
					{
						gameData.players[gameData.activePlayer].pawns[i].Activate();
						gameData.globalState = GlobalState.WAITING;
						return;
					}
				}
				MovePawn(drawNumber);
			}
		}

		void MovePawn(int drawNumber)
		{
			List<Pawn> movablePawn = new List<Pawn>();
			List<Pawn> moveKickPawn = new List<Pawn>();

			for (int i = 0; i < gameData.players[gameData.activePlayer].pawns.Count; i++)
			{
				Pawn currentPawn = gameData.players[gameData.activePlayer].pawns[i];
				currentPawn.drawNumber = drawNumber;

				if (currentPawn.GetStatus())
				{
					if (currentPawn.CheckPossibleKick(currentPawn.pawnTeam))
					{
						moveKickPawn.Add(currentPawn);
						continue;
					}

					if (currentPawn.CheckPossibleMove())
					{
						movablePawn.Add(currentPawn);
					}
				}
			}

			if (moveKickPawn.Count > 0)
			{
				int randomSelect = Random.Range(0, moveKickPawn.Count);
				moveKickPawn[randomSelect].StartMove();
				return;
			}

			if (movablePawn.Count > 0)
			{
				foreach (Pawn candidate in movablePawn)
				{
					if (candidate.fullRoute[candidate.currentPos + drawNumber].hasSpecial == Special.EXTRA_MOVE)
					{
						candidate.StartMove();
						return;
					}
					else if (candidate.fullRoute[candidate.currentPos + drawNumber].hasSpecial == Special.NONE)
					{
						candidate.StartMove();
						return;
					}
				}
				int randomSelect = Random.Range(0, movablePawn.Count);
				movablePawn[randomSelect].StartMove();
				return;
			}

			gameData.globalState = GlobalState.CHANGE_PLAYER;
		}

		IEnumerator ChangePlayer()
		{
			if (gameData.changingPlayer)
			{
				yield break;
			}

			gameData.changingPlayer = true;

			yield return new WaitForSeconds(1);
			SetActivePlayer();

			gameData.changingPlayer = false;
		}

		void SetActivePlayer()
		{
			gameData.activePlayer += 1;
			gameData.activePlayer %= gameData.players.Count;

			int available = 0;
			for (int i = 0; i < gameData.players.Count; i++)
			{
				if (!gameData.players[i].hasWon)
				{
					available += 1;
				}
			}

			if (gameData.players[gameData.activePlayer].hasWon && available > 1)
			{
				SetActivePlayer();
				return;
			}
			else if (available < 2)
			{
				gameData.globalState = GlobalState.GAME_OVER;
				return;
			}
			gameData.globalState = GlobalState.DRAW_CARD;
		}

		public void SetTurnPossible(bool possible)
		{
			gameData.turnPossible = possible;
		}

		public bool winCondition()
		{
			for (int i = 0; i < gameData.players[gameData.activePlayer].pawns.Count; i++)
			{
				if (!gameData.players[gameData.activePlayer].pawns[i].hasWon())
				{
					return false;
				}
			}

			infoWindow.ShowMessage($"{gameData.players[gameData.activePlayer].playerName} Win!");
			AchievmentRecord.checkAchievment("PACIFIST", gameData.players[gameData.activePlayer].record.pawnKills, gameData.players[gameData.activePlayer].playerType);
			audio[1].Play();
			reportWinner();

			return true;
		}

		public void reportWinner()
		{
			for (int i = 0; i < SavedSettings.finalRecord.Length; i++)
			{
				if (SavedSettings.finalRecord[i].playerName == "")
				{
					SavedSettings.finalRecord[i] = gameData.players[gameData.activePlayer].record.getFinalRecord();
					break;
				}
			}
		}

		// Human functions
		void ActivateButton(bool on)
		{
			drawButton.SetActive(on);
		}

		public void DeactivateAllSelectors()
		{
			foreach (PlayerEntity player in gameData.players)
			{
				for (int i = 0; i < player.pawns.Count; i++)
				{
					player.pawns[i].SetSelector(false);
					player.pawns[i].animator.SetBool("movable", false);
				}
			}
		}

		public void HumanDrawCard()
		{
			ActivateButton(false);
			audio[0].Play();

			drawNumber = Random.Range(1, 7);
			cardInfoWindow.ShowMessage($"{drawNumber}");
			gameData.players[gameData.activePlayer].record.countDraw(drawNumber);

			List<Pawn> possiblePawn = new List<Pawn>();

			// Add active pawns
			possiblePawn.AddRange(PossiblePawns());

			// Also add inactive pawns if drawNumber == 6
			if (drawNumber == 6)
			{
				for (int i = 0; i < gameData.players[gameData.activePlayer].pawns.Count; i++)
				{
					if (!gameData.players[gameData.activePlayer].pawns[i].GetStatus())
					{
						possiblePawn.Add(gameData.players[gameData.activePlayer].pawns[i]);
					}
				}
			}

			// Activate Selectors
			if (possiblePawn.Count > 0)
			{
				for (int i = 0; i < possiblePawn.Count; i++)
				{
					possiblePawn[i].SetSelector(true);
					possiblePawn[i].animator.SetBool("movable", true);
				}
			}
			else
			{
				gameData.globalState = GlobalState.CHANGE_PLAYER;
			}
		}

		List<Pawn> PossiblePawns()
		{
			List<Pawn> movablePawn = new List<Pawn>();
			PlayerEntity currentPlayer = gameData.players[gameData.activePlayer];

			for (int i = 0; i < currentPlayer.pawns.Count; i++)
			{
				Pawn currentPawn = currentPlayer.pawns[i];
				currentPawn.drawNumber = drawNumber;
				if (currentPawn.GetStatus())
				{
					if (currentPawn.CheckPossibleKick(currentPawn.pawnTeam))
					{
						movablePawn.Add(currentPawn);
					}
					if (currentPawn.CheckPossibleMove())
					{
						movablePawn.Add(currentPawn);
					}
				}
			}
			return movablePawn;
		}

		public int GetDrawNumber()
		{
			return drawNumber;
		}

		public void ToggleCamera()
		{
			for (int i = 0; i < cameras.Length; i++)
			{
				if (cameras[i].activeSelf)
				{
					cameras[i].SetActive(false);
				}
				else
				{
					activeCamera = cameras[i];
					cameras[i].SetActive(true);
				}
			}
		}

		public void LoadGameData()
		{
			SavedGameData loadedData = SaveSystem.LoadGame();

			gameData.globalState = loadedData.globalState;
			gameData.activePlayer = loadedData.activePlayer;
			gameData.changingPlayer = loadedData.changingPlayer;
			gameData.turnPossible = loadedData.turnPossible;
			for (int i = 0; i < loadedData.players.Count; i++)
			{
				PlayerEntity modifiedPlayer = gameData.players[i];
				SavedPlayerEntity loadedPlayer = loadedData.players[i];

				if (loadedData.players[i].playerName != "")
				{
					modifiedPlayer.playerName = loadedPlayer.playerName;
				}
				modifiedPlayer.playerType = loadedPlayer.playerType;
				modifiedPlayer.playerTeam = loadedPlayer.playerTeam;
				modifiedPlayer.isPlaying = loadedPlayer.isPlaying;
				modifiedPlayer.hasWon = loadedPlayer.hasWon;
				modifiedPlayer.record = loadedPlayer.record;
				if (modifiedPlayer.record.playerName == "")
				{
					modifiedPlayer.record.playerName = modifiedPlayer.playerName;
				}

				for (int j = 0; j < loadedPlayer.pawns.Count; j++)
				{
					Pawn modifiedPawn = gameData.players[i].pawns[j];
					SavedPawn loadedPawn = loadedData.players[i].pawns[j];

					modifiedPawn.currentPos = loadedPawn.currentPos;
					modifiedPawn.isActive = loadedPawn.isActive;
					modifiedPawn.isPlaying = loadedPawn.isPlaying;
					modifiedPawn.isImmune = loadedPawn.isImmune;
					modifiedPawn.drawNumber = loadedPawn.drawNumber;
					modifiedPawn.steps = loadedPawn.steps;
					// Make sure pawn routes already initialized
					if (modifiedPawn.fullRoute.Count == 0)
					{
						modifiedPawn.makeFullRoute();
					}
					if (modifiedPawn.currentPos != -1)
					{
						// link pawn to node based on current position on route
						modifiedPawn.currentNode = modifiedPawn.fullRoute[modifiedPawn.currentPos];
						modifiedPawn.currentNode.pawns.Add(modifiedPawn);
						modifiedPawn.transform.position = modifiedPawn.currentNode.transform.position;
					}
				}
			}
		}

		public void SaveGameData()
		{
			SaveSystem.SaveGame(gameData);
		}
	}
}
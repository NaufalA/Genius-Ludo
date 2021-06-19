using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
	[System.Serializable]
	public class Pawn : MonoBehaviour
	{
		public Team pawnTeam;

		[Header("ROUTES")]
		public Route commonRoute;
		public Route finalRoute;
		public List<Node> fullRoute = new List<Node>();

		[Header("NODES")]
		public Node baseNode;
		public Node startNode;
		public Node currentNode;
		public Node goalNode;
		public int currentPos;

		[Header("STATES")]
		bool isMoving;
		public bool isActive;
		public bool isPlaying;
		public bool isImmune;
		public int drawNumber;
		public int steps;

		[Header("SELECTOR")]
		public GameObject selector;
		public GameObject indicator;
		public GameObject immuneIndicator;

		[Header("ANIMATION")]
		public Animator animator;
		public AudioSource[] audio = new AudioSource[4];

		private void Start()
		{
			makeFullRoute();
			SetSelector(false);
			immuneIndicator.SetActive(isImmune);

			animator = GetComponentInChildren<Animator>();
		}

		public void makeFullRoute()
		{
			fullRoute.Clear();
			for (int i = 0; i < commonRoute.routeNodes.Count - 1; i++)
			{
				int tempPos = commonRoute.selectNodeIndex(startNode.gameObject.transform) + i;
				tempPos %= commonRoute.routeNodes.Count;

				fullRoute.Add(commonRoute.routeNodes[tempPos].GetComponent<Node>());
			}

			for (int i = 0; i < finalRoute.routeNodes.Count; i++)
			{
				fullRoute.Add(finalRoute.routeNodes[i].GetComponent<Node>());
			}
		}

		public bool GetStatus()
		{
			return isActive;
		}

		public void Activate()
		{
			currentPos = 0;
			isActive = true;
			StartCoroutine(MoveOut());
		}

		public void Deactivate()
		{
			if (isImmune)
			{
				isImmune = false;
				immuneIndicator.SetActive(false);
				audio[3].Play();
				GameManager.Instance.SetTurnPossible(true);
				GameManager.Instance.infoWindow.ShowMessage($"{GameManager.Instance.gameData.players.Find((player) => player.playerTeam == pawnTeam).playerName} pawn is protected by Immunity");
				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countImmune();
			}
			else
			{
				currentPos = -1;
				isActive = false;
				StartCoroutine(MoveIn());
				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countKill("killed");
			}
		}

		public bool CheckPossibleMove()
		{
			int goalPos = currentPos + drawNumber;

			if (goalPos > fullRoute.Count - 1)
			{
				return false;
			}

			return true;
		}

		public bool CheckPossibleKick(Team pawnTeam)
		{
			int goalPos = currentPos + drawNumber;

			if (goalPos > fullRoute.Count - 1)
			{
				return false;
			}
			else if (fullRoute[goalPos].isTaken)
			{
				if (pawnTeam == fullRoute[goalPos].pawns[0].pawnTeam)
				{
					return false;
				}
				else if (fullRoute[goalPos].pawns.Count > 1)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			return false;
		}

		public void StartMove()
		{
			steps = drawNumber;

			StartCoroutine(Move());

			if (drawNumber == 6)
			{
				isImmune = true;
				immuneIndicator.SetActive(true);
				audio[2].Play();
				GameManager.Instance.gameData.globalState = GlobalState.DRAW_CARD;
			}
			else
			{
				GameManager.Instance.gameData.globalState = GlobalState.CHANGE_PLAYER;
			}

		}

		public IEnumerator MoveOut()
		{
			if (isMoving)
			{
				yield break;
			}

			isMoving = true;

			while (MoveToNextNode(startNode.gameObject.transform.position, 1000f))
			{
				yield return null;
			}

			yield return new WaitForSeconds(1f);

			SetupNewNode();

			isMoving = false;
			GameManager.Instance.gameData.globalState = GlobalState.DRAW_CARD;
		}

		public IEnumerator MoveIn()
		{
			GameManager.Instance.SetTurnPossible(false);

			DelistOldNode();

			yield return new WaitForSeconds(0.5f);

			Vector3 baseNodePos = baseNode.gameObject.transform.position;
			while (MoveToNextNode(baseNodePos, 1000f)) { yield return null; }

			GameManager.Instance.SetTurnPossible(true);
			yield return new WaitForSeconds(0.2f);
		}

		public IEnumerator Move(float waitTime = 0f)
		{
			GameManager.Instance.SetTurnPossible(false);
			if (isMoving)
			{
				yield break;
			}

			isMoving = true;

			yield return new WaitForSeconds(waitTime);
			animator.SetBool("isMoving", true);
			audio[0].Play();

			DelistOldNode();

			while (steps > 0)
			{
				Vector3 nextPos = fullRoute[currentPos + 1].gameObject.transform.position;

				while (MoveToNextNode(nextPos)) { yield return null; }

				steps--;
				currentPos++;

				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countTravel("forward");

				yield return new WaitForSeconds(0);
			}

			SetupNewNode();

			isMoving = false;
			animator.SetBool("isMoving", false);
			audio[0].Stop();
			StartCoroutine(CheckSpecialNode());
		}

		IEnumerator MoveBack(float waitTime = 0f)
		{
			GameManager.Instance.SetTurnPossible(false);
			if (isMoving)
			{
				yield break;
			}

			isMoving = true;

			yield return new WaitForSeconds(waitTime);
			animator.SetBool("isMoving", true);
			audio[0].Play();

			DelistOldNode();

			while (steps > 0)
			{
				if (currentPos - 1 > -1)
				{
					Vector3 nextPos = fullRoute[currentPos - 1].gameObject.transform.position;

					while (MoveToNextNode(nextPos))
					{
						yield return null;
					}

					steps--;
					currentPos--;

					GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countTravel("backward");

					yield return new WaitForSeconds(0);
				}
				else if (currentPos - 1 <= -1)
				{
					SetupNewNode();
					isMoving = false;
					animator.SetBool("isMoving", false);
					audio[0].Stop();
					GameManager.Instance.SetTurnPossible(true);
					yield break;
				}
			}

			SetupNewNode();
			isMoving = false;
			animator.SetBool("isMoving", false);
			audio[0].Stop();
			GameManager.Instance.SetTurnPossible(true);
		}

		IEnumerator ExtraMove()
		{
			if (GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].playerType == PlayerType.HUMAN)
			{
				GameManager.Instance.Quiz.StartQuiz();
				while (GameManager.Instance.Quiz.takingQuiz == true)
				{
					yield return null;
				}
			}
			else
			{
				GameManager.Instance.Quiz.isCorrect = true;
				GameManager.Instance.Quiz.takingQuiz = false;
			}

			while (GameManager.Instance.Quiz.takingQuiz == true)
			{
				yield return null;
			}

			if (GameManager.Instance.Quiz.isCorrect)
			{
				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countCorrect();
				GameManager.Instance.infoWindow.ShowMessage($"{GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].playerName} got Double Move!");
				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countSpecial("extraMove");
				steps = drawNumber * 2;
				audio[1].Play();
				StartCoroutine(Move(0.5f));
			}

			yield break;
		}

		IEnumerator GoBack()
		{
			if (GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].playerType == PlayerType.HUMAN)
			{
				GameManager.Instance.Quiz.StartQuiz();
			}
			else
			{
				GameManager.Instance.Quiz.isCorrect = false;
				GameManager.Instance.Quiz.takingQuiz = false;
			}

			while (GameManager.Instance.Quiz.takingQuiz == true)
			{
				yield return null;
			}

			if (!GameManager.Instance.Quiz.isCorrect)
			{
				GameManager.Instance.infoWindow.ShowMessage($"{GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].playerName} has to Go Back!");
				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countSpecial("goBack");
				steps = drawNumber * 2;
				StartCoroutine(MoveBack(0.5f));
			}
			else
			{
				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countCorrect();
			}

			yield break;
		}

		IEnumerator InstaDeath()
		{
			if (GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].playerType == PlayerType.HUMAN)
			{
				GameManager.Instance.Quiz.StartQuiz();
				while (GameManager.Instance.Quiz.takingQuiz == true)
				{
					yield return null;
				}
			}
			else
			{
				GameManager.Instance.Quiz.isCorrect = false;
				GameManager.Instance.Quiz.takingQuiz = false;
			}

			while (GameManager.Instance.Quiz.takingQuiz == true)
			{
				yield return null;
			}

			if (!GameManager.Instance.Quiz.isCorrect)
			{
				GameManager.Instance.infoWindow.ShowMessage($"{GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].playerName} fell into Trap!");
				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countSpecial("instaDeath");
				if (!isImmune)
				{
					GameManager.Instance.flashVideo.gameObject.SetActive(true);
					StartCoroutine(GameManager.Instance.flashVideo.PlayTrapVideo(pawnTeam));
				}
				Deactivate();
			}
			else
			{
				GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countCorrect();
			}

			yield break;
		}

		IEnumerator CheckSpecialNode()
		{
			GameManager.Instance.SetTurnPossible(false);
			switch (currentNode.hasSpecial)
			{
				case Special.EXTRA_MOVE:
					StartCoroutine(ExtraMove());
					yield break;
				case Special.GO_BACK:
					StartCoroutine(GoBack());
					yield break;
				case Special.INSTA_DEATH:
					StartCoroutine(InstaDeath());
					yield break;
				case Special.NONE:
					GameManager.Instance.SetTurnPossible(true);
					yield break;
			}
		}

		bool MoveToNextNode(Vector3 goal, float speed = 4f)
		{
			transform.LookAt(goal);
			return goal != (
				(
					transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime)
				)
			);
		}

		void DelistOldNode()
		{
			currentNode.pawns.Remove(this);
			if (currentNode.pawns.Count == 0)
			{
				currentNode.isTaken = false;
			}
			currentNode = null;
		}

		void EnlistNewNode()
		{
			goalNode.pawns.Add(this);
			goalNode.isTaken = true;
			currentNode = goalNode;
			goalNode = null;
		}

		void SetupNewNode()
		{
			goalNode = fullRoute[currentPos];
			// NODE TAKEN
			if (goalNode.isTaken)
			{
				// SAME TEAM WITH PAWN
				// Join
				if (pawnTeam == goalNode.pawns[0].pawnTeam)
				{
					EnlistNewNode();
				}
				// DIFFERENT TEAM WITH PAWN
				else if (pawnTeam != goalNode.pawns[0].pawnTeam)
				{
					// SAME TEAM WITH NODE
					// Win
					if (pawnTeam == goalNode.nodeTeam)
					{
						for (int i = 0; i < goalNode.pawns.Count; i++)
						{
							if (!goalNode.pawns[i].isImmune)
							{
								GameManager.Instance.flashVideo.gameObject.SetActive(true);
								StartCoroutine(GameManager.Instance.flashVideo.PlayKillVideo(pawnTeam, goalNode.pawns[i].pawnTeam));
							}
							goalNode.pawns[i].Deactivate();
							GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countKill("kill");
						}
						EnlistNewNode();
					}
					// DIFFERENT TEAM WITH NODE
					// Join
					else if (goalNode.pawns[0].pawnTeam == goalNode.nodeTeam)
					{
						EnlistNewNode();
					}
					// NEUTRAL WITH NODE
					// Win
					else
					{
						for (int i = 0; i < goalNode.pawns.Count; i++)
						{
							if (!goalNode.pawns[i].isImmune)
							{
								GameManager.Instance.flashVideo.gameObject.SetActive(true);
								StartCoroutine(GameManager.Instance.flashVideo.PlayKillVideo(pawnTeam, goalNode.pawns[i].pawnTeam));
							}
							goalNode.pawns[i].Deactivate();
							GameManager.Instance.gameData.players[GameManager.Instance.gameData.activePlayer].record.countKill("kill");
						}
						EnlistNewNode();
					}
				}
			}
			// NODE FREE
			else
			{
				EnlistNewNode();
			}
		}

		public bool hasWon()
		{
			if (currentPos == fullRoute.Count - 1) return true;

			else return false;
		}

		// Human Input
		public void SetSelector(bool on)
		{
			isPlaying = on;
			selector.SetActive(on);
			indicator.SetActive(on);
		}

		public void OnMouseDown()
		{
			drawNumber = GameManager.Instance.GetDrawNumber();
			if (isPlaying)
			{

				if (isActive)
				{
					StartMove();
				}
				else
				{
					Activate();
				}
			}
			GameManager.Instance.DeactivateAllSelectors();
		}
	}
}
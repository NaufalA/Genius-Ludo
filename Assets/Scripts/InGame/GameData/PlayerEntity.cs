using System.Collections;
using System.Collections.Generic;

namespace InGame
{
	public enum Team
	{
		GREEN,
		YELLOW,
		BLUE,
		RED,
		NONE
	}

	public enum PlayerType
	{
		HUMAN,
		AI
	}

	[System.Serializable]
	public class PlayerEntity
	{
		public string playerName;
		public PlayerType playerType;
		public Team playerTeam;
		public List<Pawn> pawns = new List<Pawn>();
		public bool isPlaying;
		public bool hasWon;
		public PlayerRecord record;

		public PlayerEntity(Team team, string name = "", PlayerType type = PlayerType.AI)
		{
			playerName = name;
			playerTeam = team;
			playerType = type;
		}
	}
}

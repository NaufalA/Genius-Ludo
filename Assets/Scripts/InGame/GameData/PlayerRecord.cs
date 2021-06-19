using UnityEngine;

namespace InGame
{
	[System.Serializable]
	public class PlayerRecord
	{
		public string playerName;
		public PlayerType playerType;
		public Team playerTeam;
		public int drawPerformed;
		public int drawAccumulated;
		public int nodesTravelled;
		public int nodesTravelledBackwards;
		public int pawnKills;
		public int pawnKilled;
		public int immunityUsed;
		public int correctAnswer;
		public int extraMove;
		public int goBack;
		public int instaDeath;

		public PlayerRecord(string name, PlayerType type, Team team)
		{
			playerName = name;
			playerType = type;
			playerTeam = team;
			drawPerformed = 0;
			drawAccumulated = 0;
			nodesTravelled = 0;
			pawnKills = 0;
			pawnKilled = 0;
			immunityUsed = 0;
			correctAnswer = 0;
			extraMove = 0;
			goBack = 0;
			instaDeath = 0;
		}

		public PlayerRecord()
		{
			playerName = "";
		}

		public void countDraw(int drawNumber)
		{
			drawPerformed += 1;
			drawAccumulated += drawNumber;
		}

		public void countTravel(string direction)
		{
			if (direction == "forward")
			{
				nodesTravelled += 1;
			}
			else if (direction == "backward")
			{
				nodesTravelledBackwards += 1;
			}
		}

		public void countImmune()
		{
			immunityUsed += 1;
			AchievmentRecord.checkAchievment("ANGEL", immunityUsed, playerType);
		}

		public void countCorrect()
		{
			correctAnswer += 1;
			AchievmentRecord.checkAchievment("BB", correctAnswer, playerType);
		}

		public void countSpecial(string type)
		{
			if (type == "extraMove")
			{
				extraMove += 1;
				AchievmentRecord.checkAchievment("DOUBLEMOVE", extraMove, playerType);
			}
			else if (type == "goBack")
			{
				goBack += 1;
				AchievmentRecord.checkAchievment("GOBACK", goBack, playerType);
			}
			else if (type == "instaDeath")
			{
				instaDeath += 1;
				AchievmentRecord.checkAchievment("TRAP", instaDeath, playerType);
			}
		}

		public void countKill(string type)
		{
			if (type == "kill")
			{
				pawnKills += 1;
				AchievmentRecord.checkAchievment("KILLER", pawnKills, playerType);
			}
			else if (type == "killed")
			{
				pawnKilled += 1;
				AchievmentRecord.checkAchievment("MARTYR", pawnKilled, playerType);
			}
		}

		public PlayerRecord getFinalRecord()
		{
			return this;
		}
	}
}

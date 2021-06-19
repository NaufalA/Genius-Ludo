using System.Collections;
using System.Collections.Generic;

namespace InGame
{
	[System.Serializable]
	public class AchievmentEntry<T>
	{
		public string shorthand;
		public string name;
		public string description;
		public T goal;
		public bool achieved;

		public AchievmentEntry(string newShorthand, string newName, string newDescription, T newGoal)
		{
			shorthand = newShorthand;
			name = newName;
			description = newDescription;
			goal = newGoal;
			achieved = false;
		}
	}

	[System.Serializable]
	public static class AchievmentRecord
	{
		public static List<AchievmentEntry<int>> achievments = new List<AchievmentEntry<int>>()
		{
			{new AchievmentEntry<int>("WIN", "Triumph!", "Win #1 as a human player", 1)},
			{new AchievmentEntry<int>("PACIFIST", "Pacifist", "Win without killing other player's pawn", 0)},
			{new AchievmentEntry<int>("LUCK", "Good Fortune", "Get 6 card draw 3 times in a row", 3)},
			{new AchievmentEntry<int>("KILLER", "Serial Killer", "Kill 5 enemy pawns in a single game", 5)},
			{new AchievmentEntry<int>("MARTYR", "Martyr", "Have your pawn killed 5 times by other player in a single game", 5)},
			{new AchievmentEntry<int>("BB", "Big Brain", "Answer 3 questions correctly in a single game", 3)},
			{new AchievmentEntry<int>("ANGEL", "Guardian Angel", "Have your pawns saved 3 times by Immunity power up in a single game", 3)},
			{new AchievmentEntry<int>("DOUBLEMOVE", "Gotta Go Fast!", "Get Double Move power up 3 times in a single game", 3)},
			{new AchievmentEntry<int>("GOBACK", "Stop! Wait a minute", "Have to Go Back 3 times in a single game", 3)},
			{new AchievmentEntry<int>("TRAP", "Death on arrival", "Fell into trap 3 times in a single game", 3)},
		};

		public static void checkAchievment(string achievmentKey, int parameter, PlayerType playerType)
		{
			AchievmentEntry<int> achievmentEntry = achievments.Find(achievment => achievment.shorthand == achievmentKey);
			if (achievmentEntry.achieved == false)
			{
				if (playerType == PlayerType.HUMAN)
				{
					if (parameter == achievmentEntry.goal)
					{
						achievmentEntry.achieved = true;
						SaveSystem.SaveAchievments();
					}
				}
			}
		}
	}
}

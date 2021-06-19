using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InGame;

public class Achievments : MonoBehaviour
{
	[System.Serializable]
	public class AchievmentDisplay
	{
		public GameObject achievmentWindow;
		public Text achievmentName;
		public Text achievmentDescription;
	}

	public List<AchievmentDisplay> displays = new List<AchievmentDisplay>();

	void Start()
	{

		SaveSystem.LoadAchievments();
		for (int i = 0; i < AchievmentRecord.achievments.Count; i++)
		{
			displays[i].achievmentWindow.SetActive(false);

			AchievmentEntry<int> currentEntry = AchievmentRecord.achievments[i];

			displays[i].achievmentName.text = $"{currentEntry.name}";
			displays[i].achievmentDescription.text = $"{currentEntry.description}";

			if (currentEntry.achieved)
			{
				displays[i].achievmentWindow.SetActive(true);
			}
		}
	}
}
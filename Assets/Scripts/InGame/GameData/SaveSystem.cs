using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace InGame
{
	public static class SaveSystem
	{
		public static void SaveGame(InGameData inGameData)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			string savePath = $"{Application.persistentDataPath}/savegame.geniusludo";
			FileStream stream = new FileStream(savePath, FileMode.Create);

			SavedGameData data = new SavedGameData(inGameData);

			formatter.Serialize(stream, data);

			stream.Close();
		}

		public static SavedGameData LoadGame()
		{
			string savePath = $"{Application.persistentDataPath}/savegame.geniusludo";
			if (File.Exists(savePath))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(savePath, FileMode.Open);

				SavedGameData data = formatter.Deserialize(stream) as SavedGameData;

				stream.Close();

				return data;
			}
			else
			{
				return new SavedGameData();
			}
		}

		public static void SaveAchievments()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			string savePath = $"{Application.persistentDataPath}/achievments.geniusludo";
			FileStream stream = new FileStream(savePath, FileMode.Create);

			formatter.Serialize(stream, AchievmentRecord.achievments);

			stream.Close();
		}

		public static void LoadAchievments()
		{
			string savePath = $"{Application.persistentDataPath}/achievments.geniusludo";
			if (File.Exists(savePath))
			{
				Debug.Log("save found at" + savePath);
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(savePath, FileMode.Open);

				AchievmentRecord.achievments = formatter.Deserialize(stream) as List<AchievmentEntry<int>>;

				stream.Close();
			}
		}

		public static void SaveSettings()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			string savePath = $"{Application.persistentDataPath}/settings.geniusludo";
			FileStream stream = new FileStream(savePath, FileMode.Create);

			formatter.Serialize(stream, GlobalSettings.settings);

			stream.Close();
		}

		public static void LoadSettings()
		{
			string savePath = $"{Application.persistentDataPath}/settings.geniusludo";
			if (File.Exists(savePath))
			{
				Debug.Log("save found at" + savePath);
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(savePath, FileMode.Open);

				GlobalSettings.settings = formatter.Deserialize(stream) as SettingsData;

				stream.Close();

				Debug.Log(GlobalSettings.settings.sfx);
				Debug.Log(GlobalSettings.settings.bgm);
			}
		}

		public static void ClearSave(string fileName)
		{
			string savePath = $"{Application.persistentDataPath}/{fileName}";
			if (File.Exists(savePath))
			{
				File.Delete(savePath);
				Debug.Log("Save cleared from" + savePath);
			}
		}

		public static bool CheckSave(string fileName)
		{
			string savePath = $"{Application.persistentDataPath}/{fileName}";
			if (File.Exists(savePath))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
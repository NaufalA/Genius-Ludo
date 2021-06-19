using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	private void Awake()
	{
		this.gameObject.SetActive(false);
	}
	public void NavigateTo(string scene)
	{
		Time.timeScale = 1f;
		GlobalSettings.previousScene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene);
	}

	public void PauseGame()
	{
		this.gameObject.SetActive(true);
		Time.timeScale = 0f;
	}
	public void ResumeGame()
	{
		this.gameObject.SetActive(false);
		Time.timeScale = 1f;
	}

	public void TurnOnAR()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("InGameSceneAR");
	}

	public void TurnOffAR()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("InGameScene");
	}
}

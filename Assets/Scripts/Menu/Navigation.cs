using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
	public void NavigateTo(string sceneName)
	{
		GlobalSettings.previousScene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(sceneName);
	}

	public void NavigateToNoBack(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void NavigateBack()
	{
		SceneManager.LoadScene(GlobalSettings.previousScene);
		GlobalSettings.previousScene = 0;
	}
}

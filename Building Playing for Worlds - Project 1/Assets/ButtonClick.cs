using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
	public Button yourButton;
	public string scene;

	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		if (scene == "Quit")
		{
			Application.Quit();
		}
		else
		{
			SceneManager.LoadScene("Scenes/"+scene);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLayGameFromMainMenu : MonoBehaviour 
{

	
	void Start () 
	{
		
	}
	
	
	void Update () 
	{
		
	}

	
    public void PlayGame(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


	void SetInitialReferences()
	{

	}
}

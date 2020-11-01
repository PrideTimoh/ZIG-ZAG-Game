using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour 
{

    public static GameManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
       
    }

    public bool gameOver;
   

   

    void Start () 
	{
       SetInitialReferences();
	}
	
	
	void Update () 
	{
		
	}

    public void StartGame ()
    {
        //UIManager.instance.CallGameStart();
        ScoreManager.instance.StatrScore();
    }

    public void GameOver()
    {
        gameOver = true;
        UIManager.instance.GameOver();
        ScoreManager.instance.StopScore();
    }

	void SetInitialReferences()
	{

    }
}

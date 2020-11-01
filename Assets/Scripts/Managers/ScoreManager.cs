using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour 
{
    public static ScoreManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

   
    public int score;
    public int highScore;
    public float gameSpeedAdd;
    public float timeScale;

    void Start () 
	{
       // PlayerPrefs.SetInt("highScore", 0);
        PlayerPrefs.SetInt("score", score);
        SetInitialReferences();
	}


    void Update()
    {
        if(UIManager.instance.ControllPanell != null)
        {
            if (UIManager.instance.ControllPanell.activeInHierarchy == true)
            {
                CancelInvoke("IncrementScore");
            }
        }
     
        if (score == 100 || score == 1200 || score == 600)
        {
            if (MusicManager.instance.m_yeah || MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_yeah, Camera.main.transform.position, MusicManager.instance.m_fxVolume * 5f);
            }
        }
        else if (score == 1000 || score == 1500 || score == 1800 || score == 2700 || score == 1300)
        {
            if (MusicManager.instance.m_amazing || MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_amazing, Camera.main.transform.position, MusicManager.instance.m_fxVolume * 5f);
            }
        }
        else if (score == 2000 || score == 3000 || score == 400 || score == 800 || score == 1400 || score == 2300 || score == 1600)
        {
            if (MusicManager.instance.m_incredible || MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_incredible, Camera.main.transform.position, MusicManager.instance.m_fxVolume * 5f);
            }
        }


        timeScale = Time.timeScale;
        Time.timeScale += gameSpeedAdd * Time.deltaTime;


    } 
        
	void Voice(AudioClip clip)
    {
        if (MusicManager.instance.m_fxEnabled)
        {
            //clip = MusicManager.instance.m_incredible;
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, MusicManager.instance.m_fxVolume * 10f);
        }
    }
  
   public void IncrementScore()
   {
        //yield return new WaitForSeconds(1f);
        score += 1;
        
        //StartCoroutine(IncrementScore());
   }

    public void StatrScore()
    {
        InvokeRepeating("IncrementScore", .5f, 20f);
        //StartCoroutine(IncrementScore());
    }

    public void PauseScore()
    {
        CancelInvoke("IncrementScore");
        //StopCoroutine(IncrementScore());
    }

   public void StopScore()
    {
        CancelInvoke("IncrementScore");
       // StopCoroutine(IncrementScore());
        PlayerPrefs.SetInt("score", score);
        if(PlayerPrefs.HasKey("highScore"))
        {
            if(score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("highScore", score);
        }


    }

	
	void SetInitialReferences()
	{
       
        score = 0;
	}
}

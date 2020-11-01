using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour 
{
    public static UIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public float countdownSeconds;
    public float ControllSeconds = 5f;
    public GameObject pauseButton;
    public GameObject playButton;
    private PlatformSpawner platformSpawnerScript;
    public GameObject zigZagPannel;
    public GameObject gameOverPannel;
    public GameObject pausePannel;
    public GameObject ControllPanell;
    public GameObject blockerPanell;
    public GameObject countDown;
    //public GameObject tapText;
    public Text score;
    public Text score2;
    public Text inGameScore;
    public Text highScore1;
    public Text highScore2;

    bool isPaused = false;


	void Start () 
	{
        
        SetInitialReferences();
        highScore1.text = PlayerPrefs.GetInt("highScore").ToString();
    }


    private void Update()
    {
        score2.text = ScoreManager.instance.score.ToString();
        inGameScore.text = ScoreManager.instance.score.ToString();

        if (Input.GetKeyDown(KeyCode.Pause) || Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            GamePause();
            GamePlay();
        }
        
    }

    public void PauseOrPlayGame()
    {
        isPaused = !isPaused;
        GamePause();
        GamePlay();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    

    void GamePause()
    {
       
        if(!isPaused)
        {
            return;
        }
        else if(isPaused)
        {
           
            BallController.instance.dontMove = true;
            PlayerMovement.instance.idle = true;
            PlayerMovement.instance.StopIncreasingSpeed();
            ScoreManager.instance.PauseScore();
            PlatformSpawner.instance.StopSpawn();
            pausePannel.SetActive(true);
            pauseButton.SetActive(false);
            
        }

    }

    void GamePlay()
    {
        
        if(isPaused)
        {
            return;
        }
        else if(!isPaused)
        {
            BallController.instance.dontMove = false;
            PlayerMovement.instance.idle = false;
            PlayerMovement.instance.IncreaseSpeeds();
            ScoreManager.instance.StatrScore();
            PlatformSpawner.instance.Caller();
            MusicManager.instance.playRandomBkGroundMusic();
            pausePannel.SetActive(false);
            pauseButton.SetActive(true);
        }

    }
    
    public void CallGameStart()
    {
        StartCoroutine(GameStart());
    }


    IEnumerator GameStart()
    {
        zigZagPannel.GetComponent<Animator>().Play("PannelUp");

        if (PlayerPrefs.GetInt("highScore") == 0)
        {
            
            if(ControllPanell != null)
            {
                PlayerMovement.instance.idle = true;
                ControllPanell.SetActive(true);
                yield return new WaitForSeconds(ControllSeconds);
                ControllPanell.SetActive(false);
                Destroy(ControllPanell);

                countDown.SetActive(true);
                yield return new WaitForSeconds(countdownSeconds);
                countDown.SetActive(false);
                DestroyImmediate(countDown);
                DestroyImmediate(blockerPanell);

                PlayerMovement.instance.MoveLeft();
                PlayerMovement.instance.IncreaseSpeeds();

            }
        }
        else
        {
            countDown.SetActive(true);
            yield return new WaitForSeconds(countdownSeconds);
            countDown.SetActive(false);
            DestroyImmediate(countDown);
            DestroyImmediate(blockerPanell);
            PlayerMovement.instance.MoveLeft();
            PlayerMovement.instance.IncreaseSpeeds();
        }


        highScore1.text = PlayerPrefs.GetInt("highScore").ToString();
       
        platformSpawnerScript.Caller();
        pauseButton.SetActive(true);
        playButton.SetActive(true);
    }



    public void GameOver()
    {
        if (MusicManager.instance.m_gameOverSound || MusicManager.instance.m_fxEnabled)
        {
            AudioSource.PlayClipAtPoint(MusicManager.instance.m_gameOverSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume * 2f);
        }

        score.text = PlayerPrefs.GetInt("score").ToString();
        highScore2.text = PlayerPrefs.GetInt("highScore").ToString();
        gameOverPannel.SetActive(true);
        pauseButton.SetActive(false);
        playButton.SetActive(false);
        PlayerMovement.instance.StopIncreasingSpeed();
        ScoreManager.instance.StopScore();
        
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	


	void SetInitialReferences()
	{
        platformSpawnerScript = GameObject.Find("PLatformSpawner").GetComponent<PlatformSpawner>();
	}
}

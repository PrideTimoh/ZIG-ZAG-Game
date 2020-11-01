using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallController : MonoBehaviour
{

    public static BallController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    [SerializeField]
   
    public float speed;
    public GameObject particle;
    private Transform myTransform;
    private Rigidbody ballRigidbody;
    private bool isRight;
    public bool dontMove;
    public bool gameOver;
    public bool reset;
    public LayerMask groundLayer;
    RaycastHit hitInfo;

    public int lifes = 0;
    public Text lifesText;

    bool canUseMouse;

    // private MusicManager m_musicManager;

    void Start()
    {
        //canUseMouse = true;
        SetInitialReferences();
    }


    void Update()
    {
        
        //CheckIfMouseIsClicked();

        CheckIfGameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Collectable"))
        {
            lifes += 1;

            if (MusicManager.instance.m_collectSound || MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_collectSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume);
            }

            GameObject part = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(other.gameObject);
            Destroy(part, 1f);

        }
    }




    public void CheckIfMouseIsClicked()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameOver && !reset && canUseMouse == true)
        {

            if (MusicManager.instance.m_tapSound && MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_tapSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume);
            }
            dontMove = false;
            isRight = !isRight;
            GameManager.instance.StartGame();
            canUseMouse = false;
        }

        if (Input.GetKeyDown(KeyCode.A) && !gameOver && !reset || Input.GetKeyDown(KeyCode.LeftArrow) && !gameOver && !reset)
        {
            if (MusicManager.instance.m_tapSound && MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_tapSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume);
            }
            dontMove = false;
            isRight = false;
            GameManager.instance.StartGame();
        }

        if (Input.GetKeyDown(KeyCode.D) && !gameOver && !reset || Input.GetKeyDown(KeyCode.RightArrow) && !gameOver && !reset)
        {
            if (MusicManager.instance.m_tapSound && MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_tapSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume);
            }
            dontMove = false;
            isRight = true;
            GameManager.instance.StartGame();
        }



        if (isRight == true)
        {
            ballRigidbody.velocity = new Vector3(speed, 0, 0);
        }
        else if (isRight == false)
        {
            ballRigidbody.velocity = new Vector3(0, 0, speed);
        }

        if (dontMove == true)
        {
            ballRigidbody.velocity = new Vector3(0, 0, 0);
        }

    }

    void MoveDown()
    {
        // dontMove = true;
        ballRigidbody.velocity = new Vector3(0, -25, 0);

        //if(myTransform.position == new Vector3(0, -3, 0))
        //{
        //    MoveBackUp();
        //}

    }

    void MoveBackUp()
    {
        ballRigidbody.velocity = new Vector3(0, 25, 0);
        if (transform.position.y == 0.9f)
        {
            ballRigidbody.velocity = new Vector3(0, 0, 0);
        }
        //  myTransform.position = new Vector3(0, 0, 0);
        reset = false;
        //dontMove = false;


    }

    public void MoveLeft()
    {
        if (!gameOver && !reset && isRight)
        {
            if (MusicManager.instance.m_tapSound && MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_tapSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume);
            }
            dontMove = false;
            isRight = false;
            GameManager.instance.StartGame();
        }
        else if (!gameOver && !reset && !isRight)
        {
            // return;
            if (MusicManager.instance.m_tapSound && MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_tapSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume);
            }
            dontMove = false;
            isRight = false;
            GameManager.instance.StartGame();
        }
    }

    public void MoveRight()
    {
        if (!gameOver && !reset && !isRight)
        {
            if (MusicManager.instance.m_tapSound && MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_tapSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume);
            }
            dontMove = false;
            isRight = true;
            GameManager.instance.StartGame();
        }
        else if (!gameOver && !reset && isRight)
        {
            // return;
            if (MusicManager.instance.m_tapSound && MusicManager.instance.m_fxEnabled)
            {
                AudioSource.PlayClipAtPoint(MusicManager.instance.m_tapSound, Camera.main.transform.position, MusicManager.instance.m_fxVolume);
            }
            dontMove = false;
            isRight = true;
            GameManager.instance.StartGame();
        }
    }


    void CheckIfGameOver()
    {
        if (reset == true)
        {
            // gameOver = false;
            MoveDown();
            Invoke("MoveBackUp", 0.5f);
        }
        
        if(Physics.Raycast(myTransform.position, Vector3.down, out hitInfo, 1f))
        {
            Debug.Log(hitInfo.collider.name);
        }

        if (!Physics.Raycast(myTransform.position, Vector3.down,/* out hitInfo,*/  1f))
        {
            


            if (lifes <= 0)
            {
                gameOver = true;
            }
            else if (lifes > 0)
            {
                reset = true;
                lifes -= 1;
            }

        }


        if (gameOver == true)
        {
            ballRigidbody.velocity = new Vector3(0, -25, 0);
            GameManager.instance.GameOver();
        }
    }





    void SetInitialReferences()
    {
       
        //m_musicManager = GameObject.FindObjectOfType<MusicManager>();
        myTransform = GetComponent<Transform>();
        gameOver = false;
        dontMove = true;
        // isRight = true;
        ballRigidbody = GetComponent<Rigidbody>();
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }



    Rigidbody myBody;
    Animator myAnim;
    Transform myTransform;
    public GameObject collectableEffect;
    public AudioClip noo;
    public AudioClip ahhh;
    public AudioClip sound;


    public float runSpeedAddition;
    public float animAddition;
    public float groundCheckDistance = 1f;
    public float runSpeed;
    public float animSpeed;
    public float shoutTime;
    public string groundTag;
    public bool grounded;
    public bool isLeft;
    public bool isRight;
    public bool idle;

	void Start () {
        SetInit();
	}
	

	void Update () {

        animSpeed = myAnim.speed;
        
       StartCoroutine(CheckIfGameOver());
        CheckForInput();
	}

    public void IncreaseSpeeds()
    {
        InvokeRepeating("IncreaseAnimSpeed", 3, 3);
        InvokeRepeating("IncreaseRunSpeed", 3, 2);
    }

    public void StopIncreasingSpeed()
    {
        CancelInvoke("IncreaseAnimSpeed");
        CancelInvoke("IncreaseRunSpeed");
    }

    void IncreaseAnimSpeed()
    {
        if(myAnim.speed == 3)
        {
            return;
        }
        myAnim.speed += runSpeedAddition * Time.fixedDeltaTime;
        Mathf.Clamp(myAnim.speed, .5f, 3);
    }

    void IncreaseRunSpeed()
    {
        if(runSpeed == 30)
        {
            return;
        }
        runSpeed += animAddition * Time.fixedDeltaTime;
        Mathf.Clamp(runSpeed, 10, 30);
    }
   
    public void MoveLeft()
    {
        idle = false;
        isRight = false;
        isLeft = true;
    }
    public void MoveRight()
    {
        idle = false;
        isLeft = false;
        isRight = true;
    }


    void CheckForInput()
    {
               
        float i = Input.GetAxisRaw("Horizontal");



        if (i < 0)
        {
            idle = false;
            isRight = false;
            isLeft = true;
        }
        else if (i > 0)
        {
            idle = false;
            isLeft = false;
            isRight = true;
        }

        
        if (isLeft)
        {
            myBody.velocity = new Vector3(0, 0, 1 * runSpeed);
            myTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if(isRight)
        {
            myBody.velocity = new Vector3(1 * runSpeed, 0, 0);
            myTransform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }

        if(idle)
        {
            myAnim.Play("idle");
            myBody.velocity = new Vector3(0, 0, 0);
        }
        else if(!idle)
        {
            GameManager.instance.StartGame();
            myAnim.SetTrigger("Run");
        }
        

    }


    IEnumerator CheckIfGameOver()
    {
        grounded = Physics.Raycast(myTransform.position, Vector3.down, groundCheckDistance);

        if(!grounded)
        {

            MusicManager.instance.PLaySound(sound, .2f);
            CameraFolllow.instance.follow = false;
            myAnim.Play("idle");
            myBody.AddForce(new Vector3(0, -7, 0), ForceMode.Impulse);

           
            yield return new WaitForSeconds(shoutTime);
            GameManager.instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectable"))
        {
            GameObject part = Instantiate(collectableEffect, other.gameObject.transform.position,
                Quaternion.identity) as GameObject;
            Destroy(part, 1f);
        }
    }



    void SetInit()
    {
        int r = Random.Range(0, 10);
        if (r < 5)
        {
            sound = ahhh;
        }
        else if (r > 5)
        {
            sound = noo;
        }

        idle = true;
        myBody = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
    }
}

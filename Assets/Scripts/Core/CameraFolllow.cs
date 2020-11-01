using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFolllow : MonoBehaviour 
{
    public static CameraFolllow instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject target;
    private Vector3 offset;
    public float lerpRate;
    public bool follow;

	void Start () 
	{
        SetInitialReferences();
	}
	
	
	void FixedUpdate () 
	{
        Follow();
    }

    void Follow()
    {
        if(follow == false)
        {
            return;
        }
        if (GameManager.instance.gameOver)
        {
            return;
        }

        Vector3 pos = transform.position;
        Vector3 targetPos = target.transform.position - offset;
        pos = Vector3.Lerp(pos, targetPos, lerpRate * Time.deltaTime);
        transform.position = pos;
    }
	
	void SetInitialReferences()
    {
        follow = true;
        offset = target.transform.position - transform.position;
	}
}

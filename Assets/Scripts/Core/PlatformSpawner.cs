using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformSpawner : MonoBehaviour 
{

    public static PlatformSpawner instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public int numberOfPlatformsToSpawn = 100;
    public GameObject diamond1;
    public GameObject diamond2;
    public GameObject platform;
    private GameObject collector;
    Vector3 lastPos;
    float size;
    public float diamondDistance;

   // public int stopInterval = 5;

    public int spawnInterval = 1;

    int i = 10;
	
	void Start () 
	{
        SetInitialReferences();
        
       for(i = 0; i < 110; i++)
       {
            SpawnPlatforms();
           //StartCoroutine(SpawnPlatforms());
       }
       
	}
    public void Caller()
    {
        StartCoroutine(StartPLatformSpawn());
    }

    public IEnumerator StartPLatformSpawn()
    {
       
        yield return new WaitForSeconds(spawnInterval);

        //InvokeRepeating("SpawnPlatforms", 0, 0);

        //StartCoroutine(SpawnPlatforms());

        //yield return new WaitForSeconds(stopInterval);
        //StopCoroutine(SpawnPlatforms());


        for (i = 0; i < numberOfPlatformsToSpawn; i++)
        {
            SpawnPlatforms();
            //StartCoroutine(SpawnPlatforms());
        }


        //CancelInvoke("SpawnPlatforms");

       // Caller();
        StartCoroutine(StartPLatformSpawn());
        
    }

    public void StopSpawn()
    {
        //CancelInvoke("SpawnPlatforms");
        //StopCoroutine(SpawnPlatforms());
        StopCoroutine(StartPLatformSpawn());
    }


    void Update () 
	{
		if(GameManager.instance.gameOver)
        {
            StopSpawn();
        }
	}

   
    void SpawnPlatforms()
    {
        //yield return new WaitForSeconds(0);
        int rand = Random.Range(0, 100);
        if (rand < 50)
        {
            SpawnX();
        }
        
        else if (rand > 50)
        {
            SpawnZ();
        }
        
    }


    void SpawnX()
    {
        Vector3 pos = lastPos;
        pos.x += size;
        lastPos = pos;

       
        GameObject go = Instantiate(platform, pos, Quaternion.identity) as GameObject;

        Debug.Log(go.name);

        int rand = Random.Range(0, 10);
        if(rand == 0)
        {
            Instantiate(diamond1, new Vector3(pos.x, pos.y + diamondDistance, pos.z),
                diamond1.transform.rotation);
        }

    }
    
    void SpawnZ()
    {
       
        Vector3 pos = lastPos;
        pos.z += size;
        lastPos = pos;
        
        GameObject go = Instantiate(platform, pos, Quaternion.identity) as GameObject;
      

        int rand = Random.Range(0, 10);
        if (rand == 0)
        {
            Instantiate(diamond2, new Vector3(pos.x, pos.y + diamondDistance, pos.z),
                diamond2.transform.rotation);
        }

    }

    void SetInitialReferences()
	{
        lastPos = platform.transform.position;
        size = platform.transform.localScale.x;
	}
}

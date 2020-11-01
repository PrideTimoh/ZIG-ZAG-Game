using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreasureChecker : MonoBehaviour 
{
    private Rigidbody parentRigidbody;
    public float timeTillFall = 0.2f;

	void Start () 
	{
        SetInitialReferences();
	}
	
	
	


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            Invoke("FallDown", timeTillFall);
        }
    }

    void FallDown()
    {
        parentRigidbody.useGravity = true;
        parentRigidbody.isKinematic = false;

        Destroy(transform.parent.gameObject, 2f);
    }

    void SetInitialReferences()
	{
        parentRigidbody = GetComponentInParent<Rigidbody>();
	}
}

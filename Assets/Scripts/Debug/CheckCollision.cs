using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {

	public Character_Move associateScript ;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision)
	{
		if(collision.transform.CompareTag("ground") && associateScript.ReturnIsGrab())
		{	
			associateScript.ReleaseObject() ;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calling_Button : MonoBehaviour {

	public Elevator associateScript ;
	public int stage ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Calling()
	{
		associateScript.CallingElevator(stage) ;
	}
}

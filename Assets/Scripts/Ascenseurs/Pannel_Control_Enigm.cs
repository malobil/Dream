using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pannel_Control_Enigm : MonoBehaviour {

	public int leadNumberNeeded ;
	public Light elevatorLight ;
	public Elevator associateScript ;

	private int leadNumberPut = 0 ;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(leadNumberPut == leadNumberNeeded)
		{
			associateScript.ChangeElevatorStatutOn() ;
		}
	}

	public void Put()
	{
		leadNumberPut++ ;
	}
}

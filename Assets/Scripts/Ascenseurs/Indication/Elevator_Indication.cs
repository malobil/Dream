using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Indication : MonoBehaviour {

	public Elevator associateScript ;

	private Light lightComponent ;
	// Use this for initialization
	void Start () 
	{
		lightComponent = GetComponent<Light>() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!associateScript.ReturnIsOperational())
		{
			lightComponent.color = Color.red ;
		}
		else if(associateScript.ReturnIsOperational())
		{
			lightComponent.color = Color.green ;
		}
	}

}

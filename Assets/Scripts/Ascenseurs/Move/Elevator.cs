using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	public Transform elevator ;
	public Transform[] stages ;
	public int begeningStage ;
	public float speed ;
	public bool isOperational = true ;
	public Light elevatorLight ;

	private int currentStage ;
	private bool isMoving ;

	// Use this for initialization
	void Start () 
	{
		currentStage = begeningStage ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isMoving)
		{
			GoToStage() ;
		}

		if(isMoving && elevator.position == stages[currentStage].position)
		{
			isMoving = false ;
		}

		if(isOperational)
		{
			elevatorLight.color = Color.green ;
		}
		else if(!isOperational)
		{
			elevatorLight.color = Color.red ;
		}
	}

	void GoToStage()
	{
		elevator.position = Vector3.MoveTowards(elevator.position, stages[currentStage].position, speed * Time.deltaTime) ;
		//Debug.Log("Hey") ;
	}

	public void GoUp()
	{
		if(currentStage != stages.Length -1 && isOperational)
		{
			currentStage++ ;
			isMoving = true ;
			//Debug.Log(currentStage) ;
		}
	}

	public void GoDown()
	{
		if(currentStage != 0 && isOperational)
		{
			currentStage--;
			isMoving = true ;
		}
	}

	public void CallingElevator(int stage)
	{	
		if(isOperational)
		{
			currentStage = stage ;
			if(elevator.position != stages[currentStage].position)
			{
				isMoving = true ;
			}
		}
	}

	public bool ReturnIsOperational()
	{
		return isOperational ;
	}

	public void ChangeElevatorStatutOn()
	{
		isOperational = true ;
	}

	public void ChangeElevatorStatutOff()
	{
		isOperational = false ;
	}
}

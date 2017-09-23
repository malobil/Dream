using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascenseur_Button : MonoBehaviour {

	public Elevator ascenseurAssociateScript ;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void UpButton()
	{
		ascenseurAssociateScript.GoUp() ;
	}

	public void DownButton()
	{
		ascenseurAssociateScript.GoDown() ;
	}
}

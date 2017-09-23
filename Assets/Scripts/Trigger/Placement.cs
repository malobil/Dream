using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour {

	public GameObject objectToActive ;
	public GameObject objectToSet ;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == objectToSet)
		{
			objectToSet.SetActive(false) ;
			objectToActive.SetActive(true) ;
		}
	}
}

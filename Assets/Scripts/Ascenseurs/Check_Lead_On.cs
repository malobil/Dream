using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Lead_On : MonoBehaviour {	

	public Pannel_Control_Enigm associateScript ;
	public GameObject lead ;
	public GameObject associateLead ;

	private Character_Move associateScriptPlayer ;
	private bool isDo = false ;

	public Light lightIndicator ;
	

	// Use this for initialization
	void Start () 
	{
		associateScriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Move>() ;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == associateLead)
		{
			GoodOnePut() ;
		}
		else if(other.gameObject != associateLead && !isDo)
		{
			lightIndicator.color = Color.red ;
			StartCoroutine(Feedback()) ;
		}
	}

	IEnumerator Feedback()
	{
		yield return new WaitForSeconds(1f) ;
		lightIndicator.color = Color.yellow ;
		StopCoroutine(Feedback()) ;
	}

	void GoodOnePut()
	{
		associateScript.Put() ;
		lightIndicator.color = Color.green ;
		associateScriptPlayer.ReleaseObject() ;
		associateLead.gameObject.SetActive(false) ;
		lead.SetActive(true) ;
		isDo = true ;
		StopCoroutine(Feedback()) ;
		//Destroy(this) ;
	}

	public bool ReturnIsDo()
	{
		return isDo ;
	}

	public void TurnIsDo(bool stateSave)
	{
		if(stateSave)
		{
			GoodOnePut() ;
		}
	}
}

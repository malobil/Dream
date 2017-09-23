using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class UIManager : MonoBehaviour {

	private static UIManager instance ;
	public static UIManager Instance () 
	{
		return instance;
	}

	public GameObject pauseUI ;
	public Image reticule ;

	private Color32 reticuleColor ;

	void Awake ()
	{
		if (instance != null)
		{
			Destroy (gameObject);
		}
		else 
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () 
	{
		Cursor.visible = false ;
		reticuleColor = reticule.color ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void PauseUI()
	{
		Cursor.visible = true ;
		pauseUI.SetActive(true) ;
	}

	public void UnPauseUI()
	{
		Cursor.visible = false ;
		pauseUI.SetActive(false) ;
	}

	public void ChangeCursorColor(bool isTouch)
	{
		if(isTouch)
		{
			reticule.color = new Color32(30,255,0,255);	
		}
		else if(!isTouch)
		{
			reticule.color = reticuleColor ;
		}
		
	}
}

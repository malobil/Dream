using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System ;
using System.Runtime.Serialization.Formatters.Binary ;
using System.IO ;
using UnityEngine.SceneManagement ;

public class GameManager : MonoBehaviour {

	private static GameManager instance ;
	public static GameManager Instance () 
	{
		return instance;
	}

	private GameObject player ;
	private bool isPaused = false ;

	public GameObject[] elevator ;
	public GameObject[] triggerEnigmState ;

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
		player = GameObject.FindGameObjectWithTag("Player") ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Pause"))
		{
			if(isPaused)
			{
				UnPauseGame() ;
			}
			else if(!isPaused)
			{
				PauseGame() ;
			}
		}

		if(Input.GetKeyDown("n"))
		{
			Save() ;
		}
		if(Input.GetKeyDown("b"))
		{
			Load() ;
		}
		if(Input.GetKeyDown("v"))
		{
			DeleteSave() ;
		}


	}

	public void PauseGame()
	{
		Time.timeScale = 0 ;
		isPaused = true ;
		UIManager.Instance().PauseUI() ;
	}

	public void UnPauseGame()
	{
		Time.timeScale = 1 ;
		isPaused = false ;
		UIManager.Instance().UnPauseUI() ;
	}

	public bool ReturnPaused()
	{
		return isPaused ;
	}

	public void QuitGame()
	{
		Application.Quit() ;
	}

	public void DeleteSave()
	{
		File.Delete(Application.persistentDataPath + "/playerInfo.dat") ;
		Debug.Log("delete save file") ;
	}
	
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter() ;
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat") ;

		PlayerData data = new PlayerData() ;

		//player pos save, rotation and scene
		data.playerTransformZ = player.transform.position.z ;
		data.playerTransformY = player.transform.position.y ;
		data.playerTransformX = player.transform.position.x ;
		data.playerRotation = player.transform.rotation.eulerAngles.y ;
		//elevator statu save
		for(int i=0 ; i < elevator.Length ; i++)
		{
			data.elevatorStatut[i] = elevator[i].GetComponent<Elevator>().ReturnIsOperational() ;
		}
		//elevator panel save
		for(int j=0 ; j < triggerEnigmState.Length ; j++)
		{
			data.triggerEnigmSave[j] = triggerEnigmState[j].GetComponent<Check_Lead_On>().ReturnIsDo() ;
		}

		Debug.Log("save") ;
		bf.Serialize(file,data) ;
		file.Close() ;
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{

		BinaryFormatter bf = new BinaryFormatter() ;
		FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open) ;

		PlayerData data = (PlayerData)bf.Deserialize(file) ;
		
		//Player position, rotation and scene load
		player.transform.position = new Vector3(data.playerTransformX, data.playerTransformY,data.playerTransformZ) ;
		player.transform.rotation = Quaternion.Euler(0f,data.playerRotation,0f) ;
		//elevator state load
		for(int i=0 ; i < elevator.Length ; i++)
		{
			elevator[i].GetComponent<Elevator>().isOperational = data.elevatorStatut[i] ;
		}

		//elevator panel load
		for(int j=0 ; j < triggerEnigmState.Length ; j++)
		{
			triggerEnigmState[j].GetComponent<Check_Lead_On>().TurnIsDo(data.triggerEnigmSave[j]) ;
		}

		Debug.Log("Load") ;
		file.Close() ;

		}
		else if(!File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			Debug.Log("File do not exist please save before") ;
		}
	}
}

[Serializable]
class PlayerData
{
	public float playerTransformX,playerTransformZ,playerTransformY,playerRotation ;
	public bool[] elevatorStatut = new bool[100];
	public bool[] triggerEnigmSave = new bool[100] ;
	public string currentScene ;
}

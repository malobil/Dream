using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour {

	public float speed = 5 ;
	public float runSpeed = 10 ;
	public float cameraSensibility = 5 ;
	public float jumpForce = 5 ;
	public float interactionRange = 10 ;

	public GameObject camera ;


	private CharacterController cC ;

	private float horizontalMove ;
	private float verticalMove ;

	private float rotX ;
	private float rotY ;


	private float vertVelocity ;
	private bool hasJumped = false ; 
	private float currentSpeed ;

	private Transform mainCameraTransform ;

	private Rigidbody grabObjectRigidbody ;
	private bool isGrab = false ;

	private bool isClimbing = false ;

	// Use this for initialization
	void Start () 
	{
		cC = GetComponent<CharacterController>() ;
		mainCameraTransform = camera.transform ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!GameManager.Instance().ReturnPaused())
		{
			//Move
			Movement() ;

			if(!isClimbing)
			{
				//Apply gravity
				ApplyGravity() ;
			}

			//Jump condition
			if(Input.GetButtonDown("Jump"))
			{
				Jump() ;
			}

			//Run or walk condition
			if(Input.GetButton("Run"))
			{
				Run() ;
			}
			else if(!Input.GetButton("Run"))
			{
				Walk() ;
			}
		}
	}

	void FixedUpdate()
	{
		if(!GameManager.Instance().ReturnPaused())
		{
			RaycastHit hit ;

			//Debug.DrawRay(mainCameraTransform.position,mainCameraTransform.forward,Color.green) ;

			if(Physics.Raycast(mainCameraTransform.position,mainCameraTransform.forward, out hit,interactionRange))
			{
				if(Input.GetButtonDown("Fire1"))
				{
					if(hit.collider.CompareTag("upButton"))
					{
						hit.collider.GetComponent<Ascenseur_Button>().UpButton() ;
					}

					if(hit.collider.CompareTag("downButton"))
					{
						hit.collider.GetComponent<Ascenseur_Button>().DownButton() ;
					}

					if(hit.collider.CompareTag("callingButton"))
					{
						hit.collider.GetComponent<Calling_Button>().Calling() ;
					}

				}

				if(hit.collider.CompareTag("physicObject") && !isGrab && Input.GetButtonDown("Fire1"))
				{
					GrabObject(hit);
				}
				else if(isGrab && !Input.GetButton("Fire1"))
				{
					ReleaseObject();
				}

				if(hit.collider.gameObject.layer == LayerMask.NameToLayer("interactionLayer"))
				{
					UIManager.Instance().ChangeCursorColor(true) ;
				}
				else
				{
					UIManager.Instance().ChangeCursorColor(false) ;
				}
			}
		}
	}

	void Movement()
	{
		
			horizontalMove = Input.GetAxis("Horizontal") * currentSpeed ;
		if(!isClimbing)
		{
			verticalMove = Input.GetAxis("Vertical") * currentSpeed ;
		}
		else if(isClimbing)
		{
			vertVelocity = Input.GetAxis("Vertical") * currentSpeed ;
		}

		rotX = Input.GetAxis("MouseX") * cameraSensibility ; // Get the mouse axis x
		rotY -= Input.GetAxis("MouseY") * cameraSensibility ; // get the mouse axis y
		rotY = Mathf.Clamp(rotY,-60f,60f) ; // lock the camera to not do spin
		transform.Rotate(0,rotX,0) ; // rotate the player with mouse
		camera.transform.localRotation = Quaternion.Euler(rotY,0,0) ; // rotate the camera y with mouse

		Vector3 movement = new Vector3(horizontalMove,vertVelocity,verticalMove) ; // Vector du déplacement 
		movement = transform.rotation * movement ; 

		cC.Move(movement * Time.deltaTime) ;
	}

	void Jump()
	{
		hasJumped = true ;
	}

	void ApplyGravity()
	{
		if(cC.isGrounded)
		{
			if(!hasJumped)
			{
				vertVelocity = Physics.gravity.y ;
			}
			else if(hasJumped)
			{
				vertVelocity = jumpForce ;
			}
		}
		else if(!cC.isGrounded)
		{
			vertVelocity += Physics.gravity.y * Time.deltaTime ;
			vertVelocity = Mathf.Clamp(vertVelocity,-50f,jumpForce) ;
			hasJumped = false ;
		}
	}

	void Run()
	{
		currentSpeed = runSpeed ;
	}

	void Walk()
	{
		currentSpeed = speed ;
	}

	void GrabObject(RaycastHit objectHit)
	{
		isGrab = true ;
		grabObjectRigidbody = objectHit.collider.GetComponent<Rigidbody>() ;
		grabObjectRigidbody.useGravity = false ;
		grabObjectRigidbody.isKinematic = true ;
		grabObjectRigidbody.transform.parent = camera.transform ;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Ladder"))
		{
			isClimbing = true ;
			horizontalMove = 0 ;
			verticalMove = 0 ;
			Debug.Log("isClimbing : " + isClimbing) ;
		}

		if(other.CompareTag("GroundLadder"))
		{
			isClimbing = false ;
			Debug.Log("Touch down") ;
		}
		
	}

	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Ladder"))
		{
			isClimbing = false ;
		}

		Debug.Log("isClimbing : " + isClimbing) ;
	}



	public void ReleaseObject()
	{
		if(grabObjectRigidbody !=null)
		{
			grabObjectRigidbody.useGravity = true ;
			grabObjectRigidbody.isKinematic = false ;
			grabObjectRigidbody.transform.parent = null ;
			grabObjectRigidbody = null ;
		}
			isGrab = false ;
	}

	public bool ReturnIsGrab()
	{
		return isGrab ;
	}
}

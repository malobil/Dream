using UnityEngine;
using System.Collections;


public class Gizmos_Stages : MonoBehaviour 
{
	public Transform elevatorSize ;

	private Mesh mesh ;

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, elevatorSize.localScale);
    }
}
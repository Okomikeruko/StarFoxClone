using UnityEngine;
using System.Collections;

public class ShipMotionControl : MonoBehaviour {

	[SerializeField]
	private float Speed, Pitch = 1, Roll = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = transform.forward * Speed;
		rigidbody.rotation *= Quaternion.Euler(Input.GetAxis("Vertical") * Pitch, 0, -Input.GetAxis("Horizontal") * Roll);
//		rigidbody.rotation *= Quaternion.Euler(-Input.acceleration.z * Pitch, 0, -Input.acceleration.x * Roll);


		if( Input.GetMouseButtonDown (0))
		{
			// Fire
		}
	}


}

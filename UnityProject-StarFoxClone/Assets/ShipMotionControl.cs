using UnityEngine;
using System.Collections;

public class ShipMotionControl : MonoBehaviour {

	[SerializeField]
	private float Speed, acceleration, Pitch = 1, Roll = 1;
	[SerializeField]
	private ParticleSystem Destruction;
	[SerializeField]
	private GameObject Addons, PointLight;


	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = transform.forward * Speed;
//		rigidbody.rotation *= Quaternion.Euler(Input.GetAxis("Vertical") * Pitch, 0, -Input.GetAxis("Horizontal") * Roll);
		rigidbody.rotation *= Quaternion.Euler(-Input.acceleration.z * Pitch, 0, -Input.acceleration.x * Roll);


		Speed += acceleration * Time.deltaTime;

		if( Input.GetMouseButtonDown (0))
		{
			// Fire
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.relativeVelocity.magnitude > 15)
		{
			StartCoroutine(SelfDestruct());
		}
	}


	IEnumerator SelfDestruct()
	{
		Speed = acceleration = 0;
		Addons.SetActive(false);
		PointLight.SetActive(true);
		GetComponent <MeshRenderer>().enabled = false;
		Destruction.Play();
		yield return new WaitForSeconds(Destruction.duration * 16);
		Application.LoadLevel (Application.loadedLevel);
	}
}

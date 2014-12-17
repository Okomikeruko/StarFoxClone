using UnityEngine;
using System.Collections;

public class ShipMotionControl : MonoBehaviour {

	[SerializeField]
	private float Speed, acceleration, Pitch = 1, Roll = 1;
	[SerializeField]
	private ParticleSystem Destruction;
	[SerializeField]
	private GameObject Addons, PointLight, blasterFire;

	private MasterGUI masterGUI;
	private Vector3 pos;
	private bool alive;
	private float x, z;

	void Start(){
		alive = true;
		masterGUI = GameObject.Find ("MasterGUI").GetComponent<MasterGUI>();
		pos = transform.position;
	#if UNITY_IPHONE
		x = Input.acceleration.x;
		z = Input.acceleration.z;
	#endif
	}

	void Update () {
		masterGUI.distanceCovered += Vector3.Distance(pos, transform.position);
		pos = transform.position;
		rigidbody.velocity = transform.forward * Speed;

	#if UNITY_EDITOR
		rigidbody.rotation *= Quaternion.Euler(Input.GetAxis("Vertical") * Pitch, 0, -Input.GetAxis("Horizontal") * Roll);
	#endif

	#if UNITY_IPHONE
		rigidbody.rotation *= Quaternion.Euler(
			(z - Input.acceleration.z) * Pitch,
			0,
			(x - Input.acceleration.x) * Roll);
	#endif	
		Speed += acceleration * Time.deltaTime;

		if( Input.GetMouseButtonDown (0))
		{
			Fire();
		}
	}

	void OnCollisionEnter(Collision col)
	{
		GameObject.Find ("Shield").GetComponent<Sheild>().Hit();

		if(col.relativeVelocity.magnitude > 15)
		{
			StartCoroutine(SelfDestruct());
		}
	}

	void Fire()
	{
		GameObject fire = Instantiate(blasterFire, transform.position + (transform.forward * 30), transform.rotation) as GameObject;
		fire.rigidbody.velocity = transform.forward * Speed * 3;
	}

	IEnumerator SelfDestruct()
	{
		Speed = acceleration = 0;
		rigidbody.angularVelocity = Vector3.zero;
		GameObject.Find ("Main Camera").GetComponent<CameraFollow>().enabled = false;
		masterGUI.health = 0;
		Addons.SetActive(false);
		PointLight.SetActive(true);
		GetComponent <MeshRenderer>().enabled = false;
		Destruction.Play();
		yield return new WaitForSeconds(Destruction.duration * 16);
		Application.LoadLevel (Application.loadedLevel);
	}
}

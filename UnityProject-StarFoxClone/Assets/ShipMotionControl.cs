using UnityEngine;
using System.Collections;

public class ShipMotionControl : MonoBehaviour {
	
	[SerializeField]
	private float Speed, acceleration, Pitch = 1, Roll = 1, Yaw = 1, maxHealth = 150;
	[SerializeField]
	private ParticleSystem Destruction;
	[SerializeField]
	public GameObject Addons, PointLight, blasterFire;
	
	private MasterGUI masterGUI;
	private Vector3 pos;
	private float x, y, z;
	private Quaternion offset;
	
	public float currentHealth { get; set; }
	
	void Start() {
		currentHealth = maxHealth;
		masterGUI = GameObject.Find ("MasterGUI").GetComponent<MasterGUI>();
		pos = transform.position;
		#if UNITY_IPHONE
		Input.gyro.enabled = true;
		offset = Input.gyro.attitude;
		#endif
	}
	
	void Update () {
		if(currentHealth > maxHealth){
			currentHealth = maxHealth;
		}
		masterGUI.distanceCovered += Vector3.Distance(pos, transform.position);
		pos = transform.position;
		rigidbody.velocity = transform.forward * Speed;
		
		#if UNITY_EDITOR
		rigidbody.rotation *= Quaternion.Euler(Input.GetAxis("Vertical") * Pitch, 0, -Input.GetAxis("Horizontal") * Roll);
		#endif
		
		#if UNITY_IPHONE
		x = Mathf.DeltaAngle(offset.eulerAngles.y, Input.gyro.attitude.eulerAngles.y) / 36; 
		y = Mathf.DeltaAngle(offset.eulerAngles.x, Input.gyro.attitude.eulerAngles.x) / 36;
		z = Mathf.DeltaAngle(offset.eulerAngles.z, Input.gyro.attitude.eulerAngles.z) / 36;
		
		rigidbody.rotation *= Quaternion.Euler(x * Pitch, y * Yaw, z * Roll);
		
		#endif	
		Speed += acceleration * Time.deltaTime;
		
		if( Input.GetMouseButtonDown (0)){
			Fire();
		}
		
		if(currentHealth <= 0){
			StartCoroutine(SelfDestruct());
		}
		masterGUI.health = currentHealth/maxHealth;
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.transform.root.gameObject.tag == "Damage"){
			currentHealth -= col.transform.root.gameObject.GetComponent<CollisionDamage>().collisionDamage;
			if (col.transform.root.gameObject.GetComponent<health>() != null) {
				col.transform.root.gameObject.GetComponent<health>().currentHealth = 0;
			}
		}
		
		if (currentHealth > 0) {
			GameObject.Find ("Shield").GetComponent<Sheild>().Hit();
		}
	}
	
	void Fire() {
		GameObject fire = Instantiate(blasterFire, transform.position + (transform.forward * 30), transform.rotation) as GameObject;
		fire.rigidbody.velocity = transform.forward * Speed * 3;
	}
	
	IEnumerator SelfDestruct() {
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
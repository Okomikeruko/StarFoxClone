using UnityEngine;
using System.Collections;

public class MissileMotion : MonoBehaviour {

	public float speed, distance;
	private Transform target;

	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		transform.LookAt(target.position);
	}
	void Update () {
		if(this.gameObject.GetComponent<health>().currentHealth > 0){
			if (Vector3.Distance(transform.position, target.position) > distance){
				transform.LookAt(Vector3.Lerp (transform.position, target.position, Time.deltaTime));
			}
			transform.position += transform.forward * speed * Time.deltaTime;

			if( (target.position.z - transform.position.z) > 2){
				this.gameObject.GetComponent<health>().currentHealth = 0;
			}
		}
	}
}

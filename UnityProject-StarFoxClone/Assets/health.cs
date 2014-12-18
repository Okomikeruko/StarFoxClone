using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {

	public float maxHealth;
	public ParticleSystem destruction;

	private bool dead = false;
	public float currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}

	void Update()
	{
		if (currentHealth <= 0 && !dead)
		{
			StartCoroutine(destroyMe ());
			dead = true;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Weapon")
		{
			currentHealth -= col.gameObject.GetComponent<Weapon>().weaponDamage;
			StartCoroutine (hitColor());
		}
		if (col.gameObject.tag == "Damage")
		{
			currentHealth = 0;
		}
	}

	IEnumerator destroyMe(){
		foreach(MeshRenderer r in GetComponentsInChildren<MeshRenderer>()){
			r.enabled = false;
		}
		collider.enabled = false;
		destruction.Play();
		yield return new WaitForSeconds(2);
		GameObject.Destroy(this.gameObject);
	}

	IEnumerator hitColor(){
		Renderer r = GetComponentInChildren<Renderer>();
		r.material.color = Color.red;
		yield return new WaitForSeconds(0.2F);
		r.material.color = Color.white;
	}
}

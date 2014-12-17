using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float weaponDamage;

	void OnCollisionEnter(Collision col)
	{
		GameObject.Destroy (this.gameObject);
	}
}

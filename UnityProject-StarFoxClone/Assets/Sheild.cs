using UnityEngine;
using System.Collections;

public class Sheild : MonoBehaviour {

	private float hit = 0;
	public float hitStart, hitDecay;

	void Start() {
		hit = 0;
	}

	void Update () {
		if(hit > 0)
		{
			hit -= hitDecay;
		}
		renderer.material.SetFloat("_Cutoff", Mathf.Lerp (1, 0, hit));

		if(Input.GetKeyDown (KeyCode.Space)) {
			Hit ();
		}
	}

	public void Hit()
	{
		hit = hitStart;
	}
}

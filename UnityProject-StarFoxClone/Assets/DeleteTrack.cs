using UnityEngine;
using System.Collections;

public class DeleteTrack : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.name == "SpawnNode"){
			GameObject.Destroy(col.transform.root.gameObject);
		}
	}
}

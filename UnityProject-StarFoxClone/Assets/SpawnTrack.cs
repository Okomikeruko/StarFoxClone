using UnityEngine;
using System.Collections;

public class SpawnTrack : MonoBehaviour {

	public GameObject StraightTrack, ForkedTrack;
	private GameObject trackChoice;

	void OnTriggerEnter(Collider col){
		if(col.name == "SpawnNode"){
			SpawnNode node = col.GetComponent<SpawnNode>();
			if(!node.triggered){
				if(Random.Range (0, 15) > 12){
					trackChoice = ForkedTrack;
				}else{
					trackChoice = StraightTrack;
				}
				GameObject track = Instantiate(trackChoice, col.transform.position, col.transform.rotation) as GameObject;
				track.transform.rotation *= Quaternion.Euler(Random.Range(-12,12),
				                                             Random.Range(-12,12),
				                                             90 * Mathf.RoundToInt(Random.Range (0,3)));
				node.triggered = true;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	[SerializeField] 
	public PickUpType pickUpType;
	public int Amount;
	public float spin;
	private MasterGUI masterGUI;
	private ShipMotionControl ship; 

	void Start() {
		masterGUI = GameObject.Find ("MasterGUI").GetComponent<MasterGUI>();
		ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipMotionControl>();
	}

	void Update() {
		transform.rotation *= Quaternion.Euler(spin * Time.deltaTime, 0, 0);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player"){
			switch (pickUpType)
			{
			case PickUpType.health:
				ship.currentHealth += Amount;
				break;
			case PickUpType.money: 
				masterGUI.money += Amount;
				break;
			default:
				break;
			}
			GameObject.Destroy(this.gameObject); 
		}
	}
}

public enum PickUpType{
	money,
	weapon,
	health
}
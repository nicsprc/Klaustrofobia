using UnityEngine;
using System.Collections;

public class BossSensor : MonoBehaviour {

	public bool sensedBullet;
	private GameObject bullet;

	// Use this for initialization
	void Start () {
		sensedBullet = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D col){
		//layer 10 = PlayerAmmo
		if(col.gameObject.layer==10){
			sensedBullet = true;
			bullet = col.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		//layer 10 = PlayerAmmo
		if(col.gameObject.layer==10){
			sensedBullet = false;
		}
	}

	public GameObject getBullet(){
		return bullet;
	}
}

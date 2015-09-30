using UnityEngine;
using System.Collections;

public class TeleportZone : MonoBehaviour {

	[SerializeField] Transform outZone;
	private AudioSource audioSou;

	void Start(){
		audioSou = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		//layer 10 = PlayerAmmo
		if(col.gameObject.layer==11){
			audioSou.Play();
			//print ("true");
			Vector2 newPos = col.transform.position;
			newPos = outZone.position;
			col.transform.position = newPos;
		}
	}
}

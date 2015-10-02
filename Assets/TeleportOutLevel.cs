using UnityEngine;
using System.Collections;

public class TeleportOutLevel : MonoBehaviour {

	private AudioSource audioSou;
	
	void Start(){
		audioSou = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		//layer 10 = PlayerAmmo
		if(col.gameObject.layer==11){
			audioSou.Play();
			//print ("true");
			Application.LoadLevel("MainMenu");
		}
	}
}

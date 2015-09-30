using UnityEngine;
using System.Collections;

public class Shout : MonoBehaviour {

	public AudioClip shout;
	[SerializeField] CameraMovement camMov;
	private AudioSource audioSou;
	private bool isShouting;
	private float shoutDuration;
	private float timer;

	// Use this for initialization
	void Start () {
		audioSou = GetComponent<AudioSource> ();
		isShouting = false;
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Y)){
			isShouting = true;
			shoutDuration = shout.length;
			audioSou.clip = shout;
			audioSou.Play();
		}	

		//if(!audioSou.isPlaying){
			//isShouting = false;
		//}

		if(isShouting){
			timer += Time.deltaTime;

			camMov.shake = shoutDuration;
			camMov.shakeAmount = (shoutDuration-timer)/shoutDuration;

			if(timer>shoutDuration){
				isShouting = false;
			}
		}
	}
}

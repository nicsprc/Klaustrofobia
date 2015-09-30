using UnityEngine;
using System.Collections;

public class DropSomething : MonoBehaviour {

	public GameObject drop; //pingo, gota
	public float minTimeToDrop;
	public float maxTimeToDrop;
	private float timer;
	private float rand;
	private AudioSource audioSou;
	private float dis;
	private float fixedVol;
	private GameObject player;
	private float disToHear = 10f;

	void Start(){
		audioSou = GetComponent<AudioSource> ();
		fixedVol = audioSou.volume;

		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		dis = Mathf.Sqrt (Mathf.Pow(transform.position.x-player.transform.position.x,2)+Mathf.Pow(transform.position.y-player.transform.position.y,2));

		if (dis < disToHear) {
			audioSou.volume = ((disToHear - dis) / disToHear) * fixedVol;
		} else {
			audioSou.volume = 0.0f;
		}

		rand = minTimeToDrop+maxTimeToDrop*Random.value;
		timer += Time.deltaTime;
		if(timer>rand){
			Instantiate(drop,transform.position,transform.rotation);
			audioSou.Play();
			timer = 0.0f;
		}
	}
}

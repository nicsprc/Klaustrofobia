using UnityEngine;
using System.Collections;

public class StationAction : MonoBehaviour {

	public GameObject StationBullet;
	public float cdTime;
	private float timer;

	// Use this for initialization
	void Start () {
		//cdTime = 1.0f;
		timer = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > cdTime) {
			Instantiate(StationBullet,transform.position, transform.rotation);
			timer = 0f;
		}
	}
}

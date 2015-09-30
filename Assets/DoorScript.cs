using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	//private Rigidbody2D body;

	private float doorTimeOpening;

	private GameObject player;
	private float dis;
	private Vector3 p0;
	private Vector3 pF;
	private Vector3 startPos;

	private float timeStartedLerping;

	private bool isLerpingUp;
	private bool isLerpingDown;

	private bool doorPos;

	public bool isLocked;

	// Use this for initialization
	void Start () {
		//body = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		//p0 = new Vector3 (146f, -3.9f, 1f);
		//pF = new Vector3 (146f, -1.4f, 1f);
		p0 = transform.position;
		pF = transform.position;
		pF.y += 2.5f;

		isLerpingUp = false;
		isLerpingDown = false;

		doorTimeOpening = 0.1f;

	}

	void Update () {
		if (isLocked) {
			if(transform.position.y>p0.y){
				LerpDown ();
				isLerpingUp = false;
			}
		} else {
			dis = Mathf.Abs(transform.position.x - player.transform.position.x);
			if(dis<5.0f){
				LerpUp ();
				isLerpingDown = false;
			}else{
				LerpDown ();
				isLerpingUp = false;
			}
		}
	}

	void LerpUp(){
		isLerpingUp = true;
		timeStartedLerping = Time.time;

		startPos = transform.position;
	}

	void LerpDown(){
		isLerpingDown = true;
		timeStartedLerping = Time.time;
		
		startPos = transform.position;
	}

	void FixedUpdate(){
		if (isLerpingUp) {
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentage = timeSinceStarted / doorTimeOpening;

			transform.position = Vector3.Lerp(startPos,pF,percentage);
			if(percentage>=1.0f){
				isLerpingUp = false;
			}
		}

		if (isLerpingDown) {
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentage = timeSinceStarted / doorTimeOpening;
			
			transform.position = Vector3.Lerp(startPos,p0,percentage);
			if(percentage>=1.0f){
				isLerpingDown = false;
			}
		}
	}
}

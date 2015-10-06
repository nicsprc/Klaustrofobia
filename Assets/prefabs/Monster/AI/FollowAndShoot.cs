using UnityEngine;
using System.Collections;

public class FollowAndShoot : MonoBehaviour {
	
	public AudioClip audioClip;
	private AudioSource audioSou;

	private GameObject player;
	private Vector3 pPos;
	private float dis;
	private Rigidbody2D body;

	public float followDistance;
	public float followSpeed;
	public GameObject projectile;
	public float timeToShoot;
	private float timerShoot;

	private float timeMovingRandom;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		body = this.GetComponent<Rigidbody2D> ();

		audioSou = GetComponent<AudioSource> ();

		timeMovingRandom = 0.0f;
		timerShoot = 0.0f;

		Vector2 zero = body.velocity;
		zero = new Vector2 (0, 0);
		body.velocity = zero;
	}
	
	// Update is called once per frame
	void Update () {
		pPos = player.transform.position;
		dis = Mathf.Sqrt (Mathf.Pow((pPos.x-this.transform.position.x),2)+Mathf.Pow((pPos.y-this.transform.position.y),2));
	
		if (dis < followDistance) {
			follow ();
			shoot();
		} else {
			moveRandom();
		}
	}

	void follow(){
		pPos = player.transform.position;

		Vector3 dir;
		dir = player.transform.position - transform.position;
		dir = dir/Mathf.Sqrt(Mathf.Pow(dir.x,2)+Mathf.Pow(dir.y,2));
		
		Vector2 dir2D = new Vector2 (dir.x, dir.y);

		body.AddForce (followSpeed*dir2D*body.mass, ForceMode2D.Force);
	}

	void moveRandom(){
		Vector2 stahp = body.velocity;

		if ((float.IsNaN (stahp.x)) || (float.IsNaN (stahp.y))) {
			stahp.x = 0f;
			stahp.y = 0f;
		}	

		stahp = stahp / Mathf.Sqrt (Mathf.Pow(stahp.x,2)+Mathf.Pow(stahp.y,2));
		if (!(float.IsNaN (stahp.x)) && !(float.IsNaN (stahp.y))) {
			//Tenho que corrigir isso!
			body.AddForce (new Vector2 (-stahp.x*0.02f, -stahp.y*0.02f), ForceMode2D.Impulse);
		}
		timeMovingRandom += Time.deltaTime;

		if (timeMovingRandom > 5.0f) {
			Vector2 newDir = new Vector2(2*Random.value,2*Random.value);
			newDir.x = 1-newDir.x;
			newDir.y = 1-newDir.y;
			body.AddForce (newDir*body.mass, ForceMode2D.Impulse);
			timeMovingRandom = 0.0f;
		}

	}

	void shoot(){
		timerShoot += Time.deltaTime;
		
		if (timerShoot > timeToShoot) {
			Instantiate(projectile,transform.position, transform.rotation);
			audioSou.PlayOneShot(audioClip);
			timerShoot = 0f;
		}

	}
}

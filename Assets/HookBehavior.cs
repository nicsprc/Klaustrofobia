using UnityEngine;
using System.Collections;

public class HookBehavior : MonoBehaviour {

	public float hookSpeed;
	public AudioClip hookHit;

	private Vector3 shootDir;
	private Rigidbody2D body;
	private DistanceJoint2D joint;
	private GameObject player;
	private DistanceJoint2D playerJoint;
	private AudioSource audioSou;

	// Use this for initialization
	void Start () {
		audioSou = GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		body = GetComponent<Rigidbody2D> ();

		hookSpeed += GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ().hookSpeed;

		transform.parent = player.transform;

		shootDir = Input.mousePosition;
		shootDir = Camera.main.ScreenToWorldPoint(shootDir);
		shootDir = shootDir - transform.position;
		shootDir = shootDir/Mathf.Sqrt(Mathf.Pow(shootDir.x,2)+Mathf.Pow(shootDir.y,2));
		
		Vector2 vec = new Vector2 (shootDir.x, shootDir.y);

		body.AddForce (vec*hookSpeed*body.mass, ForceMode2D.Impulse);
		joint = GetComponent<DistanceJoint2D> ();
		playerJoint = player.GetComponent<DistanceJoint2D> ();
	}

	void Update(){
		//print (playerJoint.distance);
	}

	void OnCollisionEnter2D(Collision2D col){
		audioSou.clip = hookHit;
		audioSou.Play();

		player.GetComponent<Player1Skills> ().setHookFlyingTime (1000.0f);

		body.isKinematic = true;
		body.transform.parent = col.transform;
		joint.enabled = true;
		joint.anchor = new Vector2 (0, 0);
		joint.connectedAnchor = new Vector2 (0, 0);
					
		playerJoint.connectedBody = body;
		playerJoint.enabled = true;

		float dis = Mathf.Sqrt (Mathf.Pow(transform.position.x-player.transform.position.x,2)+Mathf.Pow(transform.position.y-player.transform.position.y,2));

		playerJoint.connectedAnchor = new Vector2 (0, 0);
		playerJoint.anchor = new Vector2 (0.0f, 0);
		playerJoint.distance = dis;
	}
}

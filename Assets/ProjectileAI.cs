using UnityEngine;
using System.Collections;

public class ProjectileAI : MonoBehaviour {
	
	public float inicialBulletSpeed;
	public float bulletDelayTime;
	public float finalBulletSpeed;
	public float timeToSelfDestruct;


	private Vector3 shootDir;
	private GameObject player;	
	private float timer;
	private Rigidbody2D body;
	
	// Use this for initialization
	void Start () {
		timer = 0f;
		
		player = GameObject.FindGameObjectWithTag ("Player");
		body = GetComponent<Rigidbody2D> ();
		
		shootDir = player.transform.position - transform.position;
		shootDir = shootDir/Mathf.Sqrt(Mathf.Pow(shootDir.x,2)+Mathf.Pow(shootDir.y,2));
		
		Vector2 vec = new Vector2 (shootDir.x, shootDir.y);
		
		body.AddForce(body.mass*inicialBulletSpeed*vec, ForceMode2D.Impulse);
	}
	
	void Update(){
		timer += Time.deltaTime;

		if (timer > bulletDelayTime) {
			shootDir = player.transform.position - transform.position;
			shootDir = shootDir/Mathf.Sqrt(Mathf.Pow(shootDir.x,2)+Mathf.Pow(shootDir.y,2));
			Vector2 vec = new Vector2 (shootDir.x, shootDir.y);
			body.AddForce (body.mass*vec * finalBulletSpeed, ForceMode2D.Impulse);
			timer = 0f;
		}

		if (timer > timeToSelfDestruct) {
			GameObject.Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {

		}
		GameObject.Destroy(gameObject);
	}
}

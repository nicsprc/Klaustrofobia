using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public float shieldBulletSpeed;
	private Vector3 shootDir;
	private PlayerStats playerStats;
	private float shieldTimer;
	public float shieldMaxTime;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		shootDir = Input.mousePosition;
		shootDir.z = 0.0f;
		shootDir = Camera.main.ScreenToWorldPoint(shootDir);
		shootDir = shootDir - transform.position;
		shootDir = shootDir/Mathf.Sqrt(Mathf.Pow(shootDir.x,2)+Mathf.Pow(shootDir.y,2));
		
		
		playerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();
		
		shieldBulletSpeed += playerStats.projectileSpeed;
		
		Vector2 vec = new Vector2 (shootDir.x, shootDir.y);
		GetComponent<Rigidbody2D>().AddForce(vec*shieldBulletSpeed*GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);

		//player = GameObject.FindGameObjectWithTag ("Player");
		//shieldMaxTime = player.GetComponent<Player1Skills> ().shieldCoolDown;
		body = GetComponent<Rigidbody2D> ();
		shieldTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		print (shieldTimer);

		shieldTimer += Time.deltaTime;
		if(shieldTimer>shieldMaxTime){
			GameObject.Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		body.isKinematic = true;
	}
}

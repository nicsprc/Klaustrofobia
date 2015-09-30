using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float bulletSpeed;
	private Vector3 shootDir;
	private PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		shootDir = Input.mousePosition;
		shootDir.z = 0.0f;
		shootDir = Camera.main.ScreenToWorldPoint(shootDir);
		shootDir = shootDir - transform.position;
		shootDir = shootDir/Mathf.Sqrt(Mathf.Pow(shootDir.x,2)+Mathf.Pow(shootDir.y,2));


		playerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();

		bulletSpeed += playerStats.projectileSpeed;

		Vector2 vec = new Vector2 (shootDir.x, shootDir.y);
		GetComponent<Rigidbody2D>().AddForce(vec*bulletSpeed*GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//layer 9 = Enemy
		if (coll.gameObject.layer == 9) {

		}
		GameObject.Destroy(gameObject);
	}
}

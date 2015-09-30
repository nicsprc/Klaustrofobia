using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	public float hp;
	public float damage;
	public bool invencible;

	private GameObject enemy;
	private bool dead;
	private Rigidbody2D body;
	private Animator animator;

	// Use this for initialization
	void Start () {
		dead = false;
		body = this.GetComponent<Rigidbody2D> ();

		animator = GetComponent<Animator> ();
		//damage += Mathf.CeilToInt (6f * Random.value);

		if (this.gameObject.layer == 10) {
			damage += GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().damage;
		}
	}

	void Update(){
		if (dead) {
			StartCoroutine(Died ());
		}
	}

	IEnumerator Died(){
		Vector2 pos = body.velocity;
		pos.x = 0;
		pos.y = 0;
		body.velocity = pos;

		animator.SetBool("Dead",true);
		GetComponent<FollowAndShoot>().enabled = false;

		body.gravityScale = 50;
		body.AddForce (Vector2.up, ForceMode2D.Impulse);

		yield return StartCoroutine (Fade());
		GameObject.Destroy (gameObject);
	}

	IEnumerator Fade() {
		//yield new WaitForSeconds (2f);
		for (float f = 1f; f >= 0f; f -= 0.005f) {
			Color c = GetComponent<Renderer>().material.color;
			c.a = f;
			GetComponent<Renderer>().material.color = c;
			yield return null;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		enemy = col.gameObject;

		//Layer 10 = PlayerAmmo
		if (enemy.layer == 10) {
			if(!invencible){
				this.takeDamage(enemy.GetComponent<EnemyStats>().getDamage());
			}
		}
	}

	void takeDamage(float dmg){
		hp -= dmg;
		if (hp <= 0) {
			hp = 0;
			dead = true;
			//GameObject.Destroy(gameObject);
		}			
	}

	public float getDamage(){
		return damage;
	}

	public bool isDead(){
		return dead;
	}

}

using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public float damage;
	public float projectileSpeed;
	public float hookSpeed;

	public float hpRatio;

	public float hp;
	public float maxHp;
	private GameObject enemy;
	private bool dead;

	public Texture2D cursorTexture;
	private Vector2 cursorHotspot;

	// Use this for initialization
	void Start () {
		maxHp = 20.0f;
		hp = maxHp;
		dead = false;

		cursorHotspot = new Vector2 (cursorTexture.width / 2, cursorTexture.height / 2);
		Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {

		hpRatio = hp / maxHp;

		if (dead) {
			Time.timeScale = 0;
		}	
	}

	void OnCollisionEnter2D(Collision2D col) {
		enemy = col.gameObject;
		if (enemy.tag == "StationBullet") {
			this.takeDamage(enemy.GetComponent<EnemyStats>().getDamage());
		}
		if (enemy.layer == 13) {
			this.takeDamage(enemy.GetComponent<EnemyStats>().getDamage());
		}
	}

	void takeDamage(float dmg){
		hp -= dmg;
		if (hp <= 0) {
			hp = 0;
			dead = true;
		}			
	}

	public float getHp(){
		return hp;
	}

	public void setMaxHp(float f){
		maxHp = f;
		return;
	}

	public float getMaxHp(){
		return maxHp;
	}

	public bool isDead(){
		return dead;
	}
}

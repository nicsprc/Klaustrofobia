using UnityEngine;
using System.Collections;

public class DisplayBossHp : MonoBehaviour {

	private GameObject boss;
	private GameObject player;
	private float dis;

	private bool showBossHp;

	private float bossMaxHp;
	private float bossHp;

	public GUISkin bossHpFull;
	public GUISkin bossHpEmpty;
	public GUISkin bossHpText;

	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
		player = GameObject.FindGameObjectWithTag ("Player");

		showBossHp = false;
		if (boss != null) {
			bossMaxHp = boss.GetComponent<EnemyStats> ().hp;
			bossHp = bossMaxHp;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (boss != null) {
			dis = Mathf.Sqrt (Mathf.Pow ((boss.transform.position.x - player.transform.position.x), 2) + Mathf.Pow (boss.transform.position.y - player.transform.position.y, 2));

			if (dis < 10.0f) {
				showBossHp = true;
			} else {
				showBossHp = false;
			}

			bossHp = boss.GetComponent<EnemyStats> ().hp;
		} else {
			showBossHp = false;
		}
	}

	void OnGUI(){
		if (showBossHp) {
			GUI.skin = bossHpEmpty;
			GUI.Box(new Rect(10,10,Screen.width-20,50), "");

			if(boss!=null){
				if(!boss.GetComponent<EnemyStats>().isDead()){
					GUI.skin = bossHpFull;
					GUI.Box(new Rect(12,12,(bossHp/bossMaxHp)*(Screen.width-24),46), "");
				}
			}

			GUI.skin = bossHpText;
			GUI.Box(new Rect(12,12,(Screen.width-24),46), bossHp+"/"+bossMaxHp);
		}
	}
}

using UnityEngine;
using System.Collections;

public class DisplayPlayerHp : MonoBehaviour {

	private PlayerStats playerStats;
	
	//private float playerMaxHp;
	//private float playerHp;
	
	public GUISkin playerHpFull;
	public GUISkin playerHpEmpty;
	public GUISkin playerHpText;
	
	// Use this for initialization
	void Start () {
		playerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		//playerHp = player.GetComponent<PlayerStats> ().getHp();
		//print (playerMaxHp);
	}
	
	void OnGUI(){
		GUI.skin = playerHpEmpty;
		GUI.Box(new Rect(10,Screen.height-70,Screen.width/5,30), "");
			
		if (!playerStats.isDead ()) {
			GUI.skin = playerHpFull;
			GUI.Box (new Rect (12, Screen.height - 68, (playerStats.getHp () / playerStats.getMaxHp ()) * ((Screen.width / 5) - 4), 26), "");
		}
		GUI.skin = playerHpText;
		GUI.Box(new Rect(12,Screen.height-68,(Screen.width/5)-4,26), playerStats.getHp().ToString("F0")+"/"+playerStats.getMaxHp());
	}
}

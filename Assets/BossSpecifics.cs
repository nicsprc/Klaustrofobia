using UnityEngine;
using System.Collections;

public class BossSpecifics : MonoBehaviour {

	private EnemyStats bossStats;
	private GameObject mainCamera;

	// Use this for initialization
	void Start () {
		bossStats = GetComponent<EnemyStats> ();
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (bossStats.isDead ()) {
			mainCamera.GetComponent<CameraMovement>().shake = 3.0f;
		}
	}
}

using UnityEngine;
using System.Collections;

public class AnimationEye : MonoBehaviour {

	private Animator animator;
	private float timer;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		timer = animator.GetFloat ("Eye_Timer");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		animator.SetFloat ("Eye_Timer", timer);

		if (timer > 5.0f) {
			timer = 0.0f;
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public AudioClip footsteps;
	public AudioClip jumpSound;
	public AudioClip landSound;
	private AudioSource audioSou;

	private Vector3 playerDir;
	private Rigidbody2D rb;
	public float speed;
	public float jump;
	private Animator animator;


	private bool moveRight;
	private bool moveLeft;
	private bool applyJump;
	private bool canMoveLeft;
	private bool canMoveRight;

	private bool stopLeft;
	private bool stopRight;

	private bool canJump;

	// Use this for initialization
	void Start () {
		audioSou = GetComponent<AudioSource>();
		//speed = 4.0f;
		//jump = 12.0f;
		animator = this.GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		moveLeft = false;
		moveRight = false;
		applyJump = false;

		canMoveLeft = true;
		canMoveRight = true;

		stopLeft = false;
		stopRight = false;

		canJump = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Mathf.Abs (rb.velocity.x) > speed) {
			if (rb.velocity.x < 0) {
				canMoveLeft = false;
				//Vector2 vec = rb.velocity;
				//vec.x = -speed;
				//rb.velocity = vec;
			} else {
				canMoveRight = false;
				//Vector2 vec = rb.velocity;
				//vec.x = speed;
				//rb.velocity = vec;
			}
		} else {
			canMoveLeft = true;
			canMoveRight = true;
		}

		if (canMoveLeft) {
			if (Input.GetKey (KeyCode.A)) {
				moveLeft = true;
			}
		}
		if (Input.GetKeyUp (KeyCode.A)) {
			stopLeft = true;
		}


		if (canMoveRight) {
			if (Input.GetKey (KeyCode.D)) {
				moveRight = true;
			}
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			stopRight = true;
		}

		if (canJump) {
			if (Input.GetKeyDown (KeyCode.W)) {
				applyJump = true;
				canJump = false;
			}
		}

		if (Input.GetKey (KeyCode.W)) {
			if(GetComponent<Player1Skills>().isHookOn){
				GetComponent<DistanceJoint2D>().distance -= 2.0f*Time.deltaTime;
			}
		}

		if (Input.GetKey (KeyCode.S)) {
			if(GetComponent<Player1Skills>().isHookOn){
				GetComponent<DistanceJoint2D>().distance += 2.0f*Time.deltaTime;
			}
		}


		// Animation of the player based on the mouse position

		playerDir = Input.mousePosition;
		playerDir = Camera.main.ScreenToWorldPoint(playerDir);
		playerDir = playerDir - transform.position;
		playerDir = playerDir/Mathf.Sqrt(Mathf.Pow(playerDir.x,2)+Mathf.Pow(playerDir.y,2));
		
		if (playerDir.x > 0.0f) {
			animator.SetInteger ("Direction", 1);
		} else {
			animator.SetInteger("Direction",-1);
		}

		if (Mathf.Abs(rb.velocity.x) > 0.0f) {
			animator.SetInteger("Stand",0);
		} else {
			animator.SetInteger("Stand",1);
		}
	}

	void FixedUpdate(){
		if (moveLeft) {
			rb.AddForce(rb.mass*Vector2.left ,ForceMode2D.Impulse);
			moveLeft = false;
			if(!applyJump){
				if(!audioSou.isPlaying){
					audioSou.clip = footsteps;
					audioSou.Play();
				}
			}
		}

		if (moveRight) {
			rb.AddForce(rb.mass*Vector2.right ,ForceMode2D.Impulse);
			moveRight = false;
			if(!applyJump){
				if(!audioSou.isPlaying){
					audioSou.clip = footsteps;
					audioSou.Play();
				}
			}
		}

		if (stopLeft) {
			rb.AddForce(-0.5f*rb.velocity.x*rb.mass*Vector2.right ,ForceMode2D.Impulse);
			stopLeft = false;
		}

		if (stopRight) {
			rb.AddForce(0.5f*rb.velocity.x*rb.mass*Vector2.left ,ForceMode2D.Impulse);
			stopRight = false;
		}

		if (applyJump) {
			rb.AddForce(new Vector2(0,jump*rb.mass),ForceMode2D.Impulse);
			applyJump = false;
			audioSou.PlayOneShot(jumpSound);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		canJump = true;
		audioSou.PlayOneShot(landSound);
	}
}


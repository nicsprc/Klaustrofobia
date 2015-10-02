using UnityEngine;
using System.Collections;

public class Player1Skills : MonoBehaviour {
	
	private AudioSource audioSou;

	public GameObject bullet;
	public GameObject chargedBullet;
	public float timeCharging;
	public GameObject hook;
	public float dashCoolDown;

	private float timer;
	private GameObject weapon;
	public bool isHookOn;
	private Object TheHook;
	private bool comeHere;
	private bool goAway;
	private Rigidbody2D body;

	private bool canDash;
	private bool applyDash;
	private float dashTimer;
	private bool countDashTimer;

	private bool canShoot;
	public float shootCoolDown;
	private float shootTimer;
	private bool countShootTime;

	public GameObject shield;
	private bool canShootShield;
	public float shieldCoolDown;
	private float shieldTimer;
	private bool countShieldTime;

	private GameObject flashlight;
	private bool isFlashlightOn;


	public float hookFlyingTime;
	private float hookTimer;
	private float hookConstant;

	public int offensiveSkillPointCount;
	public int offensiveSkillPointSpent;
	public int utilitySkillPointCount;
	public int utilitySkillPointSpent;

	private PlayerMovement playerMov;
	// Use this for initialization
	void Start () {
		audioSou = GetComponent<AudioSource> ();

		timer = 0.0f;
		dashTimer = 0.0f;
		weapon = GameObject.Find ("p1/Weapon");
		flashlight = GameObject.Find ("p1/Flashlight");
		body = GetComponent<Rigidbody2D> ();

		playerMov = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();

		isHookOn = false;
		comeHere = false;
		goAway = false;
		applyDash = false;
		canDash = true;

		canShoot = true;
		shootTimer = 0.0f;
		countShootTime = false;

		canShootShield = true;
		shieldTimer = 0.0f;
		countShieldTime = false;

		isFlashlightOn = false;

		hookTimer = 0.0f;
		hookConstant = hookFlyingTime;

	}
	
	// Update is called once per frame
	void Update () {
		//Shoot
		if (canShoot) {
			if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1) || Input.GetMouseButton(1)) {
				Instantiate (bullet, transform.position, transform.rotation);
				canShoot = false;
				countShootTime = true;
				//audioSou.PlayOneShot(audioClip);
			}
		}

		if (countShootTime) {
			shootTimer += Time.deltaTime;
			if(shootTimer>shootCoolDown){
				shootTimer = 0.0f;
				canShoot = true;
				countShootTime = false;
			}
		}

		if (Input.GetMouseButton (0)) {
			timer += Time.deltaTime;
			if(timer>timeCharging){
				weapon.GetComponent<Animator> ().SetBool ("Charged", true);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			timer = 0.0f;		
			if(weapon.GetComponent<Animator> ().GetBool("Charged")){
				Instantiate(chargedBullet,transform.position, transform.rotation);
			}
			weapon.GetComponent<Animator> ().SetBool ("Charged", false);
		}



		//Hook
		if (Input.GetKeyDown (KeyCode.Q)) {
			isHookOn = !isHookOn;
			if (isHookOn){ 
				TheHook = Instantiate (hook, weapon.transform.position, weapon.transform.rotation);
			}
		}

		if (isHookOn) {
			hookTimer += Time.deltaTime;
			if (hookTimer > hookFlyingTime) {
				isHookOn = false;
				hookTimer = 0.0f;
			}
		}


		if(Input.GetKeyDown(KeyCode.Z)){
			if (isHookOn){
				comeHere = !comeHere;
				goAway = false;
			}
		}

		if(Input.GetKeyDown(KeyCode.X)){
			if (isHookOn){
				goAway = !goAway;
				comeHere = false;
			}
		}

		if(comeHere){
			GetComponent<DistanceJoint2D>().distance -= playerMov.speed*Time.deltaTime;
		}

		if(goAway){
			GetComponent<DistanceJoint2D>().distance += playerMov.speed*Time.deltaTime;
		}

		if (!isHookOn) {
			comeHere = false;
			goAway = false;
			hookTimer = 0.0f;
			hookFlyingTime = hookConstant;
			GameObject.Find("p1/Line").GetComponent<LineRenderer>().SetPosition (0,new Vector3(0,0,0));
			GameObject.Find("p1/Line").GetComponent<LineRenderer>().SetPosition (1,new Vector3(0,0,0));
			Object.Destroy (TheHook);
			if (GetComponent<DistanceJoint2D> ().enabled) {
				GetComponent<DistanceJoint2D> ().enabled = false;
			}
		} else {
			GameObject.Find("p1/Line").GetComponent<LineRenderer>().SetPosition (0, transform.position);
			GameObject.Find("p1/Line").GetComponent<LineRenderer>().SetPosition (1, GameObject.FindGameObjectWithTag("Hook").transform.position);
		}


		//Dash
		if (canDash) {
			if (Input.GetKeyDown (KeyCode.E)) {
				applyDash = true;
				countDashTimer = true;
				canDash = false;
			}
		}

		if (countDashTimer) {
			dashTimer += Time.deltaTime;
			if(dashTimer>dashCoolDown){
				canDash = true;
				countDashTimer = false;
				dashTimer = 0.0f;
			}
		}

		//Flashlight
		if(Input.GetKeyDown(KeyCode.F)){
			isFlashlightOn = !isFlashlightOn;
		}

		if(isFlashlightOn){
			flashlight.GetComponent<Light>().enabled = true;

			Vector3 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);

			float dy = mousePos.y - transform.position.y;
			float dx = mousePos.x - transform.position.x;
			float ang = 0.0f;;
			ang = -Mathf.Rad2Deg*Mathf.Atan2(dy,dx);

			Vector3 rot = flashlight.transform.localEulerAngles;

			rot.x = ang;
			rot.y = 89f;
			rot.z = 0f;
			flashlight.transform.localEulerAngles = rot;

		}else{
			flashlight.GetComponent<Light>().enabled = false;
		}

		//Shield

		if (canShootShield) {
			if (Input.GetKeyDown(KeyCode.C)) {
				Instantiate (shield, transform.position, transform.rotation);
				canShootShield = false;
				countShieldTime = true;
				//audioSou.PlayOneShot(audioClip);
			}
		}
		
		if (countShieldTime) {
			shieldTimer += Time.deltaTime;
			if(shieldTimer>shieldCoolDown){
				shieldTimer = 0.0f;
				canShootShield = true;
				countShieldTime = false;
			}
		}
	}	

	void FixedUpdate(){
		if(applyDash){
			Vector3 playerDir = Input.mousePosition;
			playerDir = Camera.main.ScreenToWorldPoint(playerDir);
			playerDir = playerDir - transform.position;
			float x = playerDir.x;
			x = x/Mathf.Abs(x);
			
			Vector2 vec = 10.0f*x*Vector2.right*body.mass;
			body.AddForce(vec,ForceMode2D.Impulse);
			applyDash = false;
		}
	}

	public void setHookFlyingTime(float f){
		hookFlyingTime = f;
		return;
	}
}

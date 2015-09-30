using UnityEngine;
using System.Collections;

public class GUIElements : MonoBehaviour {

	private GameObject player;
	private PlayerStats playerStats;
	private Player1Skills playerSkills;
	private PlayerMovement playerMovement;
	private bool shift;

	public GUISkin skillsWindowGUISkin;
	public GUISkin skillsSubWindowGUISkin;

	public Texture2D upGreen;
	public Texture2D upRed;
	public Texture2D downGreen;
	public Texture2D downRed;



	private int dmgPoints;
	private int projSpeedPoints;
	private int shotCDPoints;
	private int chargeCDPoints;

	private int hpPoints;
	private int movSpeedPoints;
	private int hookSpeedPoints;
	private int jumpForcePoints;
	private int dashCDPoints;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStats = player.GetComponent<PlayerStats> ();
		playerSkills = player.GetComponent<Player1Skills> ();
		playerMovement = player.GetComponent<PlayerMovement> ();
		shift = false;

		dmgPoints = 0;
		projSpeedPoints = 0;
		shotCDPoints = 0;
		chargeCDPoints = 0;

		hpPoints = 0;
		movSpeedPoints = 0;
		hookSpeedPoints = 0;
		jumpForcePoints = 0;
		dashCDPoints = 0;


	}
	
	// Update is called once per frame
	void Update () {


		if (shift) {
			Camera.main.gameObject.GetComponent<CameraMovement>().canZoom = false;
			if ((Input.anyKeyDown)&&!((Input.GetMouseButtonDown(0))||(Input.GetMouseButtonDown(1)))) {
				shift = false;
				playerSkills.enabled = true;
			}

		} else {
			Camera.main.gameObject.GetComponent<CameraMovement>().canZoom = true;
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				shift = true;
				playerSkills.enabled = false;
			}
		}
	}

	void OnGUI(){

		if (playerStats.isDead ()) {
			GUI.Box(new Rect(0,0,Screen.width,Screen.height), "You DIEDED!");
		}

		if (shift) {
			GUI.Box(new Rect(0,0,Screen.width,Screen.height), "");


			//Skills window
			GUI.skin = skillsWindowGUISkin;

			Rect skillsWindow = new Rect(Screen.width/5,Screen.height*(0.5f/6f),Screen.width*(3f/5f),Screen.height*(5f/6f));
			GUI.Box(skillsWindow,"Customize your skills");

			//Offensive Skills
			Rect offensiveSkills = new Rect(10f,30f, (Screen.width*(3f/5f) - 20),skillsWindow.height*(1f/2f)-30f);
			offensiveSkills.position = new Vector2(skillsWindow.xMin+offensiveSkills.xMin,skillsWindow.yMin+offensiveSkills.yMin);

			GUI.Box(offensiveSkills,"Offensive Skills. "+playerSkills.offensiveSkillPointSpent+" points spents. "+playerSkills.offensiveSkillPointCount+" skill points availables.");

			float dx = offensiveSkills.width*(1f/9f);
			//Damage Box
			Rect damage = new Rect(dx,30f,dx,offensiveSkills.height-40f);
			damage.position = new Vector2(offensiveSkills.xMin+damage.xMin,offensiveSkills.yMin+damage.yMin);

			GUI.Box(damage,"Damage");


			float dd0x = damage.width*(1f/13f);
			float ddx  = damage.width*(5f/13f);
			float ddy  = damage.height*(18f/90f);
			float ddy2  = damage.height*(18f/120f);
			//Inside Damage Box

			GUI.skin = skillsSubWindowGUISkin;

			//Max damage
			Rect damageMax = new Rect(dd0x,damage.height*(18f/90f),ddx,ddy);
			damageMax.position = new Vector2(damage.xMin+damageMax.xMin,damage.yMin+damageMax.yMin);
			GUI.Box(damageMax,"-");

			//Min damage
			Rect damageMin = new Rect(0f,damage.height*(9f/90f),ddx,ddy);
			damageMin.position = new Vector2(damageMax.xMin+damageMin.xMin,damageMax.yMax+damageMin.yMin);
			GUI.Box(damageMin,"1");

			//Increase damage button
			Rect plusDmg = new Rect(dd0x,damage.height*(18f/120f),ddx,ddy2);
			plusDmg.position = new Vector2(damageMax.xMax+plusDmg.xMin,damage.yMin+plusDmg.yMin);
			if(GUI.Button(plusDmg,upGreen)){
				if(playerSkills.offensiveSkillPointCount>0){
					playerSkills.offensiveSkillPointCount--;
					playerSkills.offensiveSkillPointSpent++;
					playerStats.damage += 1;
					dmgPoints++;
				}
			}

			if(damage.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerSkills.offensiveSkillPointCount>0){
						playerSkills.offensiveSkillPointCount--;
						playerSkills.offensiveSkillPointSpent++;
						playerStats.damage += 1;
						dmgPoints++;
					}
				}
			}

			//Actual damage
			Rect playerDmg = new Rect(0f,damage.height*(9f/120f),ddx,ddy2);
			playerDmg.position = new Vector2(plusDmg.xMin+playerDmg.xMin,plusDmg.yMax+playerDmg.yMin);
			GUI.Box(playerDmg,""+playerStats.damage.ToString("F0"));

			//Decrease damage button
			Rect minusDmg = new Rect(0,damage.width*(9f/120f),ddx,ddy2);
			minusDmg.position = new Vector2(playerDmg.xMin+minusDmg.xMin,playerDmg.yMax+minusDmg.yMin);
			if(GUI.Button(minusDmg,downRed)){
				if(playerStats.damage>1){
					if(playerSkills.offensiveSkillPointSpent>0){
						playerSkills.offensiveSkillPointCount++;
						playerSkills.offensiveSkillPointSpent--;
						playerStats.damage -= 1;
						dmgPoints--;
					}
				}
			}

			if(damage.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerStats.damage>1){
						if(playerSkills.offensiveSkillPointSpent>0){
							playerSkills.offensiveSkillPointCount++;
							playerSkills.offensiveSkillPointSpent--;
							playerStats.damage -= 1;
							dmgPoints--;
						}
					}
				}
			}
			//points spent
			Rect pointsDmg = new Rect(damage.width*(1f/10f),-10f,damage.width*(8f/10f),damage.height*(1f/10f)+10f);
			pointsDmg.position = new Vector2(damage.xMin+pointsDmg.xMin,damage.yMax*(9f/10f)+pointsDmg.yMin);
			GUI.Box(pointsDmg,dmgPoints+" points");


			///////////////////////////////////////////////////////////////////////
			GUI.skin = skillsWindowGUISkin;

			//Projectile Speed Box
			Rect bulSpeed = new Rect(dx,0f,dx,offensiveSkills.height-40f);
			bulSpeed.position = new Vector2(damage.xMax+bulSpeed.xMin,damage.yMin+bulSpeed.yMin);
			
			GUI.Box(bulSpeed,"Shot Speed Bonus");

			//Inside Projectile Speed Box
			GUI.skin = skillsSubWindowGUISkin;
			//Max proj speed
			Rect projSpeedMax = new Rect(dd0x,bulSpeed.height*(18f/90f),ddx,ddy);
			projSpeedMax.position = new Vector2(bulSpeed.xMin+projSpeedMax.xMin,bulSpeed.yMin+projSpeedMax.yMin);
			GUI.Box(projSpeedMax,"90");
			
			//Min proj speed
			Rect projSpeedMin = new Rect(0f,bulSpeed.height*(9f/90f),ddx,ddy);
			projSpeedMin.position = new Vector2(projSpeedMax.xMin+projSpeedMin.xMin,projSpeedMax.yMax+projSpeedMin.yMin);
			GUI.Box(projSpeedMin,"0");
			
			//Increase proj speed button
			Rect plusProjSpeed = new Rect(dd0x,bulSpeed.height*(18f/120f),ddx,ddy2);
			plusProjSpeed.position = new Vector2(projSpeedMax.xMax+plusProjSpeed.xMin,bulSpeed.yMin+plusProjSpeed.yMin);
			if(GUI.Button(plusProjSpeed,upGreen)){
				if(playerStats.projectileSpeed<90){
					if(playerSkills.offensiveSkillPointCount>0){
						playerSkills.offensiveSkillPointCount--;
						playerSkills.offensiveSkillPointSpent++;
						playerStats.projectileSpeed += 2;
						projSpeedPoints++;
					}
				}
			}

			if(bulSpeed.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerStats.projectileSpeed<90){
						if(playerSkills.offensiveSkillPointCount>0){
							playerSkills.offensiveSkillPointCount--;
							playerSkills.offensiveSkillPointSpent++;
							playerStats.projectileSpeed += 2;
							projSpeedPoints++;
						}
					}
				}
			}
			
			//Actual proj speed
			Rect playerProjSpeed = new Rect(0f,bulSpeed.height*(9f/120f),ddx,ddy2);
			playerProjSpeed.position = new Vector2(plusProjSpeed.xMin+playerProjSpeed.xMin,plusProjSpeed.yMax+playerProjSpeed.yMin);
			GUI.Box(playerProjSpeed,""+playerStats.projectileSpeed.ToString("F0"));
			
			//Decrease proj speed button
			Rect minusProjSpeed = new Rect(0,bulSpeed.width*(9f/120f),ddx,ddy2);
			minusProjSpeed.position = new Vector2(playerProjSpeed.xMin+minusProjSpeed.xMin,playerProjSpeed.yMax+minusProjSpeed.yMin);
			if(GUI.Button(minusProjSpeed,downRed)){
				if(playerStats.projectileSpeed>0){
					if(playerSkills.offensiveSkillPointSpent>0){
						playerSkills.offensiveSkillPointCount++;
						playerSkills.offensiveSkillPointSpent--;
						playerStats.projectileSpeed -= 2;
						projSpeedPoints--;
					}
				}
			}

			if(bulSpeed.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerStats.projectileSpeed>0){
						if(playerSkills.offensiveSkillPointSpent>0){
							playerSkills.offensiveSkillPointCount++;
							playerSkills.offensiveSkillPointSpent--;
							playerStats.projectileSpeed -= 2;
							projSpeedPoints--;
						}
					}
				}
			}
			
			//points spent
			Rect pointsProjSpeed = new Rect(bulSpeed.width*(1f/10f),-10f,bulSpeed.width*(8f/10f),bulSpeed.height*(1f/10f)+10f);
			pointsProjSpeed.position = new Vector2(bulSpeed.xMin+pointsProjSpeed.xMin,bulSpeed.yMax*(9f/10f)+pointsProjSpeed.yMin);
			GUI.Box(pointsProjSpeed,projSpeedPoints+" points");
			
			
			///////////////////////////////////////////////////////////////////////
			GUI.skin = skillsWindowGUISkin;
			//Cooldown Shot Box
			Rect shotCD = new Rect(dx,0f,dx,offensiveSkills.height-40f);
			shotCD.position = new Vector2(bulSpeed.xMax+shotCD.xMin,bulSpeed.yMin+shotCD.yMin);
			
			GUI.Box(shotCD,"Shoot CD");

			//Inside shot cd Box
			GUI.skin = skillsSubWindowGUISkin;
			//Max shot cd
			Rect shotCDMax = new Rect(dd0x,shotCD.height*(18f/90f),ddx,ddy);
			shotCDMax.position = new Vector2(shotCD.xMin+shotCDMax.xMin,shotCD.yMin+shotCDMax.yMin);
			GUI.Box(shotCDMax,"1.5");
			
			//Min shot CD
			Rect shotCDMin = new Rect(0f,shotCD.height*(9f/90f),ddx,ddy);
			shotCDMin.position = new Vector2(shotCDMax.xMin+shotCDMin.xMin,shotCDMax.yMax+shotCDMin.yMin);
			GUI.Box(shotCDMin,"0.3");
			
			//Increase shot CD button
			Rect plusShotCD = new Rect(dd0x,shotCD.height*(18f/120f),ddx,ddy2);
			plusShotCD.position = new Vector2(shotCDMax.xMax+plusShotCD.xMin,shotCD.yMin+plusShotCD.yMin);
			if(GUI.Button(plusShotCD,downGreen)){
				if(playerSkills.shootCoolDown>0.35f){
					if(playerSkills.offensiveSkillPointCount>0){
						playerSkills.offensiveSkillPointCount--;
						playerSkills.offensiveSkillPointSpent++;
						playerSkills.shootCoolDown -= 0.05f;
						shotCDPoints++;
					}
				}
			}

			if(shotCD.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerSkills.shootCoolDown>0.35f){
						if(playerSkills.offensiveSkillPointCount>0){
							playerSkills.offensiveSkillPointCount--;
							playerSkills.offensiveSkillPointSpent++;
							playerSkills.shootCoolDown -= 0.05f;
							shotCDPoints++;
						}
					}
				}
			}
			
			//Actual shot CD
			Rect playerShotCD = new Rect(0f,shotCD.height*(9f/120f),ddx,ddy2);
			playerShotCD.position = new Vector2(plusShotCD.xMin+playerShotCD.xMin,plusShotCD.yMax+playerShotCD.yMin);
			GUI.Box(playerShotCD,""+playerSkills.shootCoolDown.ToString("F2"));
			
			//Decrease shot Cd button
			Rect minusShotCD = new Rect(0,shotCD.width*(9f/120f),ddx,ddy2);
			minusShotCD.position = new Vector2(playerShotCD.xMin+minusShotCD.xMin,playerShotCD.yMax+minusShotCD.yMin);
			if(GUI.Button(minusShotCD,upRed)){
				if(playerSkills.shootCoolDown<1.5f){
					if(playerSkills.offensiveSkillPointSpent>0){
						playerSkills.offensiveSkillPointCount++;
						playerSkills.offensiveSkillPointSpent--;
						playerSkills.shootCoolDown += 0.05f;
						shotCDPoints--;
					}
				}
			}

			if(shotCD.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerSkills.shootCoolDown<1.5f){
						if(playerSkills.offensiveSkillPointSpent>0){
							playerSkills.offensiveSkillPointCount++;
							playerSkills.offensiveSkillPointSpent--;
							playerSkills.shootCoolDown += 0.05f;
							shotCDPoints--;
						}
					}
				}
			}
			
			//points spent
			Rect pointsShotCD = new Rect(shotCD.width*(1f/10f),-10f,shotCD.width*(8f/10f),shotCD.height*(1f/10f)+10f);
			pointsShotCD.position = new Vector2(shotCD.xMin+pointsShotCD.xMin,shotCD.yMax*(9f/10f)+pointsShotCD.yMin);
			GUI.Box(pointsShotCD,shotCDPoints+" points");
			

			///////////////////////////////////////////////////////////////////////
			GUI.skin = skillsWindowGUISkin;
			//Cooldown Charge Box
			Rect chargeCD = new Rect(dx,0f,dx,offensiveSkills.height-40f);
			chargeCD.position = new Vector2(shotCD.xMax+chargeCD.xMin,shotCD.yMin+chargeCD.yMin);
			
			GUI.Box(chargeCD,"Charge CD");

			//Inside charge cd Box
			GUI.skin = skillsSubWindowGUISkin;
			//Max charge cd
			Rect chargeCDMax = new Rect(dd0x,chargeCD.height*(18f/90f),ddx,ddy);
			chargeCDMax.position = new Vector2(chargeCD.xMin+chargeCDMax.xMin,chargeCD.yMin+chargeCDMax.yMin);
			GUI.Box(chargeCDMax,"3");
			
			//Min charge CD
			Rect chargeCDMin = new Rect(0f,chargeCD.height*(9f/90f),ddx,ddy);
			chargeCDMin.position = new Vector2(chargeCDMax.xMin+chargeCDMin.xMin,chargeCDMax.yMax+chargeCDMin.yMin);
			GUI.Box(chargeCDMin,"0.2");
			
			//Increase charge CD button
			Rect plusChargeCD = new Rect(dd0x,chargeCD.height*(18f/120f),ddx,ddy2);
			plusChargeCD.position = new Vector2(chargeCDMax.xMax+plusChargeCD.xMin,chargeCD.yMin+plusChargeCD.yMin);
			if(GUI.Button(plusChargeCD,downGreen)){
				if(playerSkills.timeCharging>0.25f){
					if(playerSkills.offensiveSkillPointCount>0){
						playerSkills.offensiveSkillPointCount--;
						playerSkills.offensiveSkillPointSpent++;
						playerSkills.timeCharging -= 0.1f;
						chargeCDPoints++;
					}
				}
			}

			if(chargeCD.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerSkills.timeCharging>0.25f){
						if(playerSkills.offensiveSkillPointCount>0){
							playerSkills.offensiveSkillPointCount--;
							playerSkills.offensiveSkillPointSpent++;
							playerSkills.timeCharging -= 0.1f;
							chargeCDPoints++;
						}
					}
				}
			}
			
			//Actual charge CD
			Rect playerChargeCD = new Rect(0f,chargeCD.height*(9f/120f),ddx,ddy2);
			playerChargeCD.position = new Vector2(plusChargeCD.xMin+playerChargeCD.xMin,plusChargeCD.yMax+playerChargeCD.yMin);
			GUI.Box(playerChargeCD,""+playerSkills.timeCharging.ToString("F2"));
			
			//Decrease charge Cd button
			Rect minusChargeCD = new Rect(0,chargeCD.width*(9f/120f),ddx,ddy2);
			minusChargeCD.position = new Vector2(playerChargeCD.xMin+minusChargeCD.xMin,playerChargeCD.yMax+minusChargeCD.yMin);
			if(GUI.Button(minusChargeCD,upRed)){
				if(playerSkills.timeCharging<3f){
					if(playerSkills.offensiveSkillPointSpent>0){
						playerSkills.offensiveSkillPointCount++;
						playerSkills.offensiveSkillPointSpent--;
						playerSkills.timeCharging += 0.1f;
						chargeCDPoints--;
					}
				}
			}

			if(chargeCD.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerSkills.timeCharging<3f){
						if(playerSkills.offensiveSkillPointSpent>0){
							playerSkills.offensiveSkillPointCount++;
							playerSkills.offensiveSkillPointSpent--;
							playerSkills.timeCharging += 0.1f;
							chargeCDPoints--;
						}
					}
				}
			}
			
			//points spent
			Rect pointsChargeCD = new Rect(chargeCD.width*(1f/10f),-10f,shotCD.width*(8f/10f),chargeCD.height*(1f/10f)+10f);
			pointsChargeCD.position = new Vector2(chargeCD.xMin+pointsChargeCD.xMin,chargeCD.yMax*(9f/10f)+pointsChargeCD.yMin);
			GUI.Box(pointsChargeCD,chargeCDPoints+" points");
			
			
			///////////////////////////////////////////////////////////////////////
			/// 
			/// 
			/// 
			/// 
			/// 
			/// 
			/// 
			/// 
			/// ///////////////////////////////////////////////////////////////////////




			GUI.skin = skillsWindowGUISkin;
			//Utility Skills
			Rect utilitySkills = new Rect(0f,10f, (Screen.width*(3f/5f) - 20),skillsWindow.height*(1f/2f)-30f);
			utilitySkills.position = new Vector2(offensiveSkills.xMin+utilitySkills.xMin,offensiveSkills.yMax+utilitySkills.yMin);
			
			GUI.Box(utilitySkills,"Utility Skills. "+playerSkills.utilitySkillPointSpent+" skill points spent. "+playerSkills.utilitySkillPointCount+" skill points availables.");

			float d0x = utilitySkills.width*(2f/27f);
			dx = utilitySkills.width*(1f/9f);
			//Hp Box
			Rect hp = new Rect(d0x,30f,dx,utilitySkills.height-40f);
			hp.position = new Vector2(utilitySkills.xMin+hp.xMin,utilitySkills.yMin+hp.yMin);
			
			GUI.Box(hp,"HP");

			dd0x = hp.width*(1f/13f);
			ddx  = hp.width*(5f/13f);
			ddy  = hp.height*(18f/90f);
			ddy2  = hp.height*(18f/120f);

			//Inside HP Box
			GUI.skin = skillsSubWindowGUISkin;
			//Max hp
			Rect hpMax = new Rect(dd0x,hp.height*(18f/90f),ddx,ddy);
			hpMax.position = new Vector2(hp.xMin+hpMax.xMin,hp.yMin+hpMax.yMin);
			GUI.Box(hpMax,"-");
			
			//Min hp
			Rect hpMin = new Rect(0f,hp.height*(9f/90f),ddx,ddy);
			hpMin.position = new Vector2(hpMax.xMin+hpMin.xMin,hpMax.yMax+hpMin.yMin);
			GUI.Box(hpMin,"1");
			
			//Increase hp button
			Rect plusHp = new Rect(dd0x,hp.height*(18f/120f),ddx,ddy2);
			plusHp.position = new Vector2(hpMax.xMax+plusHp.xMin,hp.yMin+plusHp.yMin);
			if(GUI.Button(plusHp,upGreen)){
				if(playerSkills.utilitySkillPointCount>0){
					playerSkills.utilitySkillPointCount--;
					playerSkills.utilitySkillPointSpent++;
					float ratio = playerStats.hpRatio;
					playerStats.maxHp += 10f;
					playerStats.hp = playerStats.maxHp*ratio;
					hpPoints++;
				}
			}

			if(hp.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerSkills.utilitySkillPointCount>0){
						playerSkills.utilitySkillPointCount--;
						playerSkills.utilitySkillPointSpent++;
						float ratio = playerStats.hpRatio;
						playerStats.maxHp += 10f;
						playerStats.hp = playerStats.maxHp*ratio;
						hpPoints++;
					}
				}
			}
			
			//Actual hp
			Rect playerHp = new Rect(0f,hp.height*(9f/120f),ddx,ddy2);
			playerHp.position = new Vector2(plusHp.xMin+playerHp.xMin,plusHp.yMax+playerHp.yMin);
			GUI.Box(playerHp,playerStats.maxHp.ToString("F0"));
			
			//Decrease hp button
			Rect minusHp = new Rect(0,hp.width*(9f/120f),ddx,ddy2);
			minusHp.position = new Vector2(playerHp.xMin+minusHp.xMin,playerHp.yMax+minusHp.yMin);
			if(GUI.Button(minusHp,downRed)){
				if(playerStats.maxHp>10){
					if(playerSkills.utilitySkillPointSpent>0){
						playerSkills.utilitySkillPointCount++;
						playerSkills.utilitySkillPointSpent--;
						float ratio = playerStats.hpRatio;
						playerStats.maxHp -= 10f;
						playerStats.hp = playerStats.maxHp*ratio;
						hpPoints--;
					}
				}
			}

			if(hp.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerStats.maxHp>10){
						if(playerSkills.utilitySkillPointSpent>0){
							playerSkills.utilitySkillPointCount++;
							playerSkills.utilitySkillPointSpent--;
							float ratio = playerStats.hpRatio;
							playerStats.maxHp -= 10f;
							playerStats.hp = playerStats.maxHp*ratio;
							hpPoints--;
						}
					}
				}
			}
			
			//points spent
			Rect pointsHp = new Rect(hp.width*(1f/10f),10f,hp.width*(8f/10f),hp.height*(1f/10f)+10f);
			pointsHp.position = new Vector2(hp.xMin+pointsHp.xMin,hp.yMax*(9f/10f)+pointsHp.yMin);
			GUI.Box(pointsHp,hpPoints+" points");
			
			
			///////////////////////////////////////////////////////////////////////
			GUI.skin = skillsWindowGUISkin;

			//Mov. Speed Box
			Rect movSpeed = new Rect(d0x,0f,dx,utilitySkills.height-40f);
			movSpeed.position = new Vector2(hp.xMin+movSpeed.xMax,hp.yMin+movSpeed.yMin);
			
			GUI.Box(movSpeed,"Mov. Speed");

			//Inside mov speed Box
			GUI.skin = skillsSubWindowGUISkin;
			//Max mov speed
			Rect movSpeedMax = new Rect(dd0x,movSpeed.height*(18f/90f),ddx,ddy);
			movSpeedMax.position = new Vector2(movSpeed.xMin+movSpeedMax.xMin,movSpeed.yMin+movSpeedMax.yMin);
			GUI.Box(movSpeedMax,"15");
			
			//Min mov speed
			Rect movSpeedMin = new Rect(0f,movSpeed.height*(9f/90f),ddx,ddy);
			movSpeedMin.position = new Vector2(movSpeedMax.xMin+movSpeedMin.xMin,movSpeedMax.yMax+movSpeedMin.yMin);
			GUI.Box(movSpeedMin,"4");
			
			//Increase mov speed button
			Rect plusMovSpeed = new Rect(dd0x,movSpeed.height*(18f/120f),ddx,ddy2);
			plusMovSpeed.position = new Vector2(movSpeedMax.xMax+plusMovSpeed.xMin,movSpeed.yMin+plusMovSpeed.yMin);
			if(GUI.Button(plusMovSpeed,upGreen)){
				if(playerMovement.speed<15){
					if(playerSkills.utilitySkillPointCount>0){
						playerSkills.utilitySkillPointCount--;
						playerSkills.utilitySkillPointSpent++;
						playerMovement.speed += 0.5f;
						movSpeedPoints++;
					}
				}
			}

			if(movSpeed.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerMovement.speed<15){
						if(playerSkills.utilitySkillPointCount>0){
							playerSkills.utilitySkillPointCount--;
							playerSkills.utilitySkillPointSpent++;
							playerMovement.speed += 0.5f;
							movSpeedPoints++;
						}
					}
				}
			}
			
			//Actual mov speed
			Rect playerMovSpeed = new Rect(0f,movSpeed.height*(9f/120f),ddx,ddy2);
			playerMovSpeed.position = new Vector2(plusMovSpeed.xMin+playerMovSpeed.xMin,plusMovSpeed.yMax+playerMovSpeed.yMin);
			GUI.Box(playerMovSpeed,""+playerMovement.speed.ToString("F1"));
			
			//Decrease mov speed button
			Rect minusMovSpeed = new Rect(0,movSpeed.width*(9f/120f),ddx,ddy2);
			minusMovSpeed.position = new Vector2(playerMovSpeed.xMin+minusMovSpeed.xMin,playerMovSpeed.yMax+minusMovSpeed.yMin);
			if(GUI.Button(minusMovSpeed,downRed)){
				if(playerMovement.speed>4){
					if(playerSkills.utilitySkillPointSpent>0){
						playerSkills.utilitySkillPointCount++;
						playerSkills.utilitySkillPointSpent--;
						playerMovement.speed -= 0.5f;
						movSpeedPoints--;
					}
				}
			}

			if(movSpeed.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerMovement.speed>4){
						if(playerSkills.utilitySkillPointSpent>0){
							playerSkills.utilitySkillPointCount++;
							playerSkills.utilitySkillPointSpent--;
							playerMovement.speed -= 0.5f;
							movSpeedPoints--;
						}
					}
				}
			}
			
			//points spent
			Rect pointsMovSpeed = new Rect(movSpeed.width*(1f/10f),10f,movSpeed.width*(8f/10f),movSpeed.height*(1f/10f)+10f);
			pointsMovSpeed.position = new Vector2(movSpeed.xMin+pointsMovSpeed.xMin,movSpeed.yMax*(9f/10f)+pointsMovSpeed.yMin);
			GUI.Box(pointsMovSpeed,movSpeedPoints+" points");
			
			
			///////////////////////////////////////////////////////////////////////
			GUI.skin = skillsWindowGUISkin;
			//Hook Speed Box
			Rect hookSpeed = new Rect(d0x,0f,dx,utilitySkills.height-40f);
			hookSpeed.position = new Vector2(movSpeed.xMin+hookSpeed.xMax,movSpeed.yMin+hookSpeed.yMin);
			
			GUI.Box(hookSpeed,"Hook Speed Bonus");

			//Inside hook speed Box
			GUI.skin = skillsSubWindowGUISkin;
			//Max hook speed
			Rect hookSpeedMax = new Rect(dd0x,hookSpeed.height*(18f/90f),ddx,ddy);
			hookSpeedMax.position = new Vector2(hookSpeed.xMin+hookSpeedMax.xMin,hookSpeed.yMin+hookSpeedMax.yMin);
			GUI.Box(hookSpeedMax,"40");
			
			//Min hook speed
			Rect hookSpeedMin = new Rect(0f,hookSpeed.height*(9f/90f),ddx,ddy);
			hookSpeedMin.position = new Vector2(hookSpeedMax.xMin+hookSpeedMin.xMin,hookSpeedMax.yMax+hookSpeedMin.yMin);
			GUI.Box(hookSpeedMin,"0");
			
			//Increase hook speed button
			Rect plusHookSpeed = new Rect(dd0x,hookSpeed.height*(18f/120f),ddx,ddy2);
			plusHookSpeed.position = new Vector2(hookSpeedMax.xMax+plusHookSpeed.xMin,hookSpeed.yMin+plusHookSpeed.yMin);
			if(GUI.Button(plusHookSpeed,upGreen)){
				if(playerStats.hookSpeed<40){
					if(playerSkills.utilitySkillPointCount>0){
						playerSkills.utilitySkillPointCount--;
						playerSkills.utilitySkillPointSpent++;
						playerStats.hookSpeed += 1;
						hookSpeedPoints++;
					}
				}
			}

			if(hookSpeed.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerStats.hookSpeed<40){
						if(playerSkills.utilitySkillPointCount>0){
							playerSkills.utilitySkillPointCount--;
							playerSkills.utilitySkillPointSpent++;
							playerStats.hookSpeed += 1;
							hookSpeedPoints++;
						}
					}
				}
			}
			
			//Actual hook speed
			Rect playerHookSpeed = new Rect(0f,hookSpeed.height*(9f/120f),ddx,ddy2);
			playerHookSpeed.position = new Vector2(plusHookSpeed.xMin+playerHookSpeed.xMin,plusHookSpeed.yMax+playerHookSpeed.yMin);
			GUI.Box(playerHookSpeed,""+playerStats.hookSpeed.ToString("F0"));
			
			//Decrease hook speed button
			Rect minusHookSpeed = new Rect(0,hookSpeed.width*(9f/120f),ddx,ddy2);
			minusHookSpeed.position = new Vector2(playerHookSpeed.xMin+minusHookSpeed.xMin,playerHookSpeed.yMax+minusHookSpeed.yMin);
			if(GUI.Button(minusHookSpeed,downRed)){
				if(playerStats.hookSpeed>0){
					if(playerSkills.utilitySkillPointSpent>0){
						playerSkills.utilitySkillPointCount++;
						playerSkills.utilitySkillPointSpent--;
						playerStats.hookSpeed -= 1;
						hookSpeedPoints--;
					}
				}
			}

			if(hookSpeed.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerStats.hookSpeed>0){
						if(playerSkills.utilitySkillPointSpent>0){
							playerSkills.utilitySkillPointCount++;
							playerSkills.utilitySkillPointSpent--;
							playerStats.hookSpeed -= 1;
							hookSpeedPoints--;
						}
					}
				}
			}
			
			//points spent
			Rect pointsHookSpeed = new Rect(hookSpeed.width*(1f/10f),10f,hookSpeed.width*(8f/10f),hookSpeed.height*(1f/10f)+10f);
			pointsHookSpeed.position = new Vector2(hookSpeed.xMin+pointsHookSpeed.xMin,hookSpeed.yMax*(9f/10f)+pointsHookSpeed.yMin);
			GUI.Box(pointsHookSpeed,hookSpeedPoints+" points");
			
			
			///////////////////////////////////////////////////////////////////////


			GUI.skin = skillsWindowGUISkin;
			//Jump Force Box
			Rect jumpForce = new Rect(d0x,0f,dx,utilitySkills.height-40f);
			jumpForce.position = new Vector2(hookSpeed.xMin+jumpForce.xMax,hookSpeed.yMin+jumpForce.yMin);
			
			GUI.Box(jumpForce,"Jump Force");

			//Inside jump force Box
			GUI.skin = skillsSubWindowGUISkin;
			//Max jump force
			Rect jumpForceMax = new Rect(dd0x,jumpForce.height*(18f/90f),ddx,ddy);
			jumpForceMax.position = new Vector2(jumpForce.xMin+jumpForceMax.xMin,jumpForce.yMin+jumpForceMax.yMin);
			GUI.Box(jumpForceMax,"25");
			
			//Min jump Force
			Rect jumpForceMin = new Rect(0f,jumpForce.height*(9f/90f),ddx,ddy);
			jumpForceMin.position = new Vector2(jumpForceMax.xMin+jumpForceMin.xMin,jumpForceMax.yMax+jumpForceMin.yMin);
			GUI.Box(jumpForceMin,"12");
			
			//Increase jump force button
			Rect plusJumpForce = new Rect(dd0x,jumpForce.height*(18f/120f),ddx,ddy2);
			plusJumpForce.position = new Vector2(jumpForceMax.xMax+plusJumpForce.xMin,jumpForce.yMin+plusJumpForce.yMin);
			if(GUI.Button(plusJumpForce,upGreen)){
				if(playerMovement.jump<25){
					if(playerSkills.utilitySkillPointCount>0){
						playerSkills.utilitySkillPointCount--;
						playerSkills.utilitySkillPointSpent++;
						playerMovement.jump += 0.5f;
						jumpForcePoints++;
					}
				}
			}

			if(jumpForce.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerMovement.jump<25){
						if(playerSkills.utilitySkillPointCount>0){
							playerSkills.utilitySkillPointCount--;
							playerSkills.utilitySkillPointSpent++;
							playerMovement.jump += 0.5f;
							jumpForcePoints++;
						}
					}
				}
			}
			
			//Actual jump force
			Rect playerJumpForce = new Rect(0f,jumpForce.height*(9f/120f),ddx,ddy2);
			playerJumpForce.position = new Vector2(plusJumpForce.xMin+playerJumpForce.xMin,plusJumpForce.yMax+playerJumpForce.yMin);
			GUI.Box(playerJumpForce,""+playerMovement.jump.ToString("F1"));
			
			//Decrease jump force button
			Rect minusJumpForce = new Rect(0,jumpForce.width*(9f/120f),ddx,ddy2);
			minusJumpForce.position = new Vector2(playerJumpForce.xMin+minusJumpForce.xMin,playerJumpForce.yMax+minusJumpForce.yMin);
			if(GUI.Button(minusJumpForce,downRed)){
				if(playerMovement.jump>12){
					if(playerSkills.utilitySkillPointSpent>0){
						playerSkills.utilitySkillPointCount++;
						playerSkills.utilitySkillPointSpent--;
						playerMovement.jump -= 0.5f;
						jumpForcePoints--;
					}
				}
			}

			if(jumpForce.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerMovement.jump>12){
						if(playerSkills.utilitySkillPointSpent>0){
							playerSkills.utilitySkillPointCount++;
							playerSkills.utilitySkillPointSpent--;
							playerMovement.jump -= 0.5f;
							jumpForcePoints--;
						}
					}
				}
			}
			
			//points spent
			Rect pointsJumpForce = new Rect(jumpForce.width*(1f/10f),10f,jumpForce.width*(8f/10f),jumpForce.height*(1f/10f)+10f);
			pointsJumpForce.position = new Vector2(jumpForce.xMin+pointsJumpForce.xMin,jumpForce.yMax*(9f/10f)+pointsJumpForce.yMin);
			GUI.Box(pointsJumpForce,jumpForcePoints+" points");
			
			
			///////////////////////////////////////////////////////////////////////

			GUI.skin = skillsWindowGUISkin;
			//Cooldown dash Box
			Rect dashCD = new Rect(d0x,0f,dx,utilitySkills.height-40f);
			dashCD.position = new Vector2(jumpForce.xMin+dashCD.xMax,jumpForce.yMin+dashCD.yMin);
			
			GUI.Box(dashCD,"Dash CD");

			//Inside dash cd Box
			GUI.skin = skillsSubWindowGUISkin;
			//Max dash cd
			Rect dashCDMax = new Rect(dd0x,dashCD.height*(18f/90f),ddx,ddy);
			dashCDMax.position = new Vector2(dashCD.xMin+dashCDMax.xMin,dashCD.yMin+dashCDMax.yMin);
			GUI.Box(dashCDMax,"3");
			
			//Min dash cd
			Rect dashCDMin = new Rect(0f,dashCD.height*(9f/90f),ddx,ddy);
			dashCDMin.position = new Vector2(dashCDMax.xMin+dashCDMin.xMin,dashCDMax.yMax+dashCDMin.yMin);
			GUI.Box(dashCDMin,"0.5");
			
			//Increase dash cd button
			Rect plusDashCD = new Rect(dd0x,dashCD.height*(18f/120f),ddx,ddy2);
			plusDashCD.position = new Vector2(dashCDMax.xMax+plusDashCD.xMin,dashCD.yMin+plusDashCD.yMin);
			if(GUI.Button(plusDashCD,downGreen)){
				if(playerSkills.dashCoolDown>0.55){
					if(playerSkills.utilitySkillPointCount>0){
						playerSkills.utilitySkillPointCount--;
						playerSkills.utilitySkillPointSpent++;
						playerSkills.dashCoolDown -= 0.1f;
						dashCDPoints++;
					}
				}
			}

			if(dashCD.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")>0){
					if(playerSkills.dashCoolDown>0.55){
						if(playerSkills.utilitySkillPointCount>0){
							playerSkills.utilitySkillPointCount--;
							playerSkills.utilitySkillPointSpent++;
							playerSkills.dashCoolDown -= 0.1f;
							dashCDPoints++;
						}
					}
				}
			}


			//Actual dash cd
			Rect playerDashCD = new Rect(0f,dashCD.height*(9f/120f),ddx,ddy2);
			playerDashCD.position = new Vector2(plusDashCD.xMin+playerDashCD.xMin,plusDashCD.yMax+playerDashCD.yMin);
			GUI.Box(playerDashCD,""+playerSkills.dashCoolDown.ToString("F1"));
			
			//Decrease dash cd button
			Rect minusDashCD = new Rect(0,dashCD.width*(9f/120f),ddx,ddy2);
			minusDashCD.position = new Vector2(playerDashCD.xMin+minusDashCD.xMin,playerDashCD.yMax+minusDashCD.yMin);
			if(GUI.Button(minusDashCD,upRed)){
				if(playerSkills.dashCoolDown<3){
					if(playerSkills.utilitySkillPointSpent>0){
						playerSkills.utilitySkillPointCount++;
						playerSkills.utilitySkillPointSpent--;
						playerSkills.dashCoolDown += 0.1f;
						dashCDPoints--;
					}
				}
			}

			if(dashCD.Contains(Event.current.mousePosition)){
				if(Input.GetAxis("Mouse ScrollWheel")<0){
					if(playerSkills.dashCoolDown<3){
						if(playerSkills.utilitySkillPointSpent>0){
							playerSkills.utilitySkillPointCount++;
							playerSkills.utilitySkillPointSpent--;
							playerSkills.dashCoolDown += 0.1f;
							dashCDPoints--;
						}
					}
				}
			}
			
			//points spent
			Rect pointsDashCD = new Rect(dashCD.width*(1f/10f),10f,dashCD.width*(8f/10f),dashCD.height*(1f/10f)+10f);
			pointsDashCD.position = new Vector2(dashCD.xMin+pointsDashCD.xMin,dashCD.yMax*(9f/10f)+pointsDashCD.yMin);
			GUI.Box(pointsDashCD,dashCDPoints+" points");
			
			
			///////////////////////////////////////////////////////////////////////
		}
	}
}

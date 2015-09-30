using UnityEngine;
using System.Collections;

public class Evasao : MonoBehaviour {

	private GameObject bullet;
	private BossSensor sensor;
	private Vector2 bulletPosition;
	private Vector2 bulletSpeed;


	public float sensingTime;
	private float timer;

	private Rigidbody2D body;

	private float maxScale;
	private float minScale;
	
	private float velScaler;

	public float monsterDimension; // Com relaçao ao sprite do monstro
	private float monsterDimensionScale;

	// Use this for initialization
	void Start () {	
		sensor = GameObject.Find ("BossEye/Sensor").GetComponent<BossSensor>();
		body = GetComponent<Rigidbody2D> ();

		timer = 0.0f;

		maxScale = 3.1f;
		minScale = 1.0f;
		monsterDimensionScale = monsterDimension*transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer>sensingTime){
			sensor.sensedBullet = false;
			timer = 0.0f;
		}

		if(sensor.sensedBullet){
			if(sensor.getBullet()!=null){
				bullet = sensor.getBullet();
				bulletPosition = bullet.GetComponent<Transform>().position;
				bulletSpeed = bullet.GetComponent<Rigidbody2D>().velocity;

				float dx = transform.position.x - bulletPosition.x;
				float t = dx/bulletSpeed.x;

				float yf = bulletPosition.y + bulletSpeed.y*t;

				float maxYB = transform.position.y + (monsterDimensionScale/2);
				float minYB = transform.position.y - (monsterDimensionScale/2);

				if((yf>minYB)&&(yf<maxYB)){
					velScaler = 10.0f*(maxScale - transform.localScale.x);				
					float rnd = 0.5f-Random.value;
					rnd = rnd/Mathf.Abs(rnd);
					body.AddForce(velScaler*rnd*body.mass*Vector2.down,ForceMode2D.Impulse);
				}
			}
		}

	}

	void OnCollisionEnter2D(Collision2D col){
		//Layer 10 = PlayerAmmo
		if(col.gameObject.layer == 10){
			sensor.sensedBullet = false;

			if(transform.localScale.x>minScale){
				Vector3 scl = transform.localScale;
				scl.x -= 0.1f;
				scl.y -= 0.1f;
				transform.localScale = scl;
				monsterDimensionScale = monsterDimension*transform.localScale.x;
			}
		}
	}
}

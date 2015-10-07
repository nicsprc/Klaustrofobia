using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private GameObject player;
	private Vector3 playerDir;
	private Vector3 originalPos;

	public bool cameraPan;
	
	// How long the object should shake for.
	public float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	// Zoom
	private float camZoom;
	public bool canZoom;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		cameraPan = false;

		camZoom = 5.0f;
		canZoom = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		playerDir = Input.mousePosition;
		playerDir = Camera.main.ScreenToWorldPoint(playerDir);
		playerDir = playerDir - player.transform.position;
		playerDir = playerDir/Mathf.Sqrt(Mathf.Pow(playerDir.x,2)+Mathf.Pow(playerDir.y,2));

		// Camera movement. Follow the player and also moves a little ahead follow the mouse position.
		float c;
		if (cameraPan) {
			c = 1.8f;
		} else {
			c = 0f;
		}
		transform.position = new Vector3(player.transform.position.x+playerDir.x*c,player.transform.position.y+playerDir.y*c,-10);

		originalPos = transform.position;

		if (shake > 0)
		{
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
			transform.localPosition = originalPos;
		}

		// Zoom
		if(canZoom){
			if(Input.GetAxis("Mouse ScrollWheel")<0.0f){
				if(camZoom<9.9f){
					camZoom += 0.2f;
				}
			}
			if(Input.GetAxis("Mouse ScrollWheel")>0.0f){
				if(camZoom>1.1f){
					camZoom -= 0.2f;
				}
			}
			if(Input.GetKey(KeyCode.J)){
				camZoom = 5.0f;
			}
		}
		Camera.main.orthographicSize = camZoom;
	}

	Vector3 getOriginalPos(){
		return originalPos;
	}
}

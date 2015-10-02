using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		if (GUI.Button (new Rect (2 * Screen.width / 5, Screen.height / 3, Screen.width / 5, 50), "Start Game")) {
			Application.LoadLevel("Level1");
		}

		if (GUI.Button (new Rect (2 * Screen.width / 5, (Screen.height / 3) + 50 + 20, Screen.width / 5, 50), "Start Tutorial")) {
			Application.LoadLevel("TutorialLevel");
		}

		if (GUI.Button (new Rect (2 * Screen.width / 5, (Screen.height / 3) + 100 + 40, Screen.width / 5, 50), "Exit Game")) {
			Application.Quit();
		}
	}
}

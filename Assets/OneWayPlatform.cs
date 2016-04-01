using UnityEngine;
using System.Collections;

public class OneWayPlatform : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player = 11
        //HookPlatform = 21
        //Platform = 23
        Physics2D.IgnoreLayerCollision(11, 23, GetComponent<Rigidbody2D>().velocity.y > 0);
    }
}

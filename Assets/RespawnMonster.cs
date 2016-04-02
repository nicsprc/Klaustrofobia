using UnityEngine;
using System.Collections;

public class RespawnMonster : MonoBehaviour {

    
    public GameObject monster;
    public bool doRespawn;    

    private BoxCollider2D box;
    private GameObject instMonster;

    // Use this for initialization
    void Start () {
        doRespawn = false;
        box = GetComponent<BoxCollider2D>();

        
        float rx = Mathf.Round(box.size.x * Random.value);
        float ry = Mathf.Round(box.size.y * Random.value);

        Vector3 finalPos;
        finalPos.x = box.transform.position.x -box.size.x/2 + rx;
        finalPos.y = box.transform.position.y +box.size.y/2 - ry;
        finalPos.z = 1;
        instMonster = (GameObject)Instantiate(monster, finalPos, transform.rotation);
        instMonster.transform.parent = transform;        
    }
	
	// Update is called once per frame
	void Update () {
        if (doRespawn)
        {            
            Start();
        }	
	}
}

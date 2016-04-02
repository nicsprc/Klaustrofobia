using UnityEngine;
using System.Collections;

public class chestBehavior : MonoBehaviour {

    public GameObject[] respawnPoints;

    private RespawnMonster[] resMonster;

    private bool openChest;
    private Animator animator;


	// Use this for initialization
	void Start () {
        openChest = false;
        animator = GetComponent<Animator>();

        resMonster = new RespawnMonster[respawnPoints.Length];
        for (int i=0; i<respawnPoints.Length; i++)
        {
            resMonster[i] = respawnPoints[i].GetComponent<RespawnMonster>();
        }  
	}
	
	// Update is called once per frame
	void Update () {
        if (openChest)
        {
            for (int i=0; i<resMonster.Length; i++)
            {
                resMonster[i].doRespawn = true;
            }
            openChest = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                openChest = true;
            }

            if (openChest)
            {
                animator.SetBool("isOpen",true);
            }
        }
    }
}

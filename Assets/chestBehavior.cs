using UnityEngine;
using System.Collections;

public class chestBehavior : MonoBehaviour {

    private bool openChest;
    private Animator animator;


	// Use this for initialization
	void Start () {
        openChest = false;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
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

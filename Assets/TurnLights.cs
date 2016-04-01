using UnityEngine;
using System.Collections;

public class TurnLights : MonoBehaviour
{

    public GameObject[] ObjLights;
    private Light[] lights;
    private bool turnOn;
    private AudioSource audioSou;
    public AudioClip clip;
    

    // Use this for initialization
    void Start()
    {
        turnOn = false;        
        lights = new Light[ObjLights.Length];
        audioSou = GetComponent<AudioSource>();

        for (int i = 0; i < ObjLights.Length; i++)
        {
            lights[i] = ObjLights[i].GetComponent<Light>();            
        }
    }

    void Update()
    {
               
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            if (Input.GetButtonDown("Interact"))
            {
                turnOn = true;
                audioSou.PlayOneShot(clip);
            }
            if (Input.GetButtonUp("Interact"))
            {
                turnOn = false;
            }
            if (turnOn)
            {
                for (int i=0; i<ObjLights.Length; i++)
                {
                    lights[i].enabled = true;                    
                }                
            }            
        }
    }
}
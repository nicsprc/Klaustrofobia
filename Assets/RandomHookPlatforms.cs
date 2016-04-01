using UnityEngine;
using System.Collections;

public class RandomHookPlatforms : MonoBehaviour {

    public GameObject hookPlatform;
    public GameObject thisPlatform;
    public int nOfPlatforms;
    private GameObject plat;

    private Vector2 initialPos;
    private Vector2 spaceRange;
    private Vector3 finalPos;

    // Use this for initialization
    void Start () {
        initialPos.x = transform.position.x + 2.0f;
        initialPos.y = transform.position.y - 2.0f;

        spaceRange.x = 100.0f - 4.0f - 10.0f; //Total - bordas - tamanhoPlataforma.x
        spaceRange.y = 40.0f - 4.0f -5.0f;

        for (int i=0; i<nOfPlatforms; i++)
        {
            float rx = Mathf.Round(spaceRange.x * Random.value);
            float ry = Mathf.Round(spaceRange.y * Random.value);

            
            finalPos.x = initialPos.x + rx;
            finalPos.y = initialPos.y - ry;

            plat = (GameObject)Instantiate(hookPlatform, finalPos, transform.rotation);

            plat.transform.parent = thisPlatform.transform;

            Debug.Log(finalPos.x + "/" + finalPos.y);
        }
        Debug.Log(initialPos.x + "/" + initialPos.y);
        Debug.Log(spaceRange.x+"/"+spaceRange.y);
    }		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Update()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("First").GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void Start()
    {
        if (BabyladeGameManager.BabyladeGameManagerScript.playerCount == 1)
        {
            gameObject.tag = "FirstCollider";

        }
        else if (BabyladeGameManager.BabyladeGameManagerScript.playerCount == 2)
        {
            gameObject.tag = "SecondCollider";
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("SecondCollider"))
        {
            Vector3 a = gameObject.transform.position;
            Vector3 b = other.gameObject.transform.position;
            float firstSpeed = GameObject.FindGameObjectWithTag("First").GetComponent<Rigidbody>().velocity.magnitude;
            float secondSpeed = GameObject.FindGameObjectWithTag("Second").GetComponent<Rigidbody>().velocity.magnitude;
            BabyladeGameManager.BabyladeGameManagerScript.sparkCreate(a,b,firstSpeed,secondSpeed);
            BabyladeGameManager.BabyladeGameManagerScript.reverseSpeedAfterCollision(GameObject.FindGameObjectWithTag("First").GetComponent<Rigidbody>().velocity,
                GameObject.FindGameObjectWithTag("Second").GetComponent<Rigidbody>().velocity);

        }
    }
}

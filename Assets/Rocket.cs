using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space)) {
            rigidbody.AddRelativeForce(Vector3.up);
            Debug.Log("Thrust.");
        }
        if (Input.GetKey(KeyCode.A)) {
            Debug.Log("Rotate left");
        } else if (Input.GetKey(KeyCode.D)) {
            Debug.Log("Rotate right.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    Rigidbody rigidbody;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space)) {
            if(!audio.isPlaying) {
                audio.Play();
            }
            rigidbody.AddRelativeForce(Vector3.up);
        }
        if(Input.GetKeyUp(KeyCode.Space)) {
            audio.Stop();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(-Vector3.forward);
        }
    }
}

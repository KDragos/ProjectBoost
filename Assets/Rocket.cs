using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidbody;
    private AudioSource audio;
    public ParticleSystem particles;
    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float mainThrust = 100f;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Friendly":
                Debug.Log("Ok");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                Debug.Log("Dead");
                break;
        }
    }

    void Rotate()
    {
        rigidbody.freezeRotation = true;
        float rcsThrust = 100f;
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) {
            if (!audio.isPlaying) {
                audio.Play();
            }
            if(!particles.isPlaying) {
                particles.Play();
            }
            //Debug.Log("Thrust");
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            audio.Stop();
            particles.Stop();
            //Debug.Log("Stop thrust");
        }
    }
}

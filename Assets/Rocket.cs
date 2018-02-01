using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidbody;
    private AudioSource audio;
    public ParticleSystem particles;
    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float mainThrust = 100f;
    [SerializeField]
    AudioClip mainEngine;
    [SerializeField]
    AudioClip deathSound;
    [SerializeField]
    AudioClip trancendSound;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(state.Equals(State.Alive)) {
            RespondToThrustInput();
            RespondToRotateInput();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(!state.Equals(State.Alive)) { return; }

        switch (collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                audio.PlayOneShot(trancendSound);
                Invoke("LoadNextLevel", 1f);
                LoadNextLevel();
                break;
            default:
                state = State.Dying;
                audio.PlayOneShot(deathSound);
                Invoke("ReloadCurrentLevel", 1f);
                break;
        }
    }

    private void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void RespondToRotateInput()
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

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } else {
            audio.Stop();
            particles.Stop();
        }
    }

    private void ApplyThrust()
    {
        if (!audio.isPlaying) {
            audio.PlayOneShot(mainEngine);
        }
        if (!particles.isPlaying) {
            particles.Play();
        }
        //Debug.Log("Thrust");
        rigidbody.AddRelativeForce(Vector3.up * mainThrust);
    }
}

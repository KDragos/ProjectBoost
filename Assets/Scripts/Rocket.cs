using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidbody;
    private AudioSource audio;
    public ParticleSystem[] particles;
    public AudioClip[] sounds;

    [SerializeField]
    float levelLoadDelay = 2;
    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float mainThrust = 100f;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}
	
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
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audio.Stop();
        audio.PlayOneShot(sounds[1]);
        particles[1].Play();
        Invoke("LoadNextLevel", levelLoadDelay);
        LoadNextLevel();
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audio.Stop();
        particles[2].Play();
        audio.PlayOneShot(sounds[2]);
        Invoke("ReloadCurrentLevel", levelLoadDelay);
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
            particles[0].Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audio.isPlaying) {
            audio.PlayOneShot(sounds[0]);
        }
        if (!particles[0].isPlaying) {
            particles[0].Play();
        }
        //Debug.Log("Thrust");
    }
}

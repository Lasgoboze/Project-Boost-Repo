using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;
    [SerializeField]float delayInSeconds = 3f;
    AudioSource audioSource;
    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning)
        {
            return;
        }        

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You've bumped into a friendly object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    void StartSuccessSequence()
    {
        //todo add Particle effect upon crash.
        isTransitioning = true;
        audioSource.Stop();
        Invoke ("LoadNextLevel", delayInSeconds);
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(success);

    }

    void StartCrashSequence()
    {
        //todo add Particle effect upon crash. 
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", delayInSeconds);
        audioSource.PlayOneShot(explosion);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}

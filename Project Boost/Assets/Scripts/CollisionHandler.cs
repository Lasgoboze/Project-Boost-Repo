using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delayInSeconds = 1f;
    private void OnCollisionEnter(Collision other) 
    {
        
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
        //todo add SFX upon crash.
        //todo add Particle effect upon crash.
        Invoke ("LoadNextLevel", delayInSeconds);
        GetComponent<Movement>().enabled = false;
    }

    void StartCrashSequence()
    {
        //todo add SFX upon crash.
        //todo add Particle effect upon crash. 
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", delayInSeconds);
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

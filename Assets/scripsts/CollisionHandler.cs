using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] ParticleSystem successParicle;
    [SerializeField] ParticleSystem crashParticle;

    bool isTransitioning = false; 


    private void OnCollisionEnter(Collision other)
    {
      if (isTransitioning)
        {
            return;
        }
      switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Freindly");
                break;
            case "Finish":
                StartSuccessSequency();
                break;
            default:
                StartCrashSequency();
                break;
        }
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

    void StartCrashSequency()
    {
        isTransitioning = true;
        successParicle.Play();
        GetComponent<movment>().enabled = false;
        Invoke ("ReloadLevel",levelLoadDelay);
    }

    void StartSuccessSequency()
    {
        isTransitioning = true;
        crashParticle.Play();
        GetComponent<movment>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
}

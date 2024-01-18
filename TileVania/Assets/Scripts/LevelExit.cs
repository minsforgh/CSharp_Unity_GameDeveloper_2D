using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    //코루틴 사용
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            StartCoroutine(LoadNextLevel());
    }

    //최대 scene Index 도달시 다시 0으로
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(1);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        FindObjectOfType<ScenePersist>().resetScenePersist();
        
        SceneManager.LoadScene(nextSceneIndex);

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{   
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    bool hasCrash = false;

    void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag == "Ground" && !hasCrash)
        {   
            hasCrash = true;

            //FindObjectOfType은 현재 Hierarchy 내에서 찾는다.  
            FindObjectOfType<PlayerController>().DisableControls();

            //GetComponent는 현재 객체 내에서 해당 컴포넌트를 찾는다.
            //이번에는 PlayerController 와 CrashDetection 이 같은 Barry의 컴포넌트로 존재하기 때문에 둘의 결과가 같을 수 있었다.
            //GetComponent<PlayerController>().DisableControls();
            
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", reloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}

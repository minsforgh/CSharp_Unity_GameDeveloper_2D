using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;

    //static은 한 class의 instance 끼리 공유하는, 하나만 존재하는 
    static AudioPlayer instance;

    public AudioPlayer GetInstance()
    {
        return instance;
    }
    
    void Awake()
    {
        ManageSingleTon();
    }

    //싱글톤 패턴
    void ManageSingleTon()
    {   
        //제네릭 이용하지 않고, GetType을 통해 타입 전달하는 버전. 이렇게 짤 경우 재사용성이 좋다.
        // int instanceCount = FindObjectsOfType(GetType()).Length;   
        // if(instanceCount > 1)
        if(instance != null)
        {      
            //만약의 경우에 대비해서 비활성화 후 파괴 (파괴 전에 접근 가능성 없지 않음)
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {   
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    
    public void PlayShootingClip()
    {   
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
       PlayClip(damageClip, damageVolume);
    }
    
    void PlayClip(AudioClip clip, float volume)
    {
        Vector3 cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
    }
}

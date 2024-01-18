using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shooting은 Player 뿐만 아니라 Enemy도 하므로, Player의 OnFire가 아닌 Shooter 클래스를 새로 만들어서 실제 동작 구현
public class Shooter : MonoBehaviour
{   
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float preojectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVaricance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    //Player - OnFire에서는 isFiring만을 조작한다.
    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
    }

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            
            Rigidbody2D rb= projectile.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(projectile, preojectileLifetime);

            // WaveConfigSO의 GetRadomSpawnTime 과 유사한 방식으로 사격 간격
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVaricance, baseFiringRate + firingRateVaricance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;

    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    //Pathfinder에서 사용
    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    //Instantiate를 사용하여 currentWave의 Enemy spawn, position은 StartingWayPoint(시작 점), rotation은 중요하지 않으니 Quaternion.identity
    //4번째는 부모의 transform, 해당 부모의 자식으로 생성됨
    //코루틴 사용하여 지연 시간
    //While - 전체 무한 루프 , foreach - Wave들 순회, for - Enemy들 순회
    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO w in waveConfigs)
            {
                currentWave = w;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWayPoint().position, Quaternion.Euler(0, 0, 180), transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }while(isLooping);
    }
}

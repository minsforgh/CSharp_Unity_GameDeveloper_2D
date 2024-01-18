using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{   
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariant = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;


    public int GetEnemyCount()
    {
        return  enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    //GetChild는 Transform의 메소드, (부모 - 자식) 관계는 매개
    public Transform GetStartingWayPoint()
    {
        return pathPrefab.GetChild(0);
    }

    //for each loop 사용
    public List<Transform> GetWayPoints()
    {   
        List<Transform> wayPoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            wayPoints.Add(child);
        }
        return wayPoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    //랜덤한 (2 * spawnTimeVariant의 gap이 있는) spawnTime을 구해준다
    //단 음수가 되면 안되기 때문에, 미리 정한 minimupSpawnTime 이상의 값을 갖도록 Clamp 이용
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariant, timeBetweenEnemySpawns + spawnTimeVariant);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}

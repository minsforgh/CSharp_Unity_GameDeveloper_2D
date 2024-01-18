using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{   
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> wayPoints;
    int wayPointIndex = 0 ;

    //EnemySpanwer에서 CurrentWave를 공유받아서 사용, 두 곳에서 같은 Wave를 다뤄야 하기 때문
    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    //초기 WayPoint 위치로
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].position;
    }

    
    void Update()
    {
        FollowPath();
    }

    //MoveTowards 이용하여 다음 WayPoint로 등속 운동 (delta 만큼)
    //MoveTowards 한번 호출당 delta 만큼 이동, 시작 위치는 transform.position(현재 위치)로 매번 갱신 (매 프레임 delta 속도로 이동)
    void FollowPath()
    {
        if(wayPointIndex < wayPoints.Count)
        {
            Vector3 targetPosition = wayPoints[wayPointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

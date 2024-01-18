using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] int score = 0;

    //Play 버튼 누를 시 가정 먼저 호출됨
    //싱글톤 패턴
    void Awake()
    {   
        //현재 GameSession의 수
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        // 1초과 == 현재 session과 별개로 다른 session 이미 존재 => 현재 session 파괴
        if(numGameSession > 1)
        {
            Destroy(gameObject);
        }
        //현재 session이 유일한 session => scene 전환돼도 파괴되지 않게 DontDestroyOnLoad (싱글톤)
        else
        {
            DontDestroyOnLoad(gameObject);  
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {   
        FindObjectOfType<ScenePersist>().resetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = score.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;

    public float fillFraction;
    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion = false;

    void Update()
    {
        UpdateTimer();
    }

    public void CancleTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(timerValue <= 0)
        {   
            if(isAnsweringQuestion)
            {   
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }   
            else
            {   
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true; 
            } 
        }
        else
        {
            if(isAnsweringQuestion)
            {   
                fillFraction = timerValue / timeToCompleteQuestion; 
            }   
            else
            {   
                fillFraction = timerValue / timeToShowCorrectAnswer;
            } 
        }

        Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UICountdownController : MonoBehaviour
{
    private Action CountDownComplete;

    public event Action OnCountDownComplete
    {
        add{ CountDownComplete += value; }
        remove{ CountDownComplete -= value; }
    }

    public Text countdownText;

    private float tick;
    private bool isComplete;

    public bool isIncreaseSpeed;
    public float increaseSpeed = 0.01f;

    public float startTime = 4;
    public float stopTime = 1f;
    public float speed = 1.5f;
    public float originalSpeed = 1.5f;

    public bool hasDelayStart;
    public float delayStartValue = 1.5f;
    private bool isStarted;

    // Use this for initialization
    void Start()
    {		
    }

    private void Update()
    {
        if (!isComplete && isStarted)
        {
            if (isIncreaseSpeed)
            {
                speed += increaseSpeed;
            }

            tick -= speed * Time.fixedDeltaTime;
            //Debug.Log("start tick: " + tick);
            countdownText.text = ((int)tick).ToString();
            if (tick <= stopTime)
            {
                isComplete = true;
                isStarted = false;
                if (null != CountDownComplete)
                {
                    CountDownComplete();
                    //Debug.Log("CountDownComplete inside");
                }
            }	
        }
    }

    public void StartCountDown()
    {
        tick = startTime;
        countdownText.text = tick.ToString();
        speed = originalSpeed;
        isStarted = false;

        if (hasDelayStart)
        {
            CancelInvoke("DelayStart");
            Invoke("DelayStart", delayStartValue);
        }
        else
        {
            StartTimer();
        }
    }

    private void DelayStart()
    {
        StartTimer();
    }

    private void StartTimer()
    {		
        isComplete = false;
        isStarted = true;
        //Debug.Log("start timer!!!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameRateController : MonoBehaviour
{
    private const string GAME_RATE_SAVE_KEY = "GAME_RATE_KEY";
    private const string GAME_RATE_DONT_SHOW_KEY = "GAME_RATE_DONT_SHOW_KEY";

    public const float RATE_CLICK_DELAY = 0.70f;

    public string googleBundleIdentifier = "com.gigadrillgames.fingervsants";
    public string ituneStoreAppID = "1175782438";
    public bool enableRateUS = true;
    public int totalGameCount = 10;

    private Action ClickRateNow;

    public event Action OnClickRateNow
    {
        add{ ClickRateNow += value;}
        remove{ ClickRateNow -= value;}
    }

    private Action ClickRateLater;

    public event Action OnClickRateLater
    {
        add{ ClickRateLater += value;}
        remove{ ClickRateLater -= value;}
    }

    private Action ClickDontShowRate;

    public event Action OnClickDontShowRate
    {
        add{ ClickDontShowRate += value;}
        remove{ ClickDontShowRate -= value;}
    }

    private bool disableClick;

    private void OnEnable()
    {
        disableClick = true;
        CancelInvoke("EnableClick");
        Invoke("EnableClick", RATE_CLICK_DELAY);
    }

    public void EnableClick()
    {
        disableClick = false;
    }

    public void ClickRateIt()
    {
        if (disableClick)
        {
            return;
        }

        SaveRate(GAME_RATE_SAVE_KEY, 1);

        // open url for rating
        #if UNITY_ANDROID
        Application.OpenURL("http://play.google.com/store/apps/details?id=" + googleBundleIdentifier);
        #elif UNITY_IOS
        Application.OpenURL("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=" + ituneStoreAppID + "&pageNumber=0&sortOrdering=2&type=Purple+Software&mt=8");
        #endif

        if (null != ClickRateNow)
        {
            ClickRateNow();
        }

        ShowHide(false);
    }

    public void ClickLater()
    {
        if (disableClick)
        {
            return;
        }

        if (null != ClickRateLater)
        {
            ClickRateLater();
        }

        ShowHide(false);
    }

    public void ClickNo()
    {
        if (disableClick)
        {
            return;
        }

        SaveRate(GAME_RATE_DONT_SHOW_KEY, 1);

        if (null != ClickDontShowRate)
        {
            ClickDontShowRate();
        }

        ShowHide(false);
    }

    public void ShowHide(bool val)
    {
        this.gameObject.SetActive(val);    
    }

    public bool CanShowRate()
    {
        if (!enableRateUS)
        {
            return false;
        }

        if (LoadSaveRate(GAME_RATE_SAVE_KEY) == 1 || LoadSaveRate(GAME_RATE_DONT_SHOW_KEY) == 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int GetTotalGameCount()
    {
        return totalGameCount;
    }

    // use this for deleting and for testing
    public void DeleteData()
    {
        DeleteSaveData(GAME_RATE_SAVE_KEY);
        DeleteSaveData(GAME_RATE_DONT_SHOW_KEY);
    }

    // for saving rate
    private void SaveRate(string saveDatakey, int val)
    {
        PlayerPrefs.SetInt(saveDatakey, val);
    }

    // for loading rate
    private int LoadSaveRate(string saveDatakey)
    {
        return PlayerPrefs.GetInt(saveDatakey, 0);
    }

    // for deleting rate
    private void DeleteSaveData(string saveDatakey)
    {
        PlayerPrefs.DeleteKey(saveDatakey);
    }
}

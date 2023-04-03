using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileServiceHelper:ICleanable
{
    private IMobile mobileService;
    private MonoBehaviour parent;
    private UIEventManager uiEventManager;

    public void Init(MonoBehaviour parent)
    {
        this.parent = parent;

        // mobile service
        #if UNITY_ANDROID
        mobileService = new AndroidService();
        mobileService.SetLocalNotificationListener(OnLocalNotificationLoadComplete);
        mobileService.Init(this.parent);
        #elif UNITY_IOS
        mobileService = new IOSService();
        mobileService.Init(this);
        #endif

        ServiceLocator.ProvideMobileService(mobileService);

        AddEventListener();
    }

    private void AddEventListener()
    {
        uiEventManager = UIEventManager.GetInstance();
        uiEventManager.OnShare += OnShare;
    }

    private void RemoveEventListener()
    {        
        uiEventManager.OnShare -= OnShare;
        uiEventManager = null;
    }

    public void Clean()
    {
        RemoveEventListener();
    }

    public void ScheduleLocalNotification()
    {
        if (mobileService != null)
        {
            //fire after 4 hours
            mobileService.ScheduleLocalNotificationShortTime(GameConfig.GAME_TITLE, GameConfig.LOCAL_NOTIFICATION_MESSAGE
                , GameConfig.LOCAL_NOTIFICATION_TICKER_MESSAGE, 0, 3, 0, 0);

            //fire after 8 hours
            mobileService.ScheduleLocalNotificationShortTime(GameConfig.GAME_TITLE, GameConfig.LOCAL_NOTIFICATION_MESSAGE2
                , GameConfig.LOCAL_NOTIFICATION_TICKER_MESSAGE, 0, 6, 0, 0);

            //fire after 12 hours
            mobileService.ScheduleLocalNotificationShortTime(GameConfig.GAME_TITLE, GameConfig.LOCAL_NOTIFICATION_MESSAGE3
                , GameConfig.LOCAL_NOTIFICATION_TICKER_MESSAGE, 0, 12, 0, 0);


            // set notification for 60 days!!
            for (int i = 1; i <= 60; i++)
            {
                //fire after i day
                mobileService.ScheduleLocalNotificationShortTime(GameConfig.GAME_TITLE, GameConfig.LOCAL_NOTIFICATION_MESSAGE3
                    , GameConfig.LOCAL_NOTIFICATION_TICKER_MESSAGE, i, 0, 0, 0);
            }

            Debug.Log("[Main] ScheduleLocalNotification ");

        }
    }

    public void Share()
    {
        if (mobileService != null)
        {
            mobileService.Share("FingerVSBugsShare.jpg", "FingerVSBugsShare");  
        }
    }

    private void OnLocalNotificationLoadComplete(bool status)
    {
        this.parent.Invoke("ScheduleLocalNotification", 3f);
        //ScheduleLocalNotification();
        Debug.Log("[Main] OnLocalNotificationLoadComplete status: " + status);
    }

    private void OnShare()
    {
        Share();
    }
}

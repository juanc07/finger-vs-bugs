using UnityEngine;
using System.Collections;

public class ServiceLocator
{
    // for saving data on player prefs
    private static ISave saveDataService;
    private static ISave nullSaveDataService;

    private static ITPCommon tpCommonService;
    private static ITPCommon nullITPCommonService;

    private static IMobile mobileService;
    private static IMobile nullMobileService;

    private static IGoogleAds adsService;
    private static IGoogleAds nullAdsService;

    private static IUnity3dAd unity3dAdService;
    private static IUnity3dAd nullUnity3dAdService;

    private static IDataShare dataShareService;
    private static IDataShare nullDataShareService;

    private static IAnalytics analyticsService;
    private static IAnalytics nullAnalyticsService;

    // init services into null equivalent service to avoid crashed when it's used and not yet initialized
    // this is good but sometimes hard to debug that's why inside each null service have a debug warning logs
    public static void Init()
    {
        ServiceLocator.nullSaveDataService = new NullSaveDataService();
        ServiceLocator.saveDataService = ServiceLocator.nullSaveDataService;

        ServiceLocator.nullITPCommonService = new NullITPCommon();
        ServiceLocator.tpCommonService = ServiceLocator.nullITPCommonService;

        ServiceLocator.nullMobileService = new NullMobileService();
        ServiceLocator.mobileService = ServiceLocator.nullMobileService;

        ServiceLocator.nullAdsService = new NullGoogleAdsService();
        ServiceLocator.adsService = ServiceLocator.nullAdsService;

        ServiceLocator.nullDataShareService = new NullDataShareService();
        ServiceLocator.dataShareService = ServiceLocator.nullDataShareService;

        ServiceLocator.nullAnalyticsService = new NullAnalyticsService();
        ServiceLocator.analyticsService = ServiceLocator.nullAnalyticsService;

        ServiceLocator.nullUnity3dAdService = new NullUnity3dAdService();
        ServiceLocator.unity3dAdService = ServiceLocator.nullUnity3dAdService;
    }

    // you can provide any save data service here as long as its implements ISave interface
    public static void ProvidePlayerPrefDataService(ISave saveDataService)
    {
        if (saveDataService != null)
        {			
            ServiceLocator.saveDataService = saveDataService;
        }
    }

    public static void ProvideTPCommonService(ITPCommon tpCommonService)
    {
        if (tpCommonService != null)
        {			
            ServiceLocator.tpCommonService = tpCommonService;
        }
    }

    public static void ProvideMobileService(IMobile mobileService)
    {
        if (mobileService != null)
        {			
            ServiceLocator.mobileService = mobileService;
        }
    }

    public static void ProvideGoogleAdService(IGoogleAds adsService)
    {
        if (adsService != null)
        {			
            ServiceLocator.adsService = adsService;
        }
    }

    public static void ProvideDataShareService(IDataShare dataShareService)
    {
        if (dataShareService != null)
        {			
            ServiceLocator.dataShareService = dataShareService;
        }
    }

    public static void ProvideAnalyticsService(IAnalytics analyticsService)
    {
        if (analyticsService != null)
        {			
            ServiceLocator.analyticsService = analyticsService;
        }
    }

    // provide unity3d ad service
    public static void ProvideUnity3dAdService(IUnity3dAd unity3dAdService)
    {
        if (unity3dAdService != null)
        {			
            ServiceLocator.unity3dAdService = unity3dAdService;
        }
    }

    // for getting the save data service
    public static ISave GetPlayerPrefDataService()
    {
        return saveDataService;
    }

    // for getting the TPCommon service
    public static ITPCommon GetTPCommonService()
    {
        return tpCommonService;
    }

    // for getting the mobile service
    public static IMobile GetMobileService()
    {
        return mobileService;
    }

    // for getting the ads service
    public static IGoogleAds GetGoogleAdService()
    {
        return adsService;
    }

    // for getting the data share service
    public static IDataShare GetDataShareService()
    {
        return dataShareService;
    }

    // get analytics service
    public static IAnalytics GetAnalyticsService()
    {
        return analyticsService;
    }

    // get unity3d service
    public static IUnity3dAd GetUnity3dAdService()
    {
        return unity3dAdService;
    }
}

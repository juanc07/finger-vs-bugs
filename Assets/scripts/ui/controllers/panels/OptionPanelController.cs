using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class OptionPanelController : BasePanelController
{

    public Toggle vibrationToggle;

    public Text titleText;
    public Slider musicSlider;
    public Slider soundSlider;
    private ISave saveDataService;

    private void OnEnable()
    {
        Debug.Log(" [OptionPanel]: OnEnable ");
        saveDataService = ServiceLocator.GetPlayerPrefDataService();

        float bgmVolume = saveDataService.LoadFloatSaveData(PlayerDataKey.MUSIC_VOLUME);
        musicSlider.value = bgmVolume;


        float sfxVolume = saveDataService.LoadFloatSaveData(PlayerDataKey.SOUND_VOLUME);
        soundSlider.value = sfxVolume;

        checkForVibrator();
    }

    private void checkForVibrator()
    {
        Debug.Log(" [OptionPanel]: checkForVibrator ");
        if (SystemInfo.deviceModel.Contains("iPad"))
        { 
            //Do Stuff here 
            vibrationToggle.gameObject.SetActive(false);
            Debug.Log(" remove vibrator option detect ipad!! ");
        }
        else
        {
            vibrationToggle.gameObject.SetActive(true);

            int vibration = saveDataService.LoadIntSaveData(PlayerDataKey.VIBRATION);
            // set up checkbox value here
            if (vibration == 1)
            {
                vibrationToggle.isOn = true;
            }
            else
            {
                vibrationToggle.isOn = false;
            }
        }
    }


    public void ClickPlay()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.GAME_MODE);
        PlayClickSound();
    }

    public void ClickAchievements()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.ACHIEVEMENTS);
        PlayClickSound();
    }

    public void ClickMainMenu()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.MAIN_MENU);
        PlayClickSound();
    }

    public void onMusicValueChanged()
    {
        saveDataService = ServiceLocator.GetPlayerPrefDataService();
        soundManager.SetMainBGMVolume(musicSlider.value);
        soundManager.DirectSetMainBGMVolume(musicSlider.value);
        saveDataService.SaveData(PlayerDataKey.MUSIC_VOLUME, musicSlider.value);
        uiEventManager.DispatchMusicValueChange(musicSlider.value);
    }

    public void onSoundValueChanged()
    {
        saveDataService = ServiceLocator.GetPlayerPrefDataService();
        soundManager.SetMainSFXVolume(soundSlider.value);
        saveDataService.SaveData(PlayerDataKey.SOUND_VOLUME, soundSlider.value);
        uiEventManager.DispatchSoundValueChange(soundSlider.value);
    }

    public void OnVibrationCheckboxChanged()
    {
        PlayClickCheckBox();
        saveDataService = ServiceLocator.GetPlayerPrefDataService();

        if (vibrationToggle.isOn)
        {
            // on dynamic data
            gameDataManager.SetVibration(true);

            // save on playerpref
            saveDataService.SaveData(PlayerDataKey.VIBRATION, GameConfig.VIBRATION_ON);
            uiEventManager.DispatchVibrationValueChange(1);
        }
        else
        {
            // on dynamic data
            gameDataManager.SetVibration(false);

            // save on playerpref
            saveDataService.SaveData(PlayerDataKey.VIBRATION, GameConfig.VIBRATION_OFF);
            uiEventManager.DispatchVibrationValueChange(0);
        }
    }
}

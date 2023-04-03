using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasePanelController : MonoBehaviour
{
    public UIPanelType uiPanelType;
    [HideInInspector]
    public GameDataManager gameDataManager;
    [HideInInspector]
    public UIEventManager uiEventManager;
    [HideInInspector]
    public SoundManager soundManager;
    [HideInInspector]
    public GameEventManager gameEventManager;

    private void Awake()
    {
        gameDataManager = GameDataManager.GetInstance();
        gameEventManager = GameEventManager.GetInstance();
        uiEventManager = UIEventManager.GetInstance();
        soundManager = SoundManager.GetInstance();
    }

    public void PlayClickSound()
    {        
        soundManager.PlaySfx(SFX.Tap);
    }

    public void PlayClickCheckBox()
    {        
        soundManager.PlaySfx(SFX.Checkboxhit);
    }
}

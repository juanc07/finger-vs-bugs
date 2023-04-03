using UnityEngine;
using System;
using UnityEngine.UI;
using DeadMosquito.IosGoodies;
using System.Collections.Generic;

namespace DeadMosquito.IosGoodies.Example
{
    public class IosGoodiesDemoController : MonoBehaviour
    {
        #if UNITY_IOS
        GameObject mainMenuPanel;

        GameObject mapsPanel;
        GameObject uiPanel;
        GameObject sharePanel;
        GameObject openAppsPanel;
        GameObject infoPanel;

        List<GameObject> _windows;

        void Awake()
        {
            InitWindows();
        }

        private void InitWindows()
        {
            FindPanel(ref mainMenuPanel, "MainMenuPanel");
            FindPanel(ref mapsPanel, "MapsPanel");
            FindPanel(ref uiPanel, "UiPanel");
            FindPanel(ref sharePanel, "SharingPanel");
            FindPanel(ref openAppsPanel, "OpenAppsPanel");
            FindPanel(ref infoPanel, "InfoPanel");

            _windows = new List<GameObject>
            {
                mainMenuPanel,
                mapsPanel,
                uiPanel,
                sharePanel,
                openAppsPanel,
                infoPanel
            };
            _windows.ForEach(w => w.SetActive(false));
            mainMenuPanel.SetActive(true);
        }

        public void OnMapsPanel()
        {
            mapsPanel.SetActive(true);
        }

        public void OnUiPanel()
        {
            uiPanel.SetActive(true);
        }

        public void OnSharePanel()
        {
            sharePanel.SetActive(true);
        }

        public void OnOpenAppsPanel()
        {
            openAppsPanel.SetActive(true);
        }

        public void OnOpenInfoPanel()
        {
            infoPanel.SetActive(true);
        }
        #endif

        private void FindPanel(ref GameObject panel, string name)
        {
            if (panel == null)
            {
                panel = FindObject(this.gameObject, name);
            }
        }

        public static GameObject FindObject(GameObject parent, string name)
        {
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trs)
            {
                if (t.name == name)
                {
                    return t.gameObject;
                }
            }
            return null;
        }
    }
}
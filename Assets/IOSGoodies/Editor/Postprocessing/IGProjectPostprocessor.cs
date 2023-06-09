﻿using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.iOS.Xcode;

namespace DeadMosquito.IosGoodies.Editor
{
    public static class IGProjectPostprocessor
    {

        [PostProcessBuild(2000)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.iOS)
            {
                IGPostprocessUtils.ModifyPlist(path, AddImagePickerPrivacyEntries);
            }
        }

        static void AddImagePickerPrivacyEntries(PlistDocument plist)
        {
            const string CameraUsageDescriptionKey = "NSCameraUsageDescription";
            const string PhotoLibraryUsageDescriptionKey = "NSPhotoLibraryUsageDescription";

            if (!plist.HasRootElement(CameraUsageDescriptionKey))
            {
                plist.root.AsDict().SetString(CameraUsageDescriptionKey, "Plist entry Added by IGProjectPostprocessor.cs script");
            }

            if (!plist.HasRootElement(PhotoLibraryUsageDescriptionKey))
            {
                plist.root.AsDict().SetString(PhotoLibraryUsageDescriptionKey, "Plist entry Added by IGProjectPostprocessor.cs script");
            }
        }
    }
}
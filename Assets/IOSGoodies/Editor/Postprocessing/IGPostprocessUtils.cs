using UnityEngine;
using System.Collections;
using System;
using UnityEditor.iOS.Xcode;
using System.IO;

namespace DeadMosquito.IosGoodies.Editor
{
    public static class IGPostprocessUtils
    {
        public static void ModifyPlist(string projectPath, Action<PlistDocument> modifier)
        {
            try
            {
                var plistInfoFile = new PlistDocument();

                string infoPlistPath = Path.Combine(projectPath, "Info.plist");
                plistInfoFile.ReadFromString(File.ReadAllText(infoPlistPath));

                modifier(plistInfoFile);

                File.WriteAllText(infoPlistPath, plistInfoFile.WriteToString());
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static bool HasRootElement(this PlistDocument plist, string key)
        {
            return plist.root.values.ContainsKey(key);
        }
    }
}
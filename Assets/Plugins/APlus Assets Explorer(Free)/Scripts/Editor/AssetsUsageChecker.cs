//  Copyright (c) 2016-2017 amlovey
//  
#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Collections.Generic;

namespace APlusFree
{
    public static class AssetsUsageChecker
    {
        private const string ASSETCHECKERKEY = "ANYPERF_ASSETCHECKERKEY";

        [PostProcessBuild(100)]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuildTarget)
        {
            string editorLogPath = GetEdtiorFilePath();
            if (string.IsNullOrEmpty(editorLogPath) || !File.Exists(editorLogPath))
            {
                return;
            }

            string logContent = ReadLogContent(editorLogPath);
            if (string.IsNullOrEmpty(logContent))
            {
                return;
            }

            string[] lines = logContent.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            bool inRegion = false;
            int startIndex = lines.Length;

            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (lines[i].Contains("Used Assets"))
                {
                    inRegion = true;
                    startIndex = i;
                    break;
                }
            }

            List<string> usedFiles = new List<string>();

            for (int i = startIndex + 1; i < lines.Length; i++)
            {
                if (!inRegion)
                {
                    break;
                }

                if (char.IsDigit(lines[i].Trim(), 0))
                {
                    string assetsPath = GetAssetPath(lines[i]);
                    if (!string.IsNullOrEmpty(assetsPath))
                    {
                        usedFiles.Add(assetsPath);
                    }
                }
                else
                {
                    inRegion = false;
                    break;
                }
            }

            if (usedFiles.Count > 0)
            {
                string value = string.Join(",", usedFiles.ToArray());
                EditorPrefs.SetString(GetUniqueAssetCheckerKey(), value);
            }
        }

        public static string GetUniqueAssetCheckerKey()
        {
            return string.Format("{0}_{1}_FREE", Application.productName, Application.bundleIdentifier, ASSETCHECKERKEY);
        }

        private static string GetAssetPath(string log)
        {
            // 0.1 kb	 0.0% Assets/Test/SampleScenes/Scripts/CameraSwitch.cs
            //
            string[] temp = log.Split(new char[] { ' ' }, 4, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length == 4)
            {
                return temp[3];
            }

            return string.Empty;
        }

        private static string ReadLogContent(string logPath)
        {
            // Will happen "IOException: Sharing violation on path" on Windows,
            // so just copy a backup file and read it for workaroud solution.
            //
            string destFile = logPath + "backup";
            File.Copy(logPath, destFile, true);
            string data = string.Empty;

            using (FileStream fs = new FileStream(destFile, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    data = reader.ReadToEnd();
                }
            }

            File.Delete(destFile);

            return data;
        }

        public static string GetEdtiorFilePath()
        {
            string editorLogPath = "Editor.log";

            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                string unityLogFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library/Logs/Unity");
                editorLogPath = Path.Combine(unityLogFolder, editorLogPath);
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                editorLogPath = Path.Combine(Environment.ExpandEnvironmentVariables(@"%localappdata%\Unity\Editor\"), editorLogPath);
            }
            else
            {
                Debug.LogError("Not support by A+ Assets Explorer for now!");
                return null;
            }

            return editorLogPath;
        }
    }
}
#endif
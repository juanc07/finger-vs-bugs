//  Copyright (c) 2016-2017 amlovey
//  
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace APlusFree
{
    public class WebViewCommunicationService : ScriptableObject
    {
        private const string SCRIPTOBJECTNAME = "AnyPerf";
        private CallbackWrapper wrap;
        private Webview webView;

        // It's possible that AssetPostprocessor.OnPostprocessAllAssets 
        // execute on each Asset, so the DoAssetsChange() method will be
        // executed multiple times, and then save cache file async opration
        // executed multiple times. This variable is to simply reduce the save
        // cache file action times.
        //
        private int saveCount = 0;

        public WebViewCommunicationService()
        {
        }

        public void Init(Webview webView)
        {
            if (webView == null)
            {
                return;
            }

            webView.DefineScriptObject(SCRIPTOBJECTNAME, this);
            webView.SetDelegateObject(this);
            this.webView = webView;
        }

        public void DoAssetsChange(
            List<APAsset> addedAssets,
            List<APAsset> deleteAssets,
            List<APAsset> modifedAssets,
            List<string> movedFrom,
            List<APAsset> moveToAssets, int intervalCount = 5)
        {

            if (IsListNullOrEmpty(addedAssets)
                && IsListNullOrEmpty(deleteAssets)
                && IsListNullOrEmpty(modifedAssets)
                && IsListNullOrEmpty(movedFrom)
                && IsListNullOrEmpty(moveToAssets))
            {
                return;
            }

            string moveFromIds = movedFrom == null || movedFrom.Count == 0 ? "[]" : GetJsonFromStringList(movedFrom);
            string addedJson = EncodeJsonFromList(addedAssets);
            string deleteJson = EncodeJsonFromList(deleteAssets);
            string modifedJson = EncodeJsonFromList(modifedAssets);
            string movetoJson = EncodeJsonFromList(moveToAssets);
            if (webView != null)
            {
                string js = string.Format("window.doAssetsChange('{0}', '{1}', '{2}', '{3}', '{4}')",
                                            addedJson,
                                            deleteJson,
                                            modifedJson,
                                            CLZF2_Base64(Utility.SafeJson(moveFromIds)),
                                            movetoJson);
                webView.ExecuteJavascript(js);

                saveCount++;
                if (intervalCount > 0 && saveCount % intervalCount == 0)
                {
                    APCache.SaveToLocalAsync();
                    saveCount = 0;
                    Utility.DebugLog("APCache.SaveToLocalAsync()");
                }

                RefreshIconCache();
                Utility.DebugLog("DoAssetsChange");
            }
        }

        private string GetJsonFromStringList(List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (var item in list)
            {
                sb.Append(string.Format("\"{0}\",", item));
            }

            if (sb.Length > 1)
            {
                sb = sb.Remove(sb.Length - 1, 1);
            }

            sb.Append("]");
            Utility.DebugLog(Utility.SafeJson(sb.ToString()));
            return Utility.SafeJson(sb.ToString());
        }

        private bool IsListNullOrEmpty<T>(IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        private string EncodeJsonFromList(List<APAsset> assets)
        {
            string json = APCache.GetJsonFromList(assets);
            return CLZF2_Base64(json);
        }

        public void AddAssets(List<APAsset> assets)
        {
            DoAssetsChange(assets, null, null, null, null);
        }

        public void DeleteAssets(List<APAsset> assets)
        {
            DoAssetsChange(null, assets, null, null, null);
        }

        public void SetCurrentURL(string url, string data)
        {
            if (webView == null)
            {
                return;
            }

            webView.ExecuteJavascript(string.Format("window.setCurrentURL('{0}', '{1}');", url, CLZF2_Base64(data)));
            webView.ExecuteJavascript("window.refreshSearch();");
        }

        public void Refresh()
        {
            if (webView != null)
            {
                webView.ExecuteJavascript("window.Refresh();");
                return;
            }
        }

        public void UpdateTheme()
        {
            if (webView != null)
            {
                webView.ExecuteJavascript("window.applyTheme();");
                return;
            }
        }

        public void RefreshAll()
        {
            if (webView != null)
            {
                webView.ExecuteJavascript("window.RefreshAllData();");
                webView.ExecuteJavascript("window.refreshIconCache();");
            }
        }

        public void UpdateObjectsIntoCache(APAssetType type, string assetid, List<APAsset> modifedAssets = null)
        {
            APAsset asset = APResources.GetAPAssetByPath(type, assetid);

            if (APCache.HasAsset(type, assetid))
            {
                var previousAssets = APCache.GetValue(type, assetid);
                asset.Unused = previousAssets.Unused;
                APCache.SetValue(type, assetid, asset);
                if (modifedAssets != null)
                {
                    modifedAssets.Add(asset);
                }
            }
        }

        public void ExecuteJSinWebView(string jsCode)
        {
            this.webView.ExecuteJavascript(jsCode);
        }

        /// <summary>
        /// Get the texture and return the DESC sort order
        /// </summary>
        private void GetResByType(string message, object callback)
        {
#if APLUS_DEV
            var now = DateTime.Now;
#endif
            if (string.IsNullOrEmpty(message))
            {
                throw new Exception("no message provide");
            }

            string json = "[]";

            switch (message.ToLower())
            {
                case AssetType.TEXTURES:
                    json = APCache.GetAssetsLitJsonByTypeFromCache(APAssetType.Texture);
                    break;
                case AssetType.MOVIES:
                    json = APCache.GetAssetsLitJsonByTypeFromCache(APAssetType.MovieTexture);
                    break;
                default:
                    break;
            }

            json = CLZF2_Base64(json);
            wrap = new CallbackWrapper(callback);
            wrap.Send(json);

#if APLUS_DEV
            Utility.DebugLog(String.Format("Load Res {0} cost {1} ms", message, (DateTime.Now - now).TotalMilliseconds));
#endif
        }

        private string CLZF2_Base64(string s)
        {
            return Convert.ToBase64String(CLZF2.Compress(Encoding.UTF8.GetBytes(s)));
        }

        private void GetIconCache(string message, object callback)
        {
            var json = APCache.GetIconCacheJSON();
            json = Convert.ToBase64String(CLZF2.Compress(Encoding.UTF8.GetBytes(json)));
            wrap = new CallbackWrapper(callback);
            wrap.Send(json);
        }

        public void RefreshIconCache()
        {
            if (webView != null)
            {
                webView.ExecuteJavascript("window.refreshIconCache()");
            }
        }

        private void PingAssets(string id, object callback)
        {
            if (string.IsNullOrEmpty(id))
            {
                string message = string.Format("Asset not found: {0}", id);
                EditorUtility.DisplayDialog("404: Not found", message, "OK");
                return;
            }

            Utility.DebugLog(string.Format("Ping {0}", id));

            UnityEngine.Object obj;

            if (Utility.IsSubAsset(id))
            {
                Utility.DebugLog("Ping: Is SubAsset");
                obj = GetAnimationObjectFromModel(id);
            }
            else
            {
                Utility.DebugLog("Ping: Is Not SubAsset");
                obj = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(id));
            }

            Utility.DebugLog(string.Format("Trying to ping {0}", obj));

            if (obj == null)
            {
                return;
            }

            

            EditorGUIUtility.PingObject(obj);
            Selection.activeObject = obj;
        }

        private void MultiSelect(string paths, object callback)
        {
            if (string.IsNullOrEmpty(paths))
            {
                return;
            }

            var decompressedIds = Encoding.UTF8.GetString(CLZF2.Decompress(Convert.FromBase64String(paths)));
            
            Utility.DebugLog(string.Format("MultiSelect: {0}", decompressedIds));

            var assetIds = decompressedIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var objects = new UnityEngine.Object[assetIds.Length];
            for (int i = 0; i < assetIds.Length; i++)
            {
                string id = assetIds[i];
                if (Utility.IsSubAsset(id))
                {
                    Utility.DebugLog("MultiSelect: Is SubAsset");
                    var obj = GetAnimationObjectFromModel(id);
                    if (obj != null)
                    {
                        objects[i] = obj;
                    }
                }
                else
                {
                    Utility.DebugLog("MultiSelect: Is Not SubAsset");
                    objects[i] = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(id));
                }
            }

            if (objects.Length > 0)
            {
                Selection.objects = objects;
                if (Event.current != null)
                {
                    Event.current.Use();
                };
            }
        }

        private UnityEngine.Object GetAnimationObjectFromModel(string assetid)
        {
            string guid = Utility.GetGuidFromAssetId(assetid);
            string fileId = Utility.GetFileIdFromAssetId(assetid);

            Utility.DebugLog(string.Format("Find Animation in {0} with id {1}", guid, fileId));

            if (string.IsNullOrEmpty(guid) || string.IsNullOrEmpty(fileId))
            {
                return null;
            }

            var objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(guid));
            Utility.DebugLog(string.Format("Get {0} items in {1}", objects.Length, assetid));

            foreach (var obj in objects)
            {
                if (obj is AnimationClip && Utility.GetLocalIndentifierOfObject(obj).ToString() == fileId)
                {
                    // Release loaded objects
                    //
                    foreach (var item in objects)
                    {
                        if (obj != item)
                        {
                            APResources.UnloadAsset(item);
                        }
                    }

                    return obj;
                }
            }

            foreach (var item in objects)
            {
                APResources.UnloadAsset(item);
            }

            return null;
        }

        private void GetEditorPerfsValue(string key, object callback)
        {
            var value = EditorPrefs.GetString(key);
            wrap = new CallbackWrapper(callback);
            wrap.Send(value);
        }

        private void SetEditorPerfsValue(string KeyValue, object callback)
        {
            var keyAndValue = KeyValue.Split(new char[] { '$' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (keyAndValue.Length == 2)
            {
                EditorPrefs.SetString(keyAndValue[0], keyAndValue[1]);
            }
        }

        private void DeleteAssets(string delIds, object callback)
        {
            if (string.IsNullOrEmpty(delIds))
            {
                return;
            }

            var ids = delIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (ids.Length == 0)
            {
                return;
            }

            string title = "Delete selected file?";
            string message = "You cannot undo this action!";
            if (EditorUtility.DisplayDialog(title, message, "Delete", "Cancel"))
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(ids[i]));
                }

                AssetDatabase.Refresh();
            }
        }

        private void TriggerBuild(string args, object callback)
        {
            APlusWindow.FindUnusedAssets();
        }

        private void RenameAssets(string assets, object callback)
        {
            Utility.DebugLog(string.Format("Rename {0}", assets));

            if (string.IsNullOrEmpty(assets))
            {
                return;
            }

            string[] temp = assets.Split(new char[] { ']', '[' }, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length != 3)
            {
                return;
            }

            var assetIds = temp[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var newNames = temp[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string title = "Renaming";
            float progress = 0;
            if (assetIds.Length == newNames.Length)
            {
                int changedCount = 0;
                int passCount = 0;
                Dictionary<string, List<long>> fileIdMaps = new Dictionary<string, List<long>>();
                Dictionary<string, List<string>> newNamesMap = new Dictionary<string, List<string>>();

                for (int i = 0; i < assetIds.Length; i++)
                {
                    if (Utility.IsSubAsset(assetIds[i]))
                    {
                        var guid = Utility.GetGuidFromAssetId(assetIds[i]);
                        var fileId = Utility.GetFileIdFromAssetId(assetIds[i]);
                        long fileId_long = -1;

                        if (long.TryParse(fileId, out fileId_long))
                        {
                            if (fileIdMaps.ContainsKey(guid))
                            {
                                fileIdMaps[guid].Add(fileId_long);
                                newNamesMap[guid].Add(newNames[i]);
                            }
                            else
                            {
                                fileIdMaps.Add(guid, new List<long>() { fileId_long });
                                newNamesMap.Add(guid, new List<string>() { newNames[i] });
                            }
                        }
                        else
                        {
                            passCount++;
                        }
                        continue;
                    }

                    var assetPath = AssetDatabase.GUIDToAssetPath(assetIds[i]);
                    AssetImporter importer = AssetImporter.GetAtPath(assetPath);
                    if (importer.ToString().Contains("UnityEngine.NativeFormatImporter"))
                    {
                        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath));
                        serializedObject.targetObject.name = newNames[i];
                        serializedObject.ApplyModifiedProperties();
                        AssetDatabase.SaveAssets();
                    }

                    var errorMessage = AssetDatabase.RenameAsset(AssetDatabase.GUIDToAssetPath(assetIds[i]), newNames[i]);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        changedCount++;
                    }
                    else
                    {
                        Debug.LogWarning(errorMessage);
                    }

                    progress = (changedCount + passCount) * 1.0f / assetIds.Length;
                    if (EditorUtility.DisplayCancelableProgressBar(title, assetIds[i], progress))
                    {
                        wrap = new CallbackWrapper(callback);
                        wrap.Send("done");
                        EditorUtility.ClearProgressBar();
                        return;
                    }
                }

                AssetDatabase.Refresh();

                int errorCount = assetIds.Length - passCount - changedCount;
                string msg = string.Format("{0} Success, {1} Failed, {2} Passed", changedCount, errorCount, passCount);
                EditorUtility.DisplayDialog("Bulk Rename Result", msg, "OK");

                wrap = new CallbackWrapper(callback);
                wrap.Send("done");
                EditorUtility.ClearProgressBar();
            }
        }

        private void RefreshCache(string msg, object callback)
        {
            APlusWindow.RefreshCache();
        }

        private void GetAssetsInActiveScene(string msg, object callback)
        {
            string currentScenePath = string.Empty;
#if UNITY_5_3_OR_NEWER
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (!string.IsNullOrEmpty(scene.path))
            {
                currentScenePath = scene.path;
            }
            var dependencies = AssetDatabase.GetDependencies(currentScenePath);
#else
            currentScenePath = EditorApplication.currentScene;
            var dependencies = AssetDatabase.GetDependencies(new string[] { currentScenePath });
#endif

            var queryString = "Id:(null)";
            if (!string.IsNullOrEmpty(currentScenePath))
            {
                var guids = dependencies.Select(d => AssetDatabase.AssetPathToGUID(d)).ToArray();
                if (guids.Length > 0)
                {
                    queryString = string.Format("Id:{0}", string.Join("|", guids));
                }
            } 
            
            SetCurrentURL(string.Format("res/{0}", msg), queryString);
        }
    }
}
#endif
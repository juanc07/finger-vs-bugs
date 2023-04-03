// 
// DOCUMENTATION FOR THIS CLASS: https://github.com/TarasOsiris/iOS-Goodies-Docs/wiki/IGActionSheet.cs
//

#if UNITY_IOS
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using DeadMosquito.IosGoodies.Internal;

namespace DeadMosquito.IosGoodies
{
    /// <summary>
    /// Class to show https://developer.apple.com/reference/uikit/uiactionsheet
    /// </summary>
    public static class IGActionSheet
    {
        /// <summary>
        /// Displays action sheet with cancel button and provided options.
        /// </summary>
        /// <param name="title">Action sheet title</param>
        /// <param name="cancelBtnTitle">Action sheet cancel button title</param>
        /// <param name="onCancelClicked">Action sheet cancel button callback</param>
        /// <param name="otherButtonTitles">Titles of other buttons</param>
        /// <param name="onOtherButtonClicked">Action sheet other button callback, index is the same as index array passed to method</param>
        public static void ShowActionSheet(string title,
                                           string cancelBtnTitle, Action onCancelClicked,
                                           string[] otherButtonTitles, Action<int> onOtherButtonClicked)
        {
            int cancellBtnIndex = otherButtonTitles.Length;
            Action<int> callback = index =>
            {
                if (index == cancellBtnIndex)
                {
                    onCancelClicked();
                }
                else
                {
                    onOtherButtonClicked(index);
                }
            };

            _showActionSheet(title, cancelBtnTitle, null, string.Join("|", otherButtonTitles),
                IGUtils.ActionIntCallaback, callback.GetPointer());
        }

        /// <summary>
        /// Displays action sheet with cancel and destructive button and provided options.
        /// </summary>
        /// <param name="title">Action sheet title</param>
        /// <param name="cancelBtnTitle">Action sheet cancel button title</param>
        /// <param name="onCancelClicked">Action sheet cancel button callback</param>
        /// <param name="destructiveButtonTitle">Action sheet destructive button title</param>
        /// <param name="onDestructiveButtonClicked">Action sheet destructive button callback</param>
        /// <param name="otherButtonTitles">Titles of other buttons</param>
        /// <param name="onOtherButtonClicked">Action sheet other button callback, index is the same as index array passed to method</param>
        public static void ShowActionSheet(string title,
                                           string cancelBtnTitle, Action onCancelClicked,
                                           string destructiveButtonTitle, Action onDestructiveButtonClicked,
                                           string[] otherButtonTitles, Action<int> onOtherButtonClicked)
        {
            int cancellBtnIndex = otherButtonTitles.Length;
            int destructiveButtonIndex = otherButtonTitles.Length + 1;

            Action<int> callback = index =>
            {
                if (index == cancellBtnIndex)
                {
                    onCancelClicked();
                }
                else if (index == destructiveButtonIndex)
                {
                    onDestructiveButtonClicked();
                }
                else
                {
                    onOtherButtonClicked(index);
                }
            };
            _showActionSheet(title, cancelBtnTitle, destructiveButtonTitle, string.Join("|", otherButtonTitles),
                IGUtils.ActionIntCallaback, callback.GetPointer());
        }

        [DllImport("__Internal")]
        private static extern void _showActionSheet(
            string title,
            string cancelBtnTitle,
            string destructiveButtonTitle,
            string otherBtnTitles,
            IGUtils.ActionIntCallbackDelegate callback,
            IntPtr callbackPtr);
    }
}
#endif

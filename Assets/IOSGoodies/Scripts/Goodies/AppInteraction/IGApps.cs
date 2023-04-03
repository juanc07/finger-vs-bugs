// 
// DOCUMENTATION FOR THIS CLASS: https://github.com/TarasOsiris/iOS-Goodies-Docs/wiki/IGApps.cs
//

#if UNITY_IOS
using DeadMosquito.IosGoodies.Internal;
using System.Collections.Generic;
using UnityEngine;

namespace DeadMosquito.IosGoodies
{
    public static class IGApps
    {
        /// <summary>
        /// Opens the YouTube video to view.
        /// If the YouTube video cannot be viewed on the device, iOS displays an appropriate warning message to the user.
        /// </summary>
        /// <param name="videoId">Id of the YouTube video</param>
        public static void OpenYoutubeVideo(string videoId)
        {
            Check.Argument.IsStrNotNullOrEmpty(videoId, "videoId");

            if (IGUtils.IsIosCheck())
            {
                return;
            }

            IGUtils._openUrl(string.Format("http://www.youtube.com/watch?v={0}", videoId));
        }

        /// <summary>
        /// Starts the face time video call.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        public static void StartFaceTimeVideoCall(string userId)
        {
            Check.Argument.IsStrNotNullOrEmpty(userId, "userId");

            if (IGUtils.IsIosCheck())
            {
                return;
            }

            IGUtils._openUrl(string.Format("facetime:{0}", userId));
        }

        /// <summary>
        /// Starts the face time audio call.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        public static void StartFaceTimeAudioCall(string userId)
        {
            Check.Argument.IsStrNotNullOrEmpty(userId, "userId");

            if (IGUtils.IsIosCheck())
            {
                return;
            }

            IGUtils._openUrl(string.Format("facetime-audio:{0}", userId));
        }
    }
}
#endif

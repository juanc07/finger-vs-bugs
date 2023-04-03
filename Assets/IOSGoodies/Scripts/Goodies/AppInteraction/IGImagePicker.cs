//
// DOCUMENTATION FOR THIS CLASS: https://github.com/TarasOsiris/iOS-Goodies-Docs/wiki/IGImagePicker.cs
//

#if UNITY_IOS
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using DeadMosquito.IosGoodies.Internal;
using AOT;

namespace DeadMosquito.IosGoodies
{
    /// <summary>
    /// Class allows you to pick images from gallery and camera and receive Texture2D in a callback
    /// </summary>
    public static class IGImagePicker
    {
        #region camera

        /// <summary>
        /// Camera type to take picture.
        /// </summary>
        public enum CameraType
        {
            /// <summary>
            /// The rear camera.
            /// </summary>
            Rear = 0,
            /// <summary>
            /// The front camera.
            /// </summary>
            Front = 1
        }

        /// <summary>
        /// Camera flash mode.
        /// </summary>
        public enum CameraFlashMode
        {
            /// <summary>
            /// Flash off.
            /// </summary>
            Off = -1,
            /// <summary>
            /// Flash auto.
            /// </summary>
            Auto = 0,
            /// <summary>
            /// Flash on.
            /// </summary>
            On = 1
        }

        /// <summary>
        /// Picks the image from camera.
        /// </summary>
        /// <param name="callback">Image picked callback. Passes <see cref="Texture2D"/> as a parameter. Must not be null</param>
        /// <param name="onCancel">On cancel callback.</param>
        /// <param name="compressionQuality">Compression quality. Must be between 0 to 1</param>
        /// <param name="allowEditing">If set to <c>true</c> allow editing.</param>
        /// <param name="cameraType">Camera type. Front or Rear camera to use?</param>
        /// <param name="flashMode">Flash mode to take picture.</param>
        public static void PickImageFromCamera(Action<Texture2D> callback, Action onCancel,
                                               float compressionQuality = 1f,
                                               bool allowEditing = true,
                                               CameraType cameraType = CameraType.Front,
                                               CameraFlashMode flashMode = CameraFlashMode.Auto)
        {
            if (IGUtils.IsIosCheck())
            {
                return;
            }

            Check.Argument.IsNotNull(callback, "callback");
            compressionQuality = Mathf.Clamp01(compressionQuality);

            _pickImageFromCamera(
                ImageResultCallback, callback.GetPointer(),
                IGUtils.ActionVoidCallback, onCancel.GetPointer(),
                compressionQuality, allowEditing, cameraType == CameraType.Rear,
                (int)flashMode);
        }

        /// <summary>
        /// Picks the image from photo library.
        /// </summary>
        /// <param name="callback">Image picked callback. Passes <see cref="Texture2D"/> as a parameter. Must not be null</param>
        /// <param name="onCancel">On cancel callback.</param>
        /// <param name="compressionQuality">Compression quality. Must be between 0 to 1</param>
        /// <param name="allowEditing">If set to <c>true</c> allow editing.</param>
        public static void PickImageFromPhotoLibrary(Action<Texture2D> callback, Action onCancel,
                                                     float compressionQuality = 1f,
                                                     bool allowEditing = true)
        {
            if (IGUtils.IsIosCheck())
            {
                return;
            }

            Check.Argument.IsNotNull(callback, "callback");
            compressionQuality = Mathf.Clamp01(compressionQuality);

            _pickImageFromGallery(
                ImageResultCallback, callback.GetPointer(),
                IGUtils.ActionVoidCallback, onCancel.GetPointer(),
                (int)GallerySourceType.PhotoLibrary, compressionQuality, allowEditing);
        }

        /// <summary>
        /// Picks the image from photos album.
        /// </summary>
        /// <param name="callback">Image picked callback. Passes <see cref="Texture2D"/> as a parameter. Must not be null</param>
        /// <param name="onCancel">On cancel callback.</param>
        /// <param name="compressionQuality">Compression quality. Must be between 0 to 1</param>
        /// <param name="allowEditing">If set to <c>true</c> allow editing.</param>
        public static void PickImageFromPhotosAlbum(Action<Texture2D> callback, Action onCancel,
                                                    float compressionQuality = 1f,
                                                    bool allowEditing = true)
        {
            if (IGUtils.IsIosCheck())
            {
                return;
            }

            Check.Argument.IsNotNull(callback, "callback");
            compressionQuality = Mathf.Clamp01(compressionQuality);

            _pickImageFromGallery(
                ImageResultCallback, callback.GetPointer(),
                IGUtils.ActionVoidCallback, onCancel.GetPointer(),
                (int)GallerySourceType.PhotosAlbum, compressionQuality, allowEditing);
        }

        #endregion

        #region gallery

        enum GallerySourceType
        {
            PhotoLibrary = 0,
            PhotosAlbum = 1
        }

        #endregion

        internal delegate void ImageResultDelegate(IntPtr callbackPtr,IntPtr byteArrPtr,int arrayLength);

        [MonoPInvokeCallback(typeof(ImageResultDelegate))]
        public static void ImageResultCallback(IntPtr callbackPtr, IntPtr byteArrPtr, int arrayLength)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log("Picked img ptr: " + byteArrPtr.ToInt32() + ", array length: " + arrayLength);
            }

            byte[] buffer = new byte[arrayLength];

            if (Debug.isDebugBuild)
            {
                Debug.Log("ImageResultCallback");
            }

            Marshal.Copy(byteArrPtr, buffer, 0, arrayLength);
            var tex = new Texture2D(2, 2);
            tex.LoadImage(buffer);

            if (callbackPtr != IntPtr.Zero)
            {
                var action = callbackPtr.Cast<Action<Texture2D>>();
                action(tex);
            }
        }

        #region external

        [DllImport("__Internal")]
        private static extern void _pickImageFromCamera(
            ImageResultDelegate callback, IntPtr callbackPtr,
            IGUtils.ActionVoidCallbackDelegate cancelCallback, IntPtr cancelPtr,
            float compressionQuality,
            bool allowEditing,
            bool isRearCamera,
            int flashMode);

        [DllImport("__Internal")]
        private static extern void _pickImageFromGallery(
            ImageResultDelegate callback, IntPtr callbackPtr,
            IGUtils.ActionVoidCallbackDelegate cancelCallback, IntPtr cancelPtr,
            int source,
            float compressionQuality,
            bool allowEditing);

        #endregion

    }
}
#endif

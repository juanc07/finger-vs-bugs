using DeadMosquito.IosGoodies;
using UnityEngine;
using UnityEngine.UI;

namespace DeadMosquito.IosGoodies.Example
{
    public class IGOpenAppsExample : MonoBehaviour
    {
        public Image image;

        #if UNITY_IOS
        public void OnOpenYouTubeVideo()
        {
            const string videoId = "rZ2csdtP440";
            IGApps.OpenYoutubeVideo(videoId);
        }

        public void OnFaceTimeVideoCall()
        {
            IGApps.StartFaceTimeVideoCall("user@example.com");
        }

        public void OnFaceTimeAudioCall()
        {
            IGApps.StartFaceTimeAudioCall("user@example.com");
        }

        #region image_pickers

        public void PickImageFromCamera()
        {
            const bool allowEditing = true;
            const float compressionQuality = 0.8f;
            const IGImagePicker.CameraType cameraType = IGImagePicker.CameraType.Front;
            const IGImagePicker.CameraFlashMode flashMode = IGImagePicker.CameraFlashMode.On;

            IGImagePicker.PickImageFromCamera(tex =>
                {
                    Debug.Log("Successfully picked image from camera");
                    image.sprite = SpriteFromTex2D(tex);
                    // IMPORTANT! Call this method to clean memory if you are picking and discarding images
                    Resources.UnloadUnusedAssets();
                }, 
                () => Debug.Log("Picking image from camera cancelled"), 
                compressionQuality,
                allowEditing, cameraType, flashMode);
        }

        public void PickImageFromPhotoLibrary()
        {
            const bool allowEditing = false;
            const float compressionQuality = 0.5f;

            IGImagePicker.PickImageFromPhotoLibrary(tex =>
                {
                    Debug.Log("Successfully picked image from photo library");
                    image.sprite = SpriteFromTex2D(tex);
                    // IMPORTANT! Call this method to clean memory if you are picking and discarding images
                    Resources.UnloadUnusedAssets();
                }, 
                () => Debug.Log("Picking image from photo library cancelled"), 
                compressionQuality,
                allowEditing);
        }

        public void PickImageFromPhotosAlbum()
        {
            const bool allowEditing = true;
            const float compressionQuality = 0.1f;

            IGImagePicker.PickImageFromPhotosAlbum(tex =>
                {
                    Debug.Log("Successfully picked image from photos album");
                    image.sprite = SpriteFromTex2D(tex);
                    // IMPORTANT! Call this method to clean memory if you are picking and discarding images
                    Resources.UnloadUnusedAssets();
                }, 
                () => Debug.Log("Picking image from photos album cancelled"), 
                compressionQuality,
                allowEditing);
        }

        #endregion

        static Sprite SpriteFromTex2D(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        #endif
    }
}
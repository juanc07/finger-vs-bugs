using UnityEngine;
using DeadMosquito.IosGoodies;

namespace DeadMosquito.IosGoodies.Example
{
    public class IGShareExample : MonoBehaviour
    {
        Texture2D _image;

        public Texture2D Image
        {
            get
            {
                if (_image == null)
                {
                    _image = Resources.Load<Texture2D>("icon");
                }
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        void Awake()
        {
            // test getting instance
            Debug.Log(Image);
        }

        const string Message = "iOS Native Goodies by Dead Mosquito Games http://u3d.as/zMp #AssetStore";

        #if UNITY_IOS
        public void OnShareTextWithImage()
        {
            IGShare.Share(() =>
                Debug.Log("DONE sharing"), "iOS Goodies is a really cool #unity3d plugin!", Image);
        }

        public void OnSendSms()
        {
            IGShare.SendSmsViaDefaultApp("123456789", "My message!");
        }

        public void OnSendSmsEmbedded()
        {
            IGShare.SendSmsViaController("123456789", "Hello worksadk wa dwad !!!", () => Debug.Log("Success"),
                () => Debug.Log("Cancel"), () => Debug.Log("Failure"));
        }

        public void OnTweet()
        {
            if (IGShare.IsTwitterSharingAvailable())
            {
                IGShare.Tweet(
                    () => Debug.Log("Tweeted Successfully"), 
                    () => Debug.Log("Tweeting Cancelled"), Message, Image);
            }
            else
            {
                Debug.Log("Native tweeting is not available on this device");
            }
        }

        public void OnPostToFacebook()
        {
            if (IGShare.IsFacebookSharingAvailable())
            {
                IGShare.PostToFacebook(
                    () => Debug.Log("Posted to Facebook Successfully"), 
                    () => Debug.Log("Posting to Facebook Cancelled"), Message, Image);
            }
            else
            {
                Debug.Log("Native posting to Facebook is not available on this device");
            }
        }

        public void OnSendEmail()
        {
            var recipients = new[] { "x@gmail.com", "hello@gmail.com" };
            var ccRecipients = new[] { "cc@gmail.com" };
            var bccRecipients = new[] { "bcc@gmail.com", "bcc-guys@gmail.com" };
            IGShare.SendEmail(recipients, "The Subject", Message, ccRecipients, bccRecipients);
        }
        #endif
    }
}

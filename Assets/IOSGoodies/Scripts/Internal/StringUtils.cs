#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace DeadMosquito.IosGoodies.Internal
{

    public static class StringUtils
    {
        const string Comma = ",";

        public static string JoinByComma(this string[] array)
        {
            return string.Join(Comma, array);
        }

        // http://answers.unity3d.com/questions/341147/encode-string-to-url-friendly-format.html
        public static string Escape(this string str)
        {
            return WWW.EscapeURL(str).Replace("+", "%20");
        }
    }
}
#endif

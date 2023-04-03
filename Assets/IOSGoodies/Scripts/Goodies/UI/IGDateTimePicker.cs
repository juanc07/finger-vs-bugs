// 
// DOCUMENTATION FOR THIS CLASS: https://github.com/TarasOsiris/iOS-Goodies-Docs/wiki/IGDateTimePicker.cs
//

#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using DeadMosquito.IosGoodies.Internal;
using AOT;
using UnityEngine;

namespace DeadMosquito.IosGoodies
{
    /// <summary>
    /// Class to present native iOS UI Date and Time Pickers.
    /// </summary>
    public static class IGDateTimePicker
    {
        enum UIDatePickerMode
        {
            Time = 0,
            Date = 1,
            DateAndTime = 2,
            CountDownTimer = 3
        }

        /// <summary>
        /// Displays month, day, and year depending on the locale setting (e.g. November | 15 | 2007). 
        /// All the input will be blocked until the date is picked.
        /// </summary>
        /// <param name="dateTimePickerCallback">Callback that receives the date that user pickes.</param>
        /// <param name = "onCancel">Callback when user cancelled picking</param>
        public static void ShowDatePicker(Action<DateTime> dateTimePickerCallback, Action onCancel)
        {
            if (IGUtils.IsIosCheck())
            {
                return;
            }

            Check.Argument.IsNotNull(dateTimePickerCallback, "dateTimePickerCallback");

            _showDatePicker(dateTimePickerCallback.GetPointer(), OnDateTimeSelectedCallback,
                onCancel.GetPointer(), IGUtils.ActionVoidCallback, (int)UIDatePickerMode.Date);
        }

        /// <summary>
        /// Displays hour, minute, and optionally AM/PM designation depending on the locale setting (e.g. 6 | 53 | PM).
        ///  All the input will be blocked until the time is picked.
        /// </summary>
        /// <param name="dateTimePickerCallback">Date time picker callback.</param>
        /// <param name = "onCancel">Callback when user cancelled picking</param>
        public static void ShowTimePicker(Action<DateTime> dateTimePickerCallback, Action onCancel)
        {
            if (IGUtils.IsIosCheck())
            {
                return;
            }

            Check.Argument.IsNotNull(dateTimePickerCallback, "dateTimePickerCallback");

            _showDatePicker(dateTimePickerCallback.GetPointer(), OnDateTimeSelectedCallback,
                onCancel.GetPointer(), IGUtils.ActionVoidCallback, (int)UIDatePickerMode.Time);
        }

        /// <summary>
        /// Displays date, hour, minute, and optionally AM/PM designation depending on the locale setting (e.g. Wed Nov 15 | 6 | 53 | PM)
        /// All the input will be blocked until the date and time is picked.
        /// </summary>
        /// <param name="dateTimePickerCallback">Date time picker callback.</param>
        /// <param name = "onCancel">Callback when user cancelled picking</param>
        public static void ShowDateAndTimePicker(Action<DateTime> dateTimePickerCallback, Action onCancel)
        {
            if (IGUtils.IsIosCheck())
            {
                return;
            }

            Check.Argument.IsNotNull(dateTimePickerCallback, "dateTimePickerCallback");

            _showDatePicker(dateTimePickerCallback.GetPointer(), OnDateTimeSelectedCallback, 
                onCancel.GetPointer(), IGUtils.ActionVoidCallback, (int)UIDatePickerMode.DateAndTime);
        }

        /// <summary>
        /// Displays hour and minute (e.g. 1 | 53).
        /// All the input will be blocked until the date and time is picked.
        /// </summary>
        /// <param name="dateTimePickerCallback">Date time picker callback.</param>
        /// <param name = "onCancel">Callback when user cancelled picking</param>
        public static void ShowCountDownTimer(Action<DateTime> dateTimePickerCallback, Action onCancel)
        {
            if (IGUtils.IsIosCheck())
            {
                return;
            }

            Check.Argument.IsNotNull(dateTimePickerCallback, "dateTimePickerCallback");

            _showDatePicker(dateTimePickerCallback.GetPointer(), OnDateTimeSelectedCallback, 
                onCancel.GetPointer(), IGUtils.ActionVoidCallback, (int)UIDatePickerMode.CountDownTimer);
        }

        internal delegate void OnDateSelectedDelegate(IntPtr actionPtr,int year,int month,int day,int hour,int minute);

        [MonoPInvokeCallback(typeof(OnDateSelectedDelegate))]
        static void OnDateTimeSelectedCallback(IntPtr actionPtr, int year, int month, int day, int hour, int minute)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log(string.Format("OnDateTimeSelectedCallback y:{0} m:{1} d:{2} h:{3} min:{4}", year, month, day, hour, minute));
            }
            if (actionPtr != IntPtr.Zero)
            {
                var action = actionPtr.Cast<Action<DateTime>>();
                action(new DateTime(year, month, day, hour, minute, 0));
            }
        }

        [DllImport("__Internal")]
        private static extern void _showDatePicker(IntPtr successCallbackPtr, OnDateSelectedDelegate onDateSelectedCallback,
                                                   IntPtr cancellCallbackPtr, IGUtils.ActionVoidCallbackDelegate onCancelCallback,
                                                   int datePickerType);
    }
}
#endif

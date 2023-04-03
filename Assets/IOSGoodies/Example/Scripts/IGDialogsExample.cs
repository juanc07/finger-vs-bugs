using UnityEngine;
using DeadMosquito.IosGoodies;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;

namespace DeadMosquito.IosGoodies.Example
{
    public class IGDialogsExample : MonoBehaviour
    {
        #if UNITY_IOS
        public Text dateText;
        public Text timeText;
        public Text dateAndTimeText;
        public Text countdownTimeText;

        public void OnShowConfirmationDialog()
        {
            IGDialogs.ShowOneBtnDialog("Title", "Message", "Confirm", () => Debug.Log("Button clicked!"));
        }

        public void OnShowTwoButtonDialog()
        {
            IGDialogs.ShowTwoBtnDialog("Title", "My awesome message!", 
                "Confirm", () => Debug.Log("Confirm button clicked!"),
                "Cancel", () => Debug.Log("Cancel clicked!"));
        }

        public void OnShowThreeButtonDialog()
        {
            IGDialogs.ShowThreeBtnDialog("Title", "My awesome message!", 
                "Option 1", () => Debug.Log("Option 1 button clicked!"),
                "Option 2", () => Debug.Log("Option 2 button clicked!"),
                "Cancel", () => Debug.Log("Cancel clicked!")
            );
        }

        #region date_time_picker

        public void OnShowDatePicker()
        {
            IGDateTimePicker.ShowDatePicker(OnDateSelected,
                () => Debug.Log("Picking date was cancelled"));
        }

        public void OnShowTimePicker()
        {
            IGDateTimePicker.ShowTimePicker(OnTimeSelected,
                () => Debug.Log("Picking time was cancelled"));
        }

        public void OnShowDateAndTimePicker()
        {
            IGDateTimePicker.ShowDateAndTimePicker(OnDateAndTimeTimeSelected,
                () => Debug.Log("Picking date and time was cancelled"));
        }

        public void OnShowCountdownTimer()
        {
            IGDateTimePicker.ShowCountDownTimer(OnCountDownTimeSelected,
                () => Debug.Log("Picking date and time was cancelled"));
        }

        void OnDateSelected(DateTime date)
        {
            Debug.Log(string.Format("Date selected: year: {0}, month: {1}, day {2}", 
                    date.Year, date.Month, date.Day));
            var pickedDate = date.ToString("yyyy MMMMM dd");
            dateText.text = string.Format("Date Picker\n{0}", pickedDate);
        }

        void OnTimeSelected(DateTime time)
        {
            Debug.Log(string.Format("Time selected: hour: {0}, minute: {1}", 
                    time.Hour, time.Minute));
            var pickedTime = time.ToString("hh:mm");
            timeText.text = string.Format("Time Picker\n{0}", pickedTime);
        }

        void OnDateAndTimeTimeSelected(DateTime dateTime)
        {
            Debug.Log(string.Format("Date & Time selected: year: {0}, month: {1}, day {2}, hour: {3}, minute: {4}",
                    dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute));

            var pickedDate = dateTime.ToString("G");
            dateAndTimeText.text = string.Format("Date & Time Picker\n{0}", pickedDate);
        }

        void OnCountDownTimeSelected(DateTime countdownTime)
        {
            
            Debug.Log(string.Format("Countdown time selected: hour: {0}, minute: {1}", 
                    countdownTime.Hour, countdownTime.Minute));
            var pickedTime = string.Format("{0}:{1}", countdownTime.Hour, countdownTime.Minute);
            countdownTimeText.text = string.Format("Time Picker\n{0}", pickedTime);
        }

        #endregion

        static readonly string[] ActionSheetOptions = { "Option 1", "Option 2", "Option 3" };
        static readonly string[] ActionSheetMoreOptions = { "Option 1", "Option 2", "Option 3", "Extra 1", "Extra 2" };

        public void OnShowActionSheet()
        {
            IGActionSheet.ShowActionSheet("Title", "Cancel", () => Debug.Log("Cancel Clicked"), 
                ActionSheetOptions, index => Debug.Log(ActionSheetOptions[index] + " Clicked"));
        }

        public void OnShowActionSheetWithDestructiveButton()
        {
            IGActionSheet.ShowActionSheet("Title", 
                "Cancel", () => Debug.Log("Cancel Clicked"), 
                "Destroy All!", () => Debug.Log("Destroy All Clicked"), 
                ActionSheetMoreOptions, index => Debug.Log(ActionSheetMoreOptions[index] + " Clicked"));
        }

        #endif
    }
}
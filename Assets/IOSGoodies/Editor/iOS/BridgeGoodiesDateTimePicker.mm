#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "GoodiesDateTimePicker.h"

extern "C" {

GoodiesDateTimePicker *pickerController;

void _showDatePicker(void *callbackPtr, OnDateSelectedDelegate *onDateSelectedDelegate,
        void *cancelPtr, ActionVoidCallbackDelegate onCancel, int datePickerType) {
    pickerController = nil;
    pickerController = [[GoodiesDateTimePicker alloc] initWithCallbackPtr:callbackPtr
                                            onDateSelectedDelegate:onDateSelectedDelegate
                                                       onCancelPtr:cancelPtr
                                                  onCancelDelegate:onCancel
                                                    datePickerType:datePickerType];
    [pickerController showPicker];
}
}

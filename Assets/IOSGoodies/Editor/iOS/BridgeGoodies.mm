//
//  Goodies.cpp
//  TestIosLibrary
//
//  Created by Taras Leskiv on 28/07/16.
//  Copyright © 2016 Dead Mosquito Games. All rights reserved.
//

#import "GoodiesAlertHandler.h"
#import "BridgeGoodisFunctionDefs.h"
#import "GoodiesUtils.h"
#import "GoodiesActionSheetDelegate.h"
#import "GoodiesImagePickerDelegate.h"
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

typedef void(ImageResultDelegate)(void *callbackPtr, const void *byteArrPtr, int arrayLength);

extern "C" {

GoodiesAlertHandler *handler;
GoodiesActionSheetDelegate *goodiesActionSheetDelegate;
GoodiesImagePickerDelegate *imagePickerDelegate;

void _showConfirmationDialog(const char *title, const char *message,
        const char *buttonTitle,
        ActionVoidCallbackDelegate callback,
        void *onSuccessActionPtr) {
    NSString *titleStr = [GoodiesUtils createNSStringFrom:title];
    NSString *messageStr = [GoodiesUtils createNSStringFrom:message];
    NSString *buttonTitleStr = [GoodiesUtils createNSStringFrom:buttonTitle];

    handler = [GoodiesAlertHandler new];
    handler.callbackButtonClicked = ^(long index) {
        callback(onSuccessActionPtr);
    };

    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:titleStr
                                                    message:messageStr
                                                   delegate:handler
                                          cancelButtonTitle:nil
                                          otherButtonTitles:buttonTitleStr, nil];
    [alert show];
}

void _showQuestionDialog(const char *title, const char *message,
        const char *buttonOkTitle,
        const char *buttonCancelTitle,
        ActionVoidCallbackDelegate callback,
        void *onSuccessActionPtr, void *onCancelActionPtr) {

    NSString *titleStr = [GoodiesUtils createNSStringFrom:title];
    NSString *messageStr = [GoodiesUtils createNSStringFrom:message];
    NSString *buttonTitleStr = [GoodiesUtils createNSStringFrom:buttonOkTitle];
    NSString *buttonCancelStr =
            [GoodiesUtils createNSStringFrom:buttonCancelTitle];

    handler = [GoodiesAlertHandler new];
    handler.callbackButtonClicked = ^(long index) {
        if (index == 0) {
            callback(onCancelActionPtr);
        } else {
            callback(onSuccessActionPtr);
        }
    };

    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:titleStr
                                                    message:messageStr
                                                   delegate:handler
                                          cancelButtonTitle:buttonCancelStr
                                          otherButtonTitles:buttonTitleStr, nil];
    [alert show];
}

void _showOptionalDialog(const char *title, const char *message,
        const char *buttonFirst,
        const char *buttonSecond,
        const char *buttonCancel,
        ActionVoidCallbackDelegate callback,
        void *onFirstButtonActionPtr,
        void *onSecondButtonActionPtr,
        void *onCancelActionPtr) {

    NSString *titleStr = [GoodiesUtils createNSStringFrom:title];
    NSString *messageStr = [GoodiesUtils createNSStringFrom:message];

    NSString *buttonCancelStr = [GoodiesUtils createNSStringFrom:buttonCancel];
    NSString *buttonFirstStr = [GoodiesUtils createNSStringFrom:buttonFirst];
    NSString *buttonSecondStr = [GoodiesUtils createNSStringFrom:buttonSecond];

    handler = [GoodiesAlertHandler new];
    handler.callbackButtonClicked = ^(long index) {
        switch (index) {
            case 0:
                callback(onCancelActionPtr);
                break;

            case 1:
                callback(onFirstButtonActionPtr);
                break;

            default:
                callback(onSecondButtonActionPtr);
                break;
        }
    };

    UIAlertView *alert =
            [[UIAlertView alloc] initWithTitle:titleStr
                                       message:messageStr
                                      delegate:handler
                             cancelButtonTitle:buttonCancelStr
                             otherButtonTitles:buttonFirstStr, buttonSecondStr, nil];
    [alert show];
}

void _showShareMessageWithImage(const char *message, const void *data,
        const unsigned long data_length,
        ActionVoidCallbackDelegate callback,
        void *onSharedActionPtr) {

    NSString *messageStr = [GoodiesUtils createNSStringFrom:message];
    NSMutableArray *array = [NSMutableArray new];

    [array addObject:messageStr];

    if (data_length > 0) {
        NSData *imageData = [[NSData alloc] initWithBytes:data length:data_length];
        UIImage *image = [UIImage imageWithData:imageData];
        [array addObject:image];
    }

    UIActivityViewController *controller =
            [[UIActivityViewController alloc] initWithActivityItems:array
                                              applicationActivities:nil];
    if ([controller respondsToSelector:@selector(popoverPresentationController)]) {
        controller.popoverPresentationController.sourceView = UnityGetGLView();
    }
    UIActivityViewController *weakController = controller;

    [UnityGetGLViewController() presentViewController:controller
                                             animated:true
                                           completion:nil];

    [controller setCompletionWithItemsHandler:^(UIActivityType activityType, BOOL completed, NSArray *returnedItems, NSError *activityError) {
        callback(onSharedActionPtr);
        weakController.completionWithItemsHandler = nil;
    }];
}

void _showActionSheet(
        const char *title,
        const char *cancelButtonTitle,
        const char *destructiveButtonTitle,
        const char *otherBtnTitles,
        ActionIntCallbackDelegate callback, void *callbackPtr) {
    NSString *titleStr = [GoodiesUtils createNSStringFrom:title];
    NSString *cancelButtonTitleStr = [GoodiesUtils createNSStringFrom:cancelButtonTitle];
    NSString *destructiveButtonTitleStr = [GoodiesUtils createNSStringFrom:destructiveButtonTitle];
    NSString *otherBtnTitlesStr = [GoodiesUtils createNSStringFrom:otherBtnTitles];
    NSArray<NSString *> *buttonItems = [otherBtnTitlesStr componentsSeparatedByString:@"|"];

    UIActionSheet *actionSheet = [[UIActionSheet alloc] initWithTitle:titleStr
                                                             delegate:nil
                                                    cancelButtonTitle:nil
                                               destructiveButtonTitle:nil
                                                    otherButtonTitles:nil];
    for (NSString *buttonTitle in buttonItems) {
        [actionSheet addButtonWithTitle:buttonTitle];
    }
    [actionSheet addButtonWithTitle:cancelButtonTitleStr];
    actionSheet.cancelButtonIndex = [buttonItems count];

    if (destructiveButtonTitle) {
        [actionSheet addButtonWithTitle:destructiveButtonTitleStr];
        actionSheet.destructiveButtonIndex = [buttonItems count] + 1;
    }

    goodiesActionSheetDelegate = [GoodiesActionSheetDelegate new];
    goodiesActionSheetDelegate.callbackButtonClicked = ^(long index) {
        callback(callbackPtr, index);
    };

    actionSheet.delegate = goodiesActionSheetDelegate;
    [actionSheet showInView:UnityGetGLView()];
}

void _openUrl(const char *link) {
    NSString *linkStr = [GoodiesUtils createNSStringFrom:link];
    NSURL *url = [NSURL URLWithString:linkStr];

    UIApplication *application = [UIApplication sharedApplication];
    if ([application respondsToSelector:@selector(openURL:options:completionHandler:)]) {
        [application openURL:url
                     options:@{}
           completionHandler:nil];
    } else {
        [application openURL:url];
    }
}

void _pickImageFromCamera(
        ImageResultDelegate callback, void *callbackPtr,
        ActionVoidCallbackDelegate cancelCallback, void *cancelPtr,
        // options
        float compressionQuality,
        bool allowsEditing,
        bool rearCamera /*rear, front*/,
        int flashMode) {

    if ([UIImagePickerController isSourceTypeAvailable:UIImagePickerControllerSourceTypeCamera]) {
        UIImagePickerController *pickerView = [[UIImagePickerController alloc] init];

        pickerView.sourceType = UIImagePickerControllerSourceTypeCamera;
        pickerView.cameraDevice = rearCamera ? UIImagePickerControllerCameraDeviceRear : UIImagePickerControllerCameraDeviceFront;
        pickerView.cameraFlashMode = (UIImagePickerControllerCameraFlashMode) flashMode;
        pickerView.allowsEditing = allowsEditing;

        // delegate
        imagePickerDelegate = [[GoodiesImagePickerDelegate alloc] initWithCompressionQuality:compressionQuality];
        imagePickerDelegate.imagePicked = ^(const void *arrayPtr, int length) {
            callback(callbackPtr, arrayPtr, length);
            [pickerView dismissViewControllerAnimated:NO completion:nil];
        };
        imagePickerDelegate.imagePickCancelled = ^() {
            [pickerView dismissViewControllerAnimated:YES completion:nil];
            if (cancelPtr) {
                cancelCallback(cancelPtr);
            }
        };
        pickerView.delegate = imagePickerDelegate;
        [UnityGetGLViewController() presentViewController:pickerView animated:YES completion:^{

        }];
    } else {
        NSLog(@"Picking image from camera not available on current device");
    }
}

void _pickImageFromGallery(
        ImageResultDelegate callback, void *callbackPtr,
        ActionVoidCallbackDelegate cancelCallback, void *cancelPtr,
        /*options*/
        int source,
        float compressionQuality,
        bool allowsEditing) {
    const int sourceTypePhotoLibrary = 0;

    UIImagePickerControllerSourceType sourceType;
    if (source == sourceTypePhotoLibrary) {
        sourceType = UIImagePickerControllerSourceTypePhotoLibrary;
    } else {
        sourceType = UIImagePickerControllerSourceTypeSavedPhotosAlbum;
    }

    if ([UIImagePickerController isSourceTypeAvailable:sourceType]) {
        UIImagePickerController *pickerView = [[UIImagePickerController alloc] init];
        pickerView.sourceType = sourceType;
        pickerView.allowsEditing = allowsEditing;
        pickerView.mediaTypes = @[@"public.image"];

        // iPad
        if ([[UIDevice currentDevice] userInterfaceIdiom] == UIUserInterfaceIdiomPad) {
            pickerView.modalPresentationStyle = UIModalPresentationPopover;
            UIPopoverPresentationController *presentationController = [pickerView popoverPresentationController];
            presentationController.permittedArrowDirections = UIPopoverArrowDirectionAny;
            presentationController.sourceView = UnityGetGLView();
        }

        // delegate
        imagePickerDelegate = [[GoodiesImagePickerDelegate alloc] initWithCompressionQuality:compressionQuality];
        imagePickerDelegate.imagePicked = ^(const void *arrayPtr, int length) {
            callback(callbackPtr, arrayPtr, length);
            [pickerView dismissViewControllerAnimated:NO completion:nil];
        };
        imagePickerDelegate.imagePickCancelled = ^() {
            [pickerView dismissViewControllerAnimated:YES completion:nil];
            if (cancelPtr) {
                cancelCallback(cancelPtr);
            }
        };
        pickerView.delegate = imagePickerDelegate;
        [UnityGetGLViewController() presentViewController:pickerView animated:YES completion:^{

        }];
    } else {
        NSLog(@"Picking image from gallery not available on current device");
    }
}

}

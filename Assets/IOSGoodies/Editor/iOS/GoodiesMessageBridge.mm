//
//  GoodiesMessageBridge.m
//  Unity-iPhone
//
//  Created by Taras Leskiv on 19/09/16.
//
//

#import <Foundation/Foundation.h>
#import "BridgeGoodisFunctionDefs.h"
#import "GoodiesUtils.h"
#import "GoodiesUiMessageDelegate.h"
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <MessageUI/MessageUI.h>

extern "C" {

GoodiesUiMessageDelegate *msgDelegate;

void _sendSms(const char *phoneNumber, const char *msgText,
        ActionVoidCallbackDelegate callback,
        void *onSuccessActionPtr,
        void *onCancelActionPtr,
        void *onFailureActionPtr) {
    NSString *phoneNumberStr = [GoodiesUtils createNSStringFrom:phoneNumber];
    NSString *msgTextStr = [GoodiesUtils createNSStringFrom:msgText];

    if (![MFMessageComposeViewController canSendText]) {
        UIAlertView *warningAlert = [[UIAlertView alloc] initWithTitle:@"Error" message:@"Your device doesn't support SMS!" delegate:nil cancelButtonTitle:@"OK" otherButtonTitles:nil];
        [warningAlert show];
        return;
    }

    msgDelegate = [GoodiesUiMessageDelegate new];
    msgDelegate.callbackSentSuccessfully = ^{
        callback(onSuccessActionPtr);
    };
    msgDelegate.callbackCancelled = ^{
        callback(onCancelActionPtr);
    };
    msgDelegate.callbackFailed = ^{
        callback(onFailureActionPtr);
    };
    MFMessageComposeViewController *messageController = [[MFMessageComposeViewController alloc] init];
    messageController.messageComposeDelegate = msgDelegate;

    NSArray *recipients = @[phoneNumberStr];
    [messageController setRecipients:recipients];
    [messageController setBody:msgTextStr];

    // Present message view controller on screen
    [UnityGetGLViewController() presentViewController:messageController animated:YES completion:nil];
}
}

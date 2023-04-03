//
//  GoodiesAlertHandler.m
//  Unity-iPhone
//
//  Created by Taras Leskiv on 03/09/16.
//
//

#import "GoodiesAlertHandler.h"

@implementation GoodiesAlertHandler

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex {
    self.callbackButtonClicked(buttonIndex);
}

- (void)alertView:(UIAlertView *)alertView willDismissWithButtonIndex:(NSInteger)buttonIndex {
}

- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
}

@end

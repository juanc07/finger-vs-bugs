//
//  GoodiesAlertHandler.h
//  Unity-iPhone
//
//  Created by Taras Leskiv on 03/09/16.
//
//

#import <Foundation/Foundation.h>

@interface GoodiesAlertHandler : NSObject <UIAlertViewDelegate>

@property(nonatomic, copy) void (^callbackButtonClicked)(long index);

@end

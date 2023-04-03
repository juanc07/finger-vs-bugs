//
// Created by Taras Leskiv on 18/01/2017.
//

#import "GoodiesImagePickerDelegate.h"

@implementation GoodiesImagePickerDelegate {
}

- (instancetype)initWithCompressionQuality:(float)compressionQuality {
    self = [super init];
    if (self) {
        _compressionQuality = compressionQuality;
    }

    return self;
}


- (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary<NSString *, id> *)info {
    UIImage *img = [info valueForKey:UIImagePickerControllerEditedImage];
    if (img == nil) {
        img = [info valueForKey:UIImagePickerControllerOriginalImage];
    }
    NSData *pictureData = UIImageJPEGRepresentation(img, _compressionQuality);
    _imagePicked(pictureData.bytes, pictureData.length);
    pictureData = nil;
}

- (void)imagePickerControllerDidCancel:(UIImagePickerController *)picker {
    if (_imagePickCancelled) {
        _imagePickCancelled();
    }
}
@end

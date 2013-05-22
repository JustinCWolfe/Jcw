//
//  Crypto.h
//  Jcw.Common
//
//  Created by Justin Wolfe on 9/10/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import <CommonCrypto/CommonCryptor.h>
#import <CommonCrypto/CommonDigest.h>
#import <Foundation/Foundation.h>


@interface Crypto : NSObject {
	
}

- (NSData *) doCipherDES64: (NSData *) inputData: (NSString *) key: (CCOperation) operation;

+ (NSData *) DecryptAES256: (NSData *) inputData: (NSString *) key;
+ (NSData *) EncryptAES256: (NSData *) inputData: (NSString *) key;

+ (NSData *) DecryptDES64: (NSData *) inputData: (NSString *) key;
+ (NSData *) EncryptDES64: (NSData *) inputData: (NSString *) key;

+ (NSData *) ComputeSHA1HashFrom: (NSData *) data;

@end 
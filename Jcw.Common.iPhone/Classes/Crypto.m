//
//  Crypto.m
//  Jcw.Common
//
//  Created by Justin Wolfe on 9/10/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import "Crypto.h"


@implementation Crypto

- (NSData *) doCipherDES64: (NSData *) inputData: (NSString *) key: (CCOperation) operation {
	// 'key' should be 64 bytes for DES, will be null-padded otherwise room for terminator (unused)
	NSData *keyData = [key dataUsingEncoding: NSASCIIStringEncoding];
	NSData *ivData = [key dataUsingEncoding: NSASCIIStringEncoding];
	
	// Calculate the number of bytes needed for the encrypted data - should be rounded up to 
	// the nearest block size divisible value.
	size_t outputBufferPtrSize = (inputData.length % kCCBlockSizeDES != 0) ? 
		(inputData.length + kCCBlockSizeDES) & ~(kCCBlockSizeDES - 1) : 
		inputData.length;
	
	// By default the input buffer size should match the output buffer size.
	size_t inputBufferPtrSize = outputBufferPtrSize;
	
	// By default we will not add any padding in the encryption process.
	CCOptions options = 0;
	
	// When encrypting, if the length of the data we are encrypting doesn't match the size of
	// the padded output buffer, add the padding option for the encryption.  Also in this case
	// set the input buffer size to the actual size of the input buffer rather than the padded 
	// size of the output buffer calculated above.
	if (operation == kCCEncrypt && inputData.length != outputBufferPtrSize) {
		inputBufferPtrSize = inputData.length;
		options |= kCCOptionPKCS7Padding;
	}

	NSMutableData *outputData = [[[NSMutableData alloc] initWithLength: outputBufferPtrSize] autorelease];
	
	// decrypts in-place, since this is a mutable data object
	size_t numBytesDecrypted = 0;
	CCCryptorStatus result = CCCrypt (operation, 
									  kCCAlgorithmDES, 
									  options,
									  keyData.bytes,
									  kCCKeySizeDES, 
									  ivData.bytes,
									  inputData.bytes, /* input */
									  inputBufferPtrSize, /* input length */
									  [outputData mutableBytes], /* output */
									  outputBufferPtrSize, /* output length */
									  &numBytesDecrypted);
	
	(operation == kCCEncrypt) ?
		NSLog(@"EncryptDES64 result: %i. Encrypted %lu bytes", result, numBytesDecrypted) :
		NSLog(@"DecryptDES64 result: %i. Decrypted %lu bytes", result, numBytesDecrypted);
	
	return outputData;
}

+ (NSData *) DecryptAES256: (NSData *) inputData: (NSString *) key {
	// 'key' should be 32 bytes for AES256, will be null-padded otherwise
	// room for terminator (unused)
	char *keyPtr = malloc (kCCKeySizeAES256 + 1);
	
	// Calculate the number of bytes needed for the encrypted data - should be rounded up to 
	// the nearest block size divisible value.
	size_t outputBufferPtrSize = (inputData.length + kCCBlockSizeAES128) & ~(kCCBlockSizeDES - 1);
	
	// fill with zeroes (for padding)
	bzero( keyPtr, sizeof(keyPtr) ); 
	
	// fetch key data
	[key getCString: keyPtr maxLength: sizeof(keyPtr) encoding: NSUTF8StringEncoding];
	
	NSMutableData *outputData = [[[NSMutableData alloc] initWithLength: outputBufferPtrSize] autorelease];
	
	// encrypts in-place, since this is a mutable data object
	size_t numBytesEncrypted = 0;
	CCCryptorStatus result = CCCrypt( kCCDecrypt, kCCAlgorithmAES128, kCCOptionPKCS7Padding,
									 keyPtr, kCCKeySizeAES256,
									 NULL /* initialization vector (optional) */,
									 inputData.bytes, inputData.length, /* input */
									 [outputData mutableBytes], outputData.length, /* output */
									 &numBytesEncrypted );
	NSLog(@"DecryptAES256 result: %i. Decrypted %lu bytes", result, numBytesEncrypted);
	free (keyPtr);
	
	return (result == kCCSuccess) ? 
		outputData : 
		nil;
}

+ (NSData *) EncryptAES256: (NSData *) inputData: (NSString *) key {
	// 'key' should be 32 bytes for AES256, will be null-padded otherwise
	// room for terminator (unused)
	char *keyPtr = malloc(kCCKeySizeAES256 + 1); 
	
	// Calculate the number of bytes needed for the encrypted data - should be rounded up to 
	// the nearest block size divisible value.
	size_t outputBufferPtrSize = (inputData.length + kCCBlockSizeAES128) & ~(kCCBlockSizeDES - 1);
	
	// fill with zeroes (for padding)
	bzero( keyPtr, sizeof(keyPtr) );
	
	// fetch key data
	[key getCString: keyPtr maxLength: sizeof(keyPtr) encoding: NSASCIIStringEncoding];
	
	NSMutableData *outputData = [[[NSMutableData alloc] initWithLength: outputBufferPtrSize] autorelease];
	
	// encrypts in-place, since this is a mutable data object
	size_t numBytesEncrypted = 0;
	CCCryptorStatus result = CCCrypt( kCCEncrypt, kCCAlgorithmAES128, kCCOptionPKCS7Padding,
									 keyPtr, kCCKeySizeAES256,
									 NULL /* initialization vector (optional) */,
									 inputData.bytes, [inputData length], /* input */
									 [outputData mutableBytes], [outputData length], /* output */
									 &numBytesEncrypted );
	NSLog(@"EncryptAES256 result: %i. Encrypted %lu bytes", result, numBytesEncrypted);	
	free(keyPtr);
	
	return (result == kCCSuccess) ? 
		outputData : 
		nil;
}

+ (NSData *) DecryptDES64: (NSData *) inputData: (NSString *) key {
	Crypto *crypto = [[Crypto alloc] init];
	
	NSData *outputData = [crypto doCipherDES64: inputData: key: kCCDecrypt];
	[crypto release];
	
	return outputData;
}

+ (NSData *) EncryptDES64: (NSData *) inputData: (NSString *) key {
	Crypto *crypto = [[Crypto alloc] init];
	
	NSData *outputData = [crypto doCipherDES64: inputData: key: kCCEncrypt];
	[crypto release];
	
	return outputData;
}

+ (NSData *) ComputeSHA1HashFrom: (NSData *) data {
	uint8_t digest[CC_SHA1_DIGEST_LENGTH];
	CC_SHA1 (data.bytes, data.length, digest);
	return [NSData dataWithBytes: digest length: CC_SHA1_DIGEST_LENGTH];
}

@end 
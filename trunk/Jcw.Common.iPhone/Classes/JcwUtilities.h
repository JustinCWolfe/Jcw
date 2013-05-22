//
//  JcwUtilities.h
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 2/23/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import <Foundation/Foundation.h>


id nilIsNull (id value);


double const JCW_G;
double const JCW_GE;
double const JCW_FTLBSPERSEC_IN_ONE_HP;

@interface JcwUtilities : NSObject {

}

+ (double) GetMetersFromInches: (double) inches;
+ (double) GetInchesFromMeters: (double) meters;
+ (double) GetFeetFromMeters: (double) meters;
+ (double) GetMetersFromFeet: (double) feet;
+ (double) GetFeetFromInches: (double) inches;
+ (double) GetFeetLbsFromNewtonMeters: (double) newtonMeters;
+ (double) GetMPHFromFeetPerSecond: (double) feetPerSecond;
+ (double) GetFeetPerSecondFromMPH: (double) mph;
+ (double) GetHPFromFeetLbsPerSecond: (double) feetLbsPerSecond;
+ (double) GetKilogramsFromPoundsWeight: (double) pounds;
+ (double) GetPoundsWeightFromKilograms: (double) kilograms;
+ (double) GetCaloriesFromJoules: (double) joules;

+ (UInt16) Convert15To16Bit: (Byte) low: (Byte) high;
+ (UInt32) JoinLowAndHighByte: (Byte) low: (Byte) high;
+ (UInt16) Convert12To16Bit: (Byte) low: (Byte) high;
+ (void) SplitIntoLowAndHighBytes: (int) combined: (Byte *) outLow: (Byte *) outHigh;

+ (double) CalculateVoltage: (double) value;

+ (NSComparisonResult) FloatCompare:(NSString *) value1: (NSString *) value2;
+ (NSComparisonResult) IntCompare:(NSString *) value1: (NSString *) value2;

+ (NSArray *) GetNumericAscSortDescriptor;
+ (NSArray *) GetNumericDescSortDescriptor;

+ (NSString *) GetDataFilePath;

+ (NSDictionary *) GetFilteredDictionaryForObjectsKindOfClass: (NSDictionary *) source: (Class) classFilter: (BOOL) includeByFilter;
+ (NSSet *) GetFilteredSetForObjectsKindOfClass: (NSSet *) source: (Class) classFilter: (BOOL) includeByFilter;

+ (NSString *) GetCurrentDateTime: (NSString *) timeSeparator: (NSString *) dateSeparator: (NSString *) dateTimeSeparator;
+ (NSString *) GetDateStringFromDate: (NSDate *) dateToConvert: (NSString *) dateFormat;
+ (NSDate *) GetDateFromString: (NSString *) dateToConvert: (NSString *) dateFormat;
+ (NSString *) GetWindowsDateStringFromDate: (NSDate *) dateToConvert;
+ (NSDate *) GetDateFromWindowsDateString: (NSString *) dateToConvert;

+ (NSData *) GetDataFromFile: (NSString *) filename: (NSStringEncoding) stringEncoding;
+ (NSString *) GetStringFrom: (NSData *) data: (NSStringEncoding) stringEncoding;
+ (NSData *) GetDataFrom: (NSString *) stringData: (NSStringEncoding) stringEncoding;
+ (NSData *) GetDataFrom: (NSArray *) array;
+ (NSArray *) GetArrayFrom: (NSData *) data;
+ (NSArray *) GetArrayFromByte: (NSData *) data;

+ (NSArray *) GetIntNumbersFromStrings: (NSArray *) stringArray;
+ (NSArray *) GetStringsFromIntNumbers: (NSArray *) numberArray;

+ (NSArray *) Reverse: (NSArray *) array;
+ (BOOL) VectorsAreEqual: (NSArray *) v1: (NSArray *) v2: (NSNumberFormatter *) comparisonFormatter;

@end

//
//  JcwUtilities.m
//  Jcw.Common 
//
//  Created by Justin C. Wolfe on 2/23/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import "JcwUtilities.h"


id nilIsNull (id value) { 
	return value ? value : [NSNull null]; 
}


double const JCW_G = 9.80665;
double const JCW_GE = 32.17417;
double const JCW_FTLBSPERSEC_IN_ONE_HP = 550;


@implementation JcwUtilities

static NSSortDescriptor *numericArrayAscSortDescriptor;
static NSSortDescriptor *numericArrayDescSortDescriptor;

+ (void) initialize {
	if (numericArrayAscSortDescriptor == nil) {
		numericArrayAscSortDescriptor = [[NSSortDescriptor alloc] initWithKey:@"intValue" ascending:YES];	
		numericArrayDescSortDescriptor = [[NSSortDescriptor alloc] initWithKey:@"intValue" ascending:NO];	
	}
}

+ (double) GetMetersFromInches: (double) inches {
	// there are .0254 meters in 1 inch
	return inches * .0254;
}

+ (double) GetInchesFromMeters: (double) meters {
	// there are 39.37007874 inches in 1 meter 
	return meters * 39.37007874;
}

+ (double) GetFeetFromMeters: (double) meters {
	// there are 3.280839895 feet in 1 meter
	return meters * 3.280839895;
}

+ (double) GetMetersFromFeet: (double) feet {
	// there are .3048 meters in 1 foot
	return feet * .3048;
}

+ (double) GetFeetFromInches: (double) inches {
	// there are .083333333333 feet in 1 inch 
	return inches * .083333333333;
}

+ (double) GetFeetLbsFromNewtonMeters: (double) newtonMeters {
	// there are .73756214728 lb-ft in 1 N-m
	return newtonMeters * .73756214728;
}

+ (double) GetMPHFromFeetPerSecond: (double) feetPerSecond {
	// there are .68181818182 mph in 1 ft/s
	return feetPerSecond * .68181818182;
}

+ (double) GetFeetPerSecondFromMPH: (double) mph {
	// there are 1.4666666667 mph in 1 ft/s
	return mph * 1.4666666667;
}

+ (double) GetHPFromFeetLbsPerSecond: (double) feetLbsPerSecond {
	// there are .0018181817572 hp in 1 ft-lbs/s
	return feetLbsPerSecond * .0018181817572;
}

+ (double) GetKilogramsFromPoundsWeight: (double) pounds {
	// there are .45359237 kg in 1 lb
	return pounds * .45359237;
}

+ (double) GetPoundsWeightFromKilograms: (double) kilograms {
	// there are 2.2046226218 lbs in 1 kg
	return kilograms * 2.2046226218;
}


+ (double) GetCaloriesFromJoules: (double) joules {
	// there are 0.2388458966 calories in 1 joule
	return joules * 0.2388458966;
}

+ (UInt16) Convert15To16Bit: (Byte) low: (Byte) high {
	UInt16 data = (UInt16) ( high * 256 + low );
	UInt16 limit = (UInt16) ( pow ( 2, 15 ) );
	return ( data > limit ) ? limit : data;
}

+ (UInt32) JoinLowAndHighByte: (Byte) low: (Byte) high {
	UInt32 data = (UInt32) ( high * 256 + low );
	UInt32 limit = (UInt32) ( pow ( 2, 16 ) );
	return ( data > limit ) ? limit : data;
}

+ (UInt16) Convert12To16Bit: (Byte) low: (Byte) high {
	UInt16 data = (UInt16) ( high * 256 + low );
	UInt16 limit = (UInt16) ( pow ( 2, 12 ) );
	return ( data > limit ) ? limit : data;
}

+ (void) SplitIntoLowAndHighBytes: (int) combined: (Byte *) outLow: (Byte *) outHigh {
	*outHigh = (Byte) (combined / (int) pow(2, 8));
	*outLow = (Byte) (combined - *outHigh * (int) pow(2, 8));
}

+ (double) CalculateVoltage: (double) value {
	// the voltage is the 2 byte value divided by 4096 * 3.3V
	// 3.3V is the operating voltage of the device
	return ( value / pow ( 2, 12 ) ) * 3.3;
}

+ (NSComparisonResult) FloatCompare:(NSString *) value1: (NSString *) value2 {
	float fvalue1 = [value1 floatValue];
	float fvalue2 = [value2 floatValue];
	
	if (fvalue1 == fvalue1) 
		return NSOrderedSame;
	
	return (fvalue1 < fvalue2) ? 
		NSOrderedAscending : 
		NSOrderedDescending;
}

+ (NSComparisonResult) IntCompare:(NSString *) value1: (NSString *) value2 {
	int ivalue1 = [value1 intValue];
	int ivalue2 = [value2 intValue];
	
	if (ivalue1 == ivalue2) 
		return NSOrderedSame;
	
	return (ivalue1 < ivalue2) ? 
		NSOrderedAscending : 
		NSOrderedDescending;
}

+ (NSArray *) GetNumericAscSortDescriptor {
	return [[[NSArray alloc] initWithObjects: numericArrayAscSortDescriptor, nil] autorelease];
}

+ (NSArray *) GetNumericDescSortDescriptor {
	return [[[NSArray alloc] initWithObjects: numericArrayDescSortDescriptor, nil] autorelease];
}

+ (NSString *) GetDataFilePath {
	NSArray *paths = NSSearchPathForDirectoriesInDomains (NSDocumentDirectory, NSUserDomainMask, YES);
	return [paths objectAtIndex: 0];
}

+ (NSDictionary *) GetFilteredDictionaryForObjectsKindOfClass: (NSDictionary *) source: (Class) classFilter: (BOOL) includeByFilter {
	NSSet *filteredKeys = [source keysOfEntriesPassingTest: ^BOOL(id key, id object, BOOL *stop) {
		return includeByFilter ? [object isKindOfClass: classFilter] : ![object isKindOfClass: classFilter];
	}];
	
	NSMutableDictionary *filtered = [[[NSMutableDictionary alloc] init] autorelease];
	for (id key in filteredKeys) {
		[filtered setObject: [source objectForKey: key] forKey: key];
	}  
	
	return filtered;
}

+ (NSSet *) GetFilteredSetForObjectsKindOfClass: (NSSet *) source: (Class) classFilter: (BOOL) includeByFilter {
	NSPredicate *predicate = [NSPredicate predicateWithBlock: ^BOOL(id evalulatedObject, NSDictionary *bindings) {
		return includeByFilter ? [evalulatedObject isKindOfClass: classFilter] : ![evalulatedObject isKindOfClass: classFilter];
	}];	
	return [source filteredSetUsingPredicate: predicate];
}

+ (NSString *) GetCurrentDateTime: (NSString *) dateSeparator: (NSString *) dateTimeSeparator: (NSString *) timeSeparator {
	// This should return the current date in the following format:
	// Date => 11/23/2010
	// Time => 3:30:32pm
	NSString *dateFormat = [NSString stringWithFormat: @"%@%@%@%@%@", @"MM", dateSeparator, @"dd", dateSeparator, @"yyyy"];
	NSString *timeFormat = [NSString stringWithFormat: @"%@%@%@%@%@", @"HH", timeSeparator, @"mm", timeSeparator, @"ss"];
	NSString *dateTimeFormat = [NSString stringWithFormat: @"%@%@%@", dateFormat, dateTimeSeparator, timeFormat];
	
	return [JcwUtilities GetDateStringFromDate: [NSDate date]: dateTimeFormat];
}

+ (NSString *) GetDateStringFromDate: (NSDate *) dateToConvert: (NSString *) dateFormat {
	NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
	[dateFormatter setDateFormat: dateFormat];
	
	NSString *formattedDateString = [dateFormatter stringFromDate: dateToConvert];
	[dateFormatter release];
	return formattedDateString;
}

+ (NSDate *) GetDateFromString: (NSString *) dateToConvert: (NSString *) dateFormat {
	NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
	[dateFormatter setDateFormat: dateFormat];
	
	NSDate *formattedDate = [dateFormatter dateFromString: dateToConvert];
	[dateFormatter release];
	return formattedDate;
}

// Stringified Windows dates look like: 2008-09-25T00:00:00.
+ (NSString *) GetWindowsDateStringFromDate: (NSDate *) dateToConvert {
	return [JcwUtilities GetDateStringFromDate: dateToConvert: @"yyyy-MM-dd'T'HH:mm:ss"];
}

// Stringified Windows dates look like: 2008-09-25T00:00:00.
+ (NSDate *) GetDateFromWindowsDateString: (NSString *) dateToConvert {	
	return [JcwUtilities GetDateFromString: dateToConvert: @"yyyy-MM-dd'T'HH:mm:ss"];
}

+ (NSData *) GetDataFromFile: (NSString *) filename: (NSStringEncoding) stringEncoding {
	NSError *err;
	NSString *fileStringData = [NSString stringWithContentsOfFile: filename encoding: stringEncoding error: &err];
	
	if (fileStringData != nil) 
		return [fileStringData dataUsingEncoding: stringEncoding];
		
	return nil;
}

+ (NSString *) GetStringFrom: (NSData *) data: (NSStringEncoding) stringEncoding {	
	int dataLength = [data length];
	
	char *cStringData = malloc (dataLength + 1);
	[data getBytes: cStringData length: dataLength];
	cStringData[dataLength] = '\0';
	
	NSString *nsStringData = [NSString stringWithCString: cStringData encoding: stringEncoding]; 
	free(cStringData);
	
	return nsStringData;
}

+ (NSData *) GetDataFrom: (NSString *) stringData: (NSStringEncoding) stringEncoding {
    return [stringData dataUsingEncoding: stringEncoding];
}

+ (NSData *) GetDataFrom: (NSArray *) array {
    // Declare and allocate C array for the contents of the NSArray parameter.
    id *cArray;
    NSRange wholeArrayRange = NSMakeRange(0, [array count]); 
    cArray = malloc(sizeof(id) * wholeArrayRange.length);
    
    // Copy the NSArray parameter's objects to the C array.
    [array getObjects: cArray range: wholeArrayRange];
     
    // Initialize the NSData object with the contents of the C array.
    NSData *data = [NSData dataWithBytes: (const void *) cArray length: sizeof(id) * array.count];
    
    // Free the memory allocated for the C array since it is no longer required.
    free(cArray);
    
    return data;
}

+ (NSArray *) GetArrayFrom: (NSData *) data {  
    NSUInteger dataSize = [data length] / sizeof(id);    
    NSLog(@"Data size: %d", dataSize);

    // Get a pointer to the NSData object's data as a C array.
    id* cArray = (id*) [data bytes];
    
    // Copy the contens of the NSData object C array to a mutable NSArray.
    NSMutableArray *array = [[[NSMutableArray alloc] initWithCapacity: dataSize] autorelease];
    for (int i = 0 ; i < dataSize; i++)
    {
        id element = cArray[i]; 
        [array addObject: element];
    }
    
    return array;
}

+ (NSArray *) GetArrayFromByte: (NSData *) data {  
    NSUInteger dataSize = [data length] / sizeof(char);  
    NSLog(@"Data size: %d", dataSize);
    
    // Get a pointer to the NSData object's data as a C array.
    char* cArray = (char*) [data bytes];
    
    // Copy the contens of the NSData object C array to a mutable NSArray.
    NSMutableArray *array = [[[NSMutableArray alloc] initWithCapacity: dataSize] autorelease];
    for (int i = 0 ; i < dataSize; i++) {
        unsigned char element = cArray[i]; 
        [array addObject: [NSNumber numberWithUnsignedChar: element]];
    }
    
    return array;
}

+ (NSArray *) GetIntNumbersFromStrings: (NSArray *) stringArray {
	NSMutableArray *numbers = [[NSMutableArray alloc] init];
	
	for (id string in stringArray) {	
		// Trim whitespace, newline and control characters from the string.
		NSString *trimmedString = [string stringByTrimmingCharactersInSet: [NSCharacterSet whitespaceAndNewlineCharacterSet]];
		trimmedString = [trimmedString stringByTrimmingCharactersInSet: [NSCharacterSet controlCharacterSet]];
		
		// Skip empty strings.
		if ([trimmedString length] > 0) {
			[numbers addObject: [NSNumber numberWithInt: [trimmedString intValue]]];
		}
	}
								   
	return [numbers autorelease];
}

+ (NSArray *) GetStringsFromIntNumbers: (NSArray *) numberArray {
	NSMutableArray *strings = [[NSMutableArray alloc] init];
	
	for (id number in numberArray) {
		[strings addObject: [NSString stringWithString: [number stringValue]]];
	}
	
	return [strings autorelease];
}

+ (NSArray *) Reverse: (NSArray *) array {
	NSMutableArray *reversedArray = [NSMutableArray arrayWithCapacity: [array count]];
	
	NSEnumerator *enumerator = [array reverseObjectEnumerator];
	for (id element in enumerator) {
		[reversedArray addObject: element];
	}
	
	return reversedArray;	
}

+ (BOOL) VectorsAreEqual: (NSArray *) v1: (NSArray *) v2: (NSNumberFormatter *) comparisonFormatter {
	if (v1.count != v2.count)
		return NO;
	
	if ([v1 isEqualToArray: v2])
		return YES;
	
	double largestDelta = 0;
	BOOL vectorsAreEqual = YES;
	for (int i = 0; i < v1.count; i++) {
		NSNumber *v1AtI = [v1 objectAtIndex: i];
		NSNumber *v2AtI = [v2 objectAtIndex: i];
		NSString *v1AtIString = [comparisonFormatter stringFromNumber: v1AtI];
		NSString *v2AtIString = [comparisonFormatter stringFromNumber: v2AtI];
		if (![v1AtIString isEqual: v2AtIString]) {
			vectorsAreEqual = NO;
			double delta = fabs([v1AtI doubleValue] - [v2AtI doubleValue]);
			if (delta > largestDelta)
				largestDelta = delta;
#ifdef DEBUG_VERBOSE
			NSLog(@"i=%i, v1=%@, v2=%@, delta=%f", i, v1AtIString, v2AtIString, delta);
#endif
		}
	}
	
#ifdef DEBUG 
	if (!vectorsAreEqual)
		NSLog(@"Equal=%d, largestDelta=%f", vectorsAreEqual, largestDelta);
#endif
	
	return vectorsAreEqual;
}

@end

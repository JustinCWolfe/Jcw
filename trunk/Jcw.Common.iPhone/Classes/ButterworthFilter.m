//
//  ButterworthFilter.m
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 7/31/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import "ButterworthFilter.h"


@implementation FilterCoefficient

@synthesize numerator, denominator;

+ (id<FilterCoefficientProtocol>) getFilterCoefficientWith: (double) numerator: (double) denominator {
	FilterCoefficient *fc = [[FilterCoefficient alloc] init];
	fc.numerator = numerator;
	fc.denominator = denominator;
	return [fc autorelease];
}

@end


@implementation FilterCoefficientList

@synthesize filterCoefficients;

- (void) dealloc {
	[filterCoefficients release];
	
	[super dealloc];
}


+ (id<FilterCoefficientListProtocol>) getFilterCoefficientListWith: (NSArray *) coefficients{
	FilterCoefficientList *fcl = [[FilterCoefficientList alloc] init];
	fcl.filterCoefficients = coefficients;
	return [fcl autorelease];
}

@end


@implementation ButterworthFilter

@synthesize filterCoefficientDictionary;

- (id) init {
    self = [super init];
	if (self) {
		filterCoefficientDictionary = [[NSMutableDictionary alloc] init];
		
		// 1Hz samle frequency - each numerator coefficient below must be multipled by 1.0e-4 
		// 0.00024136223131   0.00096544892525   0.00144817338787   0.00096544892525   0.00024136223131
		// 1.00000000000000  -3.93432582079874   5.80512542105514  -3.80723245722885   0.93643324315202
		NSArray *coefficients1 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00024136223131e-4: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.00096544892525e-4: -3.93432582079874], 
		[FilterCoefficient getFilterCoefficientWith: 0.00144817338787e-4: 5.80512542105514], 
		[FilterCoefficient getFilterCoefficientWith: 0.00096544892525e-4: -3.80723245722885], 
		[FilterCoefficient getFilterCoefficientWith: 0.00024136223131e-4: 0.93643324315202],
		nil];
		
		id<FilterCoefficientListProtocol> fcl1 = [FilterCoefficientList getFilterCoefficientListWith: coefficients1];
		[filterCoefficientDictionary setObject: fcl1 forKey: @"1"];
		
		// 2Hz samle frequency - each numerator coefficient below must be multipled by 1.0e-4 
		// 0.00373937862841   0.01495751451364   0.02243627177045   0.01495751451364   0.00373937862841
		// 1.00000000000000  -3.86865666790855   5.61452684963494  -3.62276075956140   0.87689656084082		
		NSArray *coefficients2 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00373937862841e-4: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.01495751451364e-4: -3.86865666790855], 
		[FilterCoefficient getFilterCoefficientWith: 0.02243627177045e-4: 5.61452684963494], 
		[FilterCoefficient getFilterCoefficientWith: 0.01495751451364e-4: -3.62276075956140], 
		[FilterCoefficient getFilterCoefficientWith: 0.00373937862841e-4: 0.87689656084082],
		nil];
		
		id<FilterCoefficientListProtocol> fcl2 = [FilterCoefficientList getFilterCoefficientListWith: coefficients2];
		[filterCoefficientDictionary setObject: fcl2 forKey: @"2"];
		
		// 3Hz samle frequency - each numerator coefficient below must be multipled by 1.0e-4 
		// 0.01833801797357   0.07335207189429   0.11002810784144   0.07335207189429   0.01833801797357
		// 1.00000000000000  -3.80299754165438   5.42816596368389  -3.44626421809320   0.82112513689245
		NSArray *coefficients3 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.01833801797357e-4: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.07335207189429e-4: -3.80299754165438], 
		[FilterCoefficient getFilterCoefficientWith: 0.11002810784144e-4: 5.42816596368389], 
		[FilterCoefficient getFilterCoefficientWith: 0.07335207189429e-4: -3.44626421809320], 
		[FilterCoefficient getFilterCoefficientWith: 0.01833801797357e-4: 0.82112513689245],
		nil];
		
		id<FilterCoefficientListProtocol> fcl3 = [FilterCoefficientList getFilterCoefficientListWith: coefficients3];
		[filterCoefficientDictionary setObject: fcl3 forKey: @"3"];
		
		// 4Hz samle frequency - each numerator coefficient below must be multipled by 1.0e-4 
		// 0.05616562286370   0.22466249145481   0.33699373718221   0.22466249145481   0.05616562286370
		// 1.00000000000000  -3.73735339098582   5.24600330601763  -3.27743279390188   0.76887274386665
		NSArray *coefficients4 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.05616562286370e-4: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.22466249145481e-4: -3.73735339098582], 
		[FilterCoefficient getFilterCoefficientWith: 0.33699373718221e-4: 5.24600330601763], 
		[FilterCoefficient getFilterCoefficientWith: 0.22466249145481e-4: -3.27743279390188], 
		[FilterCoefficient getFilterCoefficientWith: 0.05616562286370e-4: 0.76887274386665],
		nil];
		
		id<FilterCoefficientListProtocol> fcl4 = [FilterCoefficientList getFilterCoefficientListWith: coefficients4];
		[filterCoefficientDictionary setObject: fcl4 forKey: @"4"];
		
		// 5Hz samle frequency - each numerator coefficient below must be multipled by 1.0e-4 
		// 0.13293728898744   0.53174915594978   0.79762373392467   0.53174915594978   0.13293728898744
		// 1.00000000000000  -3.67172908916194   5.06799838673419  -3.11596692520174   0.71991032729187
		NSArray *coefficients5 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.13293728898744e-4: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.53174915594978e-4: -3.67172908916194], 
		[FilterCoefficient getFilterCoefficientWith: 0.79762373392467e-4: 5.06799838673419], 
		[FilterCoefficient getFilterCoefficientWith: 0.53174915594978e-4: -3.11596692520174], 
		[FilterCoefficient getFilterCoefficientWith: 0.13293728898744e-4: 0.71991032729187],
		nil];
		
		id<FilterCoefficientListProtocol> fcl5 = [FilterCoefficientList getFilterCoefficientListWith: coefficients5];
		[filterCoefficientDictionary setObject: fcl5 forKey: @"5"];
		
		// 25Hz samle frequency
		// 0.04658290663644   0.18633162654577   0.27949743981866   0.18633162654577    0.04658290663644
		// 1.00000000000000  -0.78209519802334   0.67997852691630  -0.18267569775303   0.03011887504317
		NSArray *coefficients25 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.04658290663644: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.18633162654577: -0.78209519802334], 
		[FilterCoefficient getFilterCoefficientWith: 0.27949743981866: 0.67997852691630], 
		[FilterCoefficient getFilterCoefficientWith: 0.18633162654577: -0.18267569775303], 
		[FilterCoefficient getFilterCoefficientWith: 0.04658290663644: 0.03011887504317],
		nil];
		
		id<FilterCoefficientListProtocol> fcl25 = [FilterCoefficientList getFilterCoefficientListWith: coefficients25];
		[filterCoefficientDictionary setObject: fcl25 forKey: @"25"];
		
		// 50Hz samle frequency
		// 0.00482434335772   0.01929737343086   0.02894606014630   0.01929737343086    0.00482434335772
		// 1.00000000000000  -2.36951300718204   2.31398841441588  -1.05466540587856   0.18737949236818
		NSArray *coefficients50 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00482434335772: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.01929737343086: -2.36951300718204], 
		[FilterCoefficient getFilterCoefficientWith: 0.02894606014630: 2.31398841441588], 
		[FilterCoefficient getFilterCoefficientWith: 0.01929737343086: -1.05466540587856], 
		[FilterCoefficient getFilterCoefficientWith: 0.00482434335772: 0.18737949236818],
		nil];
		
		id<FilterCoefficientListProtocol> fcl50 = [FilterCoefficientList getFilterCoefficientListWith: coefficients50];
		[filterCoefficientDictionary setObject: fcl50 forKey: @"50"];
		
		// 75Hz samle frequency
		// 0.00117527954957   0.00470111819828   0.00705167729742   0.00470111819828    0.00117527954957
		// 1.00000000000000  -2.90904949325273   3.28399602666864  -1.68764549946868   0.33150343884590
		NSArray *coefficients75 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00117527954957: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.00470111819828: -2.90904949325273], 
		[FilterCoefficient getFilterCoefficientWith: 0.00705167729742: 3.28399602666864], 
		[FilterCoefficient getFilterCoefficientWith: 0.00470111819828: -1.68764549946868], 
		[FilterCoefficient getFilterCoefficientWith: 0.00117527954957: 0.33150343884590],
		nil];
		
		id<FilterCoefficientListProtocol> fcl75 = [FilterCoefficientList getFilterCoefficientListWith: coefficients75];
		[filterCoefficientDictionary setObject: fcl75 forKey: @"75"];
		
		// 100Hz samle frequency
		// 0.00041659920441   0.00166639681763   0.00249959522644   0.00166639681763    0.00041659920441
		// 1.00000000000000  -3.18063854887472   3.86119434899422  -2.11215535511097   0.43826514226198
		NSArray *coefficients100 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00041659920441: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.00166639681763: -3.18063854887472], 
		[FilterCoefficient getFilterCoefficientWith: 0.00249959522644: 3.86119434899422], 
		[FilterCoefficient getFilterCoefficientWith: 0.00166639681763: -2.11215535511097], 
		[FilterCoefficient getFilterCoefficientWith: 0.00041659920441: 0.43826514226198],
		nil];
		
		id<FilterCoefficientListProtocol> fcl100 = [FilterCoefficientList getFilterCoefficientListWith: coefficients100];
		[filterCoefficientDictionary setObject: fcl100 forKey: @"100"];
		
		// 125Hz samle frequency
		// 0.00018321602337   0.00073286409348   0.00109929614022   0.00073286409348    0.00018321602337
		// 1.00000000000000  -3.34406783771187   4.23886395088407  -2.40934285658632   0.51747819978804
		NSArray *coefficients125 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00018321602337: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.00073286409348: -3.34406783771187], 
		[FilterCoefficient getFilterCoefficientWith: 0.00109929614022: 4.23886395088407], 
		[FilterCoefficient getFilterCoefficientWith: 0.00073286409348: -2.40934285658632], 
		[FilterCoefficient getFilterCoefficientWith: 0.00018321602337: 0.51747819978804],
		nil];
		
		id<FilterCoefficientListProtocol> fcl125 = [FilterCoefficientList getFilterCoefficientListWith: coefficients125];
		[filterCoefficientDictionary setObject: fcl125 forKey: @"125"];
		
		// 150Hz samle frequency
		// 0.00009276462029   0.00037105848117   0.00055658772175   0.00037105848117    0.00009276462029
		// 1.00000000000000  -3.45318513758661   4.50413909163395  -2.62730361822823   0.57783389810556
		NSArray *coefficients150 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00009276462029: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.00037105848117: -3.45318513758661], 
		[FilterCoefficient getFilterCoefficientWith: 0.00055658772175: 4.50413909163395], 
		[FilterCoefficient getFilterCoefficientWith: 0.00037105848117: -2.62730361822823], 
		[FilterCoefficient getFilterCoefficientWith: 0.00009276462029: 0.57783389810556],
		nil];
		
		id<FilterCoefficientListProtocol> fcl150 = [FilterCoefficientList getFilterCoefficientListWith: coefficients150];
		[filterCoefficientDictionary setObject: fcl150 forKey: @"150"];
		
		// 175Hz samle frequency
		// 0.00005187706087   0.00020750824347   0.00031126236520   0.00020750824347    0.00005187706087
		// 1.00000000000000  -3.53119446617849   4.70036614668454  -2.79343915428201   0.62509750674983
		NSArray *coefficients175 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00005187706087: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.00020750824347: -3.53119446617849], 
		[FilterCoefficient getFilterCoefficientWith: 0.00031126236520: 4.70036614668454], 
		[FilterCoefficient getFilterCoefficientWith: 0.00020750824347: -2.79343915428201], 
		[FilterCoefficient getFilterCoefficientWith: 0.00005187706087: 0.62509750674983],
		nil];
		
		id<FilterCoefficientListProtocol> fcl175 = [FilterCoefficientList getFilterCoefficientListWith: coefficients175];
		[filterCoefficientDictionary setObject: fcl175 forKey: @"175"];
		
		// 200Hz samle frequency
		// 0.00003123897692   0.00012495590767   0.00018743386150   0.00012495590767    0.00003123897692
		// 1.00000000000000  -3.58973388711218   4.85127588251942  -2.92405265616246   0.66301048438589
		NSArray *coefficients200 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00003123897692: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.00012495590767: -3.58973388711218], 
		[FilterCoefficient getFilterCoefficientWith: 0.00018743386150: 4.85127588251942], 
		[FilterCoefficient getFilterCoefficientWith: 0.00012495590767: -2.92405265616246], 
		[FilterCoefficient getFilterCoefficientWith: 0.00003123897692: 0.66301048438589],
		nil];
		
		id<FilterCoefficientListProtocol> fcl200 = [FilterCoefficientList getFilterCoefficientListWith: coefficients200];
		[filterCoefficientDictionary setObject: fcl200 forKey: @"200"];
		
		// 225Hz samle frequency
		// 0.00001991914817   0.00007967659269   0.00011951488904   0.00007967659269    0.00001991914817
		// 1.00000000000000  -3.63528148250896   4.97088810856765  -3.02933941727876   0.69405149759084
		NSArray *coefficients225 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.00001991914817: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.00007967659269: -3.63528148250896], 
		[FilterCoefficient getFilterCoefficientWith: 0.00011951488904: 4.97088810856765], 
		[FilterCoefficient getFilterCoefficientWith: 0.00007967659269: -3.02933941727876], 
		[FilterCoefficient getFilterCoefficientWith: 0.00001991914817: 0.69405149759084],
		nil];
		
		id<FilterCoefficientListProtocol> fcl225 = [FilterCoefficientList getFilterCoefficientListWith: coefficients225];
		[filterCoefficientDictionary setObject: fcl225 forKey: @"225"];
		
		// 250Hz samle frequency - each numerator coefficient below must be multipled by 1.0e-4 
		// 0.13293728898744   0.53174915594978   0.79762373392467   0.53174915594978   0.13293728898744
		// 1.00000000000000  -3.67172908916194   5.06799838673419  -3.11596692520174   0.71991032729187
		NSArray *coefficients250 = [NSArray arrayWithObjects: 
		[FilterCoefficient getFilterCoefficientWith: 0.13293728898744e-4: 1.00000000000000],
		[FilterCoefficient getFilterCoefficientWith: 0.53174915594978e-4: -3.67172908916194], 
		[FilterCoefficient getFilterCoefficientWith: 0.79762373392467e-4: 5.06799838673419], 
		[FilterCoefficient getFilterCoefficientWith: 0.53174915594978e-4: -3.11596692520174], 
		[FilterCoefficient getFilterCoefficientWith: 0.13293728898744e-4: 0.71991032729187],
		nil];
		
		id<FilterCoefficientListProtocol> fcl250 = [FilterCoefficientList getFilterCoefficientListWith: coefficients250];
		[filterCoefficientDictionary setObject: fcl250 forKey: @"250"];
	}
	
	return self;
}

- (NSArray *) runFourthOrderFilterWith: (int) coefficientsForFrequency: (NSArray *) iVec {
	
	NSMutableArray *oVec = [[NSMutableArray alloc] init];
	id<FilterCoefficientListProtocol> fc = [filterCoefficientDictionary objectForKey: [NSString stringWithFormat: @"%i", coefficientsForFrequency]];
	
	double numerator0 = [[fc.filterCoefficients objectAtIndex: 0] numerator];
	double numerator1 = [[fc.filterCoefficients objectAtIndex: 1] numerator];
	double numerator2 = [[fc.filterCoefficients objectAtIndex: 2] numerator];
	double numerator3 = [[fc.filterCoefficients objectAtIndex: 3] numerator];
	double numerator4 = [[fc.filterCoefficients objectAtIndex: 4] numerator];
	
	double denominator0 = [[fc.filterCoefficients objectAtIndex: 0] denominator];
	double denominator1 = [[fc.filterCoefficients objectAtIndex: 1] denominator];
	double denominator2 = [[fc.filterCoefficients objectAtIndex: 2] denominator];
	double denominator3 = [[fc.filterCoefficients objectAtIndex: 3] denominator];
	double denominator4 = [[fc.filterCoefficients objectAtIndex: 4] denominator];
	
	if (fc != nil) {
		// Initialize the first count number of slots in the output vector to zero
		for (int rowIndex = 0; rowIndex < fc.filterCoefficients.count - 1; rowIndex++) {
			[oVec addObject: [NSNumber numberWithDouble: 0]];
		}
		
		// 4th order low pass butterworth filter with a freq ratio of 5/125
		for (int rowIndex = fc.filterCoefficients.count - 1; rowIndex < [iVec count]; rowIndex++) {
			
			double filteredValue = 
				1 / denominator0 * (
									
				numerator0 * [[iVec objectAtIndex: rowIndex] doubleValue] +
				numerator1 * [[iVec objectAtIndex: rowIndex - 1] doubleValue] +
				numerator2 * [[iVec objectAtIndex: rowIndex - 2] doubleValue] +			
				numerator3 * [[iVec objectAtIndex: rowIndex - 3] doubleValue] +			
				numerator4 * [[iVec objectAtIndex: rowIndex - 4] doubleValue] -
	
				denominator1 * [[oVec objectAtIndex: rowIndex - 1] doubleValue] -
				denominator2 * [[oVec objectAtIndex: rowIndex - 2] doubleValue] -			
				denominator3 * [[oVec objectAtIndex: rowIndex - 3] doubleValue] -			
				denominator4 * [[oVec objectAtIndex: rowIndex - 4] doubleValue]);
			
			[oVec addObject: [NSNumber numberWithDouble: filteredValue]];
		}
	}
	
	return [oVec autorelease];
}

- (void) dealloc {
	[filterCoefficientDictionary release];
	
	[super dealloc];
}

@end

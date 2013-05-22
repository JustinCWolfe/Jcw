//
//  ButterworthFilter.h
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 7/31/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import <Foundation/Foundation.h>


@protocol FilterCoefficientProtocol 

@property (nonatomic, assign) double numerator;
@property (nonatomic, assign) double denominator;

@end


@interface FilterCoefficient : NSObject <FilterCoefficientProtocol> {
@private
	double numerator;
	double denominator;
}

+ (id<FilterCoefficientProtocol>) getFilterCoefficientWith: (double) numerator: (double) denominator;

@end


@protocol FilterCoefficientListProtocol

@property (nonatomic, retain) NSArray *filterCoefficients;

@end



@interface FilterCoefficientList : NSObject <FilterCoefficientListProtocol> {
@private
	NSArray *filterCoefficients;
}

+ (id<FilterCoefficientListProtocol>) getFilterCoefficientListWith: (NSArray *) coefficients;

@end


@protocol ButterworthFilterProtocol

- (NSArray *) runFourthOrderFilterWith:  (int) coefficientsForFrequency: (NSArray *) inputVector;

@end


@interface ButterworthFilter : NSObject <ButterworthFilterProtocol> {
@private
	NSMutableDictionary *filterCoefficientDictionary;
}

@property (nonatomic, readonly) NSMutableDictionary *filterCoefficientDictionary;

@end

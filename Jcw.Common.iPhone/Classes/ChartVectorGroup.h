//
//  ChartVectorGroup.h
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 8/1/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import <Foundation/Foundation.h>


@protocol ChartVectorGroupProtocol

@property (nonatomic, retain) NSString *axisTitle;
@property (nonatomic, retain) NSString *caption;
@property (nonatomic, retain) NSArray *vectors;

+ (id<ChartVectorGroupProtocol>) getChartVectorGroupWith: (NSString *) axisTitle: 
	(NSString *) caption: (NSArray *) vectors;

@end


@interface ChartVectorGroup : NSObject <ChartVectorGroupProtocol> {
@private
	NSString *axisTitle;
	NSString *caption;
	NSArray *vectors;
}

@end

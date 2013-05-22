//
//  ChartVectorGroup.m
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 8/1/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import "ChartVectorGroup.h"


@implementation ChartVectorGroup

@synthesize axisTitle, caption, vectors;

+ (id<ChartVectorGroupProtocol>) getChartVectorGroupWith: (NSString *) axisTitle: 
	(NSString *) caption: (NSArray *) vectors {
	
	ChartVectorGroup *cvg = [[ChartVectorGroup alloc] init];
	cvg.axisTitle = axisTitle;
	cvg.caption = caption;
	cvg.vectors = vectors;
	
	return [cvg autorelease];
}

- (void) dealloc {
	[axisTitle release];
	[caption release];
	[vectors release];
	
	[super dealloc];
}

@end

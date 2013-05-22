//
//  ChartMetadata.h
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 7/24/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Serialization.h"
#import <UIKit/UIKit.h>


@protocol ChartItemProtocol

@property (nonatomic, readonly) BOOL editable;

- (NSString *) serialize;

@end


@protocol AxisCoordinateProtocol <ChartItemProtocol>

- (id) initWith: (NSString *) pAxisTitle: (double) pCoordinate: (NSString *) pName;

@property (nonatomic, retain) NSString *axisTitle;
@property (nonatomic, assign) double coordinate;
@property (nonatomic, retain) NSString *name;

@end


@interface AxisCoordinate	: NSObject <AxisCoordinateProtocol> {
@private
	NSString *name;
	double  coordinate;	
	NSString *axisTitle;
}

@end


@protocol MarkProtocol <ChartItemProtocol>

- (id) initWith: (NSString *) pText: (NSString *) pValue: (double) pXCoordinate: (NSString *) pXAxisTitle: (double) 
	pYCoordinate: (NSString *) pYAxisTitle: (NSString *) pMarkColorName;

@property (nonatomic, retain) NSString *text;
@property (nonatomic, retain) NSString *value;

@property (nonatomic, retain) id<AxisCoordinateProtocol> xCoordinate;
@property (nonatomic, retain) id<AxisCoordinateProtocol> yCoordinate;

@property (nonatomic, retain) NSString *markColorName;

@end


@interface Mark : NSObject <MarkProtocol> {
@private
	NSString *text;	
	NSString *value;
	AxisCoordinate *xCoordinate;
	AxisCoordinate *yCoordinate;
	NSString *markColorName;
}

@end


@interface Note : Mark {
}

- (id) initWith: (NSString *) pText: (NSString *) pValue: (double) pXCoordinate: (NSString *) pXAxisTitle: (double) pYCoordinate: (NSString *) pYAxisTitle;

@end


@interface CircleMarker : Mark {
}

- (id) initWith: (NSString *) pText: (NSString *) pValue: (double) pXCoordinate: (NSString *) pXAxisTitle: (double) pYCoordinate: (NSString *) pYAxisTitle;

@end


@protocol LineProtocol <ChartItemProtocol>

- (id) initWith: (int) pLineWidth: (NSString *) pText: (NSString *) pAxisName: (double) pAxisCoord: (NSString *) pAxisTitle: (NSString *) pLineColorName;

@property (nonatomic, assign) int lineWidth;
@property (nonatomic, retain) NSString *text;

@property (nonatomic, retain) id<AxisCoordinateProtocol> axisCoordinate;

@property (nonatomic, retain) NSString *markColorName;

@end


@interface Line : NSObject <LineProtocol> {
@private	
	int lineWidth;
	NSString *text;
	AxisCoordinate *axisCoordinate;
}

@end


@interface HorizontalLine : Line {
}

- (id) initWith: (int) pLineWidth: (NSString *) pText: (double) pAxisCoord: (NSString *) pAxisTitle: (NSString *) pLineColorName;

@end


@interface VerticalLine : Line {
}

- (id) initWith: (int) pLineWidth: (NSString *) pText: (double) pAxisCoord: (NSString *) pAxisTitle: (NSString *) pLineColorName;

@end


@protocol ZoneProtocol <ChartItemProtocol>

- (id) initWith: (NSString *) pAxisStartName: (double) pAxisStartCoord: (NSString *) pAxisEndName: (double) pAxisEndCoord: (NSString *) 
	  pAxisTitle: (NSString *) pOppositeAxisTitle: (NSString *) pZoneColorName;

@property (nonatomic, retain) id<AxisCoordinateProtocol> axisStartCoordinate;
@property (nonatomic, retain) id<AxisCoordinateProtocol> axisEndCoordinate;

@property (nonatomic, retain) NSString *oppositeAxisTitle;
@property (nonatomic, readonly) double zoneTotalValue;

@property (nonatomic, retain) NSString *markColorName;

@end


@interface Zone : NSObject <ZoneProtocol> {
@private	
	AxisCoordinate *axisStartCoordinate;
	AxisCoordinate *axisEndCoordinate;
	NSString *oppositeAxisTitle;
	NSString *markColorName;
}

@end


@interface HorizontalZone : Zone {
}

- (id) initWith: (double) pAxisStartCoord: (double) pAxisEndCoord: (NSString *) pAxisTitle: (NSString *) pOppositeAxisTitle: (NSString *) pZoneColorName;

@end


@interface VerticalZone : Zone {
}

- (id) initWith: (double) pAxisStartCoord: (double) pAxisEndCoord: (NSString *) pAxisTitle: (NSString *) pOppositeAxisTitle: (NSString *) pZoneColorName;

@end


@protocol ChartMetadataProtocol  <SerializeProtocol, NSXMLParserDelegate>

@property (nonatomic, assign) int metadataVersion;

@property (nonatomic, assign) BOOL isChartZoomedIn;
@property (nonatomic, assign) BOOL isChartZoomInSelected;
@property (nonatomic, assign) BOOL isChartZoomOutSelected;

@property (nonatomic, readonly) double xAxisMin;
@property (nonatomic, readonly) double xAxisMax;

@property (nonatomic, assign) BOOL areVerticalLinesVisible;
@property (nonatomic, retain) NSSet *verticalLines;

@property (nonatomic, assign) BOOL areHorizontalLinesVisible;
@property (nonatomic, retain) NSSet *horizontalLines;

@property (nonatomic, assign) BOOL areVerticalZonesVisible;
@property (nonatomic, retain) NSSet *verticalZones;

@property (nonatomic, assign) BOOL areHorizontalZonesVisible;
@property (nonatomic, retain) NSSet *horizontalZones;

@property (nonatomic, assign) BOOL areNotesVisible;
@property (nonatomic, retain) NSSet *notes;

@property (nonatomic, assign) BOOL areCircleMarkersVisible;
@property (nonatomic, retain) NSSet *circleMarkers;

- (void) addChartVerticalLine: (VerticalLine *) line;
- (void) addChartVerticalLine: (int) newLineWidth: (NSString *) newText:  (double) axisCoord: (NSString *) axisTitle: (NSString *) lineColorName;
- (void) removeVerticalLine: (VerticalLine *) line;
- (void) removeVerticalLine: (double) coordinate: (NSString *) axisTitle;
- (void) clearVerticalLines;

- (void) addChartHorizontalLine: (HorizontalLine *) line;
- (void) addChartHorizontalLine: (int) newLineWidth: (NSString *) newText:  (double) axisCoord: (NSString *) axisTitle: (NSString *) lineColorName;
- (void) removeHorizontalLine: (HorizontalLine *) line;
- (void) removeHorizontalLine: (double) coordinate: (NSString *) axisTitle;
- (void) clearHorizontalLines;

- (void) addChartVerticalZone: (VerticalZone *) zone;
- (void) addChartVerticalZone: (double) axisStartCoord: (double) axisEndCoord: (NSString *) axisTitle: (NSString *) oppositeAxisTitle: (NSString *) zoneColorName;
- (void) clearVerticalZones;

- (void) addChartHorizontalZone: (HorizontalZone *) zone;
- (void) addChartHorizontalZone: (double) axisStartCoord: (double) axisEndCoord: (NSString *) axisTitle: (NSString *) oppositeAxisTitle: (NSString *) zoneColorName;
- (void) clearHorizontalZones;

- (void) addChartNote: (Note *) note;
- (void) addChartNote:  (NSString *) newText: (NSString *) newValue: (double) xAxisCoord: (NSString *) xAxisTitle: (double) yAxisCoord: (NSString *) yAxisTitle;
- (void) removeChartNote: (Note *) note;
- (void) removeChartNote: (double) xCoordinate: (NSString *) xAxisTitle: (double) yCoordinate: (NSString *) yAxisTitle;
- (void) clearChartNotes;

- (void) addChartCircleMarker: (CircleMarker *) circleMarker;
- (void) addChartCircleMarker: (NSString *) newText: (NSString *) newValue: (double) xAxisCoord: (NSString *) 
				   xAxisTitle: (double) yAxisCoord: (NSString *) yAxisTitle;
- (void) removeChartCircleMarker: (CircleMarker *) marker;
- (void) removeChartCircleMarker: (double) xCoordinate: (NSString *) xAxisTitle: (double) yCoordinate: (NSString *) yAxisTitle;
- (void) clearChartCircleMarkers;

- (void) setXAxisRange: (double) minimum: (double) maximum;

@end


@interface ChartMetadata : NSObject <ChartMetadataProtocol> {
@private
	int metadataVersion;
	
	BOOL isChartZoomedIn;
	BOOL isChartZoomInSelected;
	BOOL isChartZoomOutSelected;
	
	double xAxisMin;
	double xAxisMax;
	
	BOOL areVerticalLinesVisible;
	NSMutableSet *verticalLines;
	
	BOOL areHorizontalLinesVisible;
	NSMutableSet *horizontalLines;
	
	BOOL areVerticalZonesVisible;
	NSMutableSet *verticalZones;
	
	BOOL areHorizontalZonesVisible;
	NSMutableSet *horizontalZones;
	
	BOOL areNotesVisible;
	NSMutableSet *notes;
	
	BOOL areCircleMarkersVisible;
	NSMutableSet *circleMarkers;
	
	XMLSerializer *xmlSerializer;
	BOOL parsingLineXml;
	NSMutableDictionary *subElementData;
}

// These should be assignable through the object itself while the protocol has these properties
// as readonly which is good for external clients.
@property (nonatomic, assign) double xAxisMin;
@property (nonatomic, assign) double xAxisMax;

@end



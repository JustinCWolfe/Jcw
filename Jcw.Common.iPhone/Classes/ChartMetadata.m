//
//  ChartMetadata.m
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 7/24/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import "ChartMetadata.h"


static NSNumberFormatter *serializedDataNumberFormatter;


@implementation AxisCoordinate

@synthesize name, coordinate, axisTitle;

- (id) initWith: (NSString *) pAxisTitle: (double) pCoordinate: (NSString *) pName {
    self = [super init];
	if (self) {
		self.axisTitle = pAxisTitle;
		self.coordinate = pCoordinate;
		self.name = pName;
	}
	
	return self; 
}

- (NSString *) description {
	return [NSString stringWithFormat: @"AxisTitle=%@,Coordinate=%f,Name=%@", self.axisTitle, self.coordinate, self.name];
}

- (NSUInteger) hash {
	return [self.description hash];
}

- (void) dealloc {
	[name release];
	[axisTitle release];
	
	[super dealloc];
}

/* ChartItemProtocol Implementation */

- (BOOL) editable {
	return FALSE;
}

- (NSString *) serialize {
	NSMutableString *coordinateData = [NSMutableString stringWithCapacity: 100];
	
	[coordinateData appendFormat: @"<Name>%@</Name>\r\n", self.name];
	[coordinateData appendFormat: @"<Coordinate>%@</Coordinate>\r\n", [serializedDataNumberFormatter stringFromNumber: 
																	   [NSNumber numberWithDouble: self.coordinate]]];
	[coordinateData appendFormat: @"<AxisTitle>%@</AxisTitle>\r\n", self.axisTitle];
	
	return coordinateData;
}

/* ChartItemProtocol Implementation */

@end


@implementation Mark

@synthesize text, value, xCoordinate, yCoordinate, markColorName;

- (id) initWith: (NSString *) pText: (NSString *) pValue: (double) pXCoordinate: (NSString *) pXAxisTitle: (double) 
   pYCoordinate: (NSString *) pYAxisTitle: (NSString *) pMarkColorName {

	if (self = [super init]) {
		self.text = pText;
		self.value = pValue;
		
		self.xCoordinate = [[(AxisCoordinate *)[AxisCoordinate alloc] initWith: pXAxisTitle: pXCoordinate: @"X"] autorelease];
		self.yCoordinate = [[(AxisCoordinate *)[AxisCoordinate alloc] initWith: pYAxisTitle: pYCoordinate: @"Y"] autorelease];
		
		self.markColorName = pMarkColorName;
	}
	
	return self;	
}

- (NSString *) description {
	return [NSString stringWithFormat: @"Text=%@,Value=%@,X(%@),Y(%@),MarkColorName%@", 
			text, value, xCoordinate.description, yCoordinate.description, markColorName];
}

- (NSUInteger) hash {
	return [self.description hash];
}

- (void) dealloc {
	[text release];
	[value release];
	[xCoordinate release];
	[yCoordinate release];
	[markColorName release];
	
	[super dealloc];
}

/* ChartItemProtocol Implementation */

- (BOOL) editable {
	return FALSE;
}

- (NSString *) serialize {
	/*<Text>FPE markers are displayed on Linear Velocity, Distance, Power, Power/Mass, Torque and Linear Acceleration charts.  FPE stands for First Pedal Effectiveness and is a measure, in degrees, of how effective the riders' first pedal in is.</Text>
	 <XCoordinate>
	 <Name>X</Name>
	 <Coordinate>6.892</Coordinate>
	 <AxisTitle>Time (seconds)</AxisTitle>
	 </XCoordinate>
	 <YCoordinate>
	 <Name>Y</Name>
	 <Coordinate>9.487383</Coordinate>
	 <AxisTitle>Linear Acceleration (m/sÂ²)</AxisTitle>
	 </YCoordinate>
	 <MarkColorName>Transparent</MarkColorName>*/
		
	NSMutableString *markData = [NSMutableString stringWithCapacity: 100];
	
	// If the text is null, don't serialize it.
	if (self.text.length > 0)
		[markData appendFormat: @"<Text>%@</Text>\r\n", self.text];	
	
	if (self.value.length > 0)
		[markData appendFormat: @"<Value>%@</Value>\r\n", self.value];	
	
	[markData appendString: @"<XCoordinate>\r\n"];
	[markData appendString: [self.xCoordinate serialize]];
	[markData appendString: @"</XCoordinate>\r\n"];
	[markData appendString: @"<YCoordinate>\r\n"];
	[markData appendString: [self.yCoordinate serialize]];
	[markData appendString: @"</YCoordinate>\r\n"];
	
	// If the mark color name is null, don't serialize it.
	if (self.markColorName.length > 0)
		[markData appendFormat: @"<MarkColorName>%@</MarkColorName>\r\n", self.markColorName];
	
	return markData;
}

/* ChartItemProtocol Implementation */

// Need to override equals, get hash code and to string

// Need to implement cloning method

@end


@implementation Note

- (id) initWith: (NSString *) pText: (NSString *) pValue: (double) pXCoordinate: (NSString *) pXAxisTitle: (double) pYCoordinate: (NSString *) pYAxisTitle {
	if (self = [super initWith: pText: pValue: pXCoordinate: pXAxisTitle: pYCoordinate: pYAxisTitle: nil]) {
	}
	return self;
}

/* ChartItemProtocol Implementation */

- (BOOL) editable {
	return TRUE;
}

- (NSString *) serialize {
	/*<Note>
	 <Mark Object>
	 <NoteText>FPE markers are displayed on Linear Velocity, Distance, Power, Power/Mass, Torque and Linear Acceleration charts.  FPE stands for First Pedal Effectiveness and is a measure, in degrees, of how effective the riders' first pedal in is.</NoteText>
	 </Note>*/
	
	NSMutableString *noteData = [NSMutableString stringWithCapacity: 100];
	
	[noteData appendString: @"<Note>\r\n"];
	[noteData appendString: [super serialize]];
	
	// If the text is null, don't serialize it.
	if (self.text.length > 0)
		[noteData appendFormat: @"<NoteText>%@</NoteText>\r\n", self.text];
	
	[noteData appendString: @"</Note>\r\n"];
	
	return noteData;
}

/* ChartItemProtocol Implementation */

@end


@implementation CircleMarker

- (id) initWith: (NSString *) pText: (NSString *) pValue: (double) pXCoordinate: (NSString *) pXAxisTitle: (double) pYCoordinate: (NSString *) pYAxisTitle {
	if (self = [super initWith: pText: pValue: pXCoordinate: pXAxisTitle: pYCoordinate: pYAxisTitle: nil]) {
	}
	return self;
}

/* ChartItemProtocol Implementation */

- (NSString *) serialize {
	/*<CircleMarker>
	 <Mark Object>
	 </CircleMarker>*/
	
	NSMutableString *circleMarkerData = [NSMutableString stringWithCapacity: 100];
	
	[circleMarkerData appendString: @"<CircleMarker>\r\n"];
	[circleMarkerData appendString: [super serialize]];
	[circleMarkerData appendString: @"</CircleMarker>\r\n"];
	
	return circleMarkerData;
}

/* ChartItemProtocol Implementation */

@end


@implementation Line

@synthesize lineWidth, text, axisCoordinate, markColorName;

- (id) initWith: (int) pLineWidth: (NSString *) pText: (NSString *) pAxisName: (double) pAxisCoord: (NSString *) pAxisTitle: (NSString *) pLineColorName {
	
	if (self = [super init]) {
		self.lineWidth = pLineWidth;
		self.text = pText;
	
		self.axisCoordinate = [[(AxisCoordinate *)[AxisCoordinate alloc] initWith: pAxisTitle: pAxisCoord: pAxisName] autorelease];
		
		// User create lines have a color name of DarkSlateBlue.
		self.markColorName = pLineColorName;
	}
	
	return self;	
}

- (double) axisCoordinateValue {
	return [axisCoordinate coordinate];
}

- (NSString *) description {
	return [NSString stringWithFormat: @"Width=%i,Text=%@,Coordinate=%@,MarkColorName=%@", 
			lineWidth, text, axisCoordinate.description, markColorName];
}

- (NSUInteger) hash {
	return [self.description hash];
}

- (void) dealloc {
	[text release];
	[axisCoordinate release];
	[markColorName release];
	
	[super dealloc];
}

/* ChartItemProtocol Implementation */

- (BOOL) editable {
	return FALSE;
}

- (NSString *) serialize {
	/*<LineWidth>1</LineWidth>
	 <Coordinate>
	 <Name>X</Name>
	 <Coordinate>6.456</Coordinate>
	 <AxisTitle>Angular Acceleration (m/sÂ²)</AxisTitle>
	 </Coordinate>
	 <MarkColorName>DarkSlateBlue</MarkColorName>*/
	
	NSMutableString *lineData = [NSMutableString stringWithCapacity: 100];
	
	[lineData appendFormat: @"<LineWidth>%i</LineWidth>\r\n", self.lineWidth];	
	
	// If the text is null, don't serialize it.
	if (self.text.length > 0)
		[lineData appendFormat: @"<Text>%@</Text>\r\n", self.text];	
	
	[lineData appendString: @"<Coordinate>\r\n"];
	[lineData appendString: [self.axisCoordinate serialize]];
	[lineData appendString: @"</Coordinate>\r\n"];
	
	// If the mark color name is null, don't serialize it.
	if (self.markColorName.length > 0)
		[lineData appendFormat: @"<MarkColorName>%@</MarkColorName>\r\n", self.markColorName];
	
	return lineData;
}

/* ChartItemProtocol Implementation */

@end


@implementation HorizontalLine

- (id) initWith: (int) pLineWidth: (NSString *) pText: (double) pAxisCoord: (NSString *) pAxisTitle: (NSString *) pLineColorName {
	
	if (self = [super initWith: pLineWidth: pText: @"Y": pAxisCoord: pAxisTitle: pLineColorName]) {
	}
	
	return self;	
}

/* ChartItemProtocol Implementation */

- (NSString *) serialize {
	/*<HorizontalLine>
	 <Line Object>
	 </HorizontalLine>*/
	
	NSMutableString *horizontalLineData = [NSMutableString stringWithCapacity: 100];
	
	[horizontalLineData appendString: @"<HorizontalLine>\r\n"];
	[horizontalLineData appendString: [super serialize]];
	[horizontalLineData appendString: @"</HorizontalLine>\r\n"];
	
	return horizontalLineData;
}

/* ChartItemProtocol Implementation */

@end


@implementation VerticalLine

- (id) initWith: (int) pLineWidth: (NSString *) pText: (double) pAxisCoord: (NSString *) pAxisTitle: (NSString *) pLineColorName {
	
	if (self = [super initWith: pLineWidth: pText: @"X": pAxisCoord: pAxisTitle: pLineColorName]) {
	}
	
	return self;	
}

/* ChartItemProtocol Implementation */

- (NSString *) serialize {
	/*<VerticalLine>
	 <Line Object>
	 </VerticalLine>*/
	
	NSMutableString *verticalLineData = [NSMutableString stringWithCapacity: 100];
	
	[verticalLineData appendString: @"<VerticalLine>\r\n"];
	[verticalLineData appendString: [super serialize]];
	[verticalLineData appendString: @"</VerticalLine>\r\n"];
	
	return verticalLineData;
}

/* ChartItemProtocol Implementation */

@end


@implementation Zone

@synthesize axisStartCoordinate, axisEndCoordinate, oppositeAxisTitle, zoneTotalValue, markColorName;

- (id) initWith: (NSString *) pAxisStartName: (double) pAxisStartCoord: (NSString *) pAxisEndName: (double) pAxisEndCoord: (NSString *) 
	 pAxisTitle: (NSString *) pOppositeAxisTitle: (NSString *) pZoneColorName {

	if (self = [super init]) {
		self.axisStartCoordinate = [[(AxisCoordinate *)[AxisCoordinate alloc] initWith: pAxisTitle: pAxisStartCoord: pAxisStartName] autorelease];
		self.axisEndCoordinate = [[(AxisCoordinate *)[AxisCoordinate alloc] initWith: pAxisTitle: pAxisEndCoord: pAxisEndName] autorelease];
		self.oppositeAxisTitle = pOppositeAxisTitle;
		self.markColorName = pZoneColorName;
	}
	
	return self;	
}

- (double) zoneTotalValue {
	return axisEndCoordinate.coordinate - axisStartCoordinate.coordinate; 
}

- (NSString *) description {
	return [NSString stringWithFormat: @"OppositeAxisTitle=%@,StartCoordinate(%@),EndCoordinate(%@),MarkColorName=%@", 
			oppositeAxisTitle, axisStartCoordinate.description, axisEndCoordinate.description, markColorName];
}

- (NSUInteger) hash {
	return [self.description hash];
}

- (void) dealloc {
	[axisStartCoordinate release];
	[axisEndCoordinate release];
	[oppositeAxisTitle release];
	[markColorName release];
	
	[super dealloc];
}

/* ChartItemProtocol Implementation */

- (BOOL) editable {
	return FALSE;
}

- (NSString *) serialize {
	/*<StartCoordinate>
	 <Name>xStart</Name>
	 <Coordinate>5.884</Coordinate>
	 <AxisTitle>Time (seconds)</AxisTitle>
	 </StartCoordinate>
	 <EndCoordinate>
	 <Name>xEnd</Name>
	 <Coordinate>5.944</Coordinate>
	 <AxisTitle>Time (seconds)</AxisTitle>
	 </EndCoordinate>
	 <ZoneColor />
	 <ZoneColorName>LightPink</ZoneColorName>
	 <OppositeAxisTitle>Centripetal Acceleration (ft/sÂ²)</OppositeAxisTitle>*/
	
	NSMutableString *zoneData = [NSMutableString stringWithCapacity: 100];
	
	[zoneData appendString: @"<StartCoordinate>\r\n"];
	[zoneData appendString: [self.axisStartCoordinate serialize]];
	[zoneData appendString: @"</StartCoordinate>\r\n"];
	
	[zoneData appendString: @"<EndCoordinate>\r\n"];
	[zoneData appendString: [self.axisEndCoordinate serialize]];
	[zoneData appendString: @"</EndCoordinate>\r\n"];
	
	// If the mark color name is null, don't serialize it.
	if (self.markColorName.length > 0)
		[zoneData appendFormat: @"<ZoneColorName>%@</ZoneColorName>\r\n", self.markColorName];
	
	// If the opposite axis title is null, don't serialize it.
	if (self.oppositeAxisTitle.length > 0)
		[zoneData appendFormat: @"<OppositeAxisTitle>%@</OppositeAxisTitle>\r\n", self.oppositeAxisTitle];
	
	return zoneData;
}

/* ChartItemProtocol Implementation */

@end


@implementation HorizontalZone

- (id) initWith: (double) pAxisStartCoord: (double) pAxisEndCoord: (NSString *) pAxisTitle: (NSString *) pOppositeAxisTitle: (NSString *) pZoneColorName {
	
	if (self = [super initWith: @"yStart": pAxisStartCoord: @"yEnd": pAxisEndCoord: pAxisTitle: pOppositeAxisTitle: pZoneColorName]) {
	}
	
	return self;	
}

/* ChartItemProtocol Implementation */

- (NSString *) serialize {
	/*<HorizontalZone>
	 <Zone Object>
	 </HorizontalZone>*/
	
	NSMutableString *horizontalZoneData = [NSMutableString stringWithCapacity: 100];
	
	[horizontalZoneData appendString: @"<HorizontalZone>\r\n"];
	[horizontalZoneData appendString: [super serialize]];
	[horizontalZoneData appendString: @"</HorizontalZone>\r\n"];
	
	return horizontalZoneData;
}

/* ChartItemProtocol Implementation */

@end


@implementation VerticalZone

- (id) initWith: (double) pAxisStartCoord: (double) pAxisEndCoord: (NSString *) pAxisTitle: (NSString *) pOppositeAxisTitle: (NSString *) pZoneColorName {
	
	if (self = [super initWith: @"xStart": pAxisStartCoord: @"xEnd": pAxisEndCoord: pAxisTitle: pOppositeAxisTitle: pZoneColorName]) {
	}
	
	return self;	
}

/* ChartItemProtocol Implementation */

- (NSString *) serialize {
	/*<VerticalZone>
	 <Zone Object>
	 </VerticalZone>*/
	
	NSMutableString *verticalZoneData = [NSMutableString stringWithCapacity: 100];
	
	[verticalZoneData appendString: @"<VerticalZone>\r\n"];
	[verticalZoneData appendString: [super serialize]];
	[verticalZoneData appendString: @"</VerticalZone>\r\n"];
	
	return verticalZoneData;
}

/* ChartItemProtocol Implementation */

@end


@implementation ChartMetadata

@synthesize metadataVersion;
@synthesize isChartZoomedIn, isChartZoomInSelected, isChartZoomOutSelected;
@synthesize xAxisMin, xAxisMax;
@synthesize areVerticalLinesVisible, verticalLines;
@synthesize areHorizontalLinesVisible, horizontalLines;
@synthesize areVerticalZonesVisible, verticalZones;
@synthesize areHorizontalZonesVisible, horizontalZones;
@synthesize areNotesVisible, notes;
@synthesize areCircleMarkersVisible, circleMarkers;

+ (void) initialize {
	serializedDataNumberFormatter = [[NSNumberFormatter alloc] init];
	[serializedDataNumberFormatter setFormatterBehavior: NSNumberFormatterBehavior10_4];
	[serializedDataNumberFormatter setDecimalSeparator: @"."];
	[serializedDataNumberFormatter setUsesGroupingSeparator: NO];
	[serializedDataNumberFormatter setMaximumFractionDigits: 16];
	[serializedDataNumberFormatter setNumberStyle: NSNumberFormatterDecimalStyle];	
}

- (id) init {
	if (self = [super init]) {
		metadataVersion = 2;

		isChartZoomedIn = isChartZoomInSelected = isChartZoomOutSelected = NO;
		areVerticalLinesVisible = areHorizontalLinesVisible = areVerticalZonesVisible = YES;
		areHorizontalZonesVisible = areNotesVisible = areCircleMarkersVisible = YES;
		
		verticalLines = [[NSMutableSet alloc] init];
		horizontalLines = [[NSMutableSet alloc] init];
		verticalZones = [[NSMutableSet alloc] init];
		horizontalZones = [[NSMutableSet alloc] init];
		notes = [[NSMutableSet alloc] init];
		circleMarkers = [[NSMutableSet alloc] init];
		
		NSArray *xmlElementNames = [NSArray arrayWithObjects:
									@"MetadataVersion",
									@"IsChartZoomedIn",
									@"IsChartZoomInSelected",
									@"IsChartZoomOutSelected",
									@"ChartHorizontalMinimum",
									@"ChartHorizontalMaximum",
									
									// Axis coordinate
									@"Name",
									@"Coordinate",
									@"AxisTitle",
									
									// Mark - general
									@"XCoordinate",
									@"YCoordinate",
									@"MarkColorName",
									@"Text",
									@"Value",
									
									// Notes
									@"Note",
									@"AreNotesVisible",
									@"NoteText",
									//@"ChartNotes",
									
									// Circle markers
									@"CircleMarker",
									@"AreCircleMarkersVisible",
									//@"ChartCircleMarkers",
									
									// Line - general
									@"LineWidth",
									
									// Vertical lines
									@"VerticalLine",
									@"AreVerticalLinesVisible",
									//@"ChartVerticalLines",
									
									// Horizontal lines
									@"HorizontalLine",
									@"AreHorizontalLinesVisible",
									//@"ChartHorizontalLines",
									
									// Zone - general
									@"StartCoordinate",
									@"EndCoordinate",
									@"ZoneColorName",
									@"OppositeAxisTitle",

									// Vertical zones
									@"VerticalZone",
									@"AreVerticalZonesVisible",
									//@"ChartVerticalZones",
	
									// Horizontal zones
									@"HorizontalZone",
									@"AreHorizontalZonesVisible",
									//@"ChartHorizontalZones",
									
									nil];
		
		xmlSerializer = [(XMLSerializer *)[XMLSerializer alloc] initWith: @"chart metadata": xmlElementNames: self];	
		subElementData = [[NSMutableDictionary alloc] init];
	}
	
	return self;
}

- (void) addChartVerticalLine: (VerticalLine *) line {
	if (![verticalLines containsObject: line]) {
		[verticalLines addObject: line];
	}
}

- (void) addChartVerticalLine: (int) newLineWidth: (NSString *) newText:  (double) axisCoord: (NSString *) axisTitle: (NSString *) lineColorName {
	VerticalLine *vl = [[(VerticalLine *)[VerticalLine alloc] initWith: newLineWidth: newText: axisCoord: axisTitle: lineColorName] autorelease];
	[self addChartVerticalLine: vl];
}

- (void) removeVerticalLine: (VerticalLine *) line {
	[verticalLines removeObject: line];
}

- (void) removeVerticalLine: (double) coordinate: (NSString *) axisTitle {
	//CircleMarker m = new CircleMarker ( xCoordinate, xAxisTitle, yCoordinate, yAxisTitle );
	//RemoveChartCircleMarker ( m );
}

- (void) clearVerticalLines {
	[verticalLines removeAllObjects];
}

- (void) addChartHorizontalLine: (HorizontalLine *) line {
	if (![horizontalLines containsObject: line]) {
		[horizontalLines addObject: line];
	}
}

- (void) addChartHorizontalLine: (int) newLineWidth: (NSString *) newText: (double) axisCoord: (NSString *) axisTitle: (NSString *) lineColorName {
	HorizontalLine *hl = [[(HorizontalLine *)[HorizontalLine alloc] initWith: newLineWidth: newText: axisCoord: axisTitle: lineColorName] autorelease];
	[self addChartHorizontalLine: hl];
}

- (void) removeHorizontalLine: (HorizontalLine *) line {
	[horizontalLines removeObject: line];
}

- (void) removeHorizontalLine: (double) coordinate: (NSString *) axisTitle {
	//CircleMarker m = new CircleMarker ( xCoordinate, xAxisTitle, yCoordinate, yAxisTitle );
	//RemoveChartCircleMarker ( m );
}

- (void) clearHorizontalLines {
	[horizontalLines removeAllObjects];
}

- (void) addChartVerticalZone: (VerticalZone *) zone {
	if (![verticalZones containsObject: zone]) {
		[verticalZones addObject: zone];
	}
}

- (void) addChartVerticalZone: (double) axisStartCoord: (double) axisEndCoord: (NSString *) axisTitle: (NSString *) oppositeAxisTitle: (NSString *) zoneColorName {
	VerticalZone *vz = [[(VerticalZone *)[VerticalZone alloc] initWith: axisStartCoord: axisEndCoord: axisTitle: oppositeAxisTitle: zoneColorName] autorelease];
	[self addChartVerticalZone: vz];
}

- (void) clearVerticalZones {
	[verticalZones removeAllObjects];
}

- (void) addChartHorizontalZone: (HorizontalZone *) zone {
	if (![horizontalZones containsObject: zone]) {
		[horizontalZones addObject: zone];
	}
}

- (void) addChartHorizontalZone: (double) axisStartCoord: (double) axisEndCoord: (NSString *) axisTitle: (NSString *) oppositeAxisTitle: (NSString *) zoneColorName {
	HorizontalZone *hz = [[(HorizontalZone *)[HorizontalZone alloc] initWith: axisStartCoord: axisEndCoord: axisTitle: oppositeAxisTitle: zoneColorName] autorelease];
	[self addChartHorizontalZone: hz];
}

- (void) clearHorizontalZones {
	[horizontalZones removeAllObjects];
}

- (void) addChartNote: (Note *) note {
	if (![notes containsObject: note]) {
		[notes addObject: note];
	}
}

- (void) addChartNote:  (NSString *) newText: (NSString *) newValue: (double) xAxisCoord: (NSString *) xAxisTitle: (double) yAxisCoord: (NSString *) yAxisTitle { 
	Note *note = [[(Note *)[Note alloc] initWith: newText: newValue: xAxisCoord: xAxisTitle: yAxisCoord: yAxisTitle] autorelease];
	[self addChartNote: note];
}

- (void) removeChartNote: (Note *) note {
	[notes removeObject: note];
}

- (void) removeChartNote: (double) xCoordinate: (NSString *) xAxisTitle: (double) yCoordinate: (NSString *) yAxisTitle {
	//CircleMarker m = new CircleMarker ( xCoordinate, xAxisTitle, yCoordinate, yAxisTitle );
	//RemoveChartCircleMarker ( m );
}

- (void) clearChartNotes {
	[notes removeAllObjects];
}

- (void) addChartCircleMarker: (CircleMarker *) marker {
	if (![circleMarkers containsObject: marker]) {
		[circleMarkers addObject: marker];
	}
}

- (void) addChartCircleMarker: (NSString *) newText: (NSString *) newValue: (double) xAxisCoord: (NSString *) 
				   xAxisTitle: (double) yAxisCoord: (NSString *) yAxisTitle {
	
	CircleMarker *cm = [[(CircleMarker *)[CircleMarker alloc] initWith: newText: newValue: xAxisCoord: xAxisTitle: yAxisCoord: yAxisTitle] autorelease];
	[self addChartCircleMarker: cm];
}

- (void) removeChartCircleMarker: (CircleMarker *) marker {
	[circleMarkers removeObject: marker];
}

- (void) removeChartCircleMarker: (double) xCoordinate: (NSString *) xAxisTitle: (double) yCoordinate: (NSString *) yAxisTitle {
	//CircleMarker m = new CircleMarker ( xCoordinate, xAxisTitle, yCoordinate, yAxisTitle );
	//RemoveChartCircleMarker ( m );
}

- (void) clearChartCircleMarkers {
	[circleMarkers removeAllObjects];
}

- (void) setXAxisRange: (double) minimum: (double) maximum {
	xAxisMin = minimum;
	xAxisMax = maximum;
}

- (BOOL) editable {
	return TRUE;
}

- (void) deserialize: (NSData *) data {
	[xmlSerializer deserialize: data];
}

- (void) parser: (NSXMLParser *) parser didStartElement: (NSString *) elementName namespaceURI: (NSString *) namespaceURI 
  qualifiedName: (NSString *) qualifiedName attributes: (NSDictionary *) attributeDict {
	
	// Root elements which can have child elements.
	if ([elementName isEqualToString: @"HorizontalLine"] || [elementName isEqualToString: @"VerticalLine"]) {
		// Initialize the dictionary of sub element data.
		[subElementData removeAllObjects];
		parsingLineXml = YES;
	}
	if ([elementName isEqualToString: @"HorizontalZone"] || [elementName isEqualToString: @"VerticalZone"] ||
		[elementName isEqualToString: @"Note"] || [elementName isEqualToString: @"CircleMarker"]) {
		// Initialize the dictionary of sub element data.
		[subElementData removeAllObjects];
		parsingLineXml = NO;
	}
	else if ([xmlSerializer.xmlElementNames containsObject: elementName]) {
		[xmlSerializer foundStartingElement];
	}	
}

- (void) parser: (NSXMLParser *) parser foundCharacters: (NSString *) string {
	[xmlSerializer foundCharacters: string];
}

- (void) parser: (NSXMLParser *) parser didEndElement: (NSString *) elementName namespaceURI: (NSString *) namespaceURI 
  qualifiedName: (NSString *) qName {
	
	// Deserialize chart metadata
	if ([elementName isEqualToString: @"MetadataVersion"]) {
		self.metadataVersion = 0;
		self.metadataVersion = [xmlSerializer.currentProperty intValue];
	}
	else if ([elementName isEqualToString: @"IsChartZoomedIn"]) {
		self.isChartZoomedIn = NO;
		self.isChartZoomedIn = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}	
	else if ([elementName isEqualToString: @"IsChartZoomInSelected"]) {
		self.isChartZoomInSelected = NO;
		self.isChartZoomInSelected = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}
	else if ([elementName isEqualToString: @"IsChartZoomOutSelected"]) {
		self.isChartZoomOutSelected = NO;
		self.isChartZoomOutSelected = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}
	else if ([elementName isEqualToString: @"ChartHorizontalMinimum"]) {
		self.xAxisMin = [xmlSerializer.currentProperty doubleValue];
	}
	else if ([elementName isEqualToString: @"ChartHorizontalMaximum"]) {
		self.xAxisMax = [xmlSerializer.currentProperty doubleValue];
	}
	
	// Axis coordinate
	else if ([elementName isEqualToString: @"Name"] || [elementName isEqualToString: @"AxisTitle"]) {
		[subElementData setObject: [NSString stringWithString: xmlSerializer.currentProperty] forKey: elementName];
	}
	else if ([elementName isEqualToString: @"Coordinate"]) {
		// For lines we can get 2 coordinate keys - the AxisCoordinate object itself and the actual coordinate value.
		// Since we are looking at end of the xml elements, for lines we only care about the end of the first Coordinate 
		// because the second one will be the close of the AxisCoordinate object xml block.
		if (parsingLineXml) {
			if ([subElementData valueForKey: elementName] == nil)
				[subElementData setObject: [NSString stringWithString: xmlSerializer.currentProperty] forKey: elementName];
		}
		else 
			[subElementData setObject: [NSString stringWithString: xmlSerializer.currentProperty] forKey: elementName];
	}

	// Mark - general
	else if ([elementName isEqualToString: @"MarkColorName"] || [elementName isEqualToString: @"Text"] || [elementName isEqualToString: @"Value"]) {
		[subElementData setObject: [NSString stringWithString: xmlSerializer.currentProperty] forKey: elementName];
	}
	else if ([elementName isEqualToString: @"XCoordinate"]) {
		// Rewrite the coordinate name, value and axis title in XCoordinate specific slots
		[subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"AxisTitle"]] forKey: @"XAxisTitle"];
		[subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"Coordinate"]] forKey: @"XCoordinate"];
		[subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"Name"]] forKey: @"XName"];
	}
	else if ([elementName isEqualToString: @"YCoordinate"]) {
		// Rewrite the coordinate name, value and axis title in YCoordinate specific slots
		[subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"AxisTitle"]] forKey: @"YAxisTitle"];
		[subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"Coordinate"]] forKey: @"YCoordinate"];
		[subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"Name"]] forKey: @"YName"];
	}

	// Chart notes
	else if ([elementName isEqualToString: @"AreNotesVisible"]) {
		self.areNotesVisible = NO;
		self.areNotesVisible = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}
	else if ([elementName isEqualToString: @"NoteText"]) {
		[subElementData setObject: [NSString stringWithString: xmlSerializer.currentProperty] forKey: elementName];
	}
	else if ([elementName isEqualToString: @"Note"]) {
		NSString *noteText = [subElementData objectForKey: @"NoteText"];
		NSString *value = [subElementData objectForKey: @"Text"];		
		double xCoord = [[subElementData objectForKey: @"XCoordinate"] doubleValue];
		NSString *xAxisTitle = [subElementData objectForKey: @"XAxisTitle"];
		double yCoord = [[subElementData objectForKey: @"YCoordinate"] doubleValue];
		NSString *yAxisTitle = [subElementData objectForKey: @"YAxisTitle"];
						
		[self addChartNote: noteText: value: xCoord: xAxisTitle: yCoord: yAxisTitle];
	}
	
	// Chart circle markers
	else if ([elementName isEqualToString: @"AreCircleMarkersVisible"]) {
		self.areCircleMarkersVisible = NO;
		self.areCircleMarkersVisible = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}
	else if ([elementName isEqualToString: @"CircleMarker"]) {
		NSString *text = [subElementData objectForKey: @"Text"];
		NSString *value = [subElementData objectForKey: @"Value"];
		double xCoord = [[subElementData objectForKey: @"XCoordinate"] doubleValue];
		NSString *xAxisTitle = [subElementData objectForKey: @"XAxisTitle"];
		double yCoord = [[subElementData objectForKey: @"YCoordinate"] doubleValue];
		NSString *yAxisTitle = [subElementData objectForKey: @"YAxisTitle"];
		
		[self addChartCircleMarker: text: value: xCoord: xAxisTitle: yCoord: yAxisTitle];
	}
	
	// Line - general
	else if ([elementName isEqualToString: @"LineWidth"]) {
		[subElementData setObject: [NSString stringWithString: xmlSerializer.currentProperty] forKey: elementName];
	}
	
	// Chart vertical lines
	else if ([elementName isEqualToString: @"AreVerticalLinesVisible"]) {
		self.areVerticalLinesVisible = NO;
		self.areVerticalLinesVisible = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}
	else if ([elementName isEqualToString: @"VerticalLine"]) {
		int lineWidth = [[subElementData objectForKey: @"LineWidth"] intValue];
		NSString *text = [subElementData objectForKey: @"Text"];
		NSString *lineColorName = [subElementData objectForKey: @"MarkColorName"];
		
		double coord = [[subElementData objectForKey: @"Coordinate"] doubleValue];
		NSString *axisTitle = [subElementData objectForKey: @"AxisTitle"];
		
		[self addChartVerticalLine: lineWidth: text: coord: axisTitle: lineColorName];
	}
	
	// Chart horizontal lines
	else if ([elementName isEqualToString: @"AreHorizontalLinesVisible"]) {
		self.areHorizontalLinesVisible = NO;
		self.areHorizontalLinesVisible = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}
	else if ([elementName isEqualToString: @"HorizontalLine"]) {
		int lineWidth = [[subElementData objectForKey: @"LineWidth"] intValue];
		NSString *text = [subElementData objectForKey: @"Text"];
		NSString *lineColorName = [subElementData objectForKey: @"MarkColorName"];
		
		double coord = [[subElementData objectForKey: @"CoordinateValue"] doubleValue];
		NSString *axisTitle = [subElementData objectForKey: @"AxisTitle"];
		
		[self addChartHorizontalLine: lineWidth: text: coord: axisTitle:lineColorName];
	}
	
	// Zone - general
	else if ([elementName isEqualToString: @"OppositeAxisTitle"] || [elementName isEqualToString: @"ZoneColorName"]) {
		[subElementData setObject: [NSString stringWithString: xmlSerializer.currentProperty] forKey: elementName];
	}
	 else if ([elementName isEqualToString: @"StartCoordinate"]) {
		 // Rewrite the coordinate name, value and axis title in XCoordinate specific slots
		 [subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"AxisTitle"]] forKey: @"StartAxisTitle"];
		 [subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"Coordinate"]] forKey: @"StartCoordinate"];
		 [subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"Name"]] forKey: @"StartName"];
	 }
	 else if ([elementName isEqualToString: @"EndCoordinate"]) {
		 // Rewrite the coordinate name, value and axis title in YCoordinate specific slots
		 [subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"AxisTitle"]] forKey: @"EndAxisTitle"];
		 [subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"Coordinate"]] forKey: @"EndCoordinate"];
		 [subElementData setObject: [NSString stringWithString: [subElementData objectForKey: @"Name"]] forKey: @"EndName"];
	 }
	
	// Chart vertical zones
	else if ([elementName isEqualToString: @"AreVerticalZonesVisible"]) {
		self.areVerticalZonesVisible = NO;
		self.areVerticalZonesVisible = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}
	else if ([elementName isEqualToString: @"VerticalZone"]) {
		NSString *oppositeAxisTitle = [subElementData objectForKey: @"OppositeAxisTitle"];
		NSString *zoneColorName = [subElementData objectForKey: @"ZoneColorName"];
		double startCoord = [[subElementData objectForKey: @"StartCoordinate"] doubleValue];
		NSString *startAxisTitle = [subElementData objectForKey: @"StartAxisTitle"];
		double endCoord = [[subElementData objectForKey: @"EndCoordinate"] doubleValue];
		
		[self addChartVerticalZone: startCoord: endCoord: startAxisTitle: oppositeAxisTitle: zoneColorName];
	}

	// Chart horizontal zones
	else if ([elementName isEqualToString: @"AreHorizontalZonesVisible"]) {
		self.areHorizontalZonesVisible = NO;
		self.areHorizontalZonesVisible = [xmlSerializer.currentProperty isEqualToString: @"true"] ? YES : NO;
	}
	else if ([elementName isEqualToString: @"HorizontalZone"]) {
		NSString *oppositeAxisTitle = [subElementData objectForKey: @"OppositeAxisTitle"];
		NSString *zoneColorName = [subElementData objectForKey: @"ZoneColorName"];
		double startCoord = [[subElementData objectForKey: @"StartCoordinate"] doubleValue];
		NSString *startAxisTitle = [subElementData objectForKey: @"StartAxisTitle"];
		double endCoord = [[subElementData objectForKey: @"EndCoordinate"] doubleValue];
		
		[self addChartHorizontalZone: startCoord: endCoord: startAxisTitle: oppositeAxisTitle: zoneColorName];
	}
}

- (void) serialize: (NSString *) filename {
	[xmlSerializer serialize: filename];
	
	/* ChartMetadata XML looks like
	 <?xml version="1.0"?>
	 <ChartMetadata xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	 <MetadataVersion>2</MetadataVersion>
	 <IsChartZoomedIn>false</IsChartZoomedIn>
	 <IsChartZoomInSelected>false</IsChartZoomInSelected>
	 <IsChartZoomOutSelected>false</IsChartZoomOutSelected>
	 <ChartHorizontalMinimum>0.04</ChartHorizontalMinimum>
	 <ChartHorizontalMaximum>11.648</ChartHorizontalMaximum>
	 <AreVerticalLinesVisible>true</AreVerticalLinesVisible>
	 <AreVerticalZonesVisible>true</AreVerticalZonesVisible>
	 <AreHorizontalLinesVisible>true</AreHorizontalLinesVisible>
	 <AreHorizontalZonesVisible>true</AreHorizontalZonesVisible>
	 <AreNotesVisible>true</AreNotesVisible>
	 <AreCircleMarkersVisible>true</AreCircleMarkersVisible>
	 <ChartVerticalLines/>
	 <ChartVerticalZones/>
	 <ChartHorizontalLines/>
	 <ChartHorizontalZones/>
	 <ChartNotes/>
	 <ChartCircleMarkers/>
	 </ChartMetadata> */
	
	NSMutableString *xmlData = [NSMutableString stringWithCapacity: 100];
	[xmlData appendString: @"<?xml version=\"1.0\"?>\r\n"];
	[xmlData appendString: @"<ChartMetadata xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n"];
	
	[xmlData appendFormat: @"<MetadataVersion>%i</MetadataVersion>\r\n", self.metadataVersion]; 
	[xmlData appendFormat: @"<IsChartZoomedIn>%@</IsChartZoomedIn>\r\n", self.isChartZoomedIn ? @"true" : @"false"]; 
	[xmlData appendFormat: @"<IsChartZoomInSelected>%@</IsChartZoomInSelected>\r\n", self.isChartZoomInSelected ? @"true" : @"false"]; 
	[xmlData appendFormat: @"<IsChartZoomOutSelected>%@</IsChartZoomOutSelected>\r\n", self.isChartZoomOutSelected ? @"true" : @"false"]; 
	[xmlData appendFormat: @"<ChartHorizontalMinimum>%@</ChartHorizontalMinimum>\r\n", 
	 [serializedDataNumberFormatter stringFromNumber: [NSNumber numberWithDouble: self.xAxisMin]]];
	[xmlData appendFormat: @"<ChartHorizontalMaximum>%@</ChartHorizontalMaximum>\r\n", 
	 [serializedDataNumberFormatter stringFromNumber: [NSNumber numberWithDouble:  self.xAxisMax]]];
	
	// Vertical lines
	[xmlData appendFormat: @"<AreVerticalLinesVisible>%@</AreVerticalLinesVisible>\r\n", self.areVerticalLinesVisible ? @"true" : @"false"]; 
	[xmlData appendString: @"<ChartVerticalLines>\r\n"];
	for (id verticalLine in self.verticalLines)
		[xmlData appendString: [verticalLine serialize]];
	[xmlData appendString: @"</ChartVerticalLines>\r\n"];
	
	// Vertical zones
	[xmlData appendFormat: @"<AreVerticalZonesVisible>%@</AreVerticalZonesVisible>\r\n", self.areVerticalZonesVisible ? @"true" : @"false"];
	[xmlData appendString: @"<ChartVerticalZones>\r\n"];
	for (id verticalZone in self.verticalZones) 
		[xmlData appendString: [verticalZone serialize]];
	[xmlData appendString: @"</ChartVerticalZones>\r\n"];
	
	// Horizontal lines
	[xmlData appendFormat: @"<AreHorizontalLinesVisible>%@</AreHorizontalLinesVisible>\r\n", self.areHorizontalLinesVisible ? @"true" : @"false"]; 
	[xmlData appendString: @"<ChartHorizontalLines>\r\n"];
	for (id horizontalLine in self.horizontalLines) 
		[xmlData appendString: [horizontalLine serialize]];
	[xmlData appendString: @"</ChartHorizontalLines>\r\n"];
	
	// Horizontal zones
	[xmlData appendFormat: @"<AreHorizontalZonesVisible>%@</AreHorizontalZonesVisible>\r\n", self.areHorizontalZonesVisible ? @"true" : @"false"]; 
	[xmlData appendString: @"<ChartHorizontalZones>\r\n"];
	for (id horizontalZone in self.horizontalZones)
		[xmlData appendString: [horizontalZone serialize]];
	[xmlData appendString: @"</ChartHorizontalZones>\r\n"];
	
	// Notes
	[xmlData appendFormat: @"<AreNotesVisible>%@</AreNotesVisible>\r\n", self.areNotesVisible ? @"true" : @"false"]; 
	[xmlData appendString: @"<ChartNotes>\r\n"];
	for (id note in self.notes)
		[xmlData appendString: [note serialize]];
	[xmlData appendString: @"</ChartNotes>\r\n"];
	
	// Circler Markers
	[xmlData appendFormat: @"<AreCircleMarkersVisible>%@</AreCircleMarkersVisible>\r\n", self.areCircleMarkersVisible ? @"true" : @"false"]; 
	[xmlData appendString: @"<ChartCircleMarkers>\r\n"];
	for (id circleMarker in self.circleMarkers)
		[xmlData appendString: [circleMarker serialize]];
	[xmlData appendString: @"</ChartCircleMarkers>\r\n"];
	
	// Add padding to make sure all the data gets encrypted and serialized correctly.
	[xmlData appendString: @"</ChartMetadata>          "];
	
	NSError *err;
	if (![xmlData writeToFile: filename atomically: YES encoding: NSUTF8StringEncoding error: &err]) {
		NSString *fmt = @"Error writing chart metadata to file: %@. %@";
		NSLog(fmt, filename, err);
		[NSException raise: @"Chart Metadata Serialize Error" format: fmt, filename, err];
	}
}

- (void) dealloc {
	[verticalLines release];
	[horizontalLines release];
	[verticalZones release];
	[horizontalZones release];
	[notes release];
	[circleMarkers release];
	
	[xmlSerializer release];
	
	[super dealloc];
}

@end


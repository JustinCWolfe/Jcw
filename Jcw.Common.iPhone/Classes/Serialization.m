//
//  SerializeProtocol.h
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 7/16/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import "Serialization.h"


@implementation SerializeBase

@synthesize name, parserDelegate;

- (void) deserialize: (NSData *) data {
#ifdef DEBUG_VERBOSE
	NSString *stringData = [JcwUtilities GetStringFrom: data: NSUTF8StringEncoding];
	NSLog(@"Attempting to parse %@ data: %@", name, stringData);
#endif
}

- (void) serialize: (NSString *) filename {
	NSLog(@"Attempting to serialize %@ data to file: %@", name, filename);
}

- (void) dealloc {
    [name release];
    
	[super dealloc];
}

@end


@implementation CommaDelimitedSerializer

@synthesize deserializeSelector, serializeSelector;

- (id) initWith: (NSString *) pName: (id) pParserDelegate: (SEL) pDeserializeSelector: (SEL) pSerializeSelector { 
    self = [super init];
	if (self) {
		self.name = pName;
		self.parserDelegate = pParserDelegate;
		self.deserializeSelector = pDeserializeSelector;
		self.serializeSelector = pSerializeSelector;
	}
	return self;
}

- (void) deserialize: (NSData *) data {
	[super deserialize: data];
	[parserDelegate performSelector: self.deserializeSelector withObject: data];
}

- (void) serialize: (NSString *) filename {
	[super serialize: filename];
	[parserDelegate performSelector: self.serializeSelector withObject: filename];
}

@end


@implementation XMLSerializer 

@synthesize currentProperty, xmlElementNames;

- (id) initWith: (NSString *) pName: (NSArray *) pXmlElementNames: (id<NSXMLParserDelegate>) pParserDelegate {
    self = [super init];
	if (self) {
		self.name = pName;
		self.parserDelegate = pParserDelegate;
		xmlElementNames = [pXmlElementNames retain];
	}
	return self;
}

- (void) foundStartingElement {
	NSMutableString *temp = [[NSMutableString alloc] init];
	[currentProperty release];
	currentProperty = temp;
}

- (void) foundCharacters: (NSString *) string {
	if (currentProperty) {
		[currentProperty appendString: string];
	}
}

- (void) deserialize: (NSData *) data {
	[super deserialize: data];
	
	NSXMLParser *parser = [[NSXMLParser alloc] initWithData: data];
	[parser setDelegate: parserDelegate];
	
	// For some reason, when parsing xml that was serialized from the .NET object serialization
	// process, we always get the premature document end error (number 5) but it does not 
	// seem to affect the content parsing.
	BOOL parsePassed = [parser parse];
	if (!parsePassed && parser.parserError.code != NSXMLParserPrematureDocumentEndError) {
		NSLog(@"Error parsing %@ data: %@", name, parser.parserError);
	}
	
	[parser release];	
}

- (void) dealloc {
	[currentProperty release];
	[xmlElementNames release];
	
	[super dealloc];
}

@end
//
//  SerializeProtocol.h
//  Jcw.Common
//
//  Created by Justin C. Wolfe on 7/16/10.
//  Copyright 2010 NGenRt. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "JcwUtilities.h"


@protocol SerializeProtocol

- (void) serialize: (NSString *) filename;
- (void) deserialize: (NSData *) data;

@end


@interface SerializeBase : NSObject <SerializeProtocol> {
@protected
	NSString *name;
	id parserDelegate;
}

@property (nonatomic, copy) NSString *name;
@property (nonatomic, assign) id parserDelegate;

@end


@interface CommaDelimitedSerializer : SerializeBase {
@private
	SEL deserializeSelector;
	SEL serializeSelector;
}

@property (nonatomic, assign) SEL deserializeSelector;
@property (nonatomic, assign) SEL serializeSelector;

- (id) initWith: (NSString *) name: (id) pParserDelegate: (SEL) deserializeSelector: (SEL) serializeSelector;

@end


@interface XMLSerializer : SerializeBase {
@private
	NSMutableString *currentProperty;
	NSArray *xmlElementNames;
}

@property (nonatomic, readonly) NSString *currentProperty;
@property (nonatomic, readonly) NSArray *xmlElementNames;

- (void) foundStartingElement;
- (void) foundCharacters: (NSString *) string;

- (id) initWith: (NSString *) name: (NSArray *) xmlElementNames: (id<NSXMLParserDelegate>) parserDelegate;

@end



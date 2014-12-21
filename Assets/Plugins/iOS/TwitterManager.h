//
//  TwitterManager.h
//  LoginTwitterSample
//
//  Created by Hitesh Arora on 4/3/14.
//  Copyright (c) 2014 Hitesh Arora. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <Accounts/Accounts.h>
#import "TWAPIManager.h"
#import <Social/Social.h>
#import <Twitter/Twitter.h>
#import "TWSignedRequest.h"
#import "CustomActivityIndicator.h"

#ifdef DEBUG
#   define TWDLog(fmt, ...) NSLog((@"\n%s\n" fmt), __PRETTY_FUNCTION__, ##__VA_ARGS__)
#else
#   define TWDLog(...)
#endif
#define TWALog(fmt, ...) NSLog((@"\n%s\n" fmt), __PRETTY_FUNCTION__, ##__VA_ARGS__)

@interface TwitterManager : NSObject{
    NSString *_consumerKey;
	NSString *_consumerSecret;
@private
	NSMutableData *_payload;
//	TwitterRequest _requestType;
}
@property (nonatomic, copy) NSString *consumerKey;
@property (nonatomic, copy) NSString *consumerSecret;
@property (nonatomic, retain) NSMutableData *payload;




+ (TwitterManager*)sharedManager;
+ (UIViewController*)unityViewController;

- (void)isLoggedIn;

- (void)loggedInUsername;


-(void)loginWithTwitter;
//- (void)xAuthLoginWithUsername:(NSString*)username password:(NSString*)password;

- (void)logout;

- (void)postStatusUpdate:(NSString*)status;

//- (void)postStatusUpdate:(NSString*)status withToken:(OAToken*)token;

- (void)getHomeTimeline;

// Activity Indicator View
-(void) hideActivityView;
-(void) showActivityView;
-(void) showActivityViewWithLabel:(NSString*)label;
-(void) showBezelActivityViewWithLabel:(NSString*)label;
-(void) showBezelActivityViewWithLabel:(NSString*)label andImage:(NSString*)imagePath;

//Follows operations
-(void)followUserOnTwitter:(NSString *)user_Name;
-(void)checkWhetherUserFollowsOrNot:(NSString *)user_Name;


#pragma mark - Local Notification
+(void)addAlarm;
+(void)cancelAlarm;
@end

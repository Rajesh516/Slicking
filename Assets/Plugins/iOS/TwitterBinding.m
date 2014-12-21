//
//  TwitterManager.m
//  LoginTwitterSample
//
//  Created by Hitesh Arora on 4/3/14.
//  Copyright (c) 2014 Hitesh Arora. All rights reserved.
//

#import "TwitterManager.h"

// Converts NSString to C style string by way of copy (Mono will free it)
#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]


void _twitterInit( const char * consumerKey, const char * consumerSecret )
{
	[TwitterManager sharedManager].consumerKey = GetStringParam( consumerKey );
	[TwitterManager sharedManager].consumerSecret = GetStringParam( consumerSecret );
    [[NSUserDefaults standardUserDefaults]setObject:GetStringParam( consumerKey ) forKey:TW_CONSUMER_KEY];
    [[NSUserDefaults standardUserDefaults]setObject:GetStringParam( consumerSecret ) forKey:TW_CONSUMER_SECRET];
}


void _twitterIsLoggedIn()
{
	 [[TwitterManager sharedManager] isLoggedIn];
}


void _twitterLoggedInUsername()
{
	[[TwitterManager sharedManager] loggedInUsername];
}


void _twitterLogin( const char * username, const char * password )
{
    [[TwitterManager sharedManager]loginWithTwitter];
//	[[TwitterManager sharedManager] xAuthLoginWithUsername:GetStringParam( username ) password:GetStringParam( password )];
}


void _twitterLogout()
{
	[[TwitterManager sharedManager] logout];
}


void _twitterPostStatusUpdate( const char * status )
{
	[[TwitterManager sharedManager] postStatusUpdate:GetStringParam( status )];
}


void _twitterFollowIt( const char * screen_name )
{
    [[TwitterManager sharedManager]followUserOnTwitter:GetStringParam( screen_name )];
}

void _twitterIsFollowed( const char * screen_name )
{
    
    [[TwitterManager sharedManager]checkWhetherUserFollowsOrNot:GetStringParam( screen_name )];
}

void _twitterGetHomeTimeline()
{
//	[[TwitterManager sharedManager] getHomeTimeline];
}
void _openActivityIndicator (const char *msg)
{
    TwitterManager *manager = [TwitterManager sharedManager];
    [manager showBezelActivityViewWithLabel:GetStringParam(msg)];
}

void _closeActivityIndicator ()
{
    TwitterManager *manager = [TwitterManager sharedManager];
    [manager hideActivityView];
}

void _addAlarm ()
{
//    TwitterManager *manager = [TwitterManager sharedManager];
//    [manager showBezelActivityViewWithLabel:GetStringParam(msg)];
    
    [TwitterManager addAlarm];
}

void _cancelAlarm ()
{
//    TwitterManager *manager = [TwitterManager sharedManager];
//    [manager hideActivityView];
    [TwitterManager cancelAlarm];
}


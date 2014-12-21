//
//  TwitterManager.m
//  LoginTwitterSample
//
//  Created by Hitesh Arora on 4/3/14.
//  Copyright (c) 2014 Hitesh Arora. All rights reserved.
//

#import "TwitterManager.h"

#define ERROR_TITLE_MSG @"Alert !"
#define ERROR_PERM_ACCESS @"We weren't granted access to the user's accounts"
#define ERROR_NO_ACCOUNTS @"You must add a Twitter account in Settings.app to use this demo."
#define ERROR_OK @"OK"



@interface TwitterManager ()

@property (nonatomic, strong) ACAccountStore *accountStore;
@property (nonatomic, strong) TWAPIManager *apiManager;
@property (nonatomic, strong) NSArray *accounts;
@end

UIViewController *UnityGetGLViewController();

void UnitySendMessage( const char * className, const char * methodName, const char * param );
NSString *const kLoggedInUser = @"kLoggedInUser";
NSString *const kLoggedInUserToken = @"accessToken";
NSString *const kLoggedInUserOauthSecret = @"oauthSecret";

@implementation TwitterManager
@synthesize consumerKey = _consumerKey, consumerSecret = _consumerSecret, payload = _payload;


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject



+ (TwitterManager*)sharedManager
{
	static TwitterManager *sharedSingleton;
	
	if( !sharedSingleton )
		sharedSingleton = [[TwitterManager alloc] init];
	
	return sharedSingleton;
}
+ (UIViewController*)unityViewController
{
	return UnityGetGLViewController();
}

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Private

- (NSString*)extractUsernameFromHTTPBody:(NSString*)body
{
	if( !body )
		return nil;
	
	NSArray	*tuples = [body componentsSeparatedByString: @"&"];
	if( tuples.count < 1 )
		return nil;
	
	for( NSString *tuple in tuples )
	{
		NSArray *keyValueArray = [tuple componentsSeparatedByString: @"="];
		
		if( keyValueArray.count == 2 )
		{
			NSString *key = [keyValueArray objectAtIndex: 0];
			NSString *value = [keyValueArray objectAtIndex: 1];
			
			if( [key isEqualToString:@"screen_name"] )
				return value;
		}
	}
	
	return nil;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Public

- (void)isLoggedIn
{
		NSString *tokenString = [[NSUserDefaults standardUserDefaults] objectForKey:kLoggedInUserToken];
	if( tokenString ){
        UnitySendMessage( "TwitterManager", "twitterIsUserLoginCallBack", "1" );
    }
    else{
        UnitySendMessage( "TwitterManager", "twitterIsUserLoginCallBack", "0" );
    }
    
}


- (void)loggedInUsername;
{
		NSString *tokenString = [[NSUserDefaults standardUserDefaults] objectForKey:kLoggedInUserToken];
	if( !tokenString )
		 UnitySendMessage( "TwitterManager", "twitterIsUserLoginCallBack", "" );
   // else
        // UnitySendMessage( "TwitterManager", "twitterIsUserLoginCallBack", [self extractUsernameFromHTTPBody:tokenString] );
}

- (void)logout
{
	[[NSUserDefaults standardUserDefaults] setObject:nil forKey:kLoggedInUser];
    [[NSUserDefaults standardUserDefaults] setObject:nil forKey:kLoggedInUserOauthSecret];
    [[NSUserDefaults standardUserDefaults] setObject:nil forKey:kLoggedInUserToken];
	[[NSUserDefaults standardUserDefaults] synchronize];
    UnitySendMessage( "TwitterManager", "twitterLogoutSucceded", "" );
    
}

/*
- (void)xAuthLoginWithUsername:(NSString*)username password:(NSString*)password
{
	_requestType = TwitterRequestLogin;
	P31MutableOauthRequest *request = [[P31MutableOauthRequest alloc] initWithUrl:@"https://api.twitter.com/oauth/access_token"
																			  key:_consumerKey
																		   secret:_consumerSecret
																			token:nil];
	
	[request setHTTPMethod:@"POST"];
    
    
	[request setParameters:[NSArray arrayWithObjects:
							[OARequestParameter requestParameter:@"x_auth_mode" value:@"client_auth"],
							[OARequestParameter requestParameter:@"x_auth_username" value:username],
							[OARequestParameter requestParameter:@"x_auth_password" value:password],
							nil]];
    
	[request prepareRequest];
	
	NSURLConnection *connection = [[NSURLConnection alloc] initWithRequest:request delegate:self];
	[request release];
	
    if( connection )
        _payload = [[NSMutableData alloc] init];
}
*/



/*
- (void)postStatusUpdate:(NSString*)status
{
	NSString *tokenString = [[NSUserDefaults standardUserDefaults] objectForKey:kLoggedInUser];
	if( !tokenString )
	{
		UnitySendMessage( "TwitterManager", "twitterPostDidFail", "User is not logged in" );
		return;
	}
	
	OAToken *accessToken = [[OAToken alloc] initWithHTTPResponseBody:tokenString];
	[self postStatusUpdate:status withToken:accessToken];
}


- (void)postStatusUpdate:(NSString*)status withToken:(OAToken*)token
{
	_requestType = TwitterRequestUpdateStatus;
	P31MutableOauthRequest *request = [[P31MutableOauthRequest alloc] initWithUrl:@"http://api.twitter.com/1/statuses/update.json"
																			  key:_consumerKey
																		   secret:_consumerSecret
																			token:token];
	
	NSString *body = [NSString stringWithFormat:@"status=%@", [status encodedURLString]];
	[request setHTTPMethod:@"POST"];
	[request setHTTPBody:[body dataUsingEncoding:NSUTF8StringEncoding]];
	
	[request prepareRequest];
	
	NSURLConnection *connection = [[NSURLConnection alloc] initWithRequest:request delegate:self];
	[request release];
	
    if( connection )
        _payload = [[NSMutableData alloc] init];
}



- (void)getHomeTimeline
{
	NSString *tokenString = [[NSUserDefaults standardUserDefaults] objectForKey:kLoggedInUser];
	if( !tokenString )
	{
		UnitySendMessage( "TwitterManager", "postFailed", "User is not logged in" );
		return;
	}
	
	OAToken *token = [[OAToken alloc] initWithHTTPResponseBody:tokenString];
	
	_requestType = TwitterRequestHomeTimeline;
	P31MutableOauthRequest *request = [[P31MutableOauthRequest alloc] initWithUrl:@"http://api.twitter.com/1/statuses/home_timeline.json"
																			  key:_consumerKey
																		   secret:_consumerSecret
																			token:token];
	
	[request setHTTPMethod:@"GET"];
	[request prepareRequest];
	
	NSURLConnection *connection = [[NSURLConnection alloc] initWithRequest:request delegate:self];
	[request release];
	
    if( connection )
        _payload = [[NSMutableData alloc] init];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSURLConnection Delegates

- (void)connection:(NSURLConnection*)conn didReceiveResponse:(NSURLResponse*)response
{
	[_payload setLength:0];
}


- (void)connection:(NSURLConnection*)conn didReceiveData:(NSData*)data
{
	[_payload appendData:data];
}


- (void)connectionDidFinishLoading:(NSURLConnection*)conn
{
	NSString *data = [[[NSString alloc] initWithData:_payload encoding:NSUTF8StringEncoding] autorelease];
    
	switch( _requestType )
	{
		case TwitterRequestLogin:
		{
			NSString *username = [self extractUsernameFromHTTPBody:data];
			if( !username )
			{
				UnitySendMessage( "TwitterManager", "twitterLoginDidFail", [data UTF8String] );
			}
			else
			{
				// save the token for posting
				[[NSUserDefaults standardUserDefaults] setObject:data forKey:kLoggedInUser];
				[[NSUserDefaults standardUserDefaults] synchronize];
				
				// send success message back to Unity
				UnitySendMessage( "TwitterManager", "twitterLoginSucceeded", "" );
			}
            
			break;
		}
		case TwitterRequestUpdateStatus:
		{
			// was this successful or not?
			if( [data rangeOfString:@"\"error\""].location != NSNotFound )
			{
				// try to extract a useful error message
				SBJSON *jsonParser = [[SBJSON new] autorelease];
				NSDictionary *dict = [jsonParser objectWithString:data];
				if( [dict isKindOfClass:[NSDictionary class]] && [[dict allKeys] containsObject:@"error"] )
				{
					NSString *error = [dict objectForKey:@"error"];
					UnitySendMessage( "TwitterManager", "twitterPostDidFail", [error UTF8String] );
				}
				else
				{
					UnitySendMessage( "TwitterManager", "twitterPostDidFail", [data UTF8String] );
				}
			}
			else
			{
				UnitySendMessage( "TwitterManager", "twitterPostSucceeded", "" );
			}
            
			break;
		}
		case TwitterRequestHomeTimeline:
		{
			// Return statuses to Unity
			UnitySendMessage( "TwitterManager", "twitterHomeTimelineDidFinish", [data UTF8String] );
			break;
		}
	}
	
	// clean up
	self.payload = nil;
	[conn release];
}


- (void)connection:(NSURLConnection*)conn didFailWithError:(NSError*)error
{
	if( _requestType == TwitterRequestLogin )
		UnitySendMessage( "TwitterManager", "twitterLoginDidFail", [[error localizedDescription] UTF8String] );
	else if( _requestType == TwitterRequestUpdateStatus )
		UnitySendMessage( "TwitterManager", "twitterPostDidFail", [[error localizedDescription] UTF8String] );
	else
		UnitySendMessage( "TwitterManager", "twitterHomeTimelineDidFail", [[error localizedDescription] UTF8String] );
    
	
	// clean up
	self.payload = nil;
}
*/
-(void)loginWithTwitter{
    _accountStore = [[ACAccountStore alloc] init];
    _apiManager = [[TWAPIManager alloc] init];
    

    
    [self _refreshTwitterAccounts];

}
//Twitter Login functions
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
- (void)_refreshTwitterAccounts
{
    TWDLog(@"Refreshing Twitter Accounts \n");
    
    [self _obtainAccessToAccountsWithBlock:^(BOOL granted) {
        dispatch_async(dispatch_get_main_queue(), ^{
            if (granted) {
                //_reverseAuthBtn.enabled = YES;
                if([_accounts count] == 0)
                {
                    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:ERROR_TITLE_MSG message:ERROR_NO_ACCOUNTS delegate:nil cancelButtonTitle:ERROR_OK otherButtonTitles:nil];
                    [alert show];
                }
                else
                {
                       [self showActivityViewWithLabel:@"Loading..."];
                    [self twitterAccessToken];
                }
            }
            else {
                UIAlertView *alert = [[UIAlertView alloc] initWithTitle:ERROR_TITLE_MSG message:ERROR_PERM_ACCESS delegate:nil cancelButtonTitle:ERROR_OK otherButtonTitles:nil];
                [alert show];
                TWALog(@"You were not granted access to the Twitter accounts.");
                UnitySendMessage( "TwitterManager", "twitterPostDidFail","You were not granted access to the Twitter accounts.");

            }
        });
    }];
}
- (void)_obtainAccessToAccountsWithBlock:(void (^)(BOOL))block
{
    ACAccountType *twitterType = [_accountStore accountTypeWithAccountTypeIdentifier:ACAccountTypeIdentifierTwitter];
    ACAccountStoreRequestAccessCompletionHandler handler = ^(BOOL granted, NSError *error) {
        if (granted) {
            NSLog(@"Twitter Type %@",twitterType);
            
            self.accounts = [_accountStore accountsWithAccountType:twitterType];
            
            NSLog(@"Acount %@",self.accounts);
        }
        
        block(granted);
    };
    [_accountStore requestAccessToAccountsWithType:twitterType options:NULL completion:handler];
}

-(void)twitterAccessToken
{
        
        [_apiManager performReverseAuthForAccount:[_accounts objectAtIndex:0] withHandler:^(NSData *responseData, NSError *error) {
            if (responseData) {
                [CustomActivityIndicator removeView];
                NSString *responseStr = [[NSString alloc] initWithData:responseData encoding:NSUTF8StringEncoding];
                
                NSLog(@"Reverse Auth process returned: %@", responseStr);
                
                NSArray *parts = [responseStr componentsSeparatedByString:@"&"];
                //            NSString *lined = [parts componentsJoinedByString:@"\n"];
                
                
                
                NSString *accessToken = [[parts objectAtIndex:0] substringFromIndex:12];
                NSString *oauthSecret =[[parts objectAtIndex:1] substringFromIndex:19];

                NSLog(@"Access Token = %@ \n Secret = %@",accessToken,oauthSecret);
                // save the token for posting
                [[NSUserDefaults standardUserDefaults] setObject:accessToken forKey:kLoggedInUserToken];
                [[NSUserDefaults standardUserDefaults] setObject:oauthSecret forKey:kLoggedInUserOauthSecret];
				[[NSUserDefaults standardUserDefaults] synchronize];
				
				// send success message back to Unity
				UnitySendMessage( "TwitterManager", "twitterLoginSucceeded", "" );
            }
        }];

}
- (void)postStatusUpdate:(NSString*)status
{
		NSString *tokenString = [[NSUserDefaults standardUserDefaults] objectForKey:kLoggedInUserToken];
	if( !tokenString )
	{
		UnitySendMessage( "TwitterManager", "twitterPostDidFail", "User is not logged in" );
		return;
	}
	
	[self postStatusUpdate:status withController:[TwitterManager unityViewController]];
}


- (void)postStatusUpdate:(NSString*)status withController:(UIViewController*)viewController {
    SLComposeViewController *controller = (SLComposeViewController *)[TwitterManager twitterShareWithMessage:status];
    [viewController presentViewController:controller animated:YES completion:nil];

}

+(UIViewController *)twitterShareWithMessage:(NSString *)status
{
    SLComposeViewController *controller = [SLComposeViewController composeViewControllerForServiceType:SLServiceTypeTwitter];
    SLComposeViewControllerCompletionHandler myBlock = ^(SLComposeViewControllerResult result){
        if (result == SLComposeViewControllerResultCancelled)
        {
            NSLog(@"Cancelled");
            UnitySendMessage( "TwitterManager", "twitterPostDidFail", "Canceled" );
        }
        else
        {
            NSLog(@"Done");
            UnitySendMessage( "TwitterManager", "twitterPostSucceeded", "" );
        }
        [controller dismissViewControllerAnimated:YES completion:Nil];
    };
    controller.completionHandler =myBlock;
    
    //Adding the Text to the facebook post value from iOS
//    [controller setInitialText:[NSString stringWithFormat:@"%@ just posted an ad on the GirlForHire.com app. Click to download it free!",@"abc"]];
     [controller setInitialText:status];
    
    //Adding the URL to the facebook post value from iOS
    
    [controller addURL:[NSURL URLWithString:@"http://www.google.com"]];
    
    //Adding the Image to the facebook post value from iOS
    
    [controller addImage:[UIImage imageNamed:@"2-user-circle@2x~ipad.png"]];
    return controller;
}


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark CustomActivityIndicator
-(void) hideActivityView
{
    [CustomActivityIndicator removeView];
}

-(void) showActivityView
{
    [CustomActivityIndicator newActivityView];
}

-(void) showActivityViewWithLabel:(NSString*)label
{
    [CustomActivityIndicator newActivityViewWithLabel:NSLocalizedString(label, nil)];
}

-(void) showBezelActivityViewWithLabel:(NSString*)label
{
    [CustomBezelActivityIndicator newActivityViewWithLabel:NSLocalizedString(label, nil)];
}

-(void) showBezelActivityViewWithLabel:(NSString*)label andImage:(NSString*)imagePath
{
    UIImage *image = [UIImage imageWithContentsOfFile:imagePath];
	[CustomImageActivityIndicator newActivityViewWithLabel:NSLocalizedString(label, nil) withImage:image];
}
//Follow Twitter
-(void)followUserOnTwitter:(NSString *)user_Name{
    SLRequest *request = [SLRequest requestForServiceType:SLServiceTypeTwitter requestMethod:SLRequestMethodPOST URL:[NSURL URLWithString:@"https://api.twitter.com/1.1/friendships/create.json"] parameters:[NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:user_Name, @"true", nil] forKeys:[NSArray arrayWithObjects:@"screen_name", @"follow", nil]]];
    [request setAccount:[_accounts objectAtIndex:0]];
    [request performRequestWithHandler:^(NSData *responseData, NSHTTPURLResponse *urlResponse, NSError *error) {
        if(responseData) {
            NSDictionary *responseDictionary = [NSJSONSerialization JSONObjectWithData:responseData options:NSJSONReadingMutableContainers error:&error];
            if(responseDictionary) {
                
                if ([responseDictionary valueForKey:@"errors"]) {
                    NSArray *arr = [[responseDictionary valueForKey:@"errors"] valueForKey:@"message"];
                    NSLog(@"error: %@",[arr objectAtIndex:0]);
                    UnitySendMessage( "TwitterManager", "twitterFollowDidFail", [[arr objectAtIndex:0] UTF8String] );
                }
                else{
                    UnitySendMessage( "TwitterManager", "twitterFollowSucceeded", "" );
                    /*
                    if ([[responseDictionary valueForKey:@"following"] intValue]==1){
                        UnitySendMessage( "TwitterManager", "twitterPostDidFail", "User is not logged in" );
                      NSLog(@"is Followed");
                    }
                    else{
                        NSLog(@"not Followed");
                    }
                     */
                }
            }
            
        } else {
            UnitySendMessage( "TwitterManager", "twitterFollowDidFail", "User is not logged in" );
            // responseDictionary is nil
        }
    }];
}

-(void)checkWhetherUserFollowsOrNot:(NSString *)user_Name{
    SLRequest *request = [SLRequest requestForServiceType:SLServiceTypeTwitter requestMethod:SLRequestMethodPOST URL:[NSURL URLWithString:@"https://api.twitter.com/1.1/users/lookup.json"] parameters:[NSDictionary dictionaryWithObjects:[NSArray arrayWithObjects:user_Name,@"false", nil] forKeys:[NSArray arrayWithObjects:@"screen_name",@"twitter&include_entities", nil]]];
    
    [request setAccount:[_accounts objectAtIndex:0]];
    [request performRequestWithHandler:^(NSData *responseData, NSHTTPURLResponse *urlResponse, NSError *error) {
        
        if(responseData) {
            NSDictionary *responseDictionary = [NSJSONSerialization JSONObjectWithData:responseData options:NSJSONReadingMutableContainers error:&error];
            if(responseDictionary) {
                
                if ([responseDictionary isKindOfClass:[NSArray class]]) {
                    NSDictionary *dict = [(NSArray *)responseDictionary objectAtIndex:0];
                    if ([[dict valueForKey:@"following"] intValue]==1){
                        NSLog(@"is Followed");
                        UnitySendMessage( "TwitterManager", "twitterUserFollows", "1" );
                    }
                    else{
                        UnitySendMessage( "TwitterManager", "twitterUserFollows", "0" );
                        NSLog(@"not Followed");
                    }
                }
                else{
                    if ([responseDictionary valueForKey:@"errors"]) {
                        NSArray *arr = [[responseDictionary valueForKey:@"errors"] valueForKey:@"message"];
                        NSLog(@"error: %@",[arr objectAtIndex:0]);
                        UnitySendMessage( "TwitterManager", "twitterFollowDidFail", [[arr objectAtIndex:0] UTF8String] );
                    }
                    else{
                        
                        if ([[responseDictionary valueForKey:@"following"] intValue]==1){
                            UnitySendMessage( "TwitterManager", "twitterUserFollows", "1" );
                        }
                        else{
                            UnitySendMessage( "TwitterManager", "twitterUserFollows", "0" );
                        }
                    }
                }
            }
            
        } else {
            UnitySendMessage( "TwitterManager", "twitterFollowDidFail", "User is not logged in" );
            // responseDictionary is nil
        }
    }];
}




#pragma mark - LOCAL NOTIFICATION

+(void)addAlarm{
    [TwitterManager cancelAlarm];
    NSCalendar *calendar = [NSCalendar currentCalendar];
    NSDateComponents *componentsForReferenceDate = [calendar components:(NSYearCalendarUnit | NSMonthCalendarUnit | NSDayCalendarUnit)
                                                               fromDate:[NSDate date]];
    [componentsForReferenceDate setHour: 14] ;
    [componentsForReferenceDate setMinute:00] ;
    [componentsForReferenceDate setSecond:00] ;
    
    NSDate *tempDate = [calendar dateFromComponents: componentsForReferenceDate];
    
    NSDateComponents *comps = [[NSDateComponents alloc]init];
    [comps setDay:1];
    NSDate *fireDateOfNotification = [calendar dateByAddingComponents:comps
                                                               toDate:tempDate options:0];
    
    UILocalNotification *localNotif = [[UILocalNotification alloc] init];
    if (localNotif == nil)
        return;
    
    //Test your notification from here by providing the seconds, like if you uncomment the next line it'd call after 1 second!
   
    //localNotif.fireDate = [NSDate dateWithTimeIntervalSinceNow: 10];
   
    localNotif.fireDate = fireDateOfNotification;
   
    localNotif.timeZone = [NSTimeZone defaultTimeZone];
    
	// Notification details
    localNotif.alertBody = @"Where have you been? Don’t forget your daily bonus! Challenges are awaiting for you!";
	// Set the action button
    //localNotif.alertAction = @"CHEERS!";
    
    localNotif.soundName = UILocalNotificationDefaultSoundName;
    localNotif.applicationIconBadgeNumber = 1;
    
    //	// Specify custom data for the notification
    //    NSDictionary *infoDict = [NSDictionary dictionaryWithObject:@"someValue" forKey:@"someKey"];
    //    localNotif.userInfo = infoDict;
    
	// Schedule the notification
    [
     [UIApplication sharedApplication] scheduleLocalNotification:localNotif];
}
+(void)cancelAlarm{
    [[UIApplication sharedApplication] cancelAllLocalNotifications];
    [[UIApplication sharedApplication] setApplicationIconBadgeNumber:0];
}



@end

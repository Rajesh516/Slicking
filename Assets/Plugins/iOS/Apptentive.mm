// Apptentive.mm
// Copyright 2013 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#import <Foundation/Foundation.h>

#import "Apptentive.h"
#import "ATConnect.h"
#import "ATSurveys.h"

#define ApptentiveKey @"22b398412d6a1c5c17fac1c6387bc87d905d82fce41ae3bf07c11ca49c283294"

@interface Apptentive ()

// Root view controller for Unity applications can be accessed using this
// method.
extern UIViewController *UnityGetGLViewController();

@end

@implementation Apptentive

#pragma mark Unity bridge

+ (Apptentive *)pluginSharedInstance {
    static Apptentive *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[Apptentive alloc] init];
    });
    return sharedInstance;
}

#pragma mark Cleanup

- (void)dealloc {
    [super dealloc];
}
#pragma mark AppTentive

-(void)apptentiveFeedBackStart {
    [[ATConnect sharedConnection] presentMessageCenterFromViewController:UnityGetGLViewController()];
}
-(void)apptentiveRatingStartWithEvent:(NSString *)activeEvent
{
    NSLog(@"current event key: %@", activeEvent);
    [[ATConnect sharedConnection] engage:activeEvent fromViewController:UnityGetGLViewController()];
}
-(void)apptentiveShowActiveSurveysWithNoTags
{
    if ([ATSurveys hasSurveyAvailableWithNoTags])
    {
        [ATSurveys presentSurveyControllerWithNoTagsFromViewController:UnityGetGLViewController()];
    }
}

-(void)apptentiveInitWithKey:(NSString *)apikeyAppTentive
{
    [ATConnect sharedConnection].apiKey = apikeyAppTentive;
    UnitySendMessage("Apptentive",
                     "OnReceiveAd",
                     "Received ad successfully.");
}

@end

// Helper method used to convert NSStrings into C-style strings.
NSString *CreateNSStr(const char* string) {
    if (string) {
        return [NSString stringWithUTF8String:string];
    } else {
        return [NSString stringWithUTF8String:""];
    }
}


// Unity can only talk directly to C code so use these method calls as wrappers
// into the actual plugin logic.
extern "C" {
    
    // Apptentive Methods
    
    void _ApptentiveFeedBackShow()
    {
        Apptentive *objApptentive = [Apptentive pluginSharedInstance];
        [objApptentive apptentiveFeedBackStart];
    }
    
    void _ApptentiveRatingShow(const char *publisherId)
    {
        Apptentive *objApptentive = [Apptentive pluginSharedInstance];
        [objApptentive apptentiveRatingStartWithEvent:CreateNSStr(publisherId)];
    }
    
    void _ApptentiveSurveysShow()
    {
        Apptentive *objApptentive = [Apptentive pluginSharedInstance];
        [objApptentive apptentiveShowActiveSurveysWithNoTags];
    }
    
    void _ApptentiveInit(const char *publisherId)
    {
        Apptentive *objApptentive = [Apptentive pluginSharedInstance];
        [objApptentive apptentiveInitWithKey:CreateNSStr(publisherId)];
    }
}

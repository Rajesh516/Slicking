// AdMobPlugin.h
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
#import <UIKit/UIKit.h>

#import "GADBannerViewDelegate.h"
#import "GADInterstitialDelegate.h"

@class GADBannerView;
@class GADInterstitial;
@interface AdMobPlugin : NSObject<GADBannerViewDelegate,GADInterstitialDelegate> {
 @private
  // Value set by the Unity script to indicate whether the ad is to be
  // positioned at the top or bottom of the screen.
  BOOL positionAdAtTop_;
}

@property(nonatomic, retain) GADBannerView *bannerView;
@property(nonatomic, retain) GADInterstitial *interstitialView;
@property(nonatomic, retain) NSString *callbackHandlerName;

@end

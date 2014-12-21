using UnityEngine;
using System.Collections;


public class EventListener : MonoBehaviour
{
	// Listens to all the events.  All event listeners MUST be removed before this object is disposed!
	void OnEnable()
	{
		// Twitter
		TwitterManager.twitterLogin += twitterLogin;
		TwitterManager.twitterLoginFailed += twitterLoginFailed;
		TwitterManager.twitterPost += twitterPost;
		TwitterManager.twitterPostFailed += twitterPostFailed;
		TwitterManager.twitterHomeTimelineReceived += twitterHomeTimelineReceived;
		TwitterManager.twitterHomeTimelineFailed += twitterHomeTimelineFailed;
	}
	
	void OnDisable()
	{
		TwitterManager.twitterLogin -= twitterLogin;
		TwitterManager.twitterLoginFailed -= twitterLoginFailed;
		TwitterManager.twitterPost -= twitterPost;
		TwitterManager.twitterPostFailed -= twitterPostFailed;
		TwitterManager.twitterHomeTimelineReceived -= twitterHomeTimelineReceived;
		TwitterManager.twitterHomeTimelineFailed -= twitterHomeTimelineFailed;
	}

	
	// Twitter events
	void twitterLogin()
	{
		Debug.Log( "Successfully logged in to Twitter" );
	}

	void twitterLoginFailed( string error )
	{
		Debug.Log( "Twitter login failed: " + error );
	}

	void twitterPost()
	{
		Debug.Log( "Successfully posted to Twitter" );
	}

	void twitterPostFailed( string error )
	{
		Debug.Log( "Twitter post failed: " + error );
	}


	void twitterHomeTimelineFailed( string error )
	{
		Debug.Log( "Twitter HomeTimeline failed: " + error );
	}
	
	
	void twitterHomeTimelineReceived( ArrayList result )
	{
		Debug.Log( "received home timeline with tweet count: " + result.Count );
	}
	
}

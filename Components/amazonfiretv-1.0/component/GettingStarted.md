## Notifications API

The Android Fire TV SDK provides a way to create, show and cancel notifications on a TV.  The Fire TV notification API are based on [Android notifications][1].  To show notifications on Fire TV you **must** use the Fire TV notifications API.  For more info see the [Notifications API][2] doc an Amazon's site. 

The notification API supports two types of notifications:

 - `BuilderType.Info`, for general messages to the user, with optional actions.
 - `BuilderType.MediaInfo`, for information on media (artist, title) playing in the background. 

Create a notification with the `AmazonNotification.Builder` class similar to the methods used with Android Notifications.  The `AmazonNotification.Builder` class provides a new method `SetType` to indicate the type (`BuilderType.Info` or `BuilderType.MediaInfo`) of notification that is intended.  
 
 
```csharp
using Amazon.Device.Notification;
...

var builder = new AmazonNotification.Builder (ApplicationContext);
builder.SetSmallIcon (Resource.Drawable.ic_launcher);
builder.SetContentTitle (title);
builder.SetContentText (text);
builder.SetType (BuilderType.Info);
```

Register the notification with the `AmazonNotificationManager`.

```csharp
var notificationManager = GetSystemService (Context.NotificationService)
	.JavaCast <AmazonNotificationManager> ();

notificationManager.Notify (notificationId, builder.Build ());
``` 

## GameController API

The GameController class is included with the Amazon Fire TV SDK. GameController provides features especially useful for games, including:

 - Methods to associate game controllers with the player numbers as defined by Amazon Fire TV.
 - Methods to query controller state at any time.
 - Input event constants specific to game controllers.
 - Behavior to enable you to process game controller input events on a per-frame basis (that is, within a game loop).
 
For more info see the [GameController API][3] doc an Amazon's site. 

You must initialize the `GameController` in your activitity's `OnCreate` method before using any of the methods of the `GameController` class.

```csharp
using Amazon.Device.GameController;
...

protected override void OnCreate (Bundle bundle)
{
	base.OnCreate (bundle);

	// Initialize with context so GameController can invoke system services
	GameController.Init(this);
}
```

Use the `GameController.GetControllerByPlayer` static method with a player number to retrieve a `GameController` object for use with frame-based event input. Player numbers are assigned by the system and can be from 1 to 4. Not all player numbers may have associated controllers. If this is the case `GameController.GetControllerbyPlayer` throws the `GameController.PlayerNumberNotFoundException` exception.

```csharp
using Amazon.Device.GameController;
...

// Iterate through all players
for (int n = 0; n < GameController.MaxPlayers; n++) {

	GameController gameController = null;
	try {
		gameController =
			GameController.GetControllerByPlayer (n + 1);
	} catch (GameController.PlayerNumberNotFoundException e) {
	}
	...
}
```

Forward the key and motion events from an activity to the `GameController` class to enable the GameController API to manage you input events.  This is achieved by overriding the `OnKeyDown`, `OnKeyUp` and `OnGenericMotionEvent` methods.

```csharp
using Amazon.Device.GameController;
...

[Activity (Label = "@string/app_name", MainLauncher = true)]
public class GameActivity : Activity
{
	public override bool OnKeyDown (Keycode keyCode, KeyEvent @event)
	{
		bool handled = false;
		try {
			handled = GameController.OnKeyDown ((int)keyCode, @event);
		} catch (GameController.DeviceNotFoundException) {
		}
		return handled || base.OnKeyDown (keyCode, @event);
	}

	public override bool OnKeyUp (Keycode keyCode, KeyEvent @event)
	{
		bool handled = false;
		try {
			handled = GameController.OnKeyUp ((int)keyCode, @event);
		} catch (GameController.DeviceNotFoundException) {
		}
		return handled || base.OnKeyUp (keyCode, @event);
	}

	public override bool OnGenericMotionEvent (MotionEvent e)
	{
		bool handled = false;
		try {
			handled = GameController.OnGenericMotionEvent (e);
		} catch (GameController.DeviceNotFoundException) {
		}
		return handled || base.OnGenericMotionEvent (e);
	}
}
```

Implement the game loop on your own thread. Inside the `Run` method for that thread, test for input and render each frame based on that input.

Within the loop, use `GameController.StartFrame` to reset the input event queue between frames. 

```csharp
using Amazon.Device.GameController;
...

while (running) {
	GameController.StartFrame ();

	//Draw the background

	//Draw each player
	for (int n = 0; n < GameController.MaxPlayers; n++) { 
		GameController gameController = null;        
		try {
			gameController = GameController.GetControllerByPlayer (n + 1);
		} catch (GameController.PlayerNumberNotFoundException) {
		}

		if (gameController != null) {
			//Move player n on the board
			float x1 = gameController.GetAxisValue (GameControllerAxis.StickLeftX);
			float y1 = gameController.GetAxisValue (GameControllerAxis.StickLeftY);

			if (gameController.IsButtonPressed (GameControllerButton.TriggerLeft)) {
				//Draw laser beam for player n
			}
		}
	}

	//Draw the foreground
}
```

##Connecting ADB

In order to deploy apps to your Amazon Fire TV you must use Android Debug Bridge (ADB.)  Your computer and Fire TV must be on the same network.

###Turn on ADB Debugging
1. From the main (Launcher) screen of Amazon Fire TV, select **Settings.**
2. Select **System > Developer Options.**
3. Select **ADB Debugging.**  

###Get the IP Address
1. From the main screen of Amazon Fire TV, select **Settings.**
2. Select System > About > Network.  
   Make note of the IP address listed on this screen.
   
###Connect ADB
1. From a cmd window or terminal change directory to the platform-tools directory of the Android SDK location on disk.  The Android SDK location can be found in the Xamarin Studio Preferences box under **Projects > SDK Locations > Android.**  
   
2. Ensure that this ADB server is running.  
   `adb kill-server`  
   `adb start-server` 
    
3. Connect ADB to the Fire TV with the &lt;ipaddress&gt; noted in the previous section.  
   `adb connect <ipaddress>`

4. Verify that your computer is connected to the Fire TV.  
   `adb devices`  
   
   ADB will respond with the following message.  
   List of devices attached  
   &lt;ipaddress&gt;:5555  device


[1]: http://developer.android.com/guide/topics/ui/notifiers/notifications.html
[2]: https://developer.amazon.com/sdk/asb/notifications.html
[3]: https://developer.amazon.com/sdk/asb/input-mgmt-games.html


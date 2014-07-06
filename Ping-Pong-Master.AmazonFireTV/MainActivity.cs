using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;

using PingPongMaster.Views;

namespace PingPongMaster.AmazonFireTV
{
	[Activity (Label = "Ping Pong Master", ScreenOrientation = ScreenOrientation.Landscape, MainLauncher = true)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//SetContentView (Resource.Layout.Main);

			Xamarin.Forms.Forms.Init (this, bundle);
			SetPage (MatchView.GetMatchView ());
		}
	}
}
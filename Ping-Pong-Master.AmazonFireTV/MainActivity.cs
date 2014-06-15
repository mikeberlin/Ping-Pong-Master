using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;

using PingPongMaster.Pages;

namespace PingPongMaster.AmazonFireTV
{
	[Activity (Label = "Ping Pong Master", MainLauncher = true)]
	//[Activity (ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			SetPage (MatchPage.GetMatchPage ());
		}
	}
}
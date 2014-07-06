using System;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Android.App;
using Android.Graphics;

using PingPongMaster;
using PingPongMaster.AmazonFireTV;

[assembly: ExportRenderer (typeof (FancyLabel), typeof (FancyLabelRenderer))]
namespace PingPongMaster.AmazonFireTV
{
	public class FancyLabelRenderer : LabelRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {
				Typeface typeFace = Typeface.CreateFromAsset (Application.Context.Assets, "fonts/Roboto-Light.ttf");

				var label = (global::Android.Widget.TextView)Control;
				label.SetTypeface (typeFace, TypefaceStyle.Normal);
				label.SetTextColor (global::Android.Graphics.Color.DarkOrange);
				label.TextSize = 48.0f;
			}
		}
	}
}
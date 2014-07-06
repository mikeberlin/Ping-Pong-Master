using System;

using Xamarin.Forms;

namespace PingPongMaster.Views
{
	public class MatchView : BaseView
	{
		public static Page GetMatchView ()
		{
			return new ContentPage { 
				Content = new FancyLabel {
					Text = "Greetings from Xamarin.Forms!\n(and custom renderers for custom fonts on Android)",
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
				},
			};
		}
	}
}
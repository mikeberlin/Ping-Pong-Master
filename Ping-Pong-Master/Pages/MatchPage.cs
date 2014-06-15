using System;
using Xamarin.Forms;

namespace PingPongMaster.Pages
{
	public class MatchPage
	{
		public static Page GetMatchPage ()
		{
			return new ContentPage { 
				Content = new Label {
					Text = "Greetings from the Match Page!",
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
				},
			};
		}
	}
}
using System;

using Xamarin.Forms;

namespace PingPongMaster.Views
{
	public class TabNavigatorPage : TabbedPage
	{
		public TabNavigatorPage ()
		{
			this.Children.Add (MatchView.GetMatchView ());
		}
	}
}
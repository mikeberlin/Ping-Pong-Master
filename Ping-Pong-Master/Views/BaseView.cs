using System;

using Xamarin.Forms;

using PingPongMaster.ViewModels;

namespace PingPongMaster
{
	public class BaseView : ContentPage
	{
		public BaseView ()
		{
			SetBinding (Page.TitleProperty, new Binding (BaseViewModel.TitlePropertyName));
			SetBinding (Page.IconProperty, new Binding (BaseViewModel.IconPropertyName));
		}
	}
}
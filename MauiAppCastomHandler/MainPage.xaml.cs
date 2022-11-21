
#if ANDROID
using Android.App;
using Android.Widget;
using Java.Lang;
using Java.Security;
using static Android.Views.View;
#endif
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;
using System.ComponentModel;
using System.Reflection;


namespace MauiAppCastomHandler;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		

    }

	
}


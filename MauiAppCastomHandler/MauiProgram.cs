#if ANDROID
using Android.Graphics;
#endif
using MauiAppCastomHandler.Controls;
using Microsoft.Maui.Platform;
using static MauiAppCastomHandler.Controls.CastomDataPiker;

namespace MauiAppCastomHandler;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        //CastomControlHandlers myHandlr = new();
        MyCastomControlHandler myHandlr = new();

        return builder.Build();
	}
}

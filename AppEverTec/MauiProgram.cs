using AppEverTec.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AppEverTec.MVVM.ViewModels;

namespace AppEverTec;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
        //builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<IChangeColor, ChangeColor>();
        builder.Services.AddSingleton<IDataService, DataService>();
        //builder.Services.AddScoped<IChangeColor, ChangeColor>();

        return builder.Build();
	}
}

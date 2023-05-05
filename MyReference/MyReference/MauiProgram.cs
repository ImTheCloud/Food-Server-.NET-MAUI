namespace MyReference;

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

        builder.Services.AddSingleton<ShowProductViewModel>();
        builder.Services.AddSingleton<ShowProductPage>();


        builder.Services.AddTransient<AddProductViewModel>();
        builder.Services.AddTransient<AddProductPage>();

        builder.Services.AddTransient<InventoryViewModel>();
        builder.Services.AddTransient<InventoryPage>();

        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddTransient<UserPage>();


        builder.Services.AddTransient<FoodService>();

        return builder.Build();
	}
}

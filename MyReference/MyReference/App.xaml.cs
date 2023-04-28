namespace MyReference;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        LoadJson();
        MainPage = new AppShell();
      

    }

    public async void LoadJson()
    {
        FoodService MyService = new FoodService();

        try
        {
            Globals.MyStaticList = await MyService.GetFood();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}

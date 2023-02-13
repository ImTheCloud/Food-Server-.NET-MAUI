namespace MyApp.ViewModel;

public partial class MainViewModel : ObservableObject
{

    [ObservableProperty]
    String monTexte = "go to details";

    public MainViewModel()
	{
	
	}

    [RelayCommand]
    async Task GoToDetailsPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object> {
            {"Databc", data}
        });
    }
}
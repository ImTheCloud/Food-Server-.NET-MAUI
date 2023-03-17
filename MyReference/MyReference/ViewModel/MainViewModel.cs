namespace MyReference.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    string monTexte = "blabla";
    public ObservableCollection<Food> MyShownList { get; } = new();


    DeviceOrientationServices MyDeviceOrientationService;

    [ObservableProperty]
    public string monCode;
    public MainViewModel()
    {
        this.MyDeviceOrientationService = new DeviceOrientationServices();

        MyDeviceOrientationService.ConfigureScanner();
    }

    [RelayCommand]
    public async Task GoToDetailPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }
    [RelayCommand]
    async Task FoodFromJSON()
    {

        if (Globals.SerialBuffer.Count != 0)Globals.SerialBuffer.Dequeue();
       // GetFromJsonServices myServices = new();
        
        

        if (IsBusy) return;

        FoodService MyService = new FoodService();

        try
        {
            IsBusy = true;
            Globals.MyStaticList = await MyService.GetFood();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally { IsBusy = false; }

        MyShownList.Clear();

        foreach (Food stu in Globals.MyStaticList)
        {
            MyShownList.Add(stu);
        }
    }
}
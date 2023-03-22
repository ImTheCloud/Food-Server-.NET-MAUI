namespace MyReference.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    string monTexte = "Inventaire";
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
    public async Task GoToMainPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(MainPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }
    [RelayCommand]

    async Task FoodFromJSON()
    {
        string data = "";
       
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



        while (Globals.SerialBuffer.Count == 0)
        {
            await Task.Delay(100);
        }
        var barcodeData = Globals.SerialBuffer.Dequeue();

        foreach (Food stu in Globals.MyStaticList)
        {
            if (stu.Code == barcodeData)
            {
                MyShownList.Add(stu);
            }
            else
            {
                //redirect 
                //  MyShownList.Add(stu);
            }

        }
    }
}
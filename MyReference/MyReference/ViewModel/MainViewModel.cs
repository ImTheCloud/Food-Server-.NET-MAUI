namespace MyReference.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]

    string activeTarget;
    public ObservableCollection<Food> MyShownList { get; } = new();


    DeviceOrientationServices MyDeviceOrientationService;

    [ObservableProperty]
    public string monCode;
    public MainViewModel()
    {
        this.MyDeviceOrientationService = new DeviceOrientationServices();

        MyDeviceOrientationService.ConfigureScanner();

        MyDeviceOrientationService.SerialBuffer.Changed += SerialBuffer_changed;
    }

    private async void SerialBuffer_changed(object sender, EventArgs e)
    {
        DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;
        var barcodeData = myQueue.Dequeue().ToString();

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
       

        MyShownList.Clear();

        foreach (Food stu in Globals.MyStaticList)
        {
            if (stu.Code == barcodeData)
            {
                MyShownList.Add(stu);
            }
            else
            {
                //IsButtonVisible = true;
                //redirection();
            }
        }
    }

    public async Task redirection()
    {
        await Shell.Current.GoToAsync(nameof(DetailPage));
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

      
}
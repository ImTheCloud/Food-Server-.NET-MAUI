namespace MyReference.ViewModel;

public partial class ShowProductViewModel : BaseViewModel
{
    [ObservableProperty]

    string activeTarget;
    public ObservableCollection<Food> MyShownList { get; } = new();


    DeviceOrientationServices MyDeviceOrientationService;

    [ObservableProperty]
    public string monCode;
    public ShowProductViewModel()
    {
        this.MyDeviceOrientationService = new DeviceOrientationServices();

        MyDeviceOrientationService.ConfigureScanner();

        MyDeviceOrientationService.SerialBuffer.Changed += SerialBuffer_changed;
    }

    private async void SerialBuffer_changed(object sender, EventArgs e)
    {
        DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;
        var barcodeData = myQueue.Dequeue().ToString();

        MyShownList.Clear();

        bool isFoodFound = false;
        foreach (Food stu in Globals.MyStaticList)
        {
            if (stu.Code == barcodeData)
            {
                MyShownList.Add(stu);
                isFoodFound = true;
            }



        }
        if (!isFoodFound)
        {
            //await GoToAddProductPage("nouveau produit");
        }

    }

    [RelayCommand]
    public async Task GoToAddProductPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(AddProductPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    public async Task GoToShowProductPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(ShowProductPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }


}
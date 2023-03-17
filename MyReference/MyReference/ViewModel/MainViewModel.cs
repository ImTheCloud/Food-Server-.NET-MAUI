namespace MyReference.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    string monTexte = "blabla";
    public ObservableCollection<Monkey> MyShownList { get; } = new();


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
    async Task MonkeysFromJSON()
    {

        if (Globals.SerialBuffer.Count != 0)Globals.SerialBuffer.Dequeue();
       // GetFromJsonServices myServices = new();
        
        

        if (IsBusy) return;

        MonkeyService MyService = new MonkeyService();

        try
        {
            IsBusy = true;
            Globals.MyStaticList = await MyService.GetMonkeys();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally { IsBusy = false; }

        MyShownList.Clear();

        foreach (Monkey stu in Globals.MyStaticList)
        {
            MyShownList.Add(stu);
        }
    }
}
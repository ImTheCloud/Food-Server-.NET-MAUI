using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Windows.Input;


namespace MyReference.ViewModel;

public partial class ShowProductViewModel : BaseViewModel
{
    [ObservableProperty]

    string activeTarget;
    public ObservableCollection<Food> MyShownList { get; } = new();


    DeviceOrientationServices MyDeviceOrientationService;

    [ObservableProperty]
    public string monCode;


    public string _code;
    public string barcodeData;
    public string Code
    {
        get { return _code; }
        set
        {
            if (_code != value)
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
    }
    public ICommand OnSearchButton => new Command(SearchingData);
    public ICommand GoToPageWithParameter{ get; }

    public ShowProductViewModel()
    {
        this.MyDeviceOrientationService = new DeviceOrientationServices();

        MyDeviceOrientationService.ConfigureScanner();

        MyDeviceOrientationService.SerialBuffer.Changed += SerialBuffer_changed;
        GoToPageWithParameter = new Command<string>(async (id) => await GotoPageWithParameter(id));

    }

    private async void SerialBuffer_changed(object sender, EventArgs e)
    {
        DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;
        barcodeData = myQueue.Dequeue().ToString();

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
            await GotoPageWithParameter("AddProductPage");
        }

    }


    public async Task GotoPageWithParameter(string id)
    {
        if (id == "AddProductPage")
        {
            await Shell.Current.GoToAsync(nameof(AddProductPage));
        }
        else if (id == "InventoryPage")
        {
            await Shell.Current.GoToAsync(nameof(InventoryPage));
        }
    }

    public async void SearchingData()
    {
        barcodeData = Code;
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
            await Application.Current.MainPage.DisplayAlert("Article Non trouvable", "Veuillez le rajouter dans la bdd", "OK");
            await GotoPageWithParameter("AddProductPage");
        }




    }


}
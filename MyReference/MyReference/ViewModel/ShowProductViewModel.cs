using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Windows.Input;

namespace MyReference.ViewModel
{
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

        public ICommand GoToPageWithParameter { get; }

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
            if (id == "AddProductPage" && Globals.isAdmin == true)
            {
                await Shell.Current.GoToAsync(nameof(AddProductPage));
            }
            else if (id == "InventoryPage" && Globals.isAdmin == true)
            {
                await Shell.Current.GoToAsync(nameof(InventoryPage));
            }
            else if (id == "AdminPage" && Globals.isAdmin == true)
            {
                await Shell.Current.GoToAsync(nameof(AdminPage));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Administrateur", "Seul les Administrateurs ont accès à cette session", "OK");
            }
        }

        [RelayCommand]
        public async void SearchingData(string code)
        {
            MyShownList.Clear();

            bool isFoodFound = false;
            foreach (Food stu in Globals.MyStaticList)
            {
                if (stu.Code == code)
                {
                    MyShownList.Add(stu);
                    isFoodFound = true;
                }else if(code == "")
                {
                    //rien faire
                }
            }

            if (!isFoodFound)
            {
                await Application.Current.MainPage.DisplayAlert("Article Non trouvable", "Veuillez le rajouter dans la bdd", "OK");

                if (Globals.isAdmin == true)
                {
                    await Shell.Current.GoToAsync("AddProductPage", true, new Dictionary<string, object>
                    {
                        {"Databc", code }
                    });
                }
            }
        }
    }
}

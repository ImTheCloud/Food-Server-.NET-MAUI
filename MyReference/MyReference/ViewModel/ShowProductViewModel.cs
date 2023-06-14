using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Windows.Input;

namespace MyReference.ViewModel
{
    public partial class ShowProductViewModel : BaseViewModel
    {
        [ObservableProperty]
        string activeTarget; // Cible active

        public ObservableCollection<Food> MyShownList { get; } = new ObservableCollection<Food>(); // Liste observable des aliments affichés

        DeviceOrientationServices MyDeviceOrientationService;

        [ObservableProperty]
        public string monCode; // Code en cours

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

        public ICommand GoToPageWithParameter { get; } // Commande pour aller à une page avec un paramètre

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
                    MyShownList.Add(stu); // Ajoute l'aliment à la liste observable
                    isFoodFound = true;
                }
            }

            if (!isFoodFound)
            {
                await GotoPageWithParameter("AddProductPage"); // Redirige vers la page d'ajout de produit si l'aliment n'est pas trouvé
            }
        }

        public async Task GotoPageWithParameter(string id)
        {
            if (id == "AddProductPage" && Globals.isAdmin == true)
            {
                await Shell.Current.GoToAsync(nameof(AddProductPage)); // Redirige vers la page d'ajout de produit
            }
            else if (id == "InventoryPage" && Globals.isAdmin == true)
            {
                await Shell.Current.GoToAsync(nameof(InventoryPage)); // Redirige vers la page d'inventaire
            }
            else if (id == "AdminPage" && Globals.isAdmin == true)
            {
                await Shell.Current.GoToAsync(nameof(AdminPage)); // Redirige vers la page d'administration
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
                    MyShownList.Add(stu); // Ajoute l'aliment à la liste observable
                    isFoodFound = true;
                }
                else if (code == "")
                {
                    // Ne rien faire
                }
            }

            if (!isFoodFound)
            {
                await Application.Current.MainPage.DisplayAlert("Article Non trouvable", "Veuillez le rajouter dans la bdd", "OK"); // Affiche une alerte si l'aliment n'est pas trouvé

                if (Globals.isAdmin == true)
                {
                    await Shell.Current.GoToAsync("AddProductPage", true, new Dictionary<string, object>
                    {
                        {"Databc", code }
                    }); // Redirige vers la page d'ajout de produit en passant le code en paramètre
                }
            }
        }
    }
}

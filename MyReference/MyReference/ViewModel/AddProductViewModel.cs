using System.Windows.Input;

namespace MyReference.ViewModel
{
    [QueryProperty(nameof(Code), "Databc")]
    public partial class AddProductViewModel : BaseViewModel
    {
        [ObservableProperty]
        string code;

        public AddProductViewModel()
        {

        }

        public ICommand SaveItem => new Command(AddFood);

        public string _name;
        public string _quantite;
        public string _details;
        public string _prix;
        public string _code;
        public string _image;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Quantite
        {
            get { return _quantite; }
            set
            {
                if (_quantite != value)
                {
                    _quantite = value;
                    OnPropertyChanged(nameof(Quantite));
                }
            }
        }

        public string Details
        {
            get { return _details; }
            set
            {
                if (_details != value)
                {
                    _details = value;
                    OnPropertyChanged(nameof(Details));
                }
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        public string Prix
        {
            get { return _prix; }
            set
            {
                if (_prix != value)
                {
                    _prix = value;
                    OnPropertyChanged(nameof(Prix));
                }
            }
        }

        public async void AddFood()
        {
            string name = Name;
            string quantite = Quantite;
            string image = Image;
            string code = Code;
            string prix = Prix;
            string details = Details;

            var item = new Food
            {
                Name = name,
                Quantite = int.Parse(quantite),
                Details = details,
                Image = image,
                Code = code,
                Prix = double.Parse(prix)
            };

            FoodService myService = new();

            Globals.MyStaticList.Add(item);
            await myService.SetFoodJson();
            await Application.Current.MainPage.DisplayAlert("Le produit a été enregistré.", "Enregistrement réussi", "OK");
        }
    }
}

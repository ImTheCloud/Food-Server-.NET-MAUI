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

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string quantite;
        
        [ObservableProperty]
        public string details;
        
        [ObservableProperty]
        public string prix;
        
        [ObservableProperty]
        public string image;

        public async void AddFood()
        {
            string name = Name;
            string quantite = Quantite;
            string image = Image;
            string code = Code;
            string prix = Prix;
            string details = Details;
            Boolean foodfind = false;

            try
            {
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
                foreach(var food in Globals.MyStaticList)
                {
                    if(item.Code== food.Code)
                    {
                        foodfind = true;
                    }
                }
                if(foodfind == false) {
                    Globals.MyStaticList.Add(item);
                    await myService.SetFoodJson();
                    await Application.Current.MainPage.DisplayAlert("Le produit a été enregistré.", "Enregistrement réussi", "OK");
                } else
                {
                    await Shell.Current.DisplayAlert("Code répété", "Vous ne pouvez pas avoir deux codes d'articles identiques", "ok");
                }
               
            }
            catch
            {
                await Shell.Current.DisplayAlert("Valeur vide", "Tous les champs sont obligatoire", "ok");
            }
          

   
        }
    }
}

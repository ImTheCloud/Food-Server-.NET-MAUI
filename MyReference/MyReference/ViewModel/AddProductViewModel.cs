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
            string name = Name; // Récupère la valeur de la propriété Name
            string quantite = Quantite; // Récupère la valeur de la propriété Quantite
            string image = Image; // Récupère la valeur de la propriété Image
            string code = Code; // Récupère la valeur de la propriété Code
            string prix = Prix; // Récupère la valeur de la propriété Prix
            string details = Details; // Récupère la valeur de la propriété Details
            bool foodfind = false; // Variable pour indiquer si le produit est déjà présent dans la liste

            try
            {
                int parsedQuantite; // Variable pour stocker la quantité convertie en entier
                double parsedPrix; // Variable pour stocker le prix converti en double

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(quantite) || string.IsNullOrWhiteSpace(prix))
                {
                    await Shell.Current.DisplayAlert("Champs obligatoires", "Veuillez remplir tous les champs", "OK");
                    return;
                }

                if (!int.TryParse(quantite, out parsedQuantite) || !double.TryParse(prix, out parsedPrix))
                {
                    await Shell.Current.DisplayAlert("Format invalide", "Les champs 'Quantité' et 'Prix' doivent contenir des nombres valides.", "OK");
                    return;
                }

                var item = new Food
                {
                    Name = name,
                    Quantite = parsedQuantite,
                    Details = details,
                    Image = image,
                    Code = code,
                    Prix = parsedPrix
                };

                FoodService myService = new(); // Instance de la classe FoodService
                foreach (var food in Globals.MyStaticList)
                {
                    if (item.Code == food.Code)
                    {
                        foodfind = true; // Le produit est déjà présent dans la liste
                    }
                }
                if (foodfind == false)
                {
                    try
                    {
                        Globals.MyStaticList.Add(item); // Ajoute le produit à la liste
                        await myService.SetFoodJson(); // Appelle la méthode SetFoodJson de FoodService pour enregistrer les modifications
                        await Application.Current.MainPage.DisplayAlert("Le produit a été enregistré.", "Enregistrement réussi", "OK");
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("Erreur", "Une erreur s'est produite lors de l'enregistrement du produit.", "OK");
                        Console.WriteLine($"Erreur lors de l'enregistrement du produit : {ex.Message}");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Code répété", "Vous ne pouvez pas avoir deux codes d'articles identiques", "OK");
                }

            }
            catch
            {
                await Shell.Current.DisplayAlert("Erreur", "Une erreur s'est produite lors de l'enregistrement du produit.", "OK");
            }
        }
    }
}



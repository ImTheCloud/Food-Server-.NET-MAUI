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
            string name = Name; // R�cup�re la valeur de la propri�t� Name
            string quantite = Quantite; // R�cup�re la valeur de la propri�t� Quantite
            string image = Image; // R�cup�re la valeur de la propri�t� Image
            string code = Code; // R�cup�re la valeur de la propri�t� Code
            string prix = Prix; // R�cup�re la valeur de la propri�t� Prix
            string details = Details; // R�cup�re la valeur de la propri�t� Details
            bool foodfind = false; // Variable pour indiquer si le produit est d�j� pr�sent dans la liste

            try
            {
                int parsedQuantite; // Variable pour stocker la quantit� convertie en entier
                double parsedPrix; // Variable pour stocker le prix converti en double

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(quantite) || string.IsNullOrWhiteSpace(prix))
                {
                    await Shell.Current.DisplayAlert("Champs obligatoires", "Veuillez remplir tous les champs", "OK");
                    return;
                }

                if (!int.TryParse(quantite, out parsedQuantite) || !double.TryParse(prix, out parsedPrix))
                {
                    await Shell.Current.DisplayAlert("Format invalide", "Les champs 'Quantit�' et 'Prix' doivent contenir des nombres valides.", "OK");
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
                        foodfind = true; // Le produit est d�j� pr�sent dans la liste
                    }
                }
                if (foodfind == false)
                {
                    try
                    {
                        Globals.MyStaticList.Add(item); // Ajoute le produit � la liste
                        await myService.SetFoodJson(); // Appelle la m�thode SetFoodJson de FoodService pour enregistrer les modifications
                        await Application.Current.MainPage.DisplayAlert("Le produit a �t� enregistr�.", "Enregistrement r�ussi", "OK");
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("Erreur", "Une erreur s'est produite lors de l'enregistrement du produit.", "OK");
                        Console.WriteLine($"Erreur lors de l'enregistrement du produit : {ex.Message}");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Code r�p�t�", "Vous ne pouvez pas avoir deux codes d'articles identiques", "OK");
                }

            }
            catch
            {
                await Shell.Current.DisplayAlert("Erreur", "Une erreur s'est produite lors de l'enregistrement du produit.", "OK");
            }
        }
    }
}



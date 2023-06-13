using System.Windows.Input;

namespace MyReference.ViewModel
{
    public partial class InventoryViewModel : BaseViewModel
    {
        public InventoryViewModel()
        {
            AllFoodCollection();
        }

        public ObservableCollection<Food> AllFoodList { get; } = new ObservableCollection<Food>(); // Liste observable de tous les aliments
        public ICommand DeleteCommand => new Command<Food>(DeleteFood); // Commande de suppression
        public ICommand OnSearchCommand => new Command(SearchingData); // Commande de recherche

        [ObservableProperty]
        public string code; // Code utilisé pour la recherche

        public void AllFoodCollection()
        {
            AllFoodList.Clear();
            foreach (Food stu in Globals.MyStaticList)
            {
                AllFoodList.Add(stu); // Ajoute chaque aliment à la liste observable
            }
        }

        private async void DeleteFood(Food food)
        {
            try
            {
                Globals.MyStaticList.Remove(food); // Supprime l'aliment de la liste globale
                AllFoodCollection();
                FoodService myService = new();
                await myService.SetFoodJson(); // Met à jour le fichier JSON
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", "Une erreur s'est produite lors de la suppression du produit.", "OK");
                Console.WriteLine($"Erreur lors de la suppression du produit : {ex.Message}");
            }
        }

        public void SearchingData()
        {
            string code = Code; // Récupère le code de recherche

            AllFoodList.Clear();
            foreach (Food stu in Globals.MyStaticList)
            {
                if (stu.Code == code)
                {
                    AllFoodList.Add(stu); // Ajoute l'aliment correspondant à la liste observable
                }
            }
        }
    }
}

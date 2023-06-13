using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Input;

namespace MyReference.ViewModel
{
    public partial class UserViewModel : BaseViewModel
    {
        UserManagementServices MyDBServices;

        public UserViewModel(UserManagementServices myDBServices)
        {
            this.MyDBServices = myDBServices;
            MyDBServices.ConfigTools();
        }

        public ObservableCollection<User> ShownList { get; set; } = new ObservableCollection<User>(); // Liste observable des utilisateurs affich�s

        public ICommand ConnexionButton => new Command(VerifyConnexion); // Commande pour v�rifier la connexion

        [ObservableProperty]
        public string name; // Nom de l'utilisateur

        [ObservableProperty]
        public string password; // Mot de passe de l'utilisateur

        public async void VerifyConnexion()
        {
            string name = Name;
            string password = Password;
            bool isCorrect = false;
            Globals.isAdmin = false;

            foreach (var item in Globals.UserList)
            {
                if (item.UserName == name && item.UserPassword == password)
                {
                    isCorrect = true;
                    if (item.UserAccessType == 1)
                    {
                        Globals.isAdmin = true;
                    }
                    await Shell.Current.GoToAsync(nameof(ShowProductPage)); // Redirige vers la page de visualisation des produits en cas de connexion r�ussie
                }
            }

            if (!isCorrect)
            {
                await Application.Current.MainPage.DisplayAlert("Connexion Failed", "Le mot de passe ou le nom est incorrect", "OK"); // Affiche une alerte en cas de connexion �chou�e
            }
        }
    }
}

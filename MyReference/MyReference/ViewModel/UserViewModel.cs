using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Input;

namespace MyReference.ViewModel;

public partial class UserViewModel : BaseViewModel

{

    UserManagementServices MyDBServices;
    public UserViewModel(UserManagementServices myDBServices)
    {
        this.MyDBServices = myDBServices;
        MyDBServices.ConfigTools();
    }

    public ObservableCollection<User> ShownList { get; set; } = new();
    public ICommand ConnexionButton => new Command(VerifyConnexion);

    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public string password;
   
  

    public async void VerifyConnexion()
    {
     
        string name = Name;
        string password = Password;
        Boolean isCorrect = false;
        Globals.isAdmin = false;


        foreach (var item in Globals.UserList)
        {
            if(item.UserName == name && item.UserPassword == password) {
                isCorrect = true;
                if(item.UserAccessType== 1)
                {
                    Globals.isAdmin = true;
                }
                await Shell.Current.GoToAsync(nameof(ShowProductPage));
            }

            
        }

        if (!isCorrect)
        {

            await Application.Current.MainPage.DisplayAlert("Connexion Failed", "le mot de passe ou le nom est incorrect", "OK");
        }


    }

}
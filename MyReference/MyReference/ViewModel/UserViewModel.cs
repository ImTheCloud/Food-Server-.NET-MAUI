using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Input;

namespace MyReference.ViewModel;

public partial class UserViewModel : BaseViewModel
{
    public ObservableCollection<User> ShownList { get; set; } = new();

    public ICommand OnFillButton => new Command(Fill);
    public ICommand ValidateButton => new Command(Fill);
    public ICommand ConnexionButton => new Command(VerifyConnexion);


    public string _name;
    public string _password;
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value) { _name = value; OnPropertyChanged(nameof(Name)); }
        }
    }

    public string Password
    {
        get { return _password; }
        set
        {
            if (_password != value) { _password = value; OnPropertyChanged(nameof(Password)); }
        }
    }


    public async void Fill()
    {
        IsBusy = true;

        List<User> MyList = new();



        string name = Name;
        string password = Password;

        var addUserItem = new User
        {
            User_ID = 10,
            UserName = name,
            UserPassword = password,
            UserAccesType = 3
        };



        ShownList.Add(addUserItem);

        try
        {
            MyList = Globals.UserSet.Tables["Users"].AsEnumerable().Select(e => new User()
            {

                User_ID = e.Field<Int16>("User_ID"),
                UserName = e.Field<string>("UserName"),
                UserPassword = e.Field<string>("UserPassword"),
                UserAccesType = e.Field<Int16>("UserAccesType"),
            }).ToList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
        }

        foreach (var item in MyList)
        {
            ShownList.Add(item);
        }

        IsBusy = false;
    }

    public async void VerifyConnexion()
    {
     
        string name = Name;
        string password = Password;

 
        foreach (var item in ShownList)
        {
            if(item.UserName == name && item.UserPassword == password) {

                
                await Shell.Current.GoToAsync(nameof(ShowProductPage));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Connexion Failed", "le mot de passe ou le nom est incorrect", "OK");
            }
        }
    }
}
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

  

    public async void VerifyConnexion()
    {
     
        string name = Name;
        string password = Password;
        //await Shell.Current.GoToAsync(nameof(ShowProductPage));
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
    public async void newFill()
    {
        UserManagementServices MyDBServices = new();
        MyDBServices.ConfigTools();

        Globals.UserList.Clear();

        if (Globals.UserSet.Tables["Access"].Rows.Count == 0)
        {
            MyDBServices.ReadFromDB();

        }
        if (Globals.UserSet.Tables["Access"].Rows.Count != 0)
        {
            Globals.UserSet.Tables["Access"].Clear();

        }


        MyDBServices.FillUsersFromDB();

        try
        {
            Globals.UserList = Globals.UserSet.Tables["Users"].AsEnumerable().Select(e => new User()
            {

                User_ID = e.Field<Int16>("User_ID"),
                UserName = e.Field<string>("UserName"),
                UserPassword = e.Field<string>("UserPassword"),
                UserAccessType = e.Field<Int16>("UserAccessType"),
            }).ToList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
        }

    }

}
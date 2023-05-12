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

    public ICommand OnFillButton => new Command(Fill);
    public ICommand OnDeleteButton => new Command(Delete);

    public ICommand OnUpdateButton => new Command(Update);

    public ICommand OnInsertButton => new Command(Insert);

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

    public async void Delete()
    {
        IsBusy = true;
        string name = Name;

        try
        {
             MyDBServices.DeleteIntoDB(name);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
        }
        IsBusy = false;

    }

    public async void Update()
    {
        IsBusy = true;


        try
        {
             MyDBServices.UpdateDB();

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
        }
        IsBusy = false;
    }
    public async void Insert()
    {
        IsBusy = true;


        string name = Name;
        string password = Password;
        try
        {
             MyDBServices.insertIntoDB(name, password,3);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
        }
        IsBusy = false;

    }


    public async void Fill()
    {
        IsBusy = true;

        List<User> MyList = new();

      
        if (Globals.UserSet.Tables["Access"].Rows.Count == 0)
        {
             MyDBServices.ReadFromDB();

        }
        if (Globals.UserSet.Tables["Access"].Rows.Count != 0)
        {
            Globals.UserSet.Tables.Clear();

        }

        

         MyDBServices.FillUsersFromDB();

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
        ShownList.Clear();

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
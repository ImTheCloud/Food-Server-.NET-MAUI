using Microsoft.Maui.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace MyReference.ViewModel
{
    public class AdminViewModel : ContentPage
    {
        public ObservableCollection<User> myUserList { get; set; } = new();
        public ICommand DeleteCommand => new Command<User>(DeleteUser);
        public ICommand AddUserCommand => new Command(AddUser);
        public ICommand onResearchCommand => new Command(SearchingData);
        public ICommand UpdateCommand => new Command(UpdateUser);


        UserManagementServices MyDBServices;

        public AdminViewModel(UserManagementServices myDBServices)
        {
            loadFromDB();
            this.MyDBServices = myDBServices;
            MyDBServices.ConfigTools();
        }

        public string _name;

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

        public string _useraddName;

        public string UserAddName
        {
            get { return _useraddName; }
            set
            {
                if (_useraddName != value)
                {
                    _useraddName = value;
                    OnPropertyChanged(nameof(UserAddName));
                }
            }
        }

        public string _userAddAccessType;

        public string UserAddAccessType
        {
            get { return _userAddAccessType; }
            set
            {
                if (_userAddAccessType != value)
                {
                    _userAddAccessType = value;
                    OnPropertyChanged(nameof(UserAddAccessType));
                }
            }
        }

        public string _userAddPassword;

        public string UserAddPassword
        {
            get { return _userAddPassword; }
            set
            {
                if (_userAddPassword != value)
                {
                    _userAddPassword = value;
                    OnPropertyChanged(nameof(UserAddPassword));
                }
            }
        }

        public void loadFromDB()
        {
            myUserList.Clear();
            try
            {
                foreach (User item in Globals.UserList)
                {
                    myUserList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Database", ex.Message, "OK");
            }
        }

        public async void DeleteUser(User user)
        {
            try
            {
                Globals.UserList.Remove(user);
                loadFromDB();
                await DeleteDB(user.UserName);
                await Shell.Current.DisplayAlert("Database", "L'utilisateur a bien été supprimé", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
            }
        }

        public async Task DeleteDB(string name)
        {
            try
            {
                MyDBServices.DeleteIntoDB(name);
                myUserList.Clear();
                loadFromDB();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
            }
        }

        public void UpdateUser()
        {
            try
            {
                string name = UserAddName;
                int accessType = int.Parse(UserAddAccessType);
                string password = UserAddPassword;
               // MyDBServices.UpdateDB(name, password, accessType);

                Shell.Current.DisplayAlert("DataBase", "L'utilisateur a été mise à jour", "ok");
            }
            catch (Exception ex)
            {
                 Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
            }
        }
        

        public void AddUser()
        {
         
            try
            {
                string name = UserAddName;
                int accessType = int.Parse(UserAddAccessType);
                string password = UserAddPassword;
                MyDBServices.insertIntoDB(name, password, accessType);
                myUserList.Clear();
               // newFill();
                loadFromDB();
            }
            catch
            {
                Shell.Current.DisplayAlert("Valeurs vides", "Tout les champs sont obligatoire", "ok");
            }
        }

        public void SearchingData()
        {
            string name = Name;
            myUserList.Clear();
            foreach (var item in Globals.UserList)
            {
                if (item.UserName == name)
                {
                    myUserList.Add(item);
                }
                else if (name == "")
                {
                    myUserList.Add(item);
                }
            }
        }

        public async void newFill()
        {
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
}

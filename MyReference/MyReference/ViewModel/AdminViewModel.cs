using System.Windows.Input;

namespace MyReference.ViewModel
{
    public partial class AdminViewModel : BaseViewModel
    {
        public ObservableCollection<User> myUserList { get; set; } = new();
        public ICommand DeleteCommand => new Command<User>(DeleteUser);
        public ICommand ToAdminUserCommand => new Command<User>(UpdateUser);
        public ICommand AddUserCommand => new Command(AddUser);
        public ICommand OnResearchCommand => new Command(SearchingData);

        UserManagementServices MyDBServices;

        public AdminViewModel(UserManagementServices myDBServices)
        {
            loadFromDB();
            this.MyDBServices = myDBServices;
            MyDBServices.ConfigTools();
        }

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string userAddName;

        [ObservableProperty]
        public string userAddAccessType;

        [ObservableProperty]
        public string userAddPassword;

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
                MyDBServices.DeleteIntoDB(user.UserName);
                myUserList.Remove(user);
                //update fonctionne pas

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
            }
        }


        public async void UpdateUser(User user)
        {
            try
            {
                DataTable usersTable = Globals.UserSet.Tables["Users"];

                foreach (DataRow row in usersTable.Rows)
                {
                    if (row["User_ID"].ToString() == user.User_ID.ToString()) 
                    {
                        if(user.UserAccessType == 1)
                        {
                            row["UserAccessType"] = 2;
                        }
                        else
                        {
                            row["UserAccessType"] = 1;
                        }
                        break;
                    }
                   
                }
                //MyDBServices.UpdateDB(); // FONCTIONNE PAS
                NewFill();
                myUserList.Clear();
                loadFromDB();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
            }
        }

        public void AddUser()
        {
            try
            {
                string userAddName = UserAddName;
                int userAddAccessType = int.Parse(UserAddAccessType);
                string userAddPassword = UserAddPassword;
                MyDBServices.insertIntoDB(userAddName, userAddPassword, userAddAccessType);
                //le update veut pas marcher, trop compliqué
               /* NewFill();
                myUserList.Clear();
                loadFromDB();
                */
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

        public async void NewFill()
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

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
            MyDBServices.ConfigTools(); // methode qui preovienne d'autre class faut ca !!! // ligne 14 AUSSI !!!!!!!!!!!!!!!!!!!!!!!
        }

        [ObservableProperty]
        public string name; // Nom pour la recherche

        [ObservableProperty]
        public string userAddName; // Nom de l'utilisateur � ajouter

        [ObservableProperty]
        public string userAddAccessType; // Type d'acc�s de l'utilisateur � ajouter

        [ObservableProperty]
        public string userAddPassword; // Mot de passe de l'utilisateur � ajouter

        public void loadFromDB()
        {
            myUserList.Clear();
            try
            {
                foreach (User item in Globals.UserList)
                {
                    myUserList.Add(item); // Ajoute les utilisateurs � la liste observable
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
                MyDBServices.DeleteIntoDB(user.UserName); // Supprime l'utilisateur de la base de donn�es
                myUserList.Remove(user); // Supprime l'utilisateur de la liste observable
                // update fonctionne pas

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
                        if (user.UserAccessType == 1)
                        {
                            row["UserAccessType"] = 2; // Met � jour le type d'acc�s de l'utilisateur
                        }
                        else
                        {
                            row["UserAccessType"] = 1;
                        }
                        break;
                    }

                }
                //MyDBServices.UpdateDB(); // Mise � jour de la base de donn�es (FONCTIONNE PAS)
                NewFill(); // Remplit � nouveau les donn�es
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
                string userAddName = UserAddName; // R�cup�re le nom de l'utilisateur � ajouter
                int userAddAccessType = int.Parse(UserAddAccessType); // R�cup�re le type d'acc�s de l'utilisateur � ajouter
                string userAddPassword = UserAddPassword; // R�cup�re le mot de passe de l'utilisateur � ajouter
                MyDBServices.insertIntoDB(userAddName, userAddPassword, userAddAccessType); // Ins�re l'utilisateur dans la base de donn�es
                                                                                            //le update veut pas marcher, trop compliqu�
                /* NewFill();
                 myUserList.Clear();
                 loadFromDB();
                 */
            }
            catch
            {
                Shell.Current.DisplayAlert("Valeurs vides", "Tous les champs sont obligatoires", "ok");
            }
        }

        public void SearchingData()
        {
            string name = Name; // R�cup�re le nom de recherche
            myUserList.Clear();
            foreach (var item in Globals.UserList)
            {
                if (item.UserName == name)
                {
                    myUserList.Add(item); // Ajoute l'utilisateur correspondant � la liste observable
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
                MyDBServices.ReadFromDB(); // Lit les donn�es depuis la base de donn�es
            }
            if (Globals.UserSet.Tables["Access"].Rows.Count != 0)
            {
                Globals.UserSet.Tables["Access"].Clear();
            }

            MyDBServices.FillUsersFromDB(); // Remplit les utilisateurs depuis la base de donn�es

            try
            {
                Globals.UserList = Globals.UserSet.Tables["Users"].AsEnumerable().Select(e => new User()
                {
                    User_ID = e.Field<Int16>("User_ID"),
                    UserName = e.Field<string>("UserName"),
                    UserPassword = e.Field<string>("UserPassword"),
                    UserAccessType = e.Field<Int16>("UserAccessType"),
                }).ToList(); // Convertit les donn�es en objets User et les stocke dans la liste globale des utilisateurs
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
            }
        }
    }
}

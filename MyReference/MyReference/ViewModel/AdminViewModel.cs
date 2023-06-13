using System.Windows.Input;

namespace MyReference.ViewModel
{
    public partial class AdminViewModel : BaseViewModel
    {
        public ObservableCollection<User> myUserList { get; set; } = new(); // Liste observable des utilisateurs

        public ICommand DeleteCommand => new Command<User>(DeleteUser); // Commande de suppression d'un utilisateur
        public ICommand ToAdminUserCommand => new Command<User>(UpdateUser); // Commande pour mettre à jour les autorisations d'un utilisateur
        public ICommand AddUserCommand => new Command(AddUser); // Commande pour ajouter un utilisateur
        public ICommand OnResearchCommand => new Command(SearchingData); // Commande pour effectuer une recherche de données

        UserManagementServices MyDBServices; // Instance de la classe UserManagementServices

        public AdminViewModel(UserManagementServices myDBServices)
        {
            loadFromDB(); // Charge les données depuis la base de données
            this.MyDBServices = myDBServices; // Initialise l'instance de UserManagementServices
            MyDBServices.ConfigTools(); // Configure les outils de la base de données
        }

        [ObservableProperty]
        public string name; // Nom pour la recherche

        [ObservableProperty]
        public string userAddName; // Nom de l'utilisateur à ajouter

        [ObservableProperty]
        public string userAddAccessType; // Type d'accès de l'utilisateur à ajouter

        [ObservableProperty]
        public string userAddPassword; // Mot de passe de l'utilisateur à ajouter

        public void loadFromDB()
        {
            myUserList.Clear();
            try
            {
                foreach (User item in Globals.UserList)
                {
                    myUserList.Add(item); // Ajoute les utilisateurs à la liste observable
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
                MyDBServices.DeleteIntoDB(user.UserName); // Supprime l'utilisateur de la base de données
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
                            row["UserAccessType"] = 2; // Met à jour le type d'accès de l'utilisateur
                        }
                        else
                        {
                            row["UserAccessType"] = 1;
                        }
                        break;
                    }

                }
                //MyDBServices.UpdateDB(); // Mise à jour de la base de données (FONCTIONNE PAS)
                NewFill(); // Remplit à nouveau les données
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
                string userAddName = UserAddName; // Récupère le nom de l'utilisateur à ajouter
                int userAddAccessType = int.Parse(UserAddAccessType); // Récupère le type d'accès de l'utilisateur à ajouter
                string userAddPassword = UserAddPassword; // Récupère le mot de passe de l'utilisateur à ajouter
                MyDBServices.insertIntoDB(userAddName, userAddPassword, userAddAccessType); // Insère l'utilisateur dans la base de données
                                                                                            //le update veut pas marcher, trop compliqué
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
            string name = Name; // Récupère le nom de recherche
            myUserList.Clear();
            foreach (var item in Globals.UserList)
            {
                if (item.UserName == name)
                {
                    myUserList.Add(item); // Ajoute l'utilisateur correspondant à la liste observable
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
                MyDBServices.ReadFromDB(); // Lit les données depuis la base de données
            }
            if (Globals.UserSet.Tables["Access"].Rows.Count != 0)
            {
                Globals.UserSet.Tables["Access"].Clear();
            }

            MyDBServices.FillUsersFromDB(); // Remplit les utilisateurs depuis la base de données

            try
            {
                Globals.UserList = Globals.UserSet.Tables["Users"].AsEnumerable().Select(e => new User()
                {
                    User_ID = e.Field<Int16>("User_ID"),
                    UserName = e.Field<string>("UserName"),
                    UserPassword = e.Field<string>("UserPassword"),
                    UserAccessType = e.Field<Int16>("UserAccessType"),
                }).ToList(); // Convertit les données en objets User et les stocke dans la liste globale des utilisateurs
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
            }
        }
    }
}

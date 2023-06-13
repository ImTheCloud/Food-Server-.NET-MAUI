namespace MyReference
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LoadJson(); // Charge les données à partir d'un fichier JSON
            MainPage = new AppShell(); // Définit la page principale de l'application comme étant AppShell
            CreateUserTables MyUserTables = new CreateUserTables(); // Crée les tables pour les utilisateurs

            importAllUsersFromDB(); // Importe tous les utilisateurs depuis la base de données
        }

        public async void importAllUsersFromDB()
        {
            UserManagementServices MyDBServices = new UserManagementServices();
            MyDBServices.ConfigTools(); // Configure les outils pour la gestion des utilisateurs

            if (Globals.UserSet.Tables["Access"].Rows.Count == 0)
            {
                MyDBServices.ReadFromDB(); // Lit les données depuis la base de données
            }

            if (Globals.UserSet.Tables["Access"].Rows.Count != 0)
            {
                Globals.UserSet.Tables["Access"].Clear(); // Efface les données existantes dans la table "Access"
            }

            MyDBServices.FillUsersFromDB(); // Remplit la liste des utilisateurs à partir de la base de données

            try
            {
                Globals.UserList = Globals.UserSet.Tables["Users"].AsEnumerable().Select(e => new User()
                {
                    User_ID = e.Field<Int16>("User_ID"),
                    UserName = e.Field<string>("UserName"),
                    UserPassword = e.Field<string>("UserPassword"),
                    UserAccessType = e.Field<Int16>("UserAccessType"),
                }).ToList(); // Convertit les données de la table "Users" en une liste d'objets User
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok"); // Affiche une alerte en cas d'erreur lors de l'accès à la base de données
            }
        }

        public async void LoadJson()
        {
            FoodService MyService = new FoodService(); // Service pour récupérer les données des aliments

            try
            {
                Globals.MyStaticList = await MyService.GetFood(); // Récupère les données des aliments à partir d'un service
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Students: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK"); // Affiche une alerte en cas d'erreur lors de la récupération des données des aliments
            }
        }
    }
}

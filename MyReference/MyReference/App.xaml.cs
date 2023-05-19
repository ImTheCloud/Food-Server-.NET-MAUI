namespace MyReference;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        LoadJson();
        MainPage = new AppShell();
        CreateUserTables MyUserTables = new();


       
        importAllUsersFromDB();
    }

    public async void importAllUsersFromDB()
    {
        UserManagementServices MyDBServices = new();
        MyDBServices.ConfigTools();


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

    public async void LoadJson()
    {
        FoodService MyService = new FoodService();

        try
        {
            Globals.MyStaticList = await MyService.GetFood();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}

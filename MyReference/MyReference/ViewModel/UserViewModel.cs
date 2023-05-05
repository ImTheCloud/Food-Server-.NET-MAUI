using System.Windows.Input;

namespace MyReference.ViewModel;

 public partial class UserViewModel : BaseViewModel
{
    public ObservableCollection<User> ShownList { get; set; } = new();

    public ICommand onFillButton => new Command(Fill);
    public async void Fill()
    {
        IsBusy = true;

        List<User> MyList = new();

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

        foreach(var item in MyList)
        {
            ShownList.Add(item);
        }
        
        IsBusy = false;
    }
}
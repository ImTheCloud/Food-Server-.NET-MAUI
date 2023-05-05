using System.Windows.Input;

namespace MyReference.ViewModel;

 public partial class UserViewModel : BaseViewModel
{
    public ObservableCollection<User> ShownList { get; set; } = new();

    public ICommand onFillButton => new Command(Fill);
    public void Fill()
    {
        IsBusy = true;
        User MyUser= new User();
        MyUser.User_ID = 1;
        MyUser.UserName = "Text";
        MyUser.UserPassword = "Text";
        MyUser.UserAccesType = 1;

        ShownList.Add(MyUser);

        IsBusy = false;


    }


}